using System;
using System.ComponentModel;
using MainModule.Models;

namespace MainModule.Managers
{
    public class QueryRunnerManager: IDisposable
    {
        private BackgroundWorker _backgroundWorker;
        private QueryImplemantationManager _queryImplemantationManager;

        public QueryRunnerManager()
        {
            _backgroundWorker = new BackgroundWorker();
            _queryImplemantationManager = new QueryImplemantationManager();

            SubscribeToBackgroundWorkerEvents();
        }

        public void RunWorker(QueryParameters queryParameters)
        {
            if(_backgroundWorker.IsBusy)
                return;

            _backgroundWorker.RunWorkerAsync(queryParameters);
        }
        
        public void Dispose()
        {
            UnSubscribeFromBackgroundWorkerEvents();
        }

        private void UnSubscribeFromBackgroundWorkerEvents()
        {
            _backgroundWorker.DoWork -= BackgroundWork;
            _backgroundWorker.RunWorkerCompleted -= BackgroundRunWorkerCompleted;
        }

        private void SubscribeToBackgroundWorkerEvents()
        {
            _backgroundWorker.DoWork += BackgroundWork;
            _backgroundWorker.RunWorkerCompleted += BackgroundRunWorkerCompleted;
        }

        private void BackgroundWork(object sender, DoWorkEventArgs e)
        {
            var queryParameters = e.Argument as QueryParameters;

            _queryImplemantationManager.ImplementQuery(queryParameters);
        }

        private void BackgroundRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}