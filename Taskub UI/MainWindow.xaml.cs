using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskHub.Logic;
using TaskHub.Models;
namespace TaskHub.UI
{
    public partial class MainWindow : Window
    {
        // Refolosim logica de la laboratorul anterior
        // Folosește simbolul @ pentru a accepta backslash-uri fără erori
        IStocareData stocare = new AdministrareTaskuriFisierText(@"D:\Taskhub\taskhub2\TaskHub.App\bin\Debug\net8.0\taskuri.txt");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnIncarca_Click(object sender, RoutedEventArgs e)
        {
            // Citim task-urile folosind metoda existentă
            List<TodoTask> lista = stocare.GetTasks();

            // Le trimitem către tabelul din interfață
            dgTaskuri.ItemsSource = lista;
        }
    }
}