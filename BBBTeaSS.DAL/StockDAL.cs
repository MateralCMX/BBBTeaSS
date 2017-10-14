using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBBTeaSS.Model;
using MateralTools.MDataBase;
using MateralTools.MConvert;
using System.Data;


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
        /// <param name="id"></param>
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

    }
}
