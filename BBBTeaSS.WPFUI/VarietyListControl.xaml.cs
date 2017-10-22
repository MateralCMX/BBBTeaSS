using BBBTeaSS.BLL;
using BBBTeaSS.Model;
using MateralTools.MResult;
using MateralTools.MVerify;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BBBTeaSS.WPFUI
{
    /// <summary>
    /// VarietyListControl.xaml 的交互逻辑
    /// </summary>
    public partial class VarietyListControl : UserControl
    {
        #region 成员
        private VarietyBLL varietyBLL { get; set; }
        private MPagingModel pageM { get; set; }
        #endregion
        /// <summary>
        /// 构造方法
        /// </summary>
        public VarietyListControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VarietyControl_Loaded(object sender, RoutedEventArgs e)
        {
            varietyBLL = new VarietyBLL();
            pageM = new MPagingModel
            {
                DataCount = 1,
                PagingCount = 1,
                PagingIndex = 1,
                PagingSize = 10
            };
            Query();
        }
        /// <summary>
        /// 查询方法
        /// </summary>
        private void Query()
        {
            string Name = TextName.Text.Trim();
            MResultPagingModel<List<VarietyModel>> resM = varietyBLL.GetVarietyInfoByIDAndName(Name, pageM.PagingIndex, pageM.PagingSize);
            if(resM.ResultType==MResultType.Success)
            {
                pageM = resM.PagingInfo;
                BindList(resM.Data);
            }
            else
            {
                ApplicationManager.ShowErrorMessageBox("NOPE！程序出错了");
            }
        }
        /// <summary>
        /// 绑定列表信息
        /// </summary>
        /// <param name="listVarietyM"></param>
        private void BindList(List<VarietyModel> listVarietyM)
        {
            MainDataGrid.ItemsSource = listVarietyM;
            BindPaginginfo(); 
        }
        /// <summary>
        /// 绑定分页信息
        /// </summary>
        private void BindPaginginfo()
        {
            LabelPageCount.Content = $"{pageM.PagingIndex}/{pageM.PagingCount}";
            TextPageIndex.Text = pageM.PagingIndex.ToString();
            BtnUpPage.IsEnabled = pageM.PagingIndex != 1;
            BtnDownPage.IsEnabled = pageM.PagingIndex != pageM.PagingCount;
        }
        
        /// <summary>
        /// 搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            VarietyInfoWindow vw = new VarietyInfoWindow();
            vw.ShowDialog();
            Query();
        }
        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainDataGrid.ItemsSource is List<VarietyModel> listM)
            {
                VarietyModel varietyModel = new VarietyModel();
                varietyModel = listM[MainDataGrid.SelectedIndex];
                VarietyInfoWindow vw = new VarietyInfoWindow
                {
                    ID = varietyModel.ID
                };
                vw.ShowDialog();
                Query();
            }
        }
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否要删除该种类？","提示",MessageBoxButton.YesNo,MessageBoxImage.Question)==MessageBoxResult.Yes)
            {
                if(MainDataGrid.ItemsSource is List<VarietyModel> listM)
                {
                    VarietyModel varietyModel = listM[MainDataGrid.SelectedIndex];

                    MResultModel resM = varietyBLL.DeleteVarietyInfo(varietyModel.ID);
                    ApplicationManager.ShowInfoMessageBox(resM.Message);
                    Query();
                } 
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpPage_Click(object sender, RoutedEventArgs e)
        {
            pageM.PagingIndex--;
            Query();
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownPage_Click(object sender, RoutedEventArgs e)
        {
            pageM.PagingIndex++;
            Query();
        }
        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGotoPage_Click(object sender, RoutedEventArgs e)
        {
            string pageIndex = TextPageIndex.Text.Trim();
            if (VerifyManager.IsRealNumber(pageIndex))
            {
                int pageIndexNum = Convert.ToInt32(pageIndex);
                if (pageIndexNum < 1)
                {
                    ApplicationManager.ShowInfoMessageBox("NOPE!页数不会是负数!");
                }
                else if (pageIndexNum > pageM.PagingCount)
                {
                    ApplicationManager.ShowInfoMessageBox("NOPE？页数会大于总页数?");
                }
                else
                {
                    pageM.PagingIndex = pageIndexNum;
                    Query();
                }
            }
            else
            {
                ApplicationManager.ShowInfoMessageBox("NOPE？页数只可以是数字!");
            }
        }



    }
}
