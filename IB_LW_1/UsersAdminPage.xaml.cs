using BusinessLogics;
using Contracts.BusinessLogicContracts;
using Contracts.ViewModels;
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
using Unity;

namespace IB_LW_1
{
    /// <summary>
    /// Логика взаимодействия для UsersAdminPage.xaml
    /// </summary>
    public partial class UsersAdminPage : Page
    {

        private List<UserViewModel> sourceUser;

        private readonly IUserLogic _logic;
        public UsersAdminPage()
        {
            InitializeComponent();
            _logic = App.Container.Resolve<IUserLogic>();
            sourceUser = new List<UserViewModel>();
            LoadData();
        }
        private void LoadData()
        {
            sourceUser = new List<UserViewModel>();
            ListViewUser.ItemsSource = null;
            try
            {
                sourceUser = _logic.Read(null);
                if (sourceUser != null)
                {
                    sourceUser.RemoveAt(0);
                    ListViewUser.ItemsSource = sourceUser;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ButtonCreateUser_Click(object sender, RoutedEventArgs e)
        {
            
            var window = App.Container.Resolve<CreateUserWindow>();
            window.id = null;
            window.ShowDialog();
            LoadData();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            var elem = ((Button)sender).Tag.ToString();            
            var window = App.Container.Resolve<CreateUserWindow>();
            window.id = Convert.ToInt32(elem);
            window.ShowDialog();
            LoadData();
        }
    }
}
