using Luluco.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Luluco.MVVM.View
{
    /// <summary>
    /// HomeView.xaml 的交互逻辑
    /// </summary>
    public partial class TemplateView : UserControl
    {
        public TemplateView()
        {
            InitializeComponent();
        }

        private void CollectionViewSource_Filter_CustomerVariable(object sender, FilterEventArgs e)
        {
            LulucoPair CustomerVariable = e.Item as LulucoPair;
            if (CustomerVariable != null)
            {
                e.Accepted = !LulucoKeyWord.AllKeyWords.Contains(CustomerVariable.Key);
            }
        }
    }
}
