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
    /// 商品业务逻辑层
    /// </summary>
    public class ProductBLL
    {
        /// <summary>
        /// 数据访问对象
        /// </summary>
        private ProductDAL productDAL;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ProductBLL()
        {
            productDAL = new ProductDAL();
        }

        /// <summary>
        /// 根据唯一标识获取商品信息
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>商品信息</returns>
        public MResultModel<ProductModel> GetProductInfoByID(long ID)
        {
            ProductModel productM = productDAL.GetProductInfoByID(ID);
            if(productM!=null)
            {
                return MResultModel<ProductModel>.GetSuccessResultM(productM, "商品信息");
            }
            else
            {
                return MResultModel<ProductModel>.GetFailResultM(null, "没有这个商品");
            }
        }

        /// <summary>
        /// 添加一个商品
        /// </summary>
        /// <param name="productM">商品模型</param>
        /// <returns>返回商品模型</returns>
        public MResultModel AddProductInfo(ProductModel productM)
        {
            if(productM!=null)
            {
                productDAL.AddProductInfo(productM);
                return MResultModel.GetSuccessResultM("添加成功");
            }
            else
            {
                return MResultModel.GetFailResultM("添加失败");
            }
        }

        /// <summary>
        /// 修改一个商品
        /// </summary>
        /// <param name="productM">商品模型</param>
        /// <returns>返回商品模型</returns>
        public MResultModel UpdateProductInfo(ProductModel productM)
        {
            if(productM!=null)
            {
                ProductModel oldProductM = productDAL.GetProductInfoByID(productM.ID);
                if (oldProductM == null || oldProductM.ID == productM.ID)
                {
                    oldProductM = productDAL.GetProductInfoByID(productM.ID);
                    if (oldProductM != null)
                    {
                        oldProductM.Name = productM.Name;
                        oldProductM.Manufactor = productM.Manufactor;
                        oldProductM.Phone = productM.Phone;
                        oldProductM.FK_Variety = productM.FK_Variety;
                        oldProductM.Region = productM.Region;
                        oldProductM.Remark = productM.Remark;
                        productDAL.UpdateProductInfo(oldProductM);
                        return MResultModel.GetSuccessResultM("修改成功");
                    }
                    else
                    {
                        return MResultModel.GetFailResultM("商品信息不存在");
                    }
                }
                else
                {
                    return MResultModel.GetFailResultM("商品信息已存在");
                }
            }
            else
            {
                return MResultModel.GetFailResultM("修改对象不存在");
            }
        }

        /// <summary>
        /// 删除一个商品
        /// </summary>
        /// <param name="ID">商品ID</param>
        /// <returns>商品模型</returns>
        public MResultModel DeleteProductInfo(long ID)
        {
            productDAL.DeleteProductInfo(ID);
            return MResultModel.GetSuccessResultM("删除成功");
        }

        /// <summary>
        /// 根据商品名称查询商品信息
        /// </summary>
        /// <param name="Name">商品名称</param>
        /// <param name="Manufactor">厂家</param>
        /// <param name="Phone">电话</param>
        /// <param name="varietyID">种类</param>
        /// <param name="Region">地区</param>
        /// <param name="Remark">备注</param>
        /// <param name="pageIndex">分页页数</param>
        /// <param name="pageSize">分页条数</param>
        /// <returns></returns>
        public MResultPagingModel<List<ProductViewModel>> GetProductInfoByIDAndName(string Name, string Manufactor, string Phone, long varietyID, string Region, int pageIndex = 1, int pageSize = 10)
        {
            MPagingData<List<ProductViewModel>> listM = productDAL.GetProductInfoByIDAndName(Name, Manufactor, Phone, varietyID, Region, pageIndex, pageSize);
            if(listM!=null)
            {
                return MResultPagingModel<List<ProductViewModel>>.GetSuccessResultM(listM, "查询成功");
            }
            else
            {
                return MResultPagingModel<List<ProductViewModel>>.GetFailResultM(null, "查询失败");
            }
        }
    }
}
