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
    /// Логика взаимодействия для SuppliesWindow.xaml
    /// </summary>
    public partial class SuppliesWindow : Window
    {
        private praktikaEntities dbContext = new praktikaEntities();
        public SuppliesWindow()
        {
            InitializeComponent();
            LoadSupplies();
            LoadData();
        }
        private void LoadData()
        {
            // Load clients into ComboBox
            cmbClients.ItemsSource = dbContext.Clients.ToList();

            cmbClients.SelectedValuePath = "ID";
            // Load agents into ComboBox
            cmbAgents.ItemsSource = dbContext.Agents.ToList();

            cmbAgents.SelectedValuePath = "ID";
            // Load realty objects into ComboBox
            cmbRealtyObjects.ItemsSource = dbContext.RealtyObject.ToList();

            cmbRealtyObjects.SelectedValuePath = "Id";
        }
        private void LoadSupplies()
        {
            // Получение списка всех клиентов из базы данных и отображение их в DataGrid
            dgSupplies.ItemsSource = dbContext.Supplies.ToList();
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
            DemandsWindow demandsWindow = new DemandsWindow();
            demandsWindow.Show();
            this.Close();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            RealtyObjectWindow realtyObjectWindow = new RealtyObjectWindow();
            realtyObjectWindow.Show();
            this.Close();
        }

        private void dgSupplies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSupplies.SelectedItem is Supplies selectedSupply)
            {
                txtOfferID.Text = selectedSupply.ID.ToString();
                txtPrice.Text = selectedSupply.Price;
                cmbClients.SelectedValue = selectedSupply.ClientID;
                cmbAgents.SelectedValue = selectedSupply.AgentID;
                cmbRealtyObjects.SelectedValue = selectedSupply.RealtyObjectID;
            }
        }
        private void AddOfferButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Extract selected IDs and price from the UI
                int id = int.Parse(txtOfferID.Text); // Получаем ID из текстового поля
                int selectedClientID = (int)cmbClients.SelectedValue;
                int selectedAgentID = (int)cmbAgents.SelectedValue;
                int selectedRealtyObjectID = (int)cmbRealtyObjects.SelectedValue;
                string price = txtPrice.Text;

                // Create a new Supplies object
                Supplies newSupply = new Supplies
                {
                    ID = id,
                    ClientID = selectedClientID,
                    AgentID = selectedAgentID,
                    RealtyObjectID = selectedRealtyObjectID,
                    Price = price
                };

                // Add the new supply to the database
                dbContext.Supplies.Add(newSupply);
                dbContext.SaveChanges();

                // Reload supplies in the DataGrid
                LoadSupplies();

                MessageBox.Show("Новое предложение успешно добавлено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении предложения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            LoadSupplies();
        }
        private void SaveOfferButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Extract selected IDs and price from the UI
                int selectedClientID = (int)cmbClients.SelectedValue;
                int selectedAgentID = (int)cmbAgents.SelectedValue;
                int selectedRealtyObjectID = (int)cmbRealtyObjects.SelectedValue;
                string price = txtPrice.Text;

                // Check if we are updating an existing supply or adding a new one
                if (int.TryParse(txtOfferID.Text, out int supplyID))
                {
                    // Update existing supply
                    var existingSupply = dbContext.Supplies.Find(supplyID);
                    if (existingSupply != null)
                    {
                        existingSupply.ClientID = selectedClientID;
                        existingSupply.AgentID = selectedAgentID;
                        existingSupply.RealtyObjectID = selectedRealtyObjectID;
                        existingSupply.Price = price;
                    }
                }
                else
                {
                    // Create a new Supplies object
                    Supplies newSupply = new Supplies
                    {
                        ClientID = selectedClientID,
                        AgentID = selectedAgentID,
                        RealtyObjectID = selectedRealtyObjectID,
                        Price = price
                    };

                    // Add the new supply to the database
                    dbContext.Supplies.Add(newSupply);
                }

                dbContext.SaveChanges();

                // Reload supplies in the DataGrid
                LoadSupplies();

                MessageBox.Show("Предложение успешно сохранено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении предложения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            LoadSupplies();
        }
        private void DeleteOfferButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtOfferID.Text, out int supplyID))
                {
                    var supplyToDelete = dbContext.Supplies.Find(supplyID);
                    if (supplyToDelete != null)
                    {
                        dbContext.Supplies.Remove(supplyToDelete);
                        dbContext.SaveChanges();

                        // Reload supplies in the DataGrid
                        LoadSupplies();

                        // Clear the fields
                        txtOfferID.Clear();
                        txtPrice.Clear();
                        cmbClients.SelectedIndex = -1;
                        cmbAgents.SelectedIndex = -1;
                        cmbRealtyObjects.SelectedIndex = -1;

                        MessageBox.Show("Предложение успешно удалено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Выберите предложение для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите предложение для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении предложения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            LoadSupplies() ;
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            HouseDemandsWindow houseDemandsWindow = new HouseDemandsWindow();
            houseDemandsWindow.Show();
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
