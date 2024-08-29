using BusinessLogics;
using Contracts.BindingModels;
using Contracts.BusinessLogicContracts;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Xml.Linq;
using Unity;

namespace IB_LW_1
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        private readonly IUserLogic _logic;
        public WorkWindow(IUserLogic logic)
        {
            InitializeComponent();
            _logic = logic;
            CheckUserRole();
        }
        private void CheckUserRole()
        {
            if (UserRole.Role == Contracts.Enums.Role.ADMIN)
            {
                MenuItemReadUsers.IsEnabled = true;
            }
            else if (UserRole.Role == Contracts.Enums.Role.USER)
            {
                MenuItemReadUsers.IsEnabled = false;
                if ((!UserRole.PasswordExpiration))
                {
                    TextBlockWarningPassword.Visibility = Visibility.Visible;
                    try
                    {
                        UserViewModel user = _logic.Read(new UserBindingModel
                        {
                            Id = UserRole.IdUser
                        })[0];
                        _logic.CreateOrUpdate(new UserBindingModel
                        {
                            Id = user.Id,
                            Login = user.Login,
                            Password = "",
                            CreatedAt = user.CreatedAt,
                            IsBlocking = user.IsBlocking,
                            MinPasswordLength = user.MinPasswordLength,
                            PasswordExpiration = user.PasswordExpiration,
                            isLowercaseLetters = user.isLowercaseLetters,
                            isUppercaseLetters = user.isUppercaseLetters,
                            isNumbers = user.isNumbers,
                            isPunctuationMarks = user.isPunctuationMarks
                        });

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }            
        }
        private void MenuItemChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (!UserRole.IsBlocking)
            {
                MainFrame.Navigate(new Uri("ChangePasswordPage.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Извините, вы заблокированы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItemReadUsers_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("UsersAdminPage.xaml", UriKind.Relative));
        }
        private void MenuItemInfoAboutApp_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("InfoAboutAppPage.xaml", UriKind.Relative));
        }
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы хотите завершить работу программы?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
