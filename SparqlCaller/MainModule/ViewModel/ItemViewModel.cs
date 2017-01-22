using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SparqlCaller.Entities;

namespace MainModule.ViewModel
{
    public class ItemViewModel:INotifyPropertyChanged
    {
        #region fields

        private string _name;
        private string _genre;
        private string _writer;
        private string _date;
        private string _director;
        private string _imgUrl;
        private double _duration;
        private double _budget;

        #endregion

        #region constructors

        public ItemViewModel(Movie movie)
        {
            if(movie == null)
                throw new ArgumentNullException(nameof(movie));
            Name = movie.Title;
            Genre = movie.Genre;
            Writer = movie.Writer;
            Director = movie.Director;
            ImgUrl = string.IsNullOrEmpty(movie.ImgUrl)
                ? Defaults.DefaultImage
                : movie.ImgUrl;
            Date = movie.DateTime?.ToShortDateString() ?? string.Empty;
            Duration = movie.Duration;
            Budget = movie.Budget;
        }

        #endregion

        #region properties

        public string ImgUrl
        {
            get { return _imgUrl; }
            set
            {
                if (_imgUrl == value)
                    return;
                _imgUrl = value;
                OnPropertyChanged();
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                if (_date == value)
                    return;
                _date = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Genre
        {
            get { return _genre; }
            set
            {
                if (_genre == value)
                    return;
                _genre = value;
                OnPropertyChanged();
            }
        }

        public string Writer
        {
            get { return _writer; }
            set
            {
                if (_writer == value)
                    return;
                _writer = value;
                OnPropertyChanged();
            }
        }

        public string Director
        {
            get { return _director; }
            set
            {
                if (_director == value)
                    return;
                _director = value;
                OnPropertyChanged();
            }
        }

        public double Duration
        {
            get { return _duration; }
            set
            {
                if(_duration == value)
                    return;
                _duration = value;
                OnPropertyChanged();
            }
        }

        public double Budget
        {
            get { return _budget; }
            set
            {
                if(_budget == value)
                    return;
                _budget = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
