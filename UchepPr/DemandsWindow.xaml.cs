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
    /// Логика взаимодействия для DemandsWindow.xaml
    /// </summary>
    public partial class DemandsWindow : Window
    {
        private praktikaEntities dbContext = new praktikaEntities();
        public DemandsWindow()
        {
            InitializeComponent();
            LoadDemands();
            LoadData();
        }
        private void LoadData()
        {
            // Load clients into ComboBox
            cmbClients.ItemsSource = dbContext.Clients.ToList();
            cmbClients.SelectedValuePath = "ID";
            cmbClients.SelectedValuePath = "ID";
            // Load agents into ComboBox
            cmbAgents.ItemsSource = dbContext.Agents.ToList();
            cmbClients.SelectedValuePath = "ID";
            cmbAgents.SelectedValuePath = "ID";
            // Load realty objects into ComboBox
        }
        private void LoadDemands()
        {
            // Получение списка всех клиентов из базы данных и отображение их в DataGrid
            dgDemands.ItemsSource = dbContext.Demands.ToList();
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
            SuppliesWindow suppliesWindow = new SuppliesWindow();
            suppliesWindow.Show();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            RealtyObjectWindow realtyObjectWindow = new RealtyObjectWindow();
            realtyObjectWindow.Show();
            this.Close();
        }

        private void SaveDemandButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Extract selected IDs and other details from the UI
                string city = txtCity.Text;
                string street = txtStreet.Text;

                // Validate and parse house and houseNumber
                int? house = string.IsNullOrEmpty(txtHouse.Text) ? (int?)null : int.Parse(txtHouse.Text);
                int? houseNumber = string.IsNullOrEmpty(txtHouseNumber.Text) ? (int?)null : int.Parse(txtHouseNumber.Text);

                // Validate and parse MinPrice and MaxPrice
                long? minPrice = string.IsNullOrEmpty(txtMinPrice.Text) ? (long?)null : long.Parse(txtMinPrice.Text);
                long? maxPrice = string.IsNullOrEmpty(txtMaxPrice.Text) ? (long?)null : long.Parse(txtMaxPrice.Text);

                if (!int.TryParse(txtID.Text, out int demandID))
                {
                    MessageBox.Show("Некорректный формат для ID.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cmbClients.SelectedValue == null || cmbAgents.SelectedValue == null)
                {
                    MessageBox.Show("Выберите клиента и агента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int clientId = (int)cmbClients.SelectedValue;
                int agentId = (int)cmbAgents.SelectedValue;

                int? houseDemandId = string.IsNullOrEmpty(txtHouseDemand.Text) ? (int?)null : int.Parse(txtHouseDemand.Text);
                int? apartmentDemandId = string.IsNullOrEmpty(txtApartmentDemand.Text) ? (int?)null : int.Parse(txtApartmentDemand.Text);
                int? landDemandId = string.IsNullOrEmpty(txtLandDemand.Text) ? (int?)null : int.Parse(txtLandDemand.Text);

                if (!int.TryParse(txtObjectType.Text, out int objectTypeId))
                {
                    MessageBox.Show("Некорректный формат для Тип объекта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Check if we are updating an existing demand or adding a new one
                if (dgDemands.SelectedItem is Demands selectedDemand)
                {
                    // Update existing demand
                    var existingDemand = dbContext.Demands.Find(selectedDemand.ID);
                    if (existingDemand != null)
                    {
                        existingDemand.Address_City = city;
                        existingDemand.Address_Street = street;
                        existingDemand.Address_House = house;
                        existingDemand.Address_Number = houseNumber;
                        existingDemand.MinPrice = minPrice;
                        existingDemand.MaxPrice = maxPrice;
                        existingDemand.ClientId = clientId;
                        existingDemand.AgentId = agentId;
                        existingDemand.HouseDemandsId = houseDemandId;
                        existingDemand.ApartmentDemandsId = apartmentDemandId;
                        existingDemand.LandDemandsId = landDemandId;
                        existingDemand.ObjectTypeId = objectTypeId;
                    }
                }
                else
                {
                    // Create a new Demands object
                    Demands newDemand = new Demands
                    {
                        ID = demandID,
                        Address_City = city,
                        Address_Street = street,
                        Address_House = house,
                        Address_Number = houseNumber,
                        MinPrice = minPrice,
                        MaxPrice = maxPrice,
                        ClientId = clientId,
                        AgentId = agentId,
                        HouseDemandsId = houseDemandId,
                        ApartmentDemandsId = apartmentDemandId,
                        LandDemandsId = landDemandId,
                        ObjectTypeId = objectTypeId
                    };

                    // Add the new demand to the database
                    dbContext.Demands.Add(newDemand);
                }

                dbContext.SaveChanges();

                // Reload demands in the DataGrid
                LoadDemands();

                MessageBox.Show("Потребность успешно сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении потребности: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void dgDemands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDemands.SelectedItem is Demands selectedDemand)
            {
                txtCity.Text = selectedDemand.Address_City;
                txtStreet.Text = selectedDemand.Address_Street;
                txtHouse.Text = selectedDemand.Address_House.ToString();
                txtHouseNumber.Text = selectedDemand.Address_Number.ToString();
                txtMinPrice.Text = selectedDemand.MinPrice.ToString();
                txtMaxPrice.Text = selectedDemand.MaxPrice.ToString();
                cmbClients.SelectedValue = selectedDemand.ClientId;
                cmbAgents.SelectedValue = selectedDemand.AgentId;
                txtHouseDemand.Text = selectedDemand.HouseDemandsId?.ToString(); // Обработка null
                txtApartmentDemand.Text = selectedDemand.ApartmentDemandsId?.ToString(); // Обработка null
                txtLandDemand.Text = selectedDemand.LandDemandsId?.ToString(); // Обработка null
                txtObjectType.Text = selectedDemand.ObjectTypeId?.ToString(); // Обработка null
            }

        }
        private void DeleteDemandButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgDemands.SelectedItem is Demands selectedDemand)
                {
                    var demandToDelete = dbContext.Demands.Find(selectedDemand.ID);
                    if (demandToDelete != null)
                    {
                        dbContext.Demands.Remove(demandToDelete);
                        dbContext.SaveChanges();

                        // Reload demands in the DataGrid
                        LoadDemands();

                        // Clear the fields
                        txtCity.Clear();
                        txtStreet.Clear();
                        txtHouse.Clear();
                        txtHouseNumber.Clear();
                        txtMinPrice.Clear();
                        txtMaxPrice.Clear();
                        cmbClients.SelectedIndex = -1;
                        cmbAgents.SelectedIndex = -1;
                        txtHouseDemand.Clear();
                        txtApartmentDemand.Clear();
                        txtLandDemand.Clear();
                        txtObjectType.Clear();

                        MessageBox.Show("Потребность успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Выберите потребность для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите потребность для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении потребности: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        //private void UpdateDemandButton_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (!int.TryParse(txtID.Text, out int demandID))
        //        {
        //            MessageBox.Show("Некорректный формат для ID.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //            return;
        //        }

        //        var existingDemand = dbContext.Demands.Find(demandID);
        //        if (existingDemand == null)
        //        {
        //            MessageBox.Show("Потребность с указанным ID не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //            return;
        //        }

        //        string city = txtCity.Text;
        //        string street = txtStreet.Text;
        //        int? house = string.IsNullOrEmpty(txtHouse.Text) ? (int?)null : int.Parse(txtHouse.Text);
        //        int? houseNumber = string.IsNullOrEmpty(txtHouseNumber.Text) ? (int?)null : int.Parse(txtHouseNumber.Text);
        //        long? minPrice = string.IsNullOrEmpty(txtMinPrice.Text) ? (long?)null : long.Parse(txtMinPrice.Text);
        //        long? maxPrice = string.IsNullOrEmpty(txtMaxPrice.Text) ? (long?)null : long.Parse(txtMaxPrice.Text);

        //        if (cmbClients.SelectedValue == null || cmbAgents.SelectedValue == null)
        //        {
        //            MessageBox.Show("Выберите клиента и агента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //            return;
        //        }

        //        int clientId = (int)cmbClients.SelectedValue;
        //        int agentId = (int)cmbAgents.SelectedValue;

        //        int? houseDemandId = string.IsNullOrEmpty(txtHouseDemand.Text) ? (int?)null : int.Parse(txtHouseDemand.Text);
        //        int? apartmentDemandId = string.IsNullOrEmpty(txtApartmentDemand.Text) ? (int?)null : int.Parse(txtApartmentDemand.Text);
        //        int? landDemandId = string.IsNullOrEmpty(txtLandDemand.Text) ? (int?)null : int.Parse(txtLandDemand.Text);

        //        if (!int.TryParse(txtObjectType.Text, out int objectTypeId))
        //        {
        //            MessageBox.Show("Некорректный формат для Тип объекта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //            return;
        //        }

        //        existingDemand.Address_City = city;
        //        existingDemand.Address_Street = street;
        //        existingDemand.Address_House = house;
        //        existingDemand.Address_Number = houseNumber;
        //        existingDemand.MinPrice = minPrice;
        //        existingDemand.MaxPrice = maxPrice;
        //        existingDemand.ClientId = clientId;
        //        existingDemand.AgentId = agentId;
        //        existingDemand.HouseDemandsId = houseDemandId;
        //        existingDemand.ApartmentDemandsId = apartmentDemandId;
        //        existingDemand.LandDemandsId = landDemandId;
        //        existingDemand.ObjectTypeId = objectTypeId;

        //        dbContext.SaveChanges();

        //        // Reload demands in the DataGrid
        //        LoadDemands();

        //        MessageBox.Show("Потребность успешно обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка при обновлении потребности: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

    }
}
