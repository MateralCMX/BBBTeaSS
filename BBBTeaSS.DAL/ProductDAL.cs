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
        /// <param name="id">唯一标示</param>
        public void DeleteProductInfo(long id)
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
        public ProductModel GetProductInfoByID(long id)
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
        public ProductModel GetProductInfoByID(string id)
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
        public MPagingData<List<ProductViewModel>> GetProductInfoByIDAndName(string name, string manufactor, string phone, long varietyID, string region, int pageIndex=1, int pageSize=10)
        {
            string whereStr = "";
            TSQLModel tsqlM = new TSQLModel
            {
                SQLStr = "select T_Product.*,T_Variety.Name as 'VarietyName' from T_Product inner join T_Variety on T_Product.FK_Variety = T_Variety.ID where T_Product.IfDelete=0",
                SQLParameters = new List<TSQLParameter>()
            };
            if(!string.IsNullOrEmpty(name))
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

            tsqlM.SQLStr += whereStr + $" limit {pageSize} Offset {(pageIndex - 1) * pageSize};";
            tsqlM.SQLStr += "select Count(*) from T_Product inner join T_Variety on T_Product.FK_Variety = T_Variety.ID where T_Product.IfDelete=0" + whereStr + ";";

            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            List<ProductViewModel> listM = ConvertManager.DataTableToList<ProductViewModel>(ds.Tables[0]);
            MPagingData<List<ProductViewModel>> pageProductM = new MPagingData<List<ProductViewModel>>();
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
    }
}
