using BBBTeaSS.BLL;
using BBBTeaSS.Model;
using MateralTools.MResult;
using MateralTools.MVerify;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// UserListControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserListControl : UserControl
    {
        #region 成员
        /// <summary>
        /// 用户业务操作对象
        /// </summary>
        private UserBLL userBLL { get; set; }
        private MPagingModel PageM { get; set; }
        #endregion
        /// <summary>
        /// 构造方法
        /// </summary>
        public UserListControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            userBLL = new UserBLL();
            PageM = new MPagingModel
            {
                DataCount = 1,
                PagingCount = 1,
                PagingIndex = 1,
                PagingSize = 10
            };
            Query();
        }
        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainDataGrid.ItemsSource is List<UserModel> listM)
            {
                UserModel userM = listM[MainDataGrid.SelectedIndex];
                UserInfoWindow uw = new UserInfoWindow
                {
                    ID = userM.ID
                };
                uw.ShowDialog();
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
            if (MessageBox.Show("是否要删除该账户？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (MainDataGrid.ItemsSource is List<UserModel> listM)
                {
                    UserModel userM = listM[MainDataGrid.SelectedIndex];
                    if (ApplicationManager.LoginUserM.ID != userM.ID)
                    {
                        MResultModel resM = userBLL.DeleteUserInfo(userM.ID);
                        ApplicationManager.ShowInfoMessageBox(resM.Message);
                        Query();
                    }
                    else
                    {
                        ApplicationManager.ShowInfoMessageBox("不能删除自己");
                    }
                }
            }
        }
        /// <summary>
        /// 重置密码按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainDataGrid.ItemsSource is List<UserModel> listM)
            {
                UserModel userM = listM[MainDataGrid.SelectedIndex];
                userM.Password = ConfigurationManager.AppSettings["DefultPassword"] ?? "123456";
                userBLL.UpdatePassword(userM.ID, userM.Password);
                ApplicationManager.ShowInfoMessageBox("密码已重置为:" + userM.Password);
            }
        }
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            UserInfoWindow uw = new UserInfoWindow();
            uw.ShowDialog();
            Query();
        }
        /// <summary>
        /// 查询方法
        /// </summary>
        private void Query()
        {
            string userID = TextUserID.Text.Trim();
            string name = TextName.Text.Trim();
            MResultPagingModel<List<UserModel>> resM = userBLL.GetUserInfoByUserIDAndName(userID, name, PageM.PagingIndex, PageM.PagingSize);
            if (resM.ResultType == MResultType.Success)
            {
                PageM = resM.PagingInfo;
                BindList(resM.Data);
            }
            else
            {
                ApplicationManager.ShowErrorMessageBox("程序出错了");
            }
        }
        /// <summary>
        /// 绑定列表显示
        /// </summary>
        /// <param name="listUserM"></param>
        private void BindList(List<UserModel> listUserM)
        {
            MainDataGrid.ItemsSource = listUserM;
            BindPaginginfo();
        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }
        /// <summary>
        /// 绑定分页信息
        /// </summary>
        private void BindPaginginfo()
        {
            LabelPageCount.Content = $"{PageM.PagingIndex}/{PageM.PagingCount}";
            TextPageIndex.Text = PageM.PagingIndex.ToString();
            BtnUpPage.IsEnabled = PageM.PagingIndex != 1;
            BtnDownPage.IsEnabled = PageM.PagingIndex != PageM.PagingCount;
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpPage_Click(object sender, RoutedEventArgs e)
        {
            PageM.PagingIndex--;
            Query();
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownPage_Click(object sender, RoutedEventArgs e)
        {
            PageM.PagingIndex++;
            Query();
        }

        private void BtnGotoPage_Click(object sender, RoutedEventArgs e)
        {
            string pageIndex = TextPageIndex.Text.Trim();
            if (VerifyManager.IsRealNumber(pageIndex))
            {
                int pageIndexNum = Convert.ToInt32(pageIndex);
                if (pageIndexNum < 1)
                {
                    ApplicationManager.ShowInfoMessageBox("脑残？页数不会是负数!");
                }
                else if (pageIndexNum > PageM.PagingCount)
                {
                    ApplicationManager.ShowInfoMessageBox("制杖？页数会大于总页数?");
                }
                else
                {
                    PageM.PagingIndex = pageIndexNum;
                    Query();
                }
            }
            else
            {
                ApplicationManager.ShowInfoMessageBox("看不懂噶？页数只可以是数字!");
            }
        }
    }
}
