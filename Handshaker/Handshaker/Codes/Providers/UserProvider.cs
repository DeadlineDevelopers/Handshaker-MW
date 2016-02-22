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
        public UserModel EntityObjectToModel(User user)
        {
            UserModel userModel = new UserModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email
            };

            return userModel;
        }
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

        /// <summary>
        /// Registered user sign in
        /// </summary>
        /// <param name="userNameOrMail"></param>
        /// <param name="password"></param>
        public string SignIn(string userNameOrMail,string password)
        {
            string signedUsername = "";
            #region hash password
                byte[] passwordAsByte = System.Text.Encoding.ASCII.GetBytes(password);
                passwordAsByte = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAsByte);
                String hashedPassword = System.Text.Encoding.ASCII.GetString(passwordAsByte);
            #endregion

            using (HandshakerEntities handshakerEntityContainer = new HandshakerEntities())
            {
                User user = handshakerEntityContainer.UserSet.FirstOrDefault(u => (u.Username.Equals(userNameOrMail) || u.Email.Equals(userNameOrMail)) && u.Password.Equals(hashedPassword));

                if (user == null)
                    Console.WriteLine("Hatalı kullanıcı adı ya da parola");
                else
                {
                    Console.WriteLine("Handshaker uygulamamıza hoşgeldiniz");
                    signedUsername = user.Username;
                }
            }

            return signedUsername;
        }

        /// <summary>
        /// Add new contact
        /// </summary>
        /// <param name="username"></param>
        public void AddNewContact(string currentUsername, string contactUsername)
        {
            using (HandshakerEntities handshakerEntityContainer = new HandshakerEntities())
            {
                User currentUser = handshakerEntityContainer.UserSet.FirstOrDefault(u => u.Username.Equals(currentUsername));
                User contactUser = handshakerEntityContainer.UserSet.FirstOrDefault(u => u.Username.Equals(contactUsername));

                if (contactUser != null)
                    currentUser.MyContacts.Add(contactUser);
                else
                    throw new Exception("" + contactUsername + " ismine sahip bir kullanıcı bulunamadı");

                handshakerEntityContainer.SaveChanges();
                Console.Write("" + contactUsername + " ismine sahip bir kullanıcıyı bağlantılarınıza başarıyla eklendi");
            }
        }

        /// <summary>
        /// List all my contacts
        /// </summary>
        /// <param name="currentUsername"></param>
        /// <returns></returns>
        public List<UserModel> GetListOfMyContacts(string currentUsername)
        {
            List<UserModel> myContactList = new List<UserModel>();

            using (HandshakerEntities handshakerEntityContainer = new HandshakerEntities())
            {
                User currentUser = handshakerEntityContainer.UserSet.FirstOrDefault(u => u.Username.Equals(currentUsername));

                if (currentUser != null)
                {
                    foreach(User myContact in currentUser.MyContacts)
                        myContactList.Add(EntityObjectToModel(myContact));
                }
                else
                    throw new Exception("" + currentUsername + " ismine sahip bir kullanıcı bulunamadı");
            }

            return myContactList;
        }
    }
}
