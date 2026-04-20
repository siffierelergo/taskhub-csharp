using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TaskHub.Models;
using TaskHub.Logic;

namespace TaskHub.UI
{
    public partial class MainWindow : Window
    {
        private const int MIN_LUNGIME_TITLU = 3;
        private const int MAX_LUNGIME_TITLU = 15;
        private IStocareData stocare = new AdministrareTaskuriFisierText("taskuri.txt");

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

        private void btnIncarca_Click(object sender, RoutedEventArgs e) => IncarcaDatele();

        private void btnCauta_Click(object sender, RoutedEventArgs e)
        {
            dgTaskuri.ItemsSource = stocare.SearchTasks(txtCautare.Text);
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            ResetareVizualaValidare();
            if (ValideazaDate())
            {
                string catStr = (cmbCategorie.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Personal";
                TaskCategory catEnum = (TaskCategory)Enum.Parse(typeof(TaskCategory), catStr);

                var nouTask = new TodoTask
                {
                    Title = txtTitlu.Text,
                    Category = catEnum,
                    DueDate = DateTime.Now.AddDays(double.Parse(txtZile.Text)),
                    Priority = TaskPriority.Medium,
                    IsCompleted = false
                };

                stocare.AddTask(nouTask);
                IncarcaDatele();
                MessageBox.Show("Task salvat!");
                btnReset_Click(null, null);
            }
        }

        private bool ValideazaDate()
        {
            bool esteValid = true;
            SolidColorBrush culoareEroare = Brushes.Tomato;

            if (string.IsNullOrWhiteSpace(txtTitlu.Text) || txtTitlu.Text.Length < MIN_LUNGIME_TITLU || txtTitlu.Text.Length > MAX_LUNGIME_TITLU)
            {
                lblTitlu.Foreground = culoareEroare;
                errTitlu.Visibility = Visibility.Visible;
                esteValid = false;
            }

            if (cmbCategorie.SelectedIndex == -1)
            {
                lblCategorie.Foreground = culoareEroare;
                esteValid = false;
            }

            if (!double.TryParse(txtZile.Text, out double zile) || zile < 0)
            {
                lblZile.Foreground = culoareEroare;
                errZile.Visibility = Visibility.Visible;
                esteValid = false;
            }

            return esteValid;
        }

        private void ResetareVizualaValidare()
        {
            SolidColorBrush culoareNormala = new SolidColorBrush(Color.FromRgb(224, 224, 224));
            lblTitlu.Foreground = culoareNormala;
            lblCategorie.Foreground = culoareNormala;
            lblZile.Foreground = culoareNormala;
            errTitlu.Visibility = Visibility.Collapsed;
            errZile.Visibility = Visibility.Collapsed;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTitlu.Clear();
            txtZile.Clear();
            cmbCategorie.SelectedIndex = -1;
            ResetareVizualaValidare();
        }
    }
}