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
    /// Логика взаимодействия для LandDemandsWindow.xaml
    /// </summary>
    public partial class LandDemandsWindow : Window
    {
        private praktikaEntities dbContext = new praktikaEntities();
        public LandDemandsWindow()
        {
            InitializeComponent();
            LoadLandDemands();
        }
        private void LoadLandDemands()
        {
            // Получение списка всех клиентов из базы данных и отображение их в DataGrid
            dgLandDemands.ItemsSource = dbContext.LandDemands.ToList();
        }
    }
}
