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
    /// 库存模型
    /// </summary>
    [TableModel("T_Stock", "ID")]
    public class StockModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [ColumnModel("ID", "INTEGER", true)]
        public long ID { get; set; }
        /// <summary>
        /// 库存商品
        /// </summary>
        [ColumnModel("FK_Product", "INTEGER")]
        public long FK_Product { get; set; }
        /// <summary>
        /// 库存时间
        /// </summary>
        [ColumnModel("StockDateTime", "TEXT")]
        public DateTime StockDateTime { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        [ColumnModel("StockNumber", "INTEGER")]
        public long StockNumber { get; set; }
        /// <summary>
        /// 库存类型(1:入库 2:出库)
        /// </summary>
        [ColumnModel("StockType", "INTEGER")]
        public long StockType { get; set; }
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
        /// <summary>
        /// 库存时间戳
        /// </summary>
        public string StockDateTimeStr
        {
            get
            {
                return StockDateTime.ToString("yyyy/MM/dd HH:mm:ss");
            }
        }
    }
    /// <summary>
    /// 库存视图模型
    /// </summary>
    public class StockViewModel
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long ProductID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string Manufactor { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 生产地区
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 种类名称
        /// </summary>
        public string VarietyName { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public long StockNum { get; set; }
    }

    /// <summary>
    /// 库存窗体模式
    /// </summary>
    public enum QueryStockWindowMode
    {
        /// <summary>
        /// 查询模式
        /// </summary>
        Query,
        /// <summary>
        /// 出库模式
        /// </summary>
        OutStock
    }
}
