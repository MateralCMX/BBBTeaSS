using BBBTeaSS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BBBTeaSS.WPFUI
{
    /// <summary>
    /// 应用程序管理器
    /// </summary>
    public static class ApplicationManager
    {
        /// <summary>
        /// 登录用户
        /// </summary>
        public static UserModel LoginUserM { get; set; }
        /// <summary>
        /// 显示提示信息框
        /// </summary>
        /// <param name="message"></param>
        public static void ShowInfoMessageBox(string message)
        {
            MessageBox.Show(message, "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary>
        /// 显示错误信息框
        /// </summary>
        /// <param name="message"></param>
        public static void ShowErrorMessageBox(string message)
        {
            MessageBox.Show(message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        /// <summary>
        /// 显示警告信息框
        /// </summary>
        /// <param name="message"></param>
        public static void ShowWarningMessageBox(string message)
        {
            MessageBox.Show(message, "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
