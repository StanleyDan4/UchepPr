using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для AgentsWindow.xaml
    /// </summary>
    public partial class AgentsWindow : Window
    {
        private praktikaEntities dbContext = new praktikaEntities();
        public AgentsWindow()
        {
            InitializeComponent();
            LoadAgents();
        }
        private void LoadAgents()
        {
            // Получение списка всех клиентов из базы данных и отображение их в DataGrid
            dgAgents.ItemsSource = dbContext.Agents.ToList();
        }
        private void LoadAgentDemands(int agentId)
        {
            var agentDemandsIds = dbContext.Demands
                                   .Where(ad => ad.AgentId == agentId)
                                   .Select(ad => ad.ID)
                                   .ToList();

            // Получаем потребности с указанными ID
            var agentDemands = dbContext.Demands
                                        .Where(d => agentDemandsIds.Contains(d.ID))
                                        .ToList();

            // Отображаем потребности в DataGrid
            dgAgentDemands.ItemsSource = agentDemands;
        }
        private void LoadAgentOffers(int agentId)
        {
            var offers = dbContext.Supplies.Where(o => o.AgentID == agentId).ToList();
            dgAgentOffers.ItemsSource = offers;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы находитесь в данном окне", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id;
            decimal dealShare;

            // Проверка на корректность ввода ID
            if (!int.TryParse(txtID.Text, out id) || id < 0)
            {
                MessageBox.Show("Некорректное значение ID. Пожалуйста, введите целое положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка на корректность ввода доли от сделки
            if (!decimal.TryParse(txtDealShare.Text, out dealShare) || dealShare < 0)
            {
                MessageBox.Show("Некорректное значение доли от сделки. Пожалуйста, введите положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Получение данных из текстовых полей
            string firstName = txtFirstName.Text;
            string middleName = txtMiddleName.Text;
            string lastName = txtLastName.Text;

            // Создание нового агента
            Agents newAgent = new Agents
            {
                ID = id,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                DealShare = dealShare.ToString() // Преобразуем долю от сделки в строку для сохранения в базе данных
            };

            // Добавление агента в базу данных
            using (var context = new praktikaEntities())
            {
                context.Agents.Add(newAgent);
                context.SaveChanges();
            }

            // Очистка полей после добавления агента
            ClearInputFields();

            MessageBox.Show("Агент успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Обновление DataGrid
            LoadAgents();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int id;
            decimal dealShare;

            // Проверка на корректность ввода ID
            if (!int.TryParse(txtID.Text, out id) || id < 0)
            {
                MessageBox.Show("Некорректное значение ID. Пожалуйста, введите целое положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка на корректность ввода доли от сделки
            if (!decimal.TryParse(txtDealShare.Text, out dealShare) || dealShare < 0)
            {
                MessageBox.Show("Некорректное значение доли от сделки. Пожалуйста, введите положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Поиск агента по ID
            var agentToUpdate = dbContext.Agents.FirstOrDefault(a => a.ID == id);

            // Если агент найден
            if (agentToUpdate != null)
            {
                // Обновляем данные агента на основе введенных в текстовые поля
                agentToUpdate.FirstName = txtFirstName.Text;
                agentToUpdate.MiddleName = txtMiddleName.Text;
                agentToUpdate.LastName = txtLastName.Text;
                agentToUpdate.DealShare = dealShare.ToString(); // Преобразуем долю от сделки в строку для сохранения в базе данных

                // Сохраняем изменения в базе данных
                dbContext.SaveChanges();

                // Обновляем отображение списка агентов
                LoadAgents();

                // Очищаем текстовые поля после обновления агента
                ClearInputFields();

                MessageBox.Show("Агент успешно изменен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Агент с указанным ID не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ClearInputFields()
        {
            txtID.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            txtDealShare.Clear();
        }

        private void dgAgents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, что выбран хотя бы один элемент
            if (dgAgents.SelectedItem != null)
            {
                // Получаем выбранный элемент (агента)
                Agents selectedAgent = (Agents)dgAgents.SelectedItem;

                // Заполняем текстовые поля данными выбранного агента
                txtID.Text = selectedAgent.ID.ToString();
                txtFirstName.Text = selectedAgent.FirstName;
                txtMiddleName.Text = selectedAgent.MiddleName;
                txtLastName.Text = selectedAgent.LastName;
                txtDealShare.Text = selectedAgent.DealShare;

                LoadAgentOffers(selectedAgent.ID);
                LoadAgentDemands(selectedAgent.ID);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dgAgents.SelectedItem != null)
            {
                // Получение выбранной книги
                Agents selectedBook = (Agents)dgAgents.SelectedItem;

                // Удаление книги из базы данных
                dbContext.Agents.Remove(selectedBook);
                dbContext.SaveChanges();

                // Обновление списка книг
                LoadAgents();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            SearchUser searchUser = new SearchUser();
            searchUser.ShowDialog();
        }

        

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            SearchRealtyObject searchRealtyObject = new SearchRealtyObject();
            searchRealtyObject.ShowDialog();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            RealtyObjectWindow realtyObjectWindow = new RealtyObjectWindow();
            realtyObjectWindow.Show();
            this.Close();
        }
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            SuppliesWindow suppliesWindow = new SuppliesWindow();
            suppliesWindow.Show();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            DemandsWindow demandsWindow = new DemandsWindow();
            demandsWindow.Show();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            DealsWindow dealsWindow = new DealsWindow();
            dealsWindow.Show();
            this.Close();
        }
    }
}
