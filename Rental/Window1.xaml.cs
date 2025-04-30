using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rental
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Vehicle NewVehicle { get; private set; }
        public Window1()
        {
            InitializeComponent();
            categoryComboBox.SelectionChanged += CategoryComboBox_SelectionChanged;
        }
        public Window1(Vehicle existingVehicle) : this()
        {
            manufacturerTextBox.Text = existingVehicle.Car.Manufacturer;
            modelTextBox.Text = existingVehicle.Car.Model;
            yearTextBox.Text = existingVehicle.Car.Year.ToString();
            priceTextBox.Text = existingVehicle.Car.Price.ToString();
            startDatePicker.SelectedDate = existingVehicle.StartDate;
            vehicleNumberTextBox.Text = existingVehicle.VehicleNumber;
            foreach (ComboBoxItem item in categoryComboBox.Items)
            {
                if (item.Content.ToString() == existingVehicle.Category.ToString())
                {
                    categoryComboBox.SelectedItem = item;
                    break;
                }
            }
            CategoryComboBox_SelectionChanged(categoryComboBox, null);
        }
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoryComboBox.SelectedItem is ComboBoxItem item)
            {
                string category = item.Content.ToString();
                string imagePath = $"C:/Users/user/Desktop/car/car/{category}.jpg";
                carImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string manufacturer = manufacturerTextBox.Text;
                string model = modelTextBox.Text;
                int year = int.Parse(yearTextBox.Text);
                double price = double.Parse(priceTextBox.Text);
                string number = vehicleNumberTextBox.Text;
                DateTime? date = startDatePicker.SelectedDate;

                if (categoryComboBox.SelectedItem == null || !date.HasValue)
                {
                    MessageBox.Show("Заповніть всі поля.");
                    return;
                }

                var category = (Category)Enum.Parse(typeof(Category), ((ComboBoxItem)categoryComboBox.SelectedItem).Content.ToString());

                var car = new Car(manufacturer, model, year, price);
                NewVehicle = new Vehicle(category, car, date.Value, price, 1, number);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }
    }
}
