using Luluco.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luluco.MVVM.ViewModel
{
    partial class MainViewModel: ObservableObject
    {

        public RelayCommands TemplateViewCommand { get; set; }
        public RelayCommands VariableViewCommand { get; set; }

        public TemplateModel TemplateVM { get; set; }
        public VariableViewModel VariableVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            TemplateVM = new TemplateModel();
            VariableVM = new VariableViewModel();
            //LoadData();
        }

        public void LoadData()
        {
            TemplateVM.LoadData();
            VariableVM.LoadData();
        }

    }
}
