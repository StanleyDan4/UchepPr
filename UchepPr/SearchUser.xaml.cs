using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для SearchUser.xaml
    /// </summary>
    public partial class SearchUser : Window
    {
        private praktikaEntities dbContext = new praktikaEntities();
        public SearchUser()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string middleName = txtMiddleName.Text.Trim();
            string lastName = txtLastName.Text.Trim();

            // Выполнение поиска агентов по введенным именам
            List<Agents> agents = dbContext.Agents
                .Where(a =>
                    (string.IsNullOrEmpty(firstName) || a.FirstName.Contains(firstName)) &&
                    (string.IsNullOrEmpty(middleName) || a.MiddleName.Contains(middleName)) &&
                    (string.IsNullOrEmpty(lastName) || a.LastName.Contains(lastName)))
                .ToList();

            // Выполнение поиска клиентов по введенным именам
            List<Clients> clients = dbContext.Clients
                .Where(c =>
                    (string.IsNullOrEmpty(firstName) || c.FirstName.Contains(firstName)) &&
                    (string.IsNullOrEmpty(middleName) || c.MiddleName.Contains(middleName)) &&
                    (string.IsNullOrEmpty(lastName) || c.LastName.Contains(lastName)))
                .ToList();

            // Вывод результатов поиска
            ShowSearchResults(agents, clients);
        }

        private void ShowSearchResults(List<Agents> agents, List<Clients> clients)
        {
            // Отображение результатов поиска
            string result = "Результаты поиска:\n\n";

            result += "Агенты:\n";
            foreach (var agent in agents)
            {
                result += $"ID: {agent.ID}, Имя: {agent.FirstName} {agent.MiddleName} {agent.LastName}, Доля от сделки: {agent.DealShare}\n";
            }

            result += "\nКлиенты:\n";
            foreach (var client in clients)
            {
                result += $"ID: {client.ID}, Имя: {client.FirstName} {client.MiddleName} {client.LastName}, Телефон: {client.Phone}, Email: {client.Email}\n";
            }

            MessageBox.Show(result, "Результаты поиска", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
