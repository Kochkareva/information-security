using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileImplement.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } 
        public bool IsBlocking { get; set; }
        public int MinPasswordLength { get; set; }
        public int PasswordExpiration { get; set; }
        public bool isLowercaseLetters { get; set; }
        public bool isUppercaseLetters { get; set;}
        public bool isNumbers { get; set; }
        public bool isPunctuationMarks { get; set; }
    }
}
