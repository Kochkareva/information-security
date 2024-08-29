using Contracts.BindingModels;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusinessLogicContracts
{
    public interface IUserLogic
    {
        List<UserViewModel> Read(UserBindingModel model);

        void CreateOrUpdate(UserBindingModel model);
        int Autrhorization(string login, string password);
        bool CheckPassword(UserViewModel user, string password);
        string PasswordHashing(string password);
    }
}
