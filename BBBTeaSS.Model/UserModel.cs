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
    public class UserModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public int Password { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public int Name { get; set; }
        /// <summary>
        /// 删除标识(0未删除1已删除)
        /// </summary>
        public bool IfDelete { get; set; }
    }
}
