using BBBTeaSS.BLL;
using BBBTeaSS.Model;
using MateralTools.MResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BBBTeaSS.WPFUI
{

    /// <summary>
    /// VarietyInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class VarietyInfoWindow : Window
    {
        #region
        /// <summary>
        /// ID
        /// </summary>
        public long? ID { get; set; }
        /// <summary>
        /// 种类业务逻辑对象
        /// </summary>
        private VarietyBLL varietyBLL;
        /// <summary>
        /// 种类模型
        /// </summary>
        private VarietyModel varietyM;
        #endregion
        /// <summary>
        /// 构造方法
        /// </summary>
        public VarietyInfoWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VarietyInfoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            varietyBLL = new VarietyBLL();
            if(ID!=null)
            {
                MResultModel<VarietyModel> resM = varietyBLL.GetVarietyInfoByID(ID.Value);
                if(resM.ResultType==MResultType.Success)
                {
                    varietyM = resM.Data;
                    SetVarietyInfo();
                }
            }
            if (varietyM==null)
            {
                varietyM = new VarietyModel
                {
                    ID = 0
                };
            }
        }
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            BindVarietyInfo();
            MResultModel resM;
            if (varietyM.ID>0)
            {
                resM = varietyBLL.UpdateVarietyInfo(varietyM);
            }
            else
            {
                resM = varietyBLL.AddVarietyInfo(varietyM);
            }
            if(resM.ResultType==MResultType.Success)
            {
                ApplicationManager.ShowInfoMessageBox(resM.Message);
            }
            else
            {
                ApplicationManager.ShowInfoMessageBox("修改失败\r\n"+ resM.Message);
            }
        }

        /// <summary>
        /// 绑定种类信息
        /// </summary>
        private void BindVarietyInfo()
        {
            varietyM.Name = TextName.Text.Trim();
        }
        /// <summary>
        /// 设置种类信息
        /// </summary>
        private void SetVarietyInfo()
        {
            TextName.Text = varietyM.Name;
        }
    }
}
