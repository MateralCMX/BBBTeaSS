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
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region 成员
        private UserBLL userBLL;
        #endregion
        public LoginWindow()
        {
            InitializeComponent();
            userBLL = new UserBLL();
        }
        /// <summary>
        /// 登录按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userName = this.TextUserName.Text.Trim();
            string password = this.TextPassword.Password;
            MResultModel<UserModel> resM = userBLL.Login(userName, password);
            if (resM.ResultType == MResultType.Success)
            {
                ApplicationManager.LoginUserM = resM.Data;
                Close();
            }
            else
            {
                ApplicationManager.ShowInfoMessageBox("登录失败\r\n" + resM.Message);
            }
        }
    }
}
