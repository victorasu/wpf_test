using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Akkodis.Data;
using Akkodis.Model;
using Microsoft.EntityFrameworkCore;

namespace Akkodis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AkkodisDbContext _context = new AkkodisDbContext();

        private CollectionViewSource ClientViewSource;
        private CollectionViewSource CarViewSource;

        public MainWindow()
        {
            InitializeComponent();
            ClientViewSource = (CollectionViewSource) FindResource(nameof(ClientViewSource));
            CarViewSource = (CollectionViewSource)FindResource(nameof(CarViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            
            _context.Clients.Load();
            _context.Cars.Load();

            ClientViewSource.Source =
                _context.Clients.Local.ToObservableCollection();
            CarViewSource.Source = 
                _context.Cars.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveAndPersist();
        }

        private void SaveAndPersist()
        {
            _context.SaveChanges();

            carsDataGrid.Items.Refresh();
            clientDataGrid.Items.Refresh();
            HandleAttributeCar();
        }

        private void clientDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            HandleAttributeCar();
        }

        private void carsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            HandleAttributeCar();
        }

        private void HandleAttributeCar()
        {
            if (carsDataGrid.SelectedItem != null && carsDataGrid.SelectedItem != CollectionView.NewItemPlaceholder &&
                clientDataGrid.SelectedItem != null && clientDataGrid.SelectedItem != CollectionView.NewItemPlaceholder) //teribil de urat dar e tarziu
            {
                var car = (Car)carsDataGrid.SelectedItem;
                var client = (Client)clientDataGrid.SelectedItem;
                AttrCarButton.Content = $"Attribute car {car.Id} to client {client.Id}";
                AttrCarButton.IsEnabled = true;
            }
            else
            {
                AttrCarButton.Content = "Attribute car to client";
                AttrCarButton.IsEnabled = false;
            }
        }

        private void AttrCarButton_Click(object sender, RoutedEventArgs e)
        {
            var car = (Car)carsDataGrid.SelectedItem;
            var client = (Client)clientDataGrid.SelectedItem;

            if (client.Cars.Contains(car))
                return;

            client.Cars.Add(car);
            SaveAndPersist();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _context.Dispose();
            base.OnClosing(e);
        }
    }
}
