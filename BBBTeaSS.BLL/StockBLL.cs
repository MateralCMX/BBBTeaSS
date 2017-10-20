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
    /// 库存业务逻辑层
    /// </summary>
    public class StockBLL
    {
        /// <summary>
        /// 数据访问对象
        /// </summary>
        private StockDAL stockDAL;
        /// <summary>
        /// 构造方法
        /// </summary>
        public StockBLL()
        {
            stockDAL = new StockDAL();
        }
    }
}
