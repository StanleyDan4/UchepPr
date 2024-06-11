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

namespace UchepPr
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private praktikaEntities dbContext = new praktikaEntities();
        public MainWindow()
        {
            InitializeComponent();
            LoadClients();
            dgClients.SelectionChanged += dgClients_SelectionChanged;
        }
        private void LoadClients()
        {
            // Получение списка всех клиентов из базы данных и отображение их в DataGrid
            dgClients.ItemsSource = dbContext.Clients.ToList();
        }
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtID.Text);
            string firstName = txtFirstName.Text;
            string middleName = txtMiddleName.Text;
            string lastName = txtLastName.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;

            // Создание нового клиента
            Clients newClient = new Clients
            {
                ID = id,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Phone = phone,
                Email = email
            };

            // Добавление клиента в базу данных
            using (var context = new praktikaEntities())
            {
                context.Clients.Add(newClient);
                context.SaveChanges();
            }

            // Очистка полей после добавления клиента
            txtID.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();

            MessageBox.Show("Client added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadClients();
        }
        //private void Refresh_Click(object sender, RoutedEventArgs e)
        //{

        //    // Обновляем данные в DataGrid для книг и заказов
        //    LoadClients();

        //}
        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem != null)
            {
                // Получение выбранной книги
                Clients selectedBook = (Clients)dgClients.SelectedItem;

                // Удаление книги из базы данных
                dbContext.Clients.Remove(selectedBook);
                dbContext.SaveChanges();

                // Обновление списка книг
                LoadClients();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UpdateClient_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что ID клиента введено и не пустое
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                int id = int.Parse(txtID.Text);

                // Поиск клиента по ID
                var clientToUpdate = dbContext.Clients.FirstOrDefault(c => c.ID == id);

                // Если клиент найден
                if (clientToUpdate != null)
                {
                    // Обновляем данные клиента на основе введенных в текстовые поля
                    clientToUpdate.FirstName = txtFirstName.Text;
                    clientToUpdate.MiddleName = txtMiddleName.Text;
                    clientToUpdate.LastName = txtLastName.Text;
                    clientToUpdate.Phone = txtPhone.Text;
                    clientToUpdate.Email = txtEmail.Text;

                    // Сохраняем изменения в базе данных
                    dbContext.SaveChanges();

                    // Обновляем отображение списка клиентов
                    LoadClients();

                    // Очищаем текстовые поля после обновления клиента
                    ClearInputFields();

                    MessageBox.Show("Client updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Client with specified ID not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter client ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ClearInputFields()
        {
            txtID.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
        }
        private void dgClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, что выбран хотя бы один элемент
            if (dgClients.SelectedItem != null)
            {
                // Получаем выбранный элемент (клиента)
                Clients selectedClient = (Clients)dgClients.SelectedItem;

                // Заполняем текстовые поля данными выбранного клиента
                txtID.Text = selectedClient.ID.ToString();
                txtFirstName.Text = selectedClient.FirstName;
                txtMiddleName.Text = selectedClient.MiddleName;
                txtLastName.Text = selectedClient.LastName;
                txtPhone.Text = selectedClient.Phone;
                txtEmail.Text = selectedClient.Email;

                LoadClientDemands(selectedClient.ID);
            }

        }
        private void LoadClientDemands(int clientId)
        {
            // Загрузка потребностей для выбранного клиента
            var demands = dbContext.Demands.Where(d => d.ClientId == clientId).ToList();
            dgClientDemands.ItemsSource = demands;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы находитесь в данном окне", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            RealtyObjectWindow realtyObjectWindow = new RealtyObjectWindow();
            realtyObjectWindow.Show();
            this.Close();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
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

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            HouseDemandsWindow houseDemands = new HouseDemandsWindow();
            houseDemands.Show();
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            ApartmentDemandsWindow apartmentDemandsWindow = new ApartmentDemandsWindow();
            apartmentDemandsWindow.Show();
            
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            LandDemandsWindow landDemandsWindow = new LandDemandsWindow();
            landDemandsWindow.Show();
        }

        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            DealsWindow dealsWindow = new DealsWindow();
            dealsWindow.Show();
            this.Close();
        }
    }
}

