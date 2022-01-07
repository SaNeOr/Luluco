using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Luluco.Core;
using Luluco.MVVM.ViewModel;

namespace Luluco.MVVM.View
{
    public partial class VariableView : UserControl
    {
        public VariableView()
        {
            InitializeComponent();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = ListBox_CustomVariable.Items.IndexOf(item);

            //var data = (ObservableCollection<LulucoPair>)ListBox_CustomVariable.ItemsSource;
            var resourceDictionary = this.Resources;
            var data = (ObservableCollection<LulucoPair>)((CollectionViewSource)(resourceDictionary["CustomerVariable"])).Source;

            data.RemoveAt(index);
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            //var data = (ObservableCollection<LulucoPair>)((CollectionViewSource)(ListBox_CustomVariable.ItemsSource)).Source;
            var resourceDictionary = this.Resources;
            var data = (ObservableCollection<LulucoPair>)((CollectionViewSource)(resourceDictionary["CustomerVariable"])).Source;

            LulucoPair newData = new LulucoPair { Key = "", Value = "" };
            data.Add(newData);


        }

        private void CollectionViewSource_Filter_CustomerVariable(object sender, FilterEventArgs e)
        {
            LulucoPair CustomerVariable = e.Item as LulucoPair;
            if (CustomerVariable != null)
            {
                e.Accepted = !LulucoKeyWord.AllKeyWords.Contains(CustomerVariable.Key);
            }

        }

        private void CollectionViewSource_Filter_KeywordVariable(object sender, FilterEventArgs e)
        {
            LulucoPair KeywordVariable = e.Item as LulucoPair;
            if (KeywordVariable != null)
            {
                e.Accepted = LulucoKeyWord.AllKeyWords.Contains(KeywordVariable.Key);
            }
        }

    }
}
