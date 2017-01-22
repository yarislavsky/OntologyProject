using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using DevExpress.XtraPrinting.Export.Imaging;
using MainModule.Managers;
using MainModule.Models;
using ViewModelBase = GalaSoft.MvvmLight.ViewModelBase;

namespace MainModule.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ItemViewModel> _items; 
        private bool _isGenreChecked;
        private bool _isDateChecked;
        private bool _isWriterChecked;
        private bool _isDirectorChecked;
        private bool _isCountryChecked;

        private string _writerName;
        private string _directorName;
        private string _genre;
        private string _country;
        private string _date;

        private string _rowLimit;
        private Visibility _isLoading;

        private ICommand _runQueryCommand;
        private ICommand _cleanItemsCommand;

        private QueryRunnerManager _queryRunnerManager;

        public string[] AllGenres => Defaults.AllGenres.Keys.ToArray();
        public string[] AllRowLimits => Defaults.AllRowLimits;

        public string RowLimit
        {
            get { return _rowLimit; }
            set
            {
                if(_rowLimit == value)
                    return;
                _rowLimit = value;
                RaisePropertyChanged(() => RowLimit);
            }
        }

        public Visibility IsLoading
        {
            get { return _isLoading; }
            set
            {
                if(_isLoading == value)
                    return;
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        public bool IsGenreChecked
        {
            get { return _isGenreChecked; }
            set
            {
                if(_isGenreChecked == value)
                    return;
                _isGenreChecked = value;
                RaisePropertyChanged(() => IsGenreChecked);
            }
        }

        public bool IsDateChecked
        {
            get { return _isDateChecked; }
            set
            {
                if(_isDateChecked == value)
                    return;
                _isDateChecked = value;
                RaisePropertyChanged(() => IsDateChecked);
            }
        }

        public bool IsWriterChecked
        {
            get { return _isWriterChecked; }
            set
            {
                if(_isWriterChecked == value)
                    return;
                _isWriterChecked = value;
                RaisePropertyChanged(() => IsWriterChecked);
            }
        }

        public bool IsDirectorChecked
        {
            get { return _isDirectorChecked; }
            set
            {
                if(_isDirectorChecked == value)
                    return;
                _isDirectorChecked = value;
                RaisePropertyChanged(() => IsDirectorChecked);
            }
        }

        public bool IsCountryChecked
        {
            get { return _isCountryChecked; }
            set
            {
                if(_isCountryChecked == value)
                    return;
                _isCountryChecked = value;
                RaisePropertyChanged(() => IsCountryChecked);
            }
        }

        public string WriterName
        {
            get { return _writerName; }
            set
            {
                if (_writerName == value)
                    return;
                _writerName = value;
                RaisePropertyChanged(() => WriterName);
            }
        }

        public string DirectorName
        {
            get { return _directorName; }
            set
            {
                if(_directorName == value)
                    return;
                _directorName = value;
                RaisePropertyChanged(() => DirectorName);
            }
        }

        public string SelectedGenre
        {
            get { return _genre; }
            set
            {
                if(_genre == value)
                    return;
                _genre = value;
                RaisePropertyChanged(() => SelectedGenre);
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                if(_country == value)
                    return;
                _country = value;
                RaisePropertyChanged(() => Country);
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                if(_date == value)
                    return;
                _date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        public ICommand RunQueryCommand => _runQueryCommand ?? (_runQueryCommand = new DelegateCommand(OnRunQuery));

        public ICommand CleanItemsCommand => _cleanItemsCommand ?? (_cleanItemsCommand = new DelegateCommand(OnCleanItems));

        public ObservableCollection<ItemViewModel> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            IsLoading = Visibility.Hidden;
            _queryRunnerManager = new QueryRunnerManager();
            SubscribeToManagerEvents();
            _items = new ObservableCollection<ItemViewModel>();
        }

        private void SubscribeToManagerEvents()
        {
            if (_queryRunnerManager != null)
            {
                _queryRunnerManager.ProcessFinished += ManagerProcessFinished;
                _queryRunnerManager.ProcessStarted += ManagerProcessStarted;
            }

        }

        private void ManagerProcessStarted()
        {
            DXSplashScreen.Show<SplashScreenViewMain>();
            //SplashScreenService.SetSplashScreenState("Retrieving data...");
        }

        private void ManagerProcessFinished(IEnumerable<ItemViewModel> obj)
        {
            DXSplashScreen.Close();
            var itemList = obj.ToList();
            foreach (var itemViewModel in itemList)
            {
                Items.Add(itemViewModel);
            }
        }

        private void OnRunQuery()
        {
            Items.Clear();
            IsLoading = Visibility.Visible;
            try
            {
                var queryParameters = GetQueryParameters();
                _queryRunnerManager.RunWorker(queryParameters);
            }
            finally
            {
                IsLoading = Visibility.Hidden;
            }
        }

        private void OnCleanItems()
        {
            Items.Clear();
            RaisePropertyChanged(() => Items);
        }

        private QueryParameters GetQueryParameters()
        {
            int rowLimit;

            return new QueryParameters
            {
                IsDateChecked = IsDateChecked,
                IsDirectorChecked = IsDirectorChecked,
                IsCountryChecked = IsCountryChecked,
                IsWriterChecked = IsWriterChecked,
                IsGenreChecked = IsGenreChecked,
                DirectorName = DirectorName,
                WriterName = WriterName,
                Country = Country,
                Date = Date,
                Genre = !string.IsNullOrEmpty(SelectedGenre) && Defaults.AllGenres.ContainsKey(SelectedGenre)
                        ? Defaults.AllGenres[SelectedGenre]
                        : String.Empty,
                RowLimit = int.TryParse(RowLimit, out rowLimit)
                            ? (int?)rowLimit
                            : null
            };
        }
    }
}