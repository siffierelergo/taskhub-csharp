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

        public MainWindow()
        {
            InitializeComponent();
            IncarcaDatele();
        }

        private void IncarcaDatele()
        {
            dgTaskuri.ItemsSource = null;
            dgTaskuri.ItemsSource = stocare.GetTasks();
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (Valideaza())
            {
                string catStr = (cmbCategorie.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Personal";
                var t = new TodoTask
                {
                    Title = txtTitlu.Text,
                    Category = (TaskCategory)Enum.Parse(typeof(TaskCategory), catStr),
                    DueDate = DateTime.Now.AddDays(double.Parse(txtZile.Text)),
                    Priority = TaskPriority.Medium,
                    IsUrgent = rbUrgentDa.IsChecked == true
                };
                stocare.AddTask(t);
                IncarcaDatele();
                btnReset_Click(null, null);
            }
        }

        private bool Valideaza()
        {
            lblTitlu.Foreground = Brushes.White;
            errTitlu.Visibility = Visibility.Collapsed;

            if (txtTitlu.Text.Length < 3 || txtTitlu.Text.Length > 15)
            {
                lblTitlu.Foreground = Brushes.Tomato;
                errTitlu.Visibility = Visibility.Visible;
                return false;
            }
            if (!double.TryParse(txtZile.Text, out _) || cmbCategorie.SelectedIndex == -1) return false;
            return true;
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            if (dgTaskuri.SelectedItem is TodoTask t)
            {
                stocare.DeleteTask(t);
                IncarcaDatele();
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTitlu.Clear(); txtZile.Clear(); cmbCategorie.SelectedIndex = -1; rbUrgentNu.IsChecked = true;
            lblTitlu.Foreground = Brushes.White; errTitlu.Visibility = Visibility.Collapsed;
        }

        private void btnIncarca_Click(object sender, RoutedEventArgs e) => IncarcaDatele();
        private void btnCauta_Click(object sender, RoutedEventArgs e) => dgTaskuri.ItemsSource = stocare.SearchTasks(txtCautare.Text);
    }
}