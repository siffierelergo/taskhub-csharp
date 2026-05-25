using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TaskHub.Models;
using TaskHub.Logic;

namespace TaskHub.UI
{
    public partial class MainWindow : Window
    {
        IStocareData stocare = new AdministrareTaskuriFisierText("taskuri.txt");
        private TodoTask taskInEditare = null;

        // DECLARARE DEPENDENCY PROPERTY PENTRU BINDING (Lab WPF avansat)
        public static readonly DependencyProperty TextButonSalvareProperty =
            DependencyProperty.Register("TextButonSalvare", typeof(string), typeof(MainWindow), new PropertyMetadata("Salvează"));

        public string TextButonSalvare
        {
            get { return (string)GetValue(TextButonSalvareProperty); }
            set { SetValue(TextButonSalvareProperty, value); }
        }

        public MainWindow() { InitializeComponent(); IncarcaDatele(); }

        private void IncarcaDatele() { dgTaskuri.ItemsSource = null; dgTaskuri.ItemsSource = stocare.GetTasks(); }

        private bool Valideaza()
        {
            bool ok = true;
            lblTitlu.Foreground = Brushes.White;
            lblCategorie.Foreground = Brushes.White;
            lblZile.Foreground = Brushes.White;

            if (string.IsNullOrWhiteSpace(txtTitlu.Text) || txtTitlu.Text.Length < 3 || txtTitlu.Text.Length > 15) { lblTitlu.Foreground = Brushes.Tomato; ok = false; }
            if (cmbCategorie.SelectedIndex == -1) { lblCategorie.Foreground = Brushes.Tomato; ok = false; }
            if (!double.TryParse(txtZile.Text, out double z) || z < 0) { lblZile.Foreground = Brushes.Tomato; ok = false; }

            return ok;
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (!Valideaza()) return;

            if (taskInEditare != null) stocare.DeleteTask(taskInEditare);

            var t = new TodoTask
            {
                Title = txtTitlu.Text,
                Category = (TaskCategory)cmbCategorie.SelectedIndex,
                DueDate = DateTime.Now.AddDays(double.Parse(txtZile.Text)),
                IsUrgent = rbUrgentDa.IsChecked == true,
                CreatedAt = taskInEditare?.CreatedAt ?? DateTime.Now,
                IsCompleted = false,
                Priority = TaskPriority.Medium
            };

            stocare.AddTask(t);
            IncarcaDatele();
            btnReset_Click(null, null);
        }

        private void btnCauta_Click(object sender, RoutedEventArgs e)
        {
            dgTaskuri.ItemsSource = stocare.SearchTasks(txtCautare.Text);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgTaskuri.SelectedItem is TodoTask t)
            {
                taskInEditare = t;
                txtTitlu.Text = t.Title;
                cmbCategorie.SelectedIndex = (int)t.Category;
                txtZile.Text = "0";
                rbUrgentDa.IsChecked = t.IsUrgent;
                rbUrgentNu.IsChecked = !t.IsUrgent;

                // DATA BINDING ÎN ACȚIUNE: Modificăm proprietatea, iar interfața grafică reacționează singură
                TextButonSalvare = "Actualizează";
                lblTitlu.Foreground = Brushes.Yellow;
            }
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            if (dgTaskuri.SelectedItem is TodoTask t) { stocare.DeleteTask(t); IncarcaDatele(); }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTitlu.Clear();
            txtZile.Clear();
            cmbCategorie.SelectedIndex = -1;
            taskInEditare = null;

            // DATA BINDING ÎN ACȚIUNE: Revenim la textul inițial prin proprietate
            TextButonSalvare = "Salvează";

            lblTitlu.Foreground = Brushes.White;
            lblCategorie.Foreground = Brushes.White;
            lblZile.Foreground = Brushes.White;
        }
    }
}