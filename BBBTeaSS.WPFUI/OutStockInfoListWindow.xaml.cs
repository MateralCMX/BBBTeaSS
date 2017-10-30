using BBBTeaSS.BLL;
using BBBTeaSS.Model;
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
using System.Windows.Shapes;

namespace BBBTeaSS.WPFUI
{
    /// <summary>
    /// OutStockInfoListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OutStockInfoListWindow : Window
    {
        public StockViewModel StockM { get; set; }
        public OutStockInfoListWindow()
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
            if (StockM != null)
            {
                this.TextProductName.Text = StockM.ProductName;
            }
            else
            {
                this.Close();
            }
        }
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            string Num = TextProductNumber.Text;
            if (VerifyManager.IsInteger(Num))
            {
                StockModel stockM = new StockModel
                {
                    FK_Product = StockM.ProductID,
                    IfDelete = false,
                    Remark = TextProductRemark.Text,
                    StockDateTime = DateTime.Now,
                    StockNumber = Convert.ToInt64(Num),
                    StockType = 2
                };
                if (stockM.StockNumber > 0)
                {
                    stockM.StockNumber = stockM.StockNumber * -1;
                }
                new StockBLL().AddStockInfo(stockM);
                ApplicationManager.ShowInfoMessageBox("出库成功。");
                Close();
            }
            else
            {
                ApplicationManager.ShowInfoMessageBox("请输入正确的数量。");
            }
        }
    }
}
