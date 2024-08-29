using Contracts.BindingModels;
using Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StoragesContracts
{
    public interface IUserStorage
    {
        List<UserViewModel> GetFullList();

        List<UserViewModel> GetFilteredList(UserBindingModel model);

        UserViewModel GetElement(UserBindingModel model);

        void Insert(UserBindingModel model);

        void Update(UserBindingModel model);
    }
}
