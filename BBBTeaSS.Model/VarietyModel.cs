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
    /// 种类模型
    /// </summary>
    [TableModel("T_Variety", "ID")]
    public class VarietyModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [ColumnModel("ID", "INTEGER", true)]
        public long ID { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        [ColumnModel("Name", "TEXT")]
        public string Name { get; set; }
        /// <summary>
        /// 删除标识(0未删除1已删除)
        /// </summary>
        [ColumnModel("IfDelete", "INTEGER")]
        public bool IfDelete { get; set; }
    }
}
