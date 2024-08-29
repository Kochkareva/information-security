using Contracts.ViewModels;
using Contracts.BusinessLogicContracts;
using Contracts.BindingModels;
using Contracts.StoragesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace BusinessLogics
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserStorage _userStorage;

        public UserLogic(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public List<UserViewModel> Read(UserBindingModel model)
        {
            if (model == null)
            {
                return _userStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<UserViewModel> { _userStorage.GetElement(model) };
            }
            return _userStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(UserBindingModel model)
        {
            var element = _userStorage.GetElement(new UserBindingModel
            {
                Login = model.Login
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть пользователь с таким именем");
            }
            if (model.Id.HasValue)
            {
                _userStorage.Update(new UserBindingModel
                {
                    Id = model.Id.Value,
                    Login = model.Login,
                    Password = PasswordHashing(model.Password),
                    CreatedAt = model.CreatedAt,
                    IsBlocking = model.IsBlocking,
                    MinPasswordLength = model.MinPasswordLength,
                    PasswordExpiration = model.PasswordExpiration,
                    isLowercaseLetters = model.isLowercaseLetters,
                    isUppercaseLetters = model.isUppercaseLetters,
                    isNumbers = model.isNumbers,
                    isPunctuationMarks = model.isPunctuationMarks
                });
            }
            else
            {
                _userStorage.Insert(new UserBindingModel
                {
                    Login = model.Login,
                    Password = PasswordHashing(model.Password),
                    CreatedAt = model.CreatedAt,
                    IsBlocking = model.IsBlocking,
                    MinPasswordLength = model.MinPasswordLength,
                    PasswordExpiration = model.PasswordExpiration,
                    isLowercaseLetters = model.isLowercaseLetters,
                    isUppercaseLetters = model.isUppercaseLetters,
                    isNumbers = model.isNumbers,
                    isPunctuationMarks = model.isPunctuationMarks
                });
            }
        }
        public string PasswordHashing(string password)
        {
            if (password != null)
            {
                string hashedPassword = "";
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }

                    hashedPassword = builder.ToString();
                }
                return hashedPassword;
            }
            else
            {
                return password;
            }
        }

        public int Autrhorization(string login, string password)
        {
            var user = _userStorage.GetElement(new UserBindingModel
            {
                Login = login
            });
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }
            if (!password.Equals(""))
            {
                password = PasswordHashing(password);
            }
            if (!user.Password.Equals(password))
            {
                throw new Exception("Неверный пароль");
            }
            return user.Id;
        }

        public bool CheckPassword(UserViewModel user, string password)
        {
            string userInput = password;
            bool hasLowercase = false;
            bool hasUppercase = false;
            bool hasDigits = false;
            bool hasPunctuation = false;
            if (user.isLowercaseLetters)
            {                
                hasLowercase = Regex.IsMatch(userInput, "[a-z]|[а-яё]");
                if(!hasLowercase)
                {
                    throw new Exception("Необходимо использовать строчные буквы");
                }
            }
            if (user.isUppercaseLetters)
            {
                hasUppercase = Regex.IsMatch(userInput, "[A-Z]|[А-ЯЁ]");
                if(!hasUppercase)
                {
                    throw new Exception("Необходимо использовать прописные буквы");
                }
            }            
            if(user.isNumbers)
            {
                hasDigits = Regex.IsMatch(userInput, "[0-9]");
                if(!hasDigits)
                {
                    throw new Exception("Необходимо использовать цифры");
                }
            }
            if (user.isPunctuationMarks)
            {
                hasPunctuation = Regex.IsMatch(userInput, @"[\p{P}]");
                if(!hasPunctuation)
                {
                    throw new Exception("Необходимо использовать знаки препинания");
                }
            }
            if(user.MinPasswordLength > 0)
            {
                if(password.Length < user.MinPasswordLength)
                {
                    throw new Exception("Длина пароля должна превышать " + user.MinPasswordLength + " символов");
                }
            }
            return true;
        }
    }
}
