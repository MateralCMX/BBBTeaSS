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
        public void DeleteVarietyInfo(int id)
        {

        }

        /// <summary>
        /// 根据唯一标识获取种类信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>种类信息</returns>
        public VarietyModel GetVarietyInfoByID(int id)
        {
            TSQLModel tsqlM = new TSQLModel();
            tsqlM.SQLStr = "select * from T_Varity where ID=@ID";
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
        public VarietyModel GetVarietyInfoByID(string varietyName)
        {
            TSQLModel tsqlM = new TSQLModel();
            tsqlM.SQLStr = "select * from T_Varity where ID=@ID";
            tsqlM.SQLParameters = new List<TSQLParameter>();
            tsqlM.SQLParameters.Add(new TSQLParameter("@ID", varietyName));
            DataSet ds = SQLiteManager.ExecuteQuery(tsqlM);
            VarietyModel varietyM = ConvertManager.DataTableToList<VarietyModel>(ds.Tables[0]).FirstOrDefault();
            return varietyM;
        }
    }
}
