using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;



namespace Rental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            vehicleListBox.Visibility = Visibility.Visible;
            rentalListBox.Visibility = Visibility.Visible;
            LoadData();
            this.Closing += Window_Closing;
        }
        public class VehicleDTO
        {
            public Category Category { get; set; }
            public string Manufacturer { get; set; }
            public string Model { get; set; }
            public int Year { get; set; }
            public double Price { get; set; }
            public DateTime StartDate { get; set; }
            public string VehicleNumber { get; set; }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult != true) // якщо ще не збережено
            {
                var result = MessageBox.Show(
                    "Зберегти зміни перед закриттям?",
                    "Підтвердження",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Save_Close(null, null);
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            var addWindow = new Window1();
            if (addWindow.ShowDialog() == true)
            {
                vehicleListBox.Items.Add(addWindow.NewVehicle);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (vehicleListBox.SelectedItem != null)
                vehicleListBox.Items.Remove(vehicleListBox.SelectedItem);
            else
                MessageBox.Show("Будь ласка, виберіть транспорт для видалення.");
        }

        private void Save_Close(object sender, RoutedEventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "vehicles.json");

            List<VehicleDTO> data = new List<VehicleDTO>();

            foreach (var item in vehicleListBox.Items)
            {
                if (item is Vehicle v)
                {
                    data.Add(new VehicleDTO
                    {
                        Category = v.Category,
                        Manufacturer = v.Car.Manufacturer,
                        Model = v.Car.Model,
                        Year = v.Car.Year,
                        Price = v.Car.Price,
                        StartDate = v.StartDate,
                        VehicleNumber = v.VehicleNumber
                    });
                }
            }

            try
            {
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при збереженні JSON: {ex.Message}");
            }
        }

        private void LoadData()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "vehicles.json");
            if (!File.Exists(path)) return;

            try
            {
                string json = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<List<VehicleDTO>>(json);

                int count = 0; 

                foreach (var dto in data)
                {
                    var car = new Car(dto.Manufacturer, dto.Model, dto.Year, dto.Price);
                    var vehicle = new Vehicle(dto.Category, car, dto.StartDate, car.Price, 0, dto.VehicleNumber);

                    vehicleListBox.Items.Add(vehicle);
                    if (count ==1||count==3||count==4)
                    {
                        rentalListBox.Items.Add(vehicle.ToShortString());
                    }
                    count++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні JSON: {ex.Message}");
            }
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (vehicleListBox.SelectedItem is Vehicle selectedVehicle)
            {
                var editWindow = new Window1(selectedVehicle);
                if (editWindow.ShowDialog() == true)
                {
                    int index = vehicleListBox.SelectedIndex;
                    vehicleListBox.Items[index] = editWindow.NewVehicle;
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть транспорт для редагування.");
            }
        }
    }
}