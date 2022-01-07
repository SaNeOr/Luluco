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

using Luluco.MVVM.ViewModel;
using Luluco.Core;
using Luluco.MVVM.View;
using System.Collections.ObjectModel;

namespace Luluco
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            // Begin dragging the window
            try
            {
                this.DragMove();
            }
            catch { }
        }


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click_Read(object sender, RoutedEventArgs e)
        {
            var currentExportPath = ExportPath.Text;
            var currentTemplatePath = TemplatePath.Text;

            MainViewModel mainViewModel = this.DataContext as MainViewModel;



            LulucoPath.ExportDirPath = currentExportPath;
            LulucoPath.LulucoConfiDirgPath = currentTemplatePath;

            LulucoPath.VarsConfigPath = LulucoPath.LulucoConfiDirgPath + "/" + "vars.json";
            LulucoPath.TemplateConfigPath = LulucoPath.LulucoConfiDirgPath + "/" + "template.json";

            mainViewModel.LoadData();

            //this.VariableContent.Content = new VariableViewModel();

            //variableViewModel.Variabledict.Clear();
            //var t = variableViewModel.Variabledict;
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            MainViewModel mainViewModel = this.DataContext as MainViewModel;
            mainViewModel.Save();
        }

        private void Button_Click_Apply(object sender, RoutedEventArgs e)
        {
            MainViewModel mainViewModel = this.DataContext as MainViewModel;
            mainViewModel.Apply();
        }


        private void CollectionViewSource_Filter_Variable_CustomerVariable(object sender, FilterEventArgs e)
        {
            LulucoPair CustomerVariable = e.Item as LulucoPair;
            if (CustomerVariable != null)
            {
                e.Accepted = !LulucoKeyWord.AllKeyWords.Contains(CustomerVariable.Key);
            }

        }

        private void CollectionViewSource_Filter_Variable_KeywordVariable(object sender, FilterEventArgs e)
        {
            LulucoPair KeywordVariable = e.Item as LulucoPair;
            if (KeywordVariable != null)
            {
                e.Accepted = LulucoKeyWord.AllKeyWords.Contains(KeywordVariable.Key);
            }
        }


        private void Button_Click_Variable_Add(object sender, RoutedEventArgs e)
        {
            //var data = (ObservableCollection<LulucoPair>)((CollectionViewSource)(ListBox_CustomVariable.ItemsSource)).Source;
            //var resourceDictionary = this.Resources;
            //var data = (ObservableCollection<LulucoPair>)((CollectionViewSource)(resourceDictionary["VariableCustomerVariable"])).Source;

            MainViewModel mainViewModel = this.DataContext as MainViewModel;
            var data = mainViewModel.VariableVM.Variabledict;

            LulucoPair newData = new LulucoPair { Key = "", Value = "" };
            data.Add(newData);


        }

        private void Button_Click_Variable_Delete(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = ListBox_CustomVariable.Items.IndexOf(item);

            //var data = (ObservableCollection<LulucoPair>)ListBox_CustomVariable.ItemsSource;
            var resourceDictionary = this.Resources;
            var data = (ObservableCollection<LulucoPair>)((CollectionViewSource)(resourceDictionary["VariableCustomerVariable"])).Source;

            data.RemoveAt(index);
        }

        private void CollectionViewSource_Filter_Templte_CustomerVariable(object sender, FilterEventArgs e)
        {
            LulucoPair KeywordVariable = e.Item as LulucoPair;
            if (KeywordVariable != null)
            {
                e.Accepted = !LulucoKeyWord.AllKeyWords.Contains(KeywordVariable.Key);
            }
        }


        private void Button_Click_CloseApp(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void Button_Click_Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

    }
}
