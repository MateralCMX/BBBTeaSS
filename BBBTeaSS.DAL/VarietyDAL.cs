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
    /// 种类数据操作类
    /// </summary>
    public class VarietyDAL
    {
        /// <summary>
        /// 添加一个种类
        /// </summary>
        /// <param name="varietyM">种类实体</param>
        public void AddVarietyInfo(VarietyModel varietyM)
        {
            SQLiteManager.Insert(varietyM);
        }

        /// <summary>
        /// 修改一个种类
        /// </summary>
        /// <param name="varietyM">种类实体</param>
        public void UpdateVarietyInfo(VarietyModel varietyM)
        {
            SQLiteManager.Update(varietyM);
        }

        /// <summary>
        /// 删除一个种类
        /// </summary>
        /// <param name="id"></param>
        public void DeleteVarietyInfo(long id)
        {
            VarietyModel varietyModel = GetVarietyInfoByID(id);
            if(varietyModel!=null)
            {
                varietyModel.IfDelete = true;
                UpdateVarietyInfo(varietyModel);
            }
        }

        /// <summary>
        /// 根据唯一标识获取种类信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>种类信息</returns>
        public VarietyModel GetVarietyInfoByID(long id)
        {
            TSQLModel tsqlM = new TSQLModel();
            tsqlM.SQLStr = "select * from T_Variety where ID=@ID";
            tsqlM.SQLParameters = new List<TSQLParameter>();
            tsqlM.SQLParameters.Add(new TSQLParameter("@ID", id));
            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            VarietyModel varietyM = ConvertManager.DataTableToList<VarietyModel>(ds.Tables[0]).FirstOrDefault();
            return varietyM;
        }

        /// <summary>
        /// 根据种类名称获取种类信息
        /// </summary>
        /// <param name="varietyName">种类名称</param>
        /// <returns>种类信息</returns>
        public VarietyModel GetVarietyInfoByID(string ID)
        {
            TSQLModel tsqlM = new TSQLModel();
            tsqlM.SQLStr = "select * from T_Variety where ID=@ID";
            tsqlM.SQLParameters = new List<TSQLParameter>();
            tsqlM.SQLParameters.Add(new TSQLParameter("@ID", ID));
            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            VarietyModel varietyM = ConvertManager.DataTableToList<VarietyModel>(ds.Tables[0]).FirstOrDefault();
            return varietyM;
        }
        /// <summary>
        /// 根据种类名称和名称查询种类信息
        /// </summary>
        /// <param name="Name">种类名称</param>
        /// <param name="pageIndex">分页页数</param>
        /// <param name="pageSize">分页条数</param>
        /// <returns>种类信息</returns>
        public MPagingData<List<VarietyModel>> GetVarietyInfoByIDAndName(string Name, int pageIndex = 1, int pageSize = 10)
        {
            string whereStr = "";
            TSQLModel tsqlM = new TSQLModel
            {
                SQLStr = "select * from T_Variety Where IfDelete = 0",
                SQLParameters = new List<TSQLParameter>()
            };
            //if (!string.IsNullOrEmpty(ID))
            //{
            //    whereStr += " And ID=@ID";
            //    tsqlM.SQLParameters.Add(new TSQLParameter("ID", ID));
            //}
            if (!string.IsNullOrEmpty(Name))
            {
                whereStr += " And Name like @Name";
                tsqlM.SQLParameters.Add(new TSQLParameter("Name", "%"+ Name +"%"));
            }
            //组合sql语句
            tsqlM.SQLStr += whereStr + $" limit {pageSize} Offset {(pageIndex - 1) * pageSize};";
            tsqlM.SQLStr += "select Count(*) from T_Variety where IfDelete = 0" + whereStr + ";";

            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            List<VarietyModel> listM = ConvertManager.DataTableToList<VarietyModel>(ds.Tables[0]);
            MPagingData<List<VarietyModel>> pageVarietyM = new MPagingData<List<VarietyModel>>();
            pageVarietyM.Data = listM;
            pageVarietyM.PageInfo = new MPagingModel();
            pageVarietyM.PageInfo.DataCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            if(pageVarietyM.PageInfo.DataCount % pageSize > 0)
            {
                pageVarietyM.PageInfo.PagingCount = (pageVarietyM.PageInfo.DataCount / pageSize) + 1;
            }
            else
            {
                pageVarietyM.PageInfo.PagingCount = (pageVarietyM.PageInfo.DataCount / pageSize);
            }
            pageVarietyM.PageInfo.PagingIndex = pageIndex;
            pageVarietyM.PageInfo.PagingSize = pageSize;
            return pageVarietyM;
        }
    }
}
