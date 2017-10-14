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
    /// 商品数据操作类
    /// </summary>
    public class ProductDAL
    {
        /// <summary>
        /// 添加一个商品
        /// </summary>
        /// <param name="productM">商品实体</param>
        public void AddProductInfo(ProductModel productM)
        {
            SQLiteManager.Insert(productM);
        }

        /// <summary>
        /// 修改一个商品
        /// </summary>
        /// <param name="productM">商品实体</param>
        public void UpdateProductInfo(ProductModel productM)
        {
            SQLiteManager.Update(productM);
        }

        /// <summary>
        /// 删除一个商品
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProductInfo(int id)
        {
            ProductModel productM = GetProductInfoByID(id);
            if(productM!=null)
            {
                productM.IfDelete = true;
                UpdateProductInfo(productM);
            }
        }

        /// <summary>
        /// 根据唯一标识获取商品信息
        /// </summary>
        /// <param name="ProductId">商品ID</param>
        /// <returns>商品信息</returns>
        public ProductModel GetProductInfoByID(int id)
        {
            TSQLModel tsqlM = new TSQLModel();
            tsqlM.SQLStr = "select * from T_Product where ID=@ID";
            tsqlM.SQLParameters = new List<TSQLParameter>();
            tsqlM.SQLParameters.Add(new TSQLParameter("@ID", id));
            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            ProductModel productM = ConvertManager.DataTableToList<ProductModel>(ds.Tables[0]).FirstOrDefault();
            return productM;
        }

        /// <summary>
        /// 根据商品名获取商品信息
        /// </summary>
        /// <param name="productName">商品名</param>
        /// <returns>商品信息</returns>
        public ProductModel GetProductInfoByID(string productName)
        {
            TSQLModel tsqlM = new TSQLModel();
            tsqlM.SQLStr = "select * from T_Product where ID=@ID";
            tsqlM.SQLParameters = new List<TSQLParameter>();
            tsqlM.SQLParameters.Add(new TSQLParameter("@ID", productName));
            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            ProductModel productM = ConvertManager.DataTableToList<ProductModel>(ds.Tables[0]).FirstOrDefault();
            return productM;
        }
    }
}
