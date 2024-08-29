using BusinessLogics;
using Contracts.BindingModels;
using Contracts.BusinessLogicContracts;
using Contracts.Enums;
using Contracts.ViewModels;
using FileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IUserLogic _logic;
        private int loginAttempts;
        private int? userId;
        public MainWindow()
        {
            InitializeComponent();
            loginAttempts = 0;
            _logic = App.Container.Resolve<IUserLogic>();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы хотите завершить работу программы?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        private void ButtonEntry_Click(object sender, RoutedEventArgs e)
        {
            if (!TextBoxLogin.Text.Equals(""))
            {
                try
                {
                    if (userId == null)
                    {
                        userId = _logic.Autrhorization(TextBoxLogin.Text, TextBoxPassword.Password);
                    }
                    UserViewModel user = _logic.Read(new UserBindingModel
                    {
                        Id = userId,
                    })[0];
                    if (TextBoxLogin.Text.Equals("ADMIN"))
                    {

                        UserRole.Role = Role.ADMIN;
                        UserRole.IdUser = userId;
                        UserRole.IsBlocking = false;

                    }
                    else
                    {
                        UserRole.IdUser = userId;
                        UserRole.Role = Role.USER;
                        UserRole.IsBlocking = user.IsBlocking;
                        if (user.PasswordExpiration > 0 && DateTime.Now < user.CreatedAt.AddMonths(user.PasswordExpiration))
                        {
                            UserRole.PasswordExpiration = false;
                        }
                        else if (user.PasswordExpiration > 0 && DateTime.Now > user.CreatedAt.AddMonths(user.PasswordExpiration))
                        {
                            UserRole.PasswordExpiration = true;
                        }
                        else if (user.PasswordExpiration == 0)
                        {
                            UserRole.PasswordExpiration = true;
                        }
                    }
                    if (user.Password.Equals("") && (user.MinPasswordLength > 0 || UserRole.Role == Role.ADMIN))
                    {
                        if (TextBoxPassword.Password.Equals(""))
                        {
                            MessageBox.Show("Пожалуйста, придумайте пароль и введите его в поле Пароль", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            bool correctPass = _logic.CheckPassword(user, TextBoxPassword.Password);
                            if (correctPass)
                            {
                                var firstEntryWindow = App.Container.Resolve<FirstEntryWindow>();
                                firstEntryWindow.password = TextBoxPassword.Password;
                                firstEntryWindow.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        var window = App.Container.Resolve<WorkWindow>();
                        window.Show();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    if(ex.Message.Equals("Неверный пароль"))
                    {
                        loginAttempts++;
                        if (loginAttempts >= 3)
                        {
                            Application.Current.Shutdown();
                        }
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля формы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
