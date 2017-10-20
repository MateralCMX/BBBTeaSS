using BBBTeaSS.Model;
using BBBTeaSS.DAL;
using MateralTools.MResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MateralTools.MEncryption;

namespace BBBTeaSS.BLL
{
    /// <summary>
    /// 用户业务逻辑层
    /// </summary>
    public class UserBLL
    {
        /// <summary>
        /// 数据访问对象
        /// </summary>
        private UserDAL userDAL;
        /// <summary>
        /// 构造方法
        /// </summary>
        public UserBLL()
        {
            userDAL = new UserDAL();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public MResultModel<UserModel> Login(string userID, string password)
        {
            UserModel userM = userDAL.GetUserInfoByUserID(userID);
            if (userM != null && EncryptionManager.MD5Encode_32(password) == userM.Password)
            {
                return MResultModel<UserModel>.GetSuccessResultM(userM, "登录成功。");
            }
            else
            {
                return MResultModel<UserModel>.GetFailResultM(null, "用户名或者密码错误。");
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="ID">用户唯一标识</param>
        /// <param name="password">要修改的密码</param>
        /// <returns></returns>
        public MResultModel<UserModel> UpdatePassword(int ID, string password)
        {
            UserModel userM = userDAL.GetUserInfoByID(ID);
            if (userM != null)
            {
                string newPassword = EncryptionManager.MD5Encode_32(password);
                if (userM.Password != newPassword)
                {
                    userM.Password = newPassword;
                    userDAL.UpdateUserInfo(userM);
                    return MResultModel<UserModel>.GetSuccessResultM(userM, "修改密码成功。");
                }
                else
                {
                    return MResultModel<UserModel>.GetFailResultM(null, "新密码不能和旧密码相同。");
                }
            }
            else
            {
                return MResultModel<UserModel>.GetFailResultM(null, "该用户不存在。");
            }
        }
    }
}
