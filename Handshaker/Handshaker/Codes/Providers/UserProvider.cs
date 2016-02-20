using Handshaker.Codes.Models;
using Handshaker.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handshaker.Codes.Providers
{
    public class UserProvider
    {
        /// <summary>
        /// Register new user
        /// </summary>
        /// <returns>new user Id</returns>
        public int SignUp(UserModel userModel)
        {
            int userId = 0;

            using (HandshakerEntities handshakerEntityContainer = new HandshakerEntities())
            {
                if (handshakerEntityContainer.UserSet.Count(u => u.Username.Equals(userModel.Username)) > 0)
                    throw new Exception("Sistemde " + userModel.Username + " adıyla bir kullanıcı bulunmaktadır");

                #region hash password
                byte[] passwordAsByte = System.Text.Encoding.ASCII.GetBytes(userModel.Password);
                passwordAsByte = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAsByte);
                String hashedPassword = System.Text.Encoding.ASCII.GetString(passwordAsByte);
                #endregion

                User newUser = new User()
                {
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Username = userModel.Username,
                    Email = userModel.Email,
                    Password = hashedPassword
                };

                handshakerEntityContainer.UserSet.AddObject(newUser);
                handshakerEntityContainer.SaveChanges();

                userId = newUser.Id;
            }

            return userId;
        }

        public void SignIn(string userNameOrMail,string password)
        {
            #region hash password
                byte[] passwordAsByte = System.Text.Encoding.ASCII.GetBytes(password);
                passwordAsByte = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAsByte);
                String hashedPassword = System.Text.Encoding.ASCII.GetString(passwordAsByte);
            #endregion

            using (HandshakerEntities handshakerEntityContainer = new HandshakerEntities())
            {
                User user = handshakerEntityContainer.UserSet.FirstOrDefault(u => (u.Username.Equals(userNameOrMail) || u.Email.Equals(userNameOrMail)) && u.Password.Equals(hashedPassword));

                if(user==null)
                    Console.WriteLine("Hatalı kullanıcı adı ya da parola");
                else
                    Console.WriteLine("Handshaker uygulamamıza hoşgeldiniz");
            }
        }
    }
}
