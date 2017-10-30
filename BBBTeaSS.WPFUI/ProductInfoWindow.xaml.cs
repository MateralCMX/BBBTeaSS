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
    /// ProductInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProductInfoWindow : Window
    {
        #region
        public long? ID { get; set; }
        public ProductBLL productBLL;
        public ProductModel productModel;
        #endregion
        /// <summary>
        /// 构造方法
        /// </summary>
        public ProductInfoWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductWindow_Loaded(object sender, RoutedEventArgs e)
        {
            productBLL = new ProductBLL();
            BindVarietyInfo();
            if (ID!=null)
            {
                MResultModel<ProductModel> resM = productBLL.GetProductInfoByID(ID.Value);
                if (resM.ResultType==MResultType.Success)
                {
                    productModel = resM.Data;
                    SetProductInfo();
                }
            }
            if(productModel==null)
            {
                productModel = new ProductModel
                {
                    ID = 0
                };
            }
        }


        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            BindProductInfo();
            MResultModel resM;
            if (productModel.ID>0)
            {
                resM = productBLL.UpdateProductInfo(productModel);
            }
            else
            {
                resM = productBLL.AddProductInfo(productModel);
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
        /// 绑定种类信息
        /// </summary>
        private void BindProductInfo()
        {
            productModel.Name = TextProductName.Text.Trim();
            productModel.Manufactor = TextManufactorName.Text.Trim();
            productModel.Phone = TextPhoneNumber.Text.Trim();
            productModel.FK_Variety =Convert.ToInt32(ComboVarietyName.SelectedValue);
            productModel.Region = TextRegionName.Text.Trim();
            productModel.Remark = Remark.Text.Trim();
        }
        /// <summary>
        /// 设置种类信息
        /// </summary>
        private void SetProductInfo()
        {
            TextProductName.Text = productModel.Name;
            TextManufactorName.Text = productModel.Manufactor;
            TextPhoneNumber.Text = productModel.Phone;
            //ComboVarietyName.SelectedValue = productModel.FK_Variety;
            if (ComboVarietyName.ItemsSource is List<VarietyModel> listM)
            {
                for (int i = 0; i < listM.Count; i++)
                {
                    if (listM[i].ID == productModel.FK_Variety)
                    {
                        ComboVarietyName.SelectedIndex = i;
                        break;
                    }
                }
            }
            TextRegionName.Text = productModel.Region;
            Remark.Text = productModel.Remark;
        }

        /// <summary>
        /// 绑定种类信息
        /// </summary>
        private void BindVarietyInfo()
        {
            MResultPagingModel<List<VarietyModel>> resM = new VarietyBLL().GetVarietyInfoByIDAndName("", 1, 999);
            if (resM.ResultType == MResultType.Success)
            {
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
    }
}
