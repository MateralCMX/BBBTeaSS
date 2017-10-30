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
    /// 商品模型
    /// </summary>
    [Serializable]
    [TableModel("T_Product", "ID")]
    public class ProductModel:MBaseModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [ColumnModel("ID", "INTEGER", true)]
        public long ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [ColumnModel("Name", "TEXT")]
        public string Name { get; set; }
        /// <summary>
        /// 厂家
        /// </summary>
        [ColumnModel("Manufactor", "TEXT")]
        public string Manufactor { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [ColumnModel("Phone", "TEXT")]
        public string Phone { get; set; }
        /// <summary>
        /// 种类
        /// </summary>
        [ColumnModel("FK_Variety", "INTEGER")]
        public long FK_Variety { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        [ColumnModel("Region", "TEXT")]
        public string Region { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [ColumnModel("Remark", "TEXT")]
        public string Remark { get; set; }
        /// <summary>
        /// 删除标识(0未删除1已删除)
        /// </summary>
        [ColumnModel("IfDelete", "INTEGER")]
        public bool IfDelete { get; set; }
    }
    [Serializable]
    public class ProductViewModel : ProductModel
    {
        /// <summary>
        /// 种类名称
        /// </summary>
        [ColumnModel("VarietyName", "TEXT")]
        public string VarietyName { get; set; }
    }
    /// <summary>
    /// 商品窗体模式
    /// </summary>
    public enum ProductWindowMode
    {
        /// <summary>
        /// 商品模式
        /// </summary>
        Product,
        /// <summary>
        /// 入库模式
        /// </summary>
        JoinStock
    }
}
