using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
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

        private ICommand _runQueryCommand;

        private QueryRunnerManager _queryRunnerManager;

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

        public ICommand RunQueryCommand => _runQueryCommand ?? (_runQueryCommand = new DelegateCommand(OnRunQuery));

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
            _queryRunnerManager = new QueryRunnerManager();
            SubscribeToManagerEvents();
            _items = new ObservableCollection<ItemViewModel>();
        }

        private void SubscribeToManagerEvents()
        {
            if (_queryRunnerManager != null)
                _queryRunnerManager.ProcessFinished += ManagerProcessFinished;

        }

        private void ManagerProcessFinished(IEnumerable<ItemViewModel> obj)
        {
            var itemList = obj.ToList();
            foreach (var itemViewModel in itemList)
            {
                Items.Add(itemViewModel);
            }
        }

        private void OnRunQuery()
        {
            var queryParameters = GetQueryParameters();
            _queryRunnerManager.RunWorker(queryParameters);
        }

        private QueryParameters GetQueryParameters()
        {
            return new QueryParameters
            {
                IsDateChecked = IsDateChecked,
                IsDirectorChecked = IsDirectorChecked,
                IsCountryChecked = IsCountryChecked,
                IsWriterChecked = IsWriterChecked,
                IsGenreChecked = IsGenreChecked,
                DirectorName = DirectorName,
                WriterName = WriterName,
            };
        }
    }
}