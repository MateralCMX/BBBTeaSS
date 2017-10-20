using Microsoft.VisualStudio.TestTools.UnitTesting;
using BBBTeaSS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBBTeaSS.Model;

namespace BBBTeaSS.DAL.Tests
{
    [TestClass()]
    public class UserDALTests
    {
        private UserDAL userDal = new UserDAL();
        [TestMethod()]
        public void AddUserInfoTest()
        {
            userDal.AddUserInfo(new UserModel() { IfDelete = false, Name = "Materal", UserID = "Admin", Password = "123456" });
        }

        [TestMethod()]
        public void GetUserInfoByIDTest()
        {
            UserModel userM = userDal.GetUserInfoByID(1);
        }

        [TestMethod()]
        public void UpdateUserInfoTest()
        {
            userDal.UpdateUserInfo(new UserModel() { ID = 1, IfDelete = false, Name = "Materal1", UserID = "Admin", Password = "123456" });
        }

        [TestMethod()]
        public void DeleteUserInfoTest()
        {
            userDal.DeleteUserInfo(1);
        }
    }
}