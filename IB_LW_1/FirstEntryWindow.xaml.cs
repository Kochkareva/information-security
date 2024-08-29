using Contracts.BindingModels;
using Contracts.BusinessLogicContracts;
using Contracts.ViewModels;
using FileImplement.Models;
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
using Unity;

namespace IB_LW_1
{
    /// <summary>
    /// Логика взаимодействия для FirstEntryWindow.xaml
    /// </summary>
    public partial class FirstEntryWindow : Window
    {
        public string password;
        private readonly IUserLogic _logic;
        public FirstEntryWindow(IUserLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (password.Equals(TextBoxPassword.Password))
            {
                try
                {
                    UserViewModel user = _logic.Read(new UserBindingModel
                    {
                        Id = UserRole.IdUser
                    })[0];
                    if (_logic.CheckPassword(user, TextBoxPassword.Password))
                    {
                        _logic.CreateOrUpdate(new UserBindingModel
                        {
                            Id = user.Id,
                            Login = user.Login,
                            Password = TextBoxPassword.Password,
                            CreatedAt = user.CreatedAt,
                            IsBlocking = user.IsBlocking,
                            MinPasswordLength = user.MinPasswordLength,
                            PasswordExpiration = user.PasswordExpiration,
                            isLowercaseLetters = user.isLowercaseLetters,
                            isUppercaseLetters = user.isUppercaseLetters,
                            isNumbers = user.isNumbers,
                            isPunctuationMarks = user.isPunctuationMarks
                        });
                        MessageBox.Show("Пароль успешно создан", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                        var workWindow = App.Container.Resolve<WorkWindow>();
                        workWindow.Show();
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window != workWindow)
                            {
                                window.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введенные пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы хотите завершить работу программы?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
