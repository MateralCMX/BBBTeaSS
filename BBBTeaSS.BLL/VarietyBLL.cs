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

        /// <summary>
        /// 根据唯一标识获取种类信息
        /// </summary>
        /// <param name="ID">种类唯一标识</param>
        /// <returns>返回种类实体</returns>
        public MResultModel<VarietyModel> GetVarietyInfoByID(long ID)
        {
            VarietyModel varietyM = varietyDAL.GetVarietyInfoByID(ID);
            if (varietyM!=null)
            {
                return MResultModel<VarietyModel>.GetSuccessResultM(varietyM, "种类信息");
            }
            else
            {
                return MResultModel<VarietyModel>.GetFailResultM(null, "没有这个种类");
            }
        }

        /// <summary>
        /// 添加一个种类信息
        /// </summary>
        /// <param name="varietyM">种类信息</param>
        /// <returns>返回种类实体</returns>
        public MResultModel AddVarietyInfo(VarietyModel varietyM)
        {
            if (varietyM!=null)
            {
                varietyDAL.AddVarietyInfo(varietyM);
                return MResultModel.GetSuccessResultM("添加成功");
            }
            else
            {
                return MResultModel.GetFailResultM("添加失败");
            }
        }

        /// <summary>
        /// 修改一个种类信息
        /// </summary>
        /// <param name="varietyM">种类型</param>
        /// <returns>返回种类实体</returns>
        public MResultModel UpdateVarietyInfo(VarietyModel varietyM)
        {
            if (varietyM!=null)
            {//修改种类信息需存在
                VarietyModel oldVarityM = varietyDAL.GetVarietyInfoByID(varietyM.ID);
                if (oldVarityM==null||oldVarityM.ID==varietyM.ID)
                {
                    oldVarityM = varietyDAL.GetVarietyInfoByID(varietyM.ID);
                    if(oldVarityM!=null)//数据存在
                    {
                        oldVarityM.Name = varietyM.Name;
                        varietyDAL.UpdateVarietyInfo(oldVarityM);
                        return MResultModel.GetSuccessResultM("修改成功");
                    }
                    else
                    {
                        return MResultModel.GetFailResultM("种类信息不存在");
                    }
                }
                else
                {
                    return MResultModel.GetFailResultM("种类信息已存在");
                }
            }
            else
            {
                return MResultModel.GetFailResultM("修改对象不存在");
            }
        }

        /// <summary>
        /// 删除一个种类信息
        /// </summary>
        /// <param name="ID">唯一标示</param>
        /// <returns>返回种类实体</returns>
        public MResultModel DeleteVarietyInfo(long ID)
        {
            varietyDAL.DeleteVarietyInfo(ID);
            return MResultModel.GetSuccessResultM("删除成功");
        }

        /// <summary>
        /// 根据种类名称和名称查询种类信息
        /// </summary>
        /// <param name="Name">种类名称</param>
        /// <param name="pageIndex">分页页数</param>
        /// <param name="pageSize">分页条数</param>
        /// <returns></returns>
        public MResultPagingModel<List<VarietyModel>> GetVarietyInfoByIDAndName(string Name,int pageIndex =1,int pageSize = 10)
        {
            MPagingData<List<VarietyModel>> listM = varietyDAL.GetVarietyInfoByIDAndName(Name, pageIndex, pageSize);
            if(listM!=null)
            {
                return MResultPagingModel<List<VarietyModel>>.GetSuccessResultM(listM, "查询成功");
            }
            else
            {
                return MResultPagingModel<List<VarietyModel>>.GetSuccessResultM(null, "查询失败");
            }
        }
    }
}
