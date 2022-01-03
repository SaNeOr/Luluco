using Luluco.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luluco.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        public RelayCommands HomeViewCommand { get; set; }
        public RelayCommands DiscoverViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public DiscoverViewModel DiscoverVM { get; set; }

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
            HomeVM = new HomeViewModel();
            DiscoverVM = new DiscoverViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommands(o => 
            {
                CurrentView = HomeVM;
            });

            DiscoverViewCommand = new RelayCommands(o =>
            {
                CurrentView = DiscoverVM;
            });


        }
    }
}
