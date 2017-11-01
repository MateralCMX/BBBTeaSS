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
    /// UserInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        #region 成员
        /// <summary>
        /// ID
        /// </summary>
        public long? ID { get; set; }
        /// <summary>
        /// 用户业务逻辑对象
        /// </summary>
        private UserBLL userBLL;
        /// <summary>
        /// 用户信息对象
        /// </summary>
        private UserModel userM;
        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        public UserInfoWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            userBLL = new UserBLL();
            if (ID != null)
            {
                MResultModel<UserModel> resM = userBLL.GetUserInfoByID(ID.Value);
                if (resM.ResultType == MResultType.Success)
                {
                    userM = resM.Data;
                    SetUserInfo();
                }
            }
            if (userM == null)
            {
                userM = new UserModel
                {
                    ID = 0
                };
            }
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            BindUserInfo();
            MResultModel resM;
            if (userM.ID > 0)
            {
                resM = userBLL.UpdateUserInfo(userM);
            }
            else
            {
                resM = userBLL.AddUserInfo(userM);
            }
            if (resM.ResultType == MResultType.Success)
            {
                ApplicationManager.ShowInfoMessageBox(resM.Message);
                Close();
            }
            else
            {
                ApplicationManager.ShowInfoMessageBox("修改失败\r\n" + resM.Message);
            }
        }

        /// <summary>
        /// 绑定用户信息
        /// </summary>
        private void BindUserInfo()
        {
            userM.Name = TextName.Text.Trim();
            userM.UserID = TextUserName.Text.Trim();
        }

        /// <summary>
        /// 设置用户信息
        /// </summary>
        private void SetUserInfo()
        {
            TextName.Text = userM.Name;
            TextUserName.Text = userM.UserID;
        }
    }
}
