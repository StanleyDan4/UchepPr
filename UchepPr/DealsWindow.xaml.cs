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
    /// Логика взаимодействия для DealsWindow.xaml
    /// </summary>
    public partial class DealsWindow : Window
    {
        private praktikaEntities dbContext = new praktikaEntities();
        public DealsWindow()
        {
            InitializeComponent();
            LoadDeals();
            LoadData();
        }
        private void LoadData()
        {
            // Load demands into ComboBox
            cmbDemands.ItemsSource = dbContext.Demands.ToList();
            cmbDemands.SelectedValuePath = "ID";

            // Load supplies into ComboBox
            cmbSupplies.ItemsSource = dbContext.Supplies.ToList();
            cmbSupplies.SelectedValuePath = "ID";
        }
        private void LoadDeals()
        {
            // Получение списка всех клиентов из базы данных и отображение их в DataGrid
            dgDeals.ItemsSource = dbContext.Deals.ToList();
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
        private void AddDealButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbDemands.SelectedValue == null || cmbSupplies.SelectedValue == null)
                {
                    MessageBox.Show("Выберите ID потребности и ID предложения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int demandId = (int)cmbDemands.SelectedValue;
                int supplyId = (int)cmbSupplies.SelectedValue;

                Deals newDeal = new Deals
                {
                    DemandID = demandId,
                    SupplyID = supplyId
                };

                dbContext.Deals.Add(newDeal);
                dbContext.SaveChanges();

                LoadDeals();

                MessageBox.Show("Сделка успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении сделки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            LoadDeals();
        }
        private void UpdateDealButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgDeals.SelectedItem is Deals selectedDeal)
                {
                    if (cmbDemands.SelectedValue == null || cmbSupplies.SelectedValue == null)
                    {
                        MessageBox.Show("Выберите ID потребности и ID предложения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    int demandId = (int)cmbDemands.SelectedValue;
                    int supplyId = (int)cmbSupplies.SelectedValue;

                    var existingDeal = dbContext.Deals.Find(selectedDeal.ID);
                    if (existingDeal != null)
                    {
                        existingDeal.DemandID = demandId;
                        existingDeal.SupplyID = supplyId;

                        dbContext.SaveChanges();

                        LoadDeals();

                        MessageBox.Show("Сделка успешно изменена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите сделку для изменения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении сделки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            LoadDeals();
        }
        private void dgDeals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDeals.SelectedItem is Deals selectedDeal)
            {
                cmbDemands.SelectedValue = selectedDeal.DemandID;
                cmbSupplies.SelectedValue = selectedDeal.SupplyID;
            }
        }
        private void DeleteDealButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgDeals.SelectedItem is Deals selectedDeal)
                {
                    var dealToDelete = dbContext.Deals.Find(selectedDeal.ID);
                    if (dealToDelete != null)
                    {
                        dbContext.Deals.Remove(dealToDelete);
                        dbContext.SaveChanges();

                        LoadDeals();

                        MessageBox.Show("Сделка успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите сделку для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении сделки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
