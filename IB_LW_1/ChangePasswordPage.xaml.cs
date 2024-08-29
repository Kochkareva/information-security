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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;

namespace IB_LW_1
{
    /// <summary>
    /// Логика взаимодействия для ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        private readonly IUserLogic _logic;
        public ChangePasswordPage()
        {
            InitializeComponent();
            _logic = App.Container.Resolve<IUserLogic>();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!TextBoxPasswordNew1.Password.Equals("") && !TextBoxPasswordNew2.Password.Equals(""))
            {
                if (TextBoxPasswordNew1.Password.Equals(TextBoxPasswordNew2.Password))
                {
                    UserViewModel user = _logic.Read(new UserBindingModel
                    {
                        Id = UserRole.IdUser
                    })[0];
                    try
                    {
                        int resultId = _logic.Autrhorization(user.Login, TextBoxPasswordOld.Password);
                        if (resultId == UserRole.IdUser)
                        {

                            if (_logic.CheckPassword(user, TextBoxPasswordNew1.Password))
                            {
                                _logic.CreateOrUpdate(new UserBindingModel
                                {
                                    Id = user.Id,
                                    Login = user.Login,
                                    Password = TextBoxPasswordNew1.Password,
                                    CreatedAt = DateTime.Now,
                                    IsBlocking = user.IsBlocking,
                                    MinPasswordLength = user.MinPasswordLength,
                                    PasswordExpiration = user.PasswordExpiration,
                                    isLowercaseLetters = user.isLowercaseLetters,
                                    isUppercaseLetters = user.isUppercaseLetters,
                                    isNumbers = user.isNumbers,
                                    isPunctuationMarks = user.isPunctuationMarks
                                });
                                MessageBox.Show("Пароль успешно обновлен", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                                TextBoxPasswordOld.Password = "";
                                TextBoxPasswordNew1.Password = "";
                                TextBoxPasswordNew2.Password = "";
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
                    MessageBox.Show("Заданные пароли не совпадают. Проверьте введенные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля формы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
