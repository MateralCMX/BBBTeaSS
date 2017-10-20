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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BBBTeaSS.WPFUI
{
    /// <summary>
    /// SetPasswordControl.xaml 的交互逻辑
    /// </summary>
    public partial class SetPasswordControl : UserControl
    {
        #region 成员
        private UserBLL userBLL;
        #endregion
        /// <summary>
        /// 构造方法
        /// </summary>
        public SetPasswordControl()
        {
            InitializeComponent();
            userBLL = new UserBLL();
        }
        /// <summary>
        /// 确定修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            string NewPassword1 = TextNewPassword1.Password;
            string NewPassword2 = TextNewPassword2.Password;
            if (NewPassword1 == NewPassword2)
            {
                MResultModel<UserModel> resM = userBLL.UpdatePassword(ApplicationManager.LoginUserM.ID, NewPassword1);
                if (resM.ResultType == MResultType.Success)
                {
                    ApplicationManager.LoginUserM = resM.Data;
                    ApplicationManager.ShowInfoMessageBox("修改密码成功");
                }
                else
                {
                    ApplicationManager.ShowInfoMessageBox("修改密码失败\r\n" + resM.Message);
                }
            }
            else
            {
                ApplicationManager.ShowInfoMessageBox("修改密码失败\r\n两次输入的密码不一样！");
            }
        }
    }
}
