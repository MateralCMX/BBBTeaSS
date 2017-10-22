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
        /// <returns>返回对象</returns>
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
        /// <returns>返回对象</returns>
        public MResultModel<UserModel> UpdatePassword(long ID, string password)
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
        /// <summary>
        /// 根据用户唯一标识获得用户信息
        /// </summary>
        /// <param name="ID">用户唯一标识</param>
        /// <returns>返回对象</returns>
        public MResultModel<UserModel> GetUserInfoByID(long ID)
        {
            UserModel userM = userDAL.GetUserInfoByID(ID);
            if (userM != null)
            {
                return MResultModel<UserModel>.GetSuccessResultM(userM, "用户信息。");
            }
            else
            {
                return MResultModel<UserModel>.GetFailResultM(null, "该用户不存在。");
            }
        }
        /// <summary>
        /// 添加一个用户信息
        /// </summary>
        /// <param name="userM">用户信息</param>
        /// <returns>返回对象</returns>
        public MResultModel AddUserInfo(UserModel userM)
        {
            if (userM != null)
            {
                userM.Password = EncryptionManager.MD5Encode_32(userM.Password);
                userDAL.AddUserInfo(userM);
                return MResultModel.GetSuccessResultM("添加成功。");
            }
            else
            {
                return MResultModel.GetFailResultM("添加对象不存在。");
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userM">要修改的用户信息</param>
        /// <returns>返回结果</returns>
        public MResultModel UpdateUserInfo(UserModel userM)
        {
            if (userM != null)//修改的用户必须存在
            {
                UserModel oldUserM = userDAL.GetUserInfoByUserID(userM.UserID);
                if (oldUserM == null || oldUserM.ID == userM.ID)//用户名不重复
                {
                    oldUserM = userDAL.GetUserInfoByID(userM.ID);
                    if (oldUserM != null)//已存在数据库
                    {
                        oldUserM.Name = userM.Name;
                        oldUserM.UserID = userM.UserID;
                        userDAL.UpdateUserInfo(oldUserM);
                        return MResultModel.GetSuccessResultM("修改成功。");
                    }
                    else
                    {
                        return MResultModel.GetFailResultM("用户信息不存在。");
                    }
                }
                else
                {
                    return MResultModel.GetFailResultM("该用户名已存在。");
                }
            }
            else
            {
                return MResultModel.GetFailResultM("修改对象不存在。");
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public MResultModel DeleteUserInfo(long ID)
        {
            userDAL.DeleteUserInfo(ID);
            return MResultModel.GetSuccessResultM("删除成功");
        }
        /// <summary>
        /// 根据用户名和姓名查询用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="name">用户名</param>
        /// <param name="pageIndex">分页页数</param>
        /// <param name="pageSize">分页条数</param>
        /// <returns></returns>
        public MResultPagingModel<List<UserModel>> GetUserInfoByUserIDAndName(string userID, string name, int pageIndex = 1, int pageSize = 10)
        {
            MPagingData<List<UserModel>> listM = userDAL.GetUserInfoByUserIDAndName(userID, name, pageIndex, pageSize);
            if (listM != null)
            {
                return MResultPagingModel<List<UserModel>>.GetSuccessResultM(listM, "查询成功");
            }
            else
            {
                return MResultPagingModel<List<UserModel>>.GetSuccessResultM(null, "查询失败");
            }
        }
    }
}