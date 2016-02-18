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
        /// Add user to UserSet
        /// </summary>
        /// <returns></returns>
        public int InsertUser(UserModel userModel)
        {
            int userId = 0;

            using (HandshakerEntities handshakerEntityContainer = new HandshakerEntities())
            {
                byte[] passwordAsByte = System.Text.Encoding.ASCII.GetBytes(userModel.Password);
                passwordAsByte = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAsByte);
                String hashedPassword = System.Text.Encoding.ASCII.GetString(passwordAsByte);

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
    }
}
