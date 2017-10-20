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
    /// 种类业务逻辑层
    /// </summary>
    public class VarietyBLL
    {
        /// <summary>
        /// 数据访问对象
        /// </summary>
        private VarietyDAL varietyDAL;
        /// <summary>
        /// 构造方法
        /// </summary>
        public VarietyBLL()
        {
            varietyDAL =new VarietyDAL();
        }
    }
}
