using BBBTeaSS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MateralTools.MDataBase;
using System.Data;
using MateralTools.MConvert;
using MateralTools.MResult;

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
        public void DeleteUserInfo(long id)
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
        public UserModel GetUserInfoByID(long id)
        {
            TSQLModel tsqlM = new TSQLModel
            {
                SQLStr = "select * from T_User where ID = @ID And IfDelete = 0",
                SQLParameters = new List<TSQLParameter>()
            };
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
            TSQLModel tsqlM = new TSQLModel
            {
                SQLStr = "select * from T_User where UserID = @UserID And IfDelete = 0",
                SQLParameters = new List<TSQLParameter>()
            };
            tsqlM.SQLParameters.Add(new TSQLParameter("@UserID", userID));
            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            UserModel userM = ConvertManager.DataTableToList<UserModel>(ds.Tables[0]).FirstOrDefault();
            return userM;
        }
        /// <summary>
        /// 根据用户名和姓名查询用户信息
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="name">名称</param>
        /// <returns>用户信息</returns>
        public MPagingData<List<UserModel>> GetUserInfoByUserIDAndName(string userID, string name, int pageIndex = 1, int pageSize = 10)
        {
            string whereStr = "";
            TSQLModel tsqlM = new TSQLModel
            {
                SQLStr = "select * from T_User where IfDelete = 0",
                SQLParameters = new List<TSQLParameter>()
            };
            if (!string.IsNullOrEmpty(userID))
            {
                whereStr += " And UserID=@UserID";
                tsqlM.SQLParameters.Add(new TSQLParameter("@UserID", userID));
            }
            if (!string.IsNullOrEmpty(name))
            {
                whereStr += " And Name like @Name";
                tsqlM.SQLParameters.Add(new TSQLParameter("@Name", "%" + name + "%"));
            }
            tsqlM.SQLStr += whereStr + $" limit {pageSize} Offset {(pageIndex - 1) * pageSize};";
            tsqlM.SQLStr += "select Count(*) from T_User where IfDelete = 0" + whereStr + ";";
            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            List<UserModel> listM = ConvertManager.DataTableToList<UserModel>(ds.Tables[0]);
            MPagingData<List<UserModel>> pageUserM = new MPagingData<List<UserModel>>();
            pageUserM.Data = listM;
            pageUserM.PageInfo = new MPagingModel();
            pageUserM.PageInfo.DataCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            if (pageUserM.PageInfo.DataCount % pageSize > 0)
            {
                pageUserM.PageInfo.PagingCount = (pageUserM.PageInfo.DataCount / pageSize) + 1;
            }
            else
            {
                pageUserM.PageInfo.PagingCount = (pageUserM.PageInfo.DataCount / pageSize);
            }
            pageUserM.PageInfo.PagingIndex = pageIndex;
            pageUserM.PageInfo.PagingSize = pageSize;
            return pageUserM;
        }
    }
}
