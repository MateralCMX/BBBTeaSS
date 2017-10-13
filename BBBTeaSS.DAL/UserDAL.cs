using BBBTeaSS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MateralTools.MDataBase;
using System.Data;
using MateralTools.MConvert;

namespace BBBTeaSS.DAL
{
    /// <summary>
    /// 用户数据操作类
    /// </summary>
    public class UserDAL
    {
        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="userM"></param>
        public void AddUserInfo(UserModel userM)
        {
            SQLiteManager.Insert(userM);
        }
        /// <summary>
        /// 修改一个用户
        /// </summary>
        /// <param name="userM"></param>
        public void UpdateUserInfo(UserModel userM)
        {
            SQLiteManager.Update(userM);
        }
        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="userM"></param>
        public void DeleteUserInfo(int id)
        {
            UserModel userM = GetUserInfoByID(id);
            if (userM != null)
            {
                userM.IfDelete = true;
                UpdateUserInfo(userM);
            }
        }
        /// <summary>
        /// 根据唯一标识获得用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>用户信息</returns>
        public UserModel GetUserInfoByID(int id)
        {
            TSQLModel tsqlM = new TSQLModel();
            tsqlM.SQLStr = "select * from T_User where ID = @ID";
            tsqlM.SQLParameters = new List<TSQLParameter>();
            tsqlM.SQLParameters.Add(new TSQLParameter("@ID", id));
            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            UserModel userM = ConvertManager.DataTableToList<UserModel>(ds.Tables[0]).FirstOrDefault();
            return userM;
        }
        /// <summary>
        /// 根据用户名获得用户信息
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <returns>用户信息</returns>
        public UserModel GetUserInfoByUserID(string userID)
        {
            TSQLModel tsqlM = new TSQLModel();
            tsqlM.SQLStr = "select * from T_User where UserID = @UserID";
            tsqlM.SQLParameters = new List<TSQLParameter>();
            tsqlM.SQLParameters.Add(new TSQLParameter("@UserID", userID));
            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            UserModel userM = ConvertManager.DataTableToList<UserModel>(ds.Tables[0]).FirstOrDefault();
            return userM;
        }
    }
}
