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
            int index = ListBox_Variable.Items.IndexOf(item);

            var data = (ObservableCollection<LulucoDict>)ListBox_Variable.ItemsSource;
            data.RemoveAt(index);
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            var data = (ObservableCollection<LulucoDict>)ListBox_Variable.ItemsSource;
            LulucoDict newData = new LulucoDict{Key = "", Value = ""};
            data.Add(newData);
        }
    }
}
