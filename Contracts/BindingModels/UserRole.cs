using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BindingModels
{
    public static class UserRole
    {
        public static Role? Role { get; set; }
        public static int? IdUser {  get; set; }
        public static bool IsBlocking {  get; set; }
        public static bool PasswordExpiration {  get; set; }
    }
}
