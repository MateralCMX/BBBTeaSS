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
    /// StockWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StockWindow : Window
    {
        #region 成员
        public long ProductID = 0;
        public string ProductName = "";

        private List<StockModel> JoinStockM = null;
        private List<StockModel> OutStockM = null;
        private StockBLL stockBll { get; set; }
        #endregion

        public StockWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextProductName.Text = ProductName;
            stockBll = new StockBLL();
            QueryJoin();
        }

        /// <summary>
        /// 查询入库方法
        /// </summary>
        private void QueryJoin()
        {
            if (OutStockM == null)
            {
                MResultModel<List<StockModel>> resM = stockBll.GetStockInfoByProductID(ProductID, 1);
                if (resM.ResultType == MResultType.Success)
                {
                    JoinStockM = resM.Data;
                }
                else
                {
                    ApplicationManager.ShowErrorMessageBox("程序出错了");
                }
            }
            BindList(JoinStockM);
        }

        /// <summary>
        /// 入库单提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnJoin_Click(object sender, RoutedEventArgs e)
        {
            QueryJoin();
        }

        /// <summary>
        /// 出库单提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOut_Click(object sender, RoutedEventArgs e)
        {
            if (OutStockM == null)
            {
                MResultModel<List<StockModel>> resM = stockBll.GetStockInfoByProductID(ProductID, 2);
                if (resM.ResultType == MResultType.Success)
                {
                    OutStockM = resM.Data;
                }
                else
                {
                    ApplicationManager.ShowErrorMessageBox("程序出错了");
                }
            }
            BindList(OutStockM);
        }

        /// <summary>
        /// 绑定列表信息
        /// </summary>
        /// <param name="listProductM"></param>
        private void BindList(List<StockModel> listProductM)
        {
            MainDataGrid.ItemsSource = listProductM;
        }

    }
}
