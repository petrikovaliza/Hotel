using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ToursPage());
            /*MainFrame.Navigate(new HotelPage());*/ // переходим к main frame, с помощью new даем место HotelPage
            Manager.MainFrame = MainFrame;

            ImportTours();
        }

        private void ImportTours()
        {
            var fileData = File.ReadAllLines(@"E:\Я\БД\Liza\06.12\toor.txt");
            var images = Directory.GetFiles(@"E:\Я\БД\Liza\06.12\Туры фото");

            foreach (var line in fileData)
            {
                var data = line.Split('\t');

                var tempTour = new Toor
                {
                    Name = data[0].Replace("\"", ""),
                    TicketCount = int.Parse(data[2]),
                    Price = decimal.Parse(data[3]),
                    IsActual = (data[4] == "0") ? false : true,
                };

                foreach (var tourType in data[5].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var currentType = TyrHotelsEnt.GetContext().Type.ToList().FirstOrDefault(p => p.Name == tourType);
                    if (currentType != null)
                        tempTour.Type.Add(currentType);
                }

                try
                {
                    tempTour.ImagePrieview = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(tempTour.Name)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //TyrHotelsEnt.GetContext().Toor.Add(tempTour);
                //TyrHotelsEnt.GetContext().SaveChanges();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack(); // переходим назада с помощью метода goback


        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)  // если frame осуществляет переход
            {
                BtnBack.Visibility = Visibility.Visible;  // то кнопка назад видна. Я назначаю ей видимость
            }
            else 
            {
                BtnBack.Visibility = Visibility.Hidden; // иначе скрываем кнопку
            }
        }
    }
}
