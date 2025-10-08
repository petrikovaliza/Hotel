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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для AddHotelPage.xaml
    /// </summary>
    public partial class AddHotelPage : Page
    {
        private Hotels _currentHotels = new Hotels();

        public AddHotelPage(Hotels selectedHotels)
        {
            InitializeComponent();

            if (selectedHotels != null)
                _currentHotels = selectedHotels;

            DataContext = _currentHotels;
            ComboCountry.ItemsSource = TyrHotelsEnt.GetContext().Country.ToList(); 
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder(); // обработка ошибок

            if (string.IsNullOrWhiteSpace(_currentHotels.Name))
                errors.AppendLine("Укажите название отеля");

            if (_currentHotels.CountOfStar < 1 || _currentHotels.CountOfStar > 5)
                errors.AppendLine("Количество звезд должно быть от 1 до 5");

            if (_currentHotels.Country == null)
                errors.AppendLine("Укажите страну");

            if (errors.Length > 0)
            { 
                MessageBox.Show(errors.ToString()); 
                return;
            }
            if (_currentHotels.ID == 0)
                TyrHotelsEnt.GetContext().Hotels.Add(_currentHotels);

            try
            {
                TyrHotelsEnt.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex) // всегда пишется по умолчанию в этом методе (Exception)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void ComboCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
