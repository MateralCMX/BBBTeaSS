using MateralTools.Base;
using MateralTools.MDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBBTeaSS.Model
{
    /// <summary>
    /// 用户模型
    /// </summary>
    [TableModel("T_User", "ID")]
    public class UserModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [ColumnModel("ID", "INTEGER",true)]
        public int ID { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        [ColumnModel("UserID", "TEXT")]
        public string UserID { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [ColumnModel("Password", "TEXT")]
        public string Password { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [ColumnModel("Name", "TEXT")]
        public string Name { get; set; }
        /// <summary>
        /// 删除标识(0未删除1已删除)
        /// </summary>
        [ColumnModel("IfDelete", "INTEGER")]
        public bool IfDelete { get; set; }
        public UserModel()
        {
            IfDelete = false;
            Password = "123456";
        }
    }
}
