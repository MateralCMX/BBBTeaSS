﻿using BBBTeaSS.BLL;
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
    /// OutStockInfoListControl.xaml 的交互逻辑
    /// </summary>
    public partial class OutStockInfoListControl : UserControl
    {
        #region 成员
        private StockBLL stockBll { get; set; }
        private MPagingModel pageM { get; set; }
        /// <summary>
        /// 商品窗口模式
        /// </summary>
        public QueryStockWindowMode Mode { get; set; }
        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        public OutStockInfoListControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutStockControl_Loaded(object sender, RoutedEventArgs e)
        {
            stockBll = new StockBLL();
            stockBll.GetStockInfoByProductID(1, 1);
            switch (Mode)
            {
                case QueryStockWindowMode.Query:
                    SelectCl.Visibility = Visibility.Visible;
                    OutStockCl.Visibility = Visibility.Hidden;
                    break;
                case QueryStockWindowMode.OutStock:
                    SelectCl.Visibility = Visibility.Hidden;
                    OutStockCl.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
            pageM = new MPagingModel
            {
                PagingIndex = 1,
                PagingSize = 10
            };
            BindVarietyInfo();
            Query();
        }

        /// <summary>
        /// 绑定种类信息
        /// </summary>
        private void BindVarietyInfo()
        {
            MResultPagingModel<List<VarietyModel>> resM = new VarietyBLL().GetVarietyInfoByIDAndName("", 1, 999);
            if (resM.ResultType == MResultType.Success)
            {
                resM.Data.Insert(0, new VarietyModel { ID = 0, Name = "全部" });
                ComboVarietyName.SelectedValuePath = "ID";
                ComboVarietyName.DisplayMemberPath = "Name";
                ComboVarietyName.ItemsSource = resM.Data;
                ComboVarietyName.SelectedIndex = 0;
            }
            else
            {
                ApplicationManager.ShowErrorMessageBox("程序出错了");
            }
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        private void Query()
        {
            string ProductName = TextProductName.Text.Trim();
            string ManufactorName = TextManufactorName.Text.Trim();
            string PhoneNumber = TextPhoneNumber.Text.Trim();
            long VarietyID = (long)ComboVarietyName.SelectedValue;
            string RegionName = TextRegionName.Text.Trim();

            MResultPagingModel<List<StockViewModel>> resM = stockBll.GetStockInfoByWhere(ProductName, ManufactorName, PhoneNumber, VarietyID, RegionName, pageM.PagingIndex, pageM.PagingSize);
            if (resM.ResultType == MResultType.Success)
            {
                pageM = resM.PagingInfo;
                BindList(resM.Data);
            }
            else
            {
                ApplicationManager.ShowErrorMessageBox("程序出错了");
            }
        }

        /// <summary>
        /// 绑定列表信息
        /// </summary>
        /// <param name="listProductM"></param>
        private void BindList(List<StockViewModel> listProductM)
        {
            MainDataGrid.ItemsSource = listProductM;
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
        /// 查看详情按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainDataGrid.ItemsSource is List<StockViewModel> listM)
            {
                StockViewModel stockM = listM[MainDataGrid.SelectedIndex];
                StockWindow jw = new StockWindow();
                jw.ProductID = stockM.ProductID;
                jw.ProductName = stockM.ProductName;
                jw.ShowDialog();
            }
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutStockButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainDataGrid.ItemsSource is List<StockViewModel> listM)
            {
                StockViewModel stockM = listM[MainDataGrid.SelectedIndex];
                OutStockInfoListWindow jw = new OutStockInfoListWindow();
                jw.StockM = stockM;
                jw.ShowDialog();
                Query();
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
        /// 跳转页
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
                    ApplicationManager.ShowInfoMessageBox("页数不会是负数！");
                }
                else if (pageIndexNum > pageM.PagingCount)
                {
                    ApplicationManager.ShowInfoMessageBox("页数不能大于总页数！");
                }
                else
                {
                    pageM.PagingIndex = pageIndexNum;
                    Query();
                }
            }
            else
            {
                ApplicationManager.ShowInfoMessageBox("页数只可以是数字！");
            }
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
    }
}