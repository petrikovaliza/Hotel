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
    /// Логика взаимодействия для HotelPage.xaml
    /// </summary>
    public partial class HotelPage : Page
    {
        public HotelPage()
        {
            InitializeComponent();
            DGridHotels.ItemsSource = TyrHotelsEnt.GetContext().Hotels.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddHotelPage((sender as Button).DataContext as Hotels));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var HotelsForRemoving = DGridHotels.SelectedItems.Cast<Hotels>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {HotelsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TyrHotelsEnt.GetContext().Hotels.RemoveRange(HotelsForRemoving);
                    TyrHotelsEnt.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridHotels.ItemsSource = TyrHotelsEnt.GetContext().Hotels.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }


        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddHotelPage(null)); // открываем addHotelPage
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                TyrHotelsEnt.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload()); // Если видимость страницы видна, то мы будем обращаться к Context, для каждой из них будем присваивать метод перезагрузки
                DGridHotels.ItemsSource = TyrHotelsEnt.GetContext().Hotels.ToList(); // чето список отелей
            }

        }
    }
}
