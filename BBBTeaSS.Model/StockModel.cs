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
    public class StockModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 库存商品
        /// </summary>
        public ProductModel FK_Product { get; set; }
        /// <summary>
        /// 库存时间
        /// </summary>
        public DateTime StockDateTime { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockNumber { get; set; }
        /// <summary>
        /// 库存类型(1:入库 2:出库)
        /// </summary>
        public int StockType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 删除标识(0未删除1已删除)
        /// </summary>
        public bool IfDelete { get; set; }
    }
}
