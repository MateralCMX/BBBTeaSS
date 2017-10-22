using BBBTeaSS.WPFUI.Model;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            OpenLoginWindow();
        }
        #region 私有方法
        /// <summary>
        /// 打开登录窗体
        /// </summary>
        private void OpenLoginWindow()
        {
            Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
            if (ApplicationManager.LoginUserM == null)
            {
                Application.Current.Shutdown();
            }
            else
            {
                Show();
            }
        }
        /// <summary>
        /// 获取自定义控件的名称
        /// </summary>
        /// <param name="uc">自定义控件对象</param>
        /// <returns>自定义控件名称</returns>
        private string GetCustomControlName(UserControl uc)
        {
            string ucName = string.Empty;
            foreach (var item in uc.GetType().GetCustomAttributes(typeof(CustomControlAttribute), true))
            {
                ucName = ((CustomControlAttribute)item).Name;
            }
            return ucName;
        }
        /// <summary>
        /// 设置自定义控件默认属性
        /// </summary>
        /// <param name="uc">自定义控件对象</param>
        private void SetCustomControlDefaultProperty(UserControl uc)
        {
            Grid.SetRow(uc, 1);
            uc.VerticalAlignment = VerticalAlignment.Top;
        }
        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="uc">要添加的控件</param>
        private void AddControl(UserControl uc)
        {
            RemoveControl();
            string ucName = GetCustomControlName(uc);
            SetLeftTxt(string.Format("正在载入 {0} ......", ucName));
            SetCustomControlDefaultProperty(uc);
            MainPanel.Children.Add(uc);
            SetRightTxt(ucName);
            SetLeftTxt("就绪");
        }
        /// <summary>
        /// 移除控件
        /// </summary>
        private void RemoveControl()
        {
            SetLeftTxt("正在返回主界面......");
            for (int i = 2; i < MainPanel.Children.Count; i++)
            {
                MainPanel.Children.RemoveAt(i);
            }
            SetRightTxt("主界面");
            SetLeftTxt("就绪");
        }
        #endregion
        #region 公有方法
        /// <summary>
        /// 设置左边提示文本
        /// </summary>
        /// <param name="message">要显示的信息</param>
        public void SetLeftTxt(string message) => this.LeftTxt.Text = message;
        /// <summary>
        /// 设置右边提示文本
        /// </summary>
        /// <param name="message">要显示的信息</param>
        public void SetRightTxt(string message) => this.RightTxt.Text = message;
        /// <summary>
        /// 设置大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(int width,int height)
        {
            Width = width;
            Height = height;
        }
        /// <summary>
        /// 显示顶部菜单
        /// </summary>
        public void ShowTopMenu() => TopMenu.Visibility = Visibility.Visible;
        /// <summary>
        /// 隐藏顶部菜单
        /// </summary>
        public void HiddenTopMenu() => TopMenu.Visibility = Visibility.Hidden;
        #endregion
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMSetPassword_Click(object sender, RoutedEventArgs e)
        {
            AddControl(new SetPasswordControl());
        }
        /// <summary>
        /// 重新登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMRelist_Click(object sender, RoutedEventArgs e)
        {
            ApplicationManager.LoginUserM = null;
            OpenLoginWindow();
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMJoinStock_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMOutStock_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMQueryStock_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 商品类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMVariety_Click(object sender, RoutedEventArgs e)
        {
            AddControl(new VarietyListControl());
        }
        /// <summary>
        /// 商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMProduct_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMUser_Click(object sender, RoutedEventArgs e)
        {
            AddControl(new UserListControl());
        }
        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMAbout_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 我的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMSetUserInfo_Click(object sender, RoutedEventArgs e)
        {
            UserInfoWindow uw = new UserInfoWindow
            {
                ID = ApplicationManager.LoginUserM.ID
            };
            uw.ShowDialog();
        }
    }
}
