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
    /// Логика взаимодействия для ApartmentDemandsWindow.xaml
    /// </summary>
    public partial class ApartmentDemandsWindow : Window
    {
        private praktikaEntities dbContext = new praktikaEntities();
        public ApartmentDemandsWindow()
        {
            InitializeComponent();
            LoadApartmentDemands();
        }
        private void LoadApartmentDemands()
        {
            // Получение списка всех клиентов из базы данных и отображение их в DataGrid
            dgApartmentDemands.ItemsSource = dbContext.ApartmentDemands.ToList();
        }
    }
}
