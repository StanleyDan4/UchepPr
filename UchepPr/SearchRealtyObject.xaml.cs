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

namespace UchepPr
{
    /// <summary>
    /// Логика взаимодействия для SearchRealtyObject.xaml
    /// </summary>
    public partial class SearchRealtyObject : Window
    {
        private praktikaEntities dbContext = new praktikaEntities();
        public SearchRealtyObject()
        {
            InitializeComponent();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string city = txtCity.Text.Trim();
            string street = txtStreet.Text.Trim();
            string house = txtHouse.Text.Trim();
            string number = txtNumber.Text.Trim();

            // Выполнение поиска объектов недвижимости по введенным данным
            List<RealtyObject> realtyObjects = dbContext.RealtyObject
                .Where(ro =>
                    (string.IsNullOrEmpty(city) || ro.Address_City.Contains(city)) &&
                    (string.IsNullOrEmpty(street) || ro.Address_Street.Contains(street)) &&
                    (string.IsNullOrEmpty(house) || ro.Address_House.ToString().Contains(house)) &&
                    (string.IsNullOrEmpty(number) || ro.Address_Number.ToString().Contains(number)))
                .ToList();

            // Вывод результатов поиска
            ShowSearchResults(realtyObjects);
        }

        private void ShowSearchResults(List<RealtyObject> realtyObjects)
        {
            // Отображение результатов поиска
            string result = "Результаты поиска:\n\n";

            foreach (var realtyObject in realtyObjects)
            {
                result += $"ID: {realtyObject.Id}, Адрес: {realtyObject.Address_City} {realtyObject.Address_Street} {realtyObject.Address_House}-{realtyObject.Address_Number}, Площадь: {realtyObject.TotalArea}, Комнаты: {realtyObject.Rooms}, Этаж: {realtyObject.Floor}\n";
            }

            txtResults.Text = result;
        }
    }
}
