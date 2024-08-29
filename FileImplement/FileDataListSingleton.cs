using FileImplement.Models;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace FileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        public List<User> Users { get; set; }
        private string key = "0123";
        private string iv = "76543210";
        private string fileKey = "file_key.txt";
        private string encryptedFile = "encrypted_file.xml";
        private string tempFilePath = Path.GetTempFileName();
        public FileDataListSingleton()
        {
            Users = LoadUsers();            
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }

        public static void SaveFileDataListSingleton()
        {
            instance.SaveUsers();
        }
        public void UpdateUsers()
        {
            if (Users != null)
            {
                var xElement = new XElement("Users");
                foreach (var user in Users)
                {
                    xElement.Add(new XElement("User",
                    new XAttribute("Id", user.Id),
                    new XElement("Login", user.Login),
                    new XElement("Password", user.Password),
                    new XElement("CreatedAt", user.CreatedAt),
                    new XElement("IsBlocking", user.IsBlocking),
                    new XElement("MinPasswordLength", user.MinPasswordLength),
                    new XElement("PasswordExpiration", user.PasswordExpiration),
                    new XElement("isLowercaseLetters", user.isLowercaseLetters),
                    new XElement("isUppercaseLetters", user.isUppercaseLetters),
                    new XElement("isNumbers", user.isNumbers),
                    new XElement("isPunctuationMarks", user.isPunctuationMarks)));
                }
                var xDocument = new XDocument(xElement);
                File.WriteAllText(tempFilePath, string.Empty);
                xDocument.Save(tempFilePath);
                instance.Users = instance.LoadUsers();
            }
        }
        public static void GetFileDataListSingleton()
        {
            instance = GetInstance();
            instance.GetEncryptingFile();
            instance.Users = instance.LoadUsers();
        }
        private List<User> LoadUsers()
        {
            var list = new List<User>();
            FileInfo fileInfo = new FileInfo(tempFilePath);
            if (File.Exists(tempFilePath) && fileInfo.Length > 0)
            {
                var xDocument = XDocument.Load(tempFilePath);
                var xElements = xDocument.Root.Elements("User").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new User
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        Login = elem.Element("Login").Value,
                        Password = elem.Element("Password").Value,
                        CreatedAt = DateTime.Parse(elem.Element("CreatedAt").Value),
                        IsBlocking = Convert.ToBoolean(elem.Element("IsBlocking").Value),
                        MinPasswordLength = Convert.ToInt32(elem.Element("MinPasswordLength").Value),
                        PasswordExpiration = Convert.ToInt32(elem.Element("PasswordExpiration").Value),
                        isLowercaseLetters = Convert.ToBoolean(elem.Element("isLowercaseLetters").Value),
                        isUppercaseLetters = Convert.ToBoolean(elem.Element("isUppercaseLetters").Value),
                        isNumbers = Convert.ToBoolean(elem.Element("isNumbers").Value),
                        isPunctuationMarks = Convert.ToBoolean(elem.Element("isPunctuationMarks").Value)
                    });
                }
            }
            return list;
        }

        private void SaveUsers()
        {
            if (Users != null)
            {
                var xElement = new XElement("Users");
                foreach (var user in Users)
                {
                    xElement.Add(new XElement("User",
                    new XAttribute("Id", user.Id),
                    new XElement("Login", user.Login),
                    new XElement("Password", user.Password),
                    new XElement("CreatedAt", user.CreatedAt),
                    new XElement("IsBlocking", user.IsBlocking),
                    new XElement("MinPasswordLength", user.MinPasswordLength),
                    new XElement("PasswordExpiration", user.PasswordExpiration),
                    new XElement("isLowercaseLetters", user.isLowercaseLetters),
                    new XElement("isUppercaseLetters", user.isUppercaseLetters),
                    new XElement("isNumbers", user.isNumbers),
                    new XElement("isPunctuationMarks", user.isPunctuationMarks)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(tempFilePath);
                FileEncrypting();
                File.Delete(tempFilePath);
            }
        }

        private void GetEncryptingFile()
        {
            if (File.Exists(encryptedFile))
            {
                FileDecrypting();
            }
            else
            {
                var xElement = new XElement("Users");
                xElement.Add(new XElement("User",
                    new XAttribute("Id", 0),
                    new XElement("Login", "ADMIN"),
                    new XElement("Password", ""),
                    new XElement("CreatedAt", DateTime.Now),
                    new XElement("IsBlocking", false),
                    new XElement("MinPasswordLength", 0),
                    new XElement("PasswordExpiration", 0),
                    new XElement("isLowercaseLetters", true),
                    new XElement("isUppercaseLetters", true),
                    new XElement("isNumbers", true),
                    new XElement("isPunctuationMarks", true)
                ));
                var xDocument = new XDocument(xElement);
                xDocument.Save(tempFilePath);
                FileEncrypting();
                GetEncryptingFile();
            }
        }

        public void FileEncrypting()
        {
            try
            {
                string randomValue = GenerateRandomValue();
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    string keyWithRandomValue = key + randomValue;
                    byte[] keyBytes = Encoding.UTF8.GetBytes(keyWithRandomValue);
                    byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                    des.Key = keyBytes;
                    des.IV = ivBytes;
                    des.Mode = CipherMode.CFB;

                    using (FileStream inputFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read))
                    {
                        using (FileStream outputFileStream = new FileStream(encryptedFile, FileMode.Create, FileAccess.Write))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, des.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                byte[] buffer = new byte[1024];
                                int bytesRead;

                                do
                                {
                                    bytesRead = inputFileStream.Read(buffer, 0, buffer.Length);
                                    cryptoStream.Write(buffer, 0, bytesRead);
                                } while (bytesRead > 0);
                            }
                        }
                    }
                    SaveRandomValueToFile(fileKey, randomValue);
                }
                File.WriteAllText(tempFilePath, string.Empty);
                Console.WriteLine("Файл успешно зашифрован.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при шифровании файла: " + ex.Message);
            }
        }

        public void FileDecrypting()
        {
            try
            {
                string randomValue = GetRandomValueFromFile(fileKey);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    string keyWithRandomValue = key + randomValue;
                    byte[] keyBytes = Encoding.UTF8.GetBytes(keyWithRandomValue);
                    byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                    des.Key = keyBytes;
                    des.IV = ivBytes;
                    des.Mode = CipherMode.CFB;
                    using (FileStream inputFileStream = new FileStream(encryptedFile, FileMode.Open, FileAccess.Read))
                    {
                        
                        using (FileStream outputFileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(inputFileStream, des.CreateDecryptor(), CryptoStreamMode.Read))
                            {
                                byte[] buffer = new byte[1024];
                                int bytesRead;

                                do
                                {
                                    bytesRead = cryptoStream.Read(buffer, 0, buffer.Length);
                                    outputFileStream.Write(buffer, 0, bytesRead);
                                } while (bytesRead > 0);
                            }
                        }
                    }
                }

                Console.WriteLine("Файл успешно расшифрован.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при расшифровке файла: " + ex.Message);
            }
        }

        private void SaveRandomValueToFile(string filename, string randomValue)
        {
            using (FileStream writer = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                byte[] randomValueBytes = Encoding.UTF8.GetBytes(randomValue);
                writer.Write(randomValueBytes, 0, randomValueBytes.Length);
            }
        }

        private string GetRandomValueFromFile(string filename)
        {
            using (FileStream inputFileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(inputFileStream))
                {
                    string randomValue = reader.ReadLine();
                    return randomValue;
                }
            }
        }

        private string GenerateRandomValue()
        {
            Random random = new Random();
            string randomValue = random.Next(1000, 10000).ToString();
            return randomValue;
        }
    }
}
