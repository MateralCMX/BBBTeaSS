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
    /// JoinStockInfoListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class JoinStockInfoListWindow : Window
    {
        #region 成员
        public ProductViewModel productM { get; set; }
        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        public JoinStockInfoListWindow()
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
            if (productM != null)
            {
                this.TextProductName.Text = productM.Name;
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
            string Num = this.TextProductNumber.Text;
            if (VerifyManager.IsInteger(Num))
            {
                StockModel stockM = new StockModel
                {
                    FK_Product = productM.ID,
                    IfDelete = false,
                    Remark = TextProductRemark.Text,
                    StockDateTime = DateTime.Now,
                    StockNumber = Convert.ToInt64(Num),
                    StockType = 1
                };
                new StockBLL().AddStockInfo(stockM);
                ApplicationManager.ShowInfoMessageBox("入库成功。");
                Close();
            }
            else
            {
                ApplicationManager.ShowInfoMessageBox("请输入正确的数量。");
            }
        }
    }
}
