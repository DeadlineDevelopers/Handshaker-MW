using Handshaker.Codes.Models;
using Handshaker.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handshaker.Codes.Providers
{
    public class IDCardProvider
    {
        public IDCardModel EntityObjectToModel(IDCard idCard)
        {
            IDCardModel idCardModel = new IDCardModel()
            {
                UserID = idCard.UserID,
                Title = idCard.Title,
                HomeAddress = idCard.HomeAddress,
                OfficeAddress = idCard.OfficeAddress,
                MobilePhone = idCard.MobilePhone,
                OfficePhone = idCard.OfficePhone,
                FacebookAddress = idCard.FacebookAddress,
                LinkedInAddress = idCard.LinkedInAddress
            };

            return idCardModel;
        }
        /// <summary>
        /// Creating and Updating ID Card
        /// </summary>
        /// <param name="idCardmodel"></param>
        /// <returns>Id Card</returns>
        public IDCardModel CreateAndUpdateIDCard(IDCardModel idCardmodel)
        {
            using (HandshakerEntities handShakerEntityContainer = new HandshakerEntities())
            {
                if (handShakerEntityContainer.IDCardSet.Count(c => c.UserID.Equals(idCardmodel.UserID)) == 0)
                {
                    IDCard newIDCard = new IDCard()
                    {
                        UserID = idCardmodel.UserID,
                        Title = idCardmodel.Title,
                        HomeAddress = idCardmodel.HomeAddress,
                        OfficeAddress = idCardmodel.OfficeAddress,
                        MobilePhone = idCardmodel.MobilePhone,
                        OfficePhone = idCardmodel.OfficePhone,
                        FacebookAddress = idCardmodel.FacebookAddress,
                        LinkedInAddress = idCardmodel.LinkedInAddress
                    };
                    handShakerEntityContainer.IDCardSet.AddObject(newIDCard);
                    handShakerEntityContainer.SaveChanges();
                    return GetIDCard(newIDCard.UserID);
                }
                else
                {
                    IDCard idCard = handShakerEntityContainer.IDCardSet.FirstOrDefault(c => c.Id.Equals(idCardmodel.Id));

                    if (idCard != null)
                    {
                        idCard.Title = idCardmodel.Title;
                        idCard.HomeAddress = idCardmodel.HomeAddress;
                        idCard.OfficeAddress = idCardmodel.OfficeAddress;
                        idCard.MobilePhone = idCardmodel.MobilePhone;
                        idCard.OfficePhone = idCardmodel.OfficePhone;
                        idCard.FacebookAddress = idCardmodel.FacebookAddress;
                        idCard.LinkedInAddress = idCardmodel.LinkedInAddress;

                        handShakerEntityContainer.SaveChanges();
                    }
                    return EntityObjectToModel(idCard);
                }
            }
        }
        /// <summary>
        /// Getting User ID Card
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>User ID Card</returns>
        public IDCardModel GetIDCard(int userID)
        {
            using (HandshakerEntities handShakerEntityContainer = new HandshakerEntities())
            {
                IDCard idCard = handShakerEntityContainer.IDCardSet.FirstOrDefault(c => c.UserID.Equals(userID));
                return EntityObjectToModel(idCard);
            }
        }
    }
}
