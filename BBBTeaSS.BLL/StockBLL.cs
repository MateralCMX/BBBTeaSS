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
        /// <summary>
        /// 添加一个库存
        /// </summary>
        /// <param name="stockM">库存实体</param>
        public void AddStockInfo(StockModel stockM)
        {
            stockDAL.AddStockInfo(stockM);
        }
        /// <summary>
        /// 根据商品名称查询商品信息
        /// </summary>
        /// <param name="ProductName">商品名称</param>
        /// <param name="manufactor">厂家</param>
        /// <param name="phone">电话</param>
        /// <param name="varietyID">种类</param>
        /// <param name="region">地区</param>
        /// <param name="Remark">备注</param>
        /// <param name="pageIndex">分页页数</param>
        /// <param name="pageSize">分页条数</param>
        /// <returns></returns>
        public MResultPagingModel<List<StockViewModel>> GetStockInfoByWhere(string name, string manufactor, string phone, long varietyID, string region, int pageIndex = 1, int pageSize = 10)
        {

            MPagingData<List<StockViewModel>> listM = stockDAL.GetStockInfoByWhere(name, manufactor, phone, varietyID, region, pageIndex, pageSize);
            if (listM != null)
            {
                return MResultPagingModel<List<StockViewModel>>.GetSuccessResultM(listM, "查询成功");
            }
            else
            {
                return MResultPagingModel<List<StockViewModel>>.GetFailResultM(null, "查询失败");
            }
        }
        /// <summary>
        /// 根据商品ID查询出入库信息
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public MResultModel<List<StockModel>> GetStockInfoByProductID(long productID,long? stockType)
        {
            List<StockModel> listM = stockDAL.GetStockInfoByProductID(productID, stockType);
            return MResultModel<List<StockModel>>.GetSuccessResultM(listM, "查询成功");
        }
    }
}
