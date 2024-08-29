using Contracts.BindingModels;
using Contracts.BusinessLogicContracts;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IB_LW_1
{
    /// <summary>
    /// Логика взаимодействия для CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        public int Id { set { id = value; } }

        private readonly IUserLogic _logic;

        public int? id;

        private bool IsBlocking;
        private bool isLowercaseLetters;
        private bool isUppercaseLetters;
        private bool isNumbers;
        private bool isPunctuationMarks;
        private UserViewModel oldUser;
        public CreateUserWindow(IUserLogic logic)
        {
            InitializeComponent();
            _logic = logic;
            ContentRendered += MainWindow_ContentRendered;
            oldUser = new UserViewModel();    
        }
        private void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                var view = _logic.Read(new UserBindingModel { Id = id })?[0];
                try
                {
                    if (view != null)
                    {
                        oldUser = view;
                        TextBoxLogin.Text = view.Login;
                        TextBoxPasswordExpiration.Text = view.PasswordExpiration.ToString();
                        TextBoxMinPasswordLength.Text = view.MinPasswordLength.ToString();
                        checkBoxBlocking.IsChecked = view.IsBlocking;
                        checkBoxLowercaseLetters.IsChecked = view.isLowercaseLetters;
                        checkBoxUppercaseLetters.IsChecked = view.isUppercaseLetters;
                        checkBoxNumbers.IsChecked = view.isNumbers;
                        checkBoxPunctuationMarks.IsChecked = view.isPunctuationMarks;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!TextBoxLogin.Text.Equals("") && !TextBoxMinPasswordLength.Text.Equals("") && !TextBoxPasswordExpiration.Text.Equals(""))
            {
                if (id.HasValue)
                {
                    if (Convert.ToInt32(TextBoxPasswordExpiration.Text) == oldUser.PasswordExpiration)
                    {
                        _logic.CreateOrUpdate(new UserBindingModel
                        {
                            Id = id,
                            Login = TextBoxLogin.Text,
                            Password = oldUser.Password,
                            CreatedAt = oldUser.CreatedAt,
                            IsBlocking = this.IsBlocking,
                            MinPasswordLength = Convert.ToInt32(TextBoxMinPasswordLength.Text),
                            PasswordExpiration = oldUser.PasswordExpiration,
                            isLowercaseLetters = this.isLowercaseLetters,
                            isUppercaseLetters = this.isUppercaseLetters,
                            isNumbers = this.isNumbers,
                            isPunctuationMarks = this.isPunctuationMarks
                        });
                    }
                    else
                    {
                        _logic.CreateOrUpdate(new UserBindingModel
                        {
                            Id = id,
                            Login = TextBoxLogin.Text,
                            Password = oldUser.Password,
                            CreatedAt = DateTime.Now,
                            IsBlocking = this.IsBlocking,
                            MinPasswordLength = Convert.ToInt32(TextBoxMinPasswordLength.Text),
                            PasswordExpiration = Convert.ToInt32(TextBoxPasswordExpiration.Text),
                            isLowercaseLetters = this.isLowercaseLetters,
                            isUppercaseLetters = this.isUppercaseLetters,
                            isNumbers = this.isNumbers,
                            isPunctuationMarks = this.isPunctuationMarks
                        });
                    }
                    MessageBox.Show("Пользователь успешно обновлен", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                    id = null;
                }
                else
                {
                    try
                    {
                        _logic.CreateOrUpdate(new UserBindingModel
                        {
                            Login = TextBoxLogin.Text,
                            Password = oldUser.Password,
                            CreatedAt = DateTime.Now,
                            IsBlocking = this.IsBlocking,
                            MinPasswordLength = Convert.ToInt32(TextBoxMinPasswordLength.Text),
                            PasswordExpiration = Convert.ToInt32(TextBoxPasswordExpiration.Text),
                            isLowercaseLetters = this.isLowercaseLetters,
                            isUppercaseLetters = this.isUppercaseLetters,
                            isNumbers = this.isNumbers,
                            isPunctuationMarks = this.isPunctuationMarks
                        });
                        MessageBox.Show("Новый пользователь успешно добавлен", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните поле формы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }

        private void checkBoxBlocking_Checked(object sender, RoutedEventArgs e)
        {
            IsBlocking = true;
        }
        private void checkBoxBlocking_Unchecked(object sender, RoutedEventArgs e)
        {
            IsBlocking = false;
        }

        private void checkBoxLowercaseLetters_Checked(object sender, RoutedEventArgs e)
        {
            isLowercaseLetters = true;
        }
        private void checkBoxLowercaseLetters_Unchecked(object sender, RoutedEventArgs e)
        {
            isLowercaseLetters = false;
        }

        private void checkBoxUppercaseLetters_Checked(object sender, RoutedEventArgs e)
        {
            isUppercaseLetters = true;
        }
        private void checkBoxUppercaseLetters_Unchecked(object sender, RoutedEventArgs e)
        {
            isUppercaseLetters = false;
        }

        private void checkBoxNumbers_Checked(object sender, RoutedEventArgs e)
        {
            isNumbers = true;
        }
        private void checkBoxNumbers_Unchecked(object sender, RoutedEventArgs e)
        {
            isNumbers = false;
        }

        private void checkBoxPunctuationMarks_Checked(object sender, RoutedEventArgs e)
        {
            isPunctuationMarks = true;
        }
        private void checkBoxPunctuationMarks_Unchecked(object sender, RoutedEventArgs e)
        {
            isPunctuationMarks = false;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; 
            }
        }

        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _);
        }
    }
}
