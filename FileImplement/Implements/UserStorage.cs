using Contracts.ViewModels;
using Contracts.BindingModels;
using FileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.StoragesContracts;

namespace FileImplement.Implements
{
    public class UserStorage : IUserStorage
    {
        private readonly FileDataListSingleton source;

        public UserStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<UserViewModel> GetFullList()
        {
            return source.Users
            .Select(CreateModel)
           .ToList();
        }

        public List<UserViewModel> GetFilteredList(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Users
            .Where(rec => rec.Login.Contains(model.Login))
           .Select(CreateModel)
           .ToList();
        }

        public UserViewModel GetElement(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var user = source.Users
            .FirstOrDefault(rec => rec.Login == model.Login ||
           rec.Id == model.Id);
            return user != null ? CreateModel(user) : null;
        }

        public void Insert(UserBindingModel model)
        {
            int maxId = source.Users.Count > 0 ? source.Users.Max(rec => rec.Id) : 0;
            var element = new User { Id = maxId + 1 };
            source.Users.Add(CreateModel(model, element));
            source.UpdateUsers();
        }

        public void Update(UserBindingModel model)
        {
            var element = source.Users.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            source.UpdateUsers();
        }  

        private static User CreateModel(UserBindingModel model, User user)
        {
            user.Login = model.Login;
            user.Password = model.Password;
            user.CreatedAt = model.CreatedAt;
            user.IsBlocking = model.IsBlocking;
            user.MinPasswordLength = model.MinPasswordLength;
            user.PasswordExpiration = model.PasswordExpiration;
            user.isLowercaseLetters = model.isLowercaseLetters;
            user.isUppercaseLetters = model.isUppercaseLetters;
            user.isNumbers = model.isNumbers;
            user.isPunctuationMarks = model.isPunctuationMarks;
            return user;
        }
        private UserViewModel CreateModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                CreatedAt = user.CreatedAt,
                IsBlocking = user.IsBlocking,
                MinPasswordLength = user.MinPasswordLength,
                PasswordExpiration = user.PasswordExpiration,
                isLowercaseLetters = user.isLowercaseLetters,
                isUppercaseLetters = user.isUppercaseLetters,
                isNumbers = user.isNumbers,
                isPunctuationMarks = user.isPunctuationMarks,
            };
        }
    }
}
