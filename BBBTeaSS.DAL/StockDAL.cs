using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBBTeaSS.Model;
using MateralTools.MDataBase;
using MateralTools.MConvert;
using System.Data;
using MateralTools.MResult;

namespace BBBTeaSS.DAL
{
    /// <summary>
    /// 库存数据操作类
    /// </summary>
    public class StockDAL
    {
        /// <summary>
        /// 添加一个库存
        /// </summary>
        /// <param name="stockM">库存实体</param>
        public void AddStockInfo(StockModel stockM)
        {
            SQLiteManager.Insert(stockM);
        }

        /// <summary>
        /// 修改一个库存
        /// </summary>
        /// <param name="stockM"></param>
        public void UpdateStockInfo(StockModel stockM)
        {
            SQLiteManager.Update(stockM);
        }

        /// <summary>
        /// 删除一个库存
        /// </summary>
        /// <param name="id">唯一标示</param>
        public void DeleteStockInfo(int id)
        {
            StockModel stockM = GetStockInfoByID(id);
            if(stockM!=null)
            {
                stockM.IfDelete = true;
                UpdateStockInfo(stockM);
            }
        }

        /// <summary>
        /// 根据唯一标识获取库存信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>库存信息</returns>
        public StockModel GetStockInfoByID(int id)
        {
            TSQLModel tsqlM = new TSQLModel();
            tsqlM.SQLStr = "select * from T_Stock where ID=@ID";
            tsqlM.SQLParameters = new List<TSQLParameter>();
            tsqlM.SQLParameters.Add(new TSQLParameter("@ID", id));
            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            StockModel stockM = ConvertManager.DataTableToList<StockModel>(ds.Tables[0]).FirstOrDefault();
            return stockM;
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
        /// <returns>返回商品信息</returns>
        public MPagingData<List<StockViewModel>> GetStockInfoByWhere(string name, string manufactor, string phone, long varietyID, string region, int pageIndex = 1, int pageSize = 10)
        {
            string whereStr = "";
            TSQLModel tsqlM = new TSQLModel
            {
                SQLStr = "select T_Product.ID as 'ProductID', T_Product.Name as 'ProductName', T_Product.Manufactor, T_Product.Phone,T_Product.Region, T_Product.FK_Variety, T_Variety.Name as 'VarietyName', Sum(T_Stock.StockNumber) AS 'StockNum' from T_Stock inner join T_Product On T_Stock.FK_Product = T_Product.ID inner join T_Variety On T_Product.FK_Variety = T_Variety.ID where T_Stock.IfDelete = 0 ",
                SQLParameters = new List<TSQLParameter>()
            };
            if (!string.IsNullOrEmpty(name))
            {
                whereStr += " And T_Product.Name like @Name";
                tsqlM.SQLParameters.Add(new TSQLParameter("@Name", "%" + name + "%"));
            }
            if (!string.IsNullOrEmpty(manufactor))
            {
                whereStr += " And T_Product.Manufactor like @Manufactor";
                tsqlM.SQLParameters.Add(new TSQLParameter("@Manufactor", "%" + manufactor + "%"));
            }
            if (!string.IsNullOrEmpty(phone))
            {
                whereStr += " And T_Product.Phone like @Phone";
                tsqlM.SQLParameters.Add(new TSQLParameter("@Phone", "%" + phone + "%"));
            }
            if (varietyID > 0)
            {
                VarietyModel varietyModel = new VarietyModel();
                whereStr += " And T_Product.FK_Variety = @VarietyID";
                tsqlM.SQLParameters.Add(new TSQLParameter("@VarietyID", varietyID));
            }
            if (!string.IsNullOrEmpty(region))
            {
                whereStr += " And T_Product.Region like @Region";
                tsqlM.SQLParameters.Add(new TSQLParameter("@Region", "%" + region + "%"));
            }

            tsqlM.SQLStr += whereStr + $" Group by FK_Product limit {pageSize} Offset {(pageIndex - 1) * pageSize};";
            tsqlM.SQLStr += "select Count(*)from(select T_Product.ID as 'ProductID', T_Product.Name as 'ProductName', T_Product.Manufactor, T_Product.Phone,T_Product.Region, T_Product.FK_Variety, T_Variety.Name as 'VarietyName', Sum(T_Stock.StockNumber) AS 'StockNum' from T_Stock inner join T_Product On T_Stock.FK_Product = T_Product.ID inner join T_Variety On T_Product.FK_Variety = T_Variety.ID where T_Stock.IfDelete = 0 " + whereStr + " Group by FK_Product);";

            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            List<StockViewModel> listM = ConvertManager.DataTableToList<StockViewModel>(ds.Tables[0]);
            MPagingData<List<StockViewModel>> pageProductM = new MPagingData<List<StockViewModel>>();
            pageProductM.Data = listM;
            pageProductM.PageInfo = new MPagingModel();
            pageProductM.PageInfo.DataCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            if (pageProductM.PageInfo.DataCount % pageSize > 0)
            {
                pageProductM.PageInfo.PagingCount = (pageProductM.PageInfo.DataCount / pageSize) + 1;
            }
            else
            {
                pageProductM.PageInfo.PagingCount = (pageProductM.PageInfo.DataCount / pageSize);
            }
            pageProductM.PageInfo.PagingIndex = pageIndex;
            pageProductM.PageInfo.PagingSize = pageSize;
            return pageProductM;
        }
        
        /// <summary>
        /// 根据商品ID查询库存信息
        /// </summary>
        /// <param name="productID">商品ID</param>
        /// <param name="stockType">出入库标记[1为出库，2为入库]</param>
        /// <returns>返回库存信息</returns>
        public List<StockModel> GetStockInfoByProductID(long productID, long? stockType)
        {
            TSQLModel tsqlM = new TSQLModel();
            tsqlM.SQLStr = "select * from T_Stock where FK_Product=@ProductID";
            if (stockType != null)
            {
                tsqlM.SQLStr += " And StockType = @StockType";
            }
            tsqlM.SQLStr += " Order By ID Desc";
            tsqlM.SQLParameters = new List<TSQLParameter>();
            tsqlM.SQLParameters.Add(new TSQLParameter("@ProductID", productID));
            tsqlM.SQLParameters.Add(new TSQLParameter("@StockType", stockType));
            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            List<StockModel> listM = ConvertManager.DataTableToList<StockModel>(ds.Tables[0]).ToList();
            for (int i = 0; i < listM.Count; i++)
            {
                listM[i].StockDateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["StockDateTime"]);
            }
            return listM;
        }
    }
}
