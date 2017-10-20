using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBBTeaSS.WPFUI.Model
{
    /// <summary>
    /// 自定义控件属性
    /// </summary>
    public class CustomControlAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">该控件的名称</param>
        public CustomControlAttribute(string name)
        {
            this.Name = name;
        }
    }
}
