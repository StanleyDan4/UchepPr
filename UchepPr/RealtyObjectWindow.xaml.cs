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
    /// Логика взаимодействия для RealtyObjectWindow.xaml
    /// </summary>
    public partial class RealtyObjectWindow : Window
    {
        private praktikaEntities dbContext = new praktikaEntities();
        public RealtyObjectWindow()
        {
            InitializeComponent();
            LoadRealtyObjects();
            LoadObjectTypes();
        }
        private void LoadObjectTypes()
        {
            dgObjectTypes.ItemsSource = dbContext.ObjectType.Take(1000).ToList();
        }
        private void LoadRealtyObjects()
        {
            dgRealtyObjects.ItemsSource = dbContext.RealtyObject.ToList();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AgentsWindow agentsWindow = new AgentsWindow();
            agentsWindow.Show();
            this.Close();
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            SearchUser searchUser = new SearchUser();
            searchUser.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                int id = int.Parse(txtID.Text);

                // Создание нового объекта недвижимости на основе введенных данных
                RealtyObject newRealtyObject = new RealtyObject
                {
                    Id = id,
                    Address_City = txtAddress_City.Text,
                    Address_Street = txtAddress_Street.Text,
                    Address_House = string.IsNullOrEmpty(txtAddress_House.Text) ? (int?)null : int.Parse(txtAddress_House.Text),
                    Address_Number = string.IsNullOrEmpty(txtAddress_Number.Text) ? (int?)null : int.Parse(txtAddress_Number.Text),
                    TotalArea = string.IsNullOrEmpty(txtTotalArea.Text) ? (double?)null : double.Parse(txtTotalArea.Text),
                    Rooms = string.IsNullOrEmpty(txtRooms.Text) ? (int?)null : int.Parse(txtRooms.Text),
                    Floor = string.IsNullOrEmpty(txtFloor.Text) ? (int?)null : int.Parse(txtFloor.Text),
                    ObjectTypeId = string.IsNullOrEmpty(txtObjectTypeId.Text) ? (int?)null : int.Parse(txtObjectTypeId.Text),
                    DistrictId = string.IsNullOrEmpty(txtDistrictId.Text) ? (int?)null : int.Parse(txtDistrictId.Text)
                };

                // Добавление нового объекта недвижимости в базу данных
                using (var context = new praktikaEntities())
                {
                    context.RealtyObject.Add(newRealtyObject);
                    context.SaveChanges();
                }

                // Очистка полей после добавления объекта недвижимости
                ClearInputFields();

                MessageBox.Show("Realty Object added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please enter Realty Object ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            LoadRealtyObjects();
        }
        private void ClearInputFields()
        {
            txtID.Clear();
            txtAddress_City.Clear();
            txtAddress_Street.Clear();
            txtAddress_House.Clear();
            txtAddress_Number.Clear();
            txtTotalArea.Clear();
            txtRooms.Clear();
            txtFloor.Clear();
            txtObjectTypeId.Clear();
            txtDistrictId.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Проверяем, что ID объекта недвижимости введено и не пустое
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                int id = int.Parse(txtID.Text);

                // Поиск объекта недвижимости по ID
                var realtyObjectToUpdate = dbContext.RealtyObject.FirstOrDefault(ro => ro.Id == id);

                // Если объект недвижимости найден
                if (realtyObjectToUpdate != null)
                {
                    // Обновляем данные объекта недвижимости на основе введенных в текстовые поля
                    realtyObjectToUpdate.Address_City = txtAddress_City.Text;
                    realtyObjectToUpdate.Address_Street = txtAddress_Street.Text;
                    realtyObjectToUpdate.Address_House = string.IsNullOrEmpty(txtAddress_House.Text) ? (int?)null : int.Parse(txtAddress_House.Text);
                    realtyObjectToUpdate.Address_Number = string.IsNullOrEmpty(txtAddress_Number.Text) ? (int?)null : int.Parse(txtAddress_Number.Text);
                    realtyObjectToUpdate.TotalArea = string.IsNullOrEmpty(txtTotalArea.Text) ? (double?)null : double.Parse(txtTotalArea.Text);
                    realtyObjectToUpdate.Rooms = string.IsNullOrEmpty(txtRooms.Text) ? (int?)null : int.Parse(txtRooms.Text);
                    realtyObjectToUpdate.Floor = string.IsNullOrEmpty(txtFloor.Text) ? (int?)null : int.Parse(txtFloor.Text);
                    realtyObjectToUpdate.ObjectTypeId = string.IsNullOrEmpty(txtObjectTypeId.Text) ? (int?)null : int.Parse(txtObjectTypeId.Text);
                    realtyObjectToUpdate.DistrictId = string.IsNullOrEmpty(txtDistrictId.Text) ? (int?)null : int.Parse(txtDistrictId.Text);

                    // Сохраняем изменения в базе данных
                    dbContext.SaveChanges();

                    // Очищаем текстовые поля после обновления объекта недвижимости
                    ClearInputFields();

                    MessageBox.Show("Realty Object updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Realty Object with specified ID not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter Realty Object ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            LoadRealtyObjects();
        }
        private void dgRealtyObjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, что выбран хотя бы один элемент
            if (dgRealtyObjects.SelectedItem != null)
            {
                // Получаем выбранный объект недвижимости
                RealtyObject selectedRealtyObject = (RealtyObject)dgRealtyObjects.SelectedItem;

                // Заполняем текстовые поля данными выбранного объекта недвижимости
                txtID.Text = selectedRealtyObject.Id.ToString();
                txtAddress_City.Text = selectedRealtyObject.Address_City;
                txtAddress_Street.Text = selectedRealtyObject.Address_Street;
                txtAddress_House.Text = selectedRealtyObject.Address_House.ToString();
                txtAddress_Number.Text = selectedRealtyObject.Address_Number.ToString();
                txtTotalArea.Text = selectedRealtyObject.TotalArea.ToString();
                txtRooms.Text = selectedRealtyObject.Rooms.ToString();
                txtFloor.Text = selectedRealtyObject.Floor.ToString();
                txtObjectTypeId.Text = selectedRealtyObject.ObjectTypeId.ToString();
                txtDistrictId.Text = selectedRealtyObject.DistrictId.ToString();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dgRealtyObjects.SelectedItem != null)
            {
                // Получение выбранной книги
                RealtyObject selectedBook = (RealtyObject)dgRealtyObjects.SelectedItem;

                // Удаление книги из базы данных
                dbContext.RealtyObject.Remove(selectedBook);
                dbContext.SaveChanges();

                // Обновление списка книг
                LoadRealtyObjects();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            SearchRealtyObject searchRealtyObject = new SearchRealtyObject();
            searchRealtyObject.Show();
        }
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            SuppliesWindow suppliesWindow = new SuppliesWindow();
            suppliesWindow.Show();
            this.Close();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            DemandsWindow demandsWindow = new DemandsWindow();
            demandsWindow.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SearchRealtyObject searchRealtyObject = new SearchRealtyObject();
            searchRealtyObject.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            DealsWindow dealsWindow = new DealsWindow();
            dealsWindow.Show();
            this.Close();
        }
    }
}
