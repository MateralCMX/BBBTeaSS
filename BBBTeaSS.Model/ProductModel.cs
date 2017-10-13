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
    public class ProductModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 厂家
        /// </summary>
        public string Manufactor { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 种类
        /// </summary>
        public VarietyModel Variety { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }
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
