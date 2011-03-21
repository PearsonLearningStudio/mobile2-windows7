using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ECollegeAPI.Services;
using ECollegeAPI;
using RestSharp;

namespace eCollegeWP7.Util
{
    public class ServiceCallTask<T> where T : BaseService
    {

        private bool _progressIndicatorEnabled = true;
        public bool ProgressIndicatorEnabled { get { return _progressIndicatorEnabled; } }

        private bool _isModal = false;
        public bool IsModal {get { return _isModal; }}

        private readonly ECollegeClient _client;
        private readonly T _service;
        private Action<T> _successHandler;
        private Action<T, RestResponse> _failureHandler;
        private Action<T> _finallyHandler;

        public ServiceCallTask(ECollegeClient client, T service)
        {
            this._client = client;
            this._service = service;
        }

        public ServiceCallTask<T> DisableProgressIndicator()
        {
            _progressIndicatorEnabled = false;
            return this;
        }
        
        public ServiceCallTask<T> MakeModal()
        {
            _isModal = true;
            return this;
        }

        public ServiceCallTask<T> AddSuccessHandler(Action<T> successHandler)
        {
            this._successHandler = successHandler;
            return this;
        }

        public ServiceCallTask<T> AddFailureHandler(Action<T,RestResponse> failureHandler)
        {
            this._failureHandler = failureHandler;
            return this;
        }

        public ServiceCallTask<T> AddFinallyHandler(Action<T> finallyHandler)
        {
            this._finallyHandler = finallyHandler;
            return this;
        }

        public ServiceCallTask<T> Execute()
        {
            return Execute(_successHandler);
        }

        public ServiceCallTask<T> Execute(Action<T> successHandler)
        {
            if (_progressIndicatorEnabled)
            {
                App.Model.PendingServiceCalls++;
            }


            var worker = new BackgroundWorker();
            worker.DoWork += (s, e) =>
                                 {
                                     _client.ExecuteService(_service, successHandler, _failureHandler, (service) =>
                                     {
                                         if (_progressIndicatorEnabled)
                                         {
                                             App.Model.PendingServiceCalls--;
                                         }
                                         if (_finallyHandler != null)
                                             _finallyHandler(service);
                                     });
                                 };
            worker.RunWorkerAsync();
            return this;
        }
    }
}
