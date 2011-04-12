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
using RestSharp;
using ECollegeAPI.Model;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using ECollegeAPI.Util;
using ECollegeAPI.Model.Boilerplate;
using ECollegeAPI.Services;
using ECollegeAPI.Exceptions;

namespace ECollegeAPI
{
    public partial class ECollegeClient
    {
        private class PendingExecuteRequest
        {
            public RestRequest Request { get; set; }
            public Action<RestResponse> Callback { get; set; }
        }

        public const string RootUri = "https://m-api.ecollege.com/";
        //public const string RootUri = "http://ecollegeapiproxy.heroku.com/";
        //public const string RootUri = "http://192.168.1.2:4000/";

        readonly string _clientString;
        readonly string _clientId;

        private bool _loginInProgress = false;
        private readonly Queue<Action> _pendingServiceCalls = new Queue<Action>();

        private string _username;
        private string _password;
        private string _grantToken;

        private Token _currentToken;
        private ECollegeClientAuthenticator _authenticator;

        public string GrantToken { get { return _grantToken; } }

        public Action<ServiceException> UnhandledExceptionHandler { get; set; }


        public ECollegeClient(string clientString, string clientId)
        {
            this._clientString = clientString;
            this._clientId = clientId;
        }

        public void SetupAuthentication(string username, string password)
        {
            this._username = username;
            this._password = password;
        }

        public void SetupAuthentication(string grantToken)
        {
            this._grantToken = grantToken;
        }

        protected string PrettyPrint(string json)
        {
            try
            {
                object jsonObject = JsonConvert.DeserializeObject(json);
                return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            } catch(Exception e)
            {
                return "Error while parsing string to json: " + e.ToString();
            }
        }

        protected string PrettyPrint(object o)
        {
            try
            {
                return JsonConvert.SerializeObject(o, Formatting.Indented);
            }
            catch (Exception e)
            {
                return "Error while parsing object to json: " + e.ToString();
            }
        }

        protected void DoBackgroundWork(Action work)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (s, e) => work();
            worker.RunWorkerAsync();
        }


        public void ExecuteService<T>(T service, Action<T> successCallback) where T : BaseService
        {
            ExecuteService<T>(service, successCallback, null, null);
        }

        public void ExecuteService<T>(T service, Action<T> successCallback, Action<ServiceException> failureCallback) where T : BaseService
        {
            ExecuteService<T>(service, successCallback, failureCallback, null);
        }

        public void ExecuteService<T>(T service, Action<T> successCallback, Action<ServiceException> failureCallback, Action<T> finallyCallback) where T : BaseService
        {
            ExecuteService<T>(service, successCallback, failureCallback, finallyCallback,null,false,false);
        }

        public void ExecuteService<T>(T service, Action<T> successCallback, Action<ServiceException> failureCallback, Action<T> finallyCallback, ECollegeResponseCache cache, bool readFromCache, bool writeToCache) where T : BaseService
        {
            DoBackgroundWork(() =>
            {
                try
                {
                    Action<string> cacheCallback = null;

                    if (cache != null && service.IsCacheable)
                    {
                        var cacheKey = service.GetCacheKey();
                        if (readFromCache) {
                            var cacheEntry = cache.Get(service.CacheScope,cacheKey);
                            if (cacheEntry != null)
                            {
                                HandleResponseContent(cacheEntry.Data, service, failureCallback, finallyCallback, successCallback, null);
                                return;
                            }
                        }
                        if (writeToCache)
                        {
                            cacheCallback = (responseContent) => cache.Put(service.CacheScope, cacheKey, responseContent);
                        }
                    }

                    var client = new RestClient(RootUri);
                    client.FollowRedirects = true;
                    client.MaxRedirects = 10;
                    client.EnsureCallbacksOnUI = false;

                    if (AuthenticateIfRequired(service, failureCallback, finallyCallback, successCallback)) return;

                    if (_authenticator != null) client.Authenticator = _authenticator;

                    var request = new RestRequest(service.Resource,service.RequestMethod);
                    service.PrepareRequest(request);
            
#if DEBUG
                    Debug.WriteLine("Request: " + request.Method + " - " + RootUri + request.Resource + " - " + PrettyPrint(request.Parameters));
#endif
                    client.ExecuteAsync(request, (response) =>
                    {
                        HandleResponse(response, service, failureCallback, finallyCallback, successCallback, cacheCallback);
                    });
                
                } catch (Exception e)
                {
                    HandleFailure(service, new ClientErrorException(service,e), failureCallback, finallyCallback);
                }
            });

        }

        protected void HandleResponse<T>(RestResponse response, T service, Action<ServiceException> failureCallback, Action<T> finallyCallback, Action<T> successCallback, Action<string> cacheCallback) where T : BaseService
        {
#if DEBUG
            Debug.WriteLine("Status: " + response.StatusCode);

            if (response.ContentType != null && response.ContentType.Contains("json")) // == "application/json")
            {
                var prettyResponse = PrettyPrint(response.Content);
                Debug.WriteLine("Response: " + prettyResponse + "\n");
            }
            else
            {
                Debug.WriteLine("Response (" + response.ContentType + "): " + response.Content + "\n");
            }
#endif
            if (response.ResponseStatus == ResponseStatus.Error)
            {
                HandleFailure(service,new ClientErrorException(service,response.ErrorMessage, response.ErrorException),failureCallback,finallyCallback);
            }
            else if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != HttpStatusCode.NotModified)
            {
                HandleFailure(service, new ServerErrorException(response, "Server Error: " + response.StatusCode + " - " + response.StatusDescription), failureCallback, finallyCallback);
            }
            else
            {
                HandleResponseContent(response.Content, service, failureCallback, finallyCallback, successCallback, cacheCallback);
            }

            Debug.WriteLine("\n\n");
        }

        protected void HandleResponseContent<T>(string responseContent, T service, Action<ServiceException> failureCallback, Action<T> finallyCallback, Action<T> successCallback, Action<string> cacheCallback) where T : BaseService
        {
            try
            {
                service.ProcessResponse(responseContent);
                HandleSuccess(service, successCallback, finallyCallback);
                if (cacheCallback != null) cacheCallback(responseContent);
            } catch(Exception e)
            {
                HandleFailure(service,new DeserializationException(e),failureCallback,finallyCallback);
            }
        }


        protected void HandleSuccess<T>(T service, Action<T> successCallback, Action<T> finallyCallback) where T : BaseService
        {
            var dispatcher = Deployment.Current.Dispatcher;
            dispatcher.BeginInvoke(() =>
            {
                successCallback(service);
                if (finallyCallback != null)
                {
                    finallyCallback(service);
                }
            });
        }

        protected void HandleFailure<T>(T service, ServiceException ex, Action<ServiceException> failureCallback, Action<T> finallyCallback) where T : BaseService
        {
            Debug.WriteLine("ERROR! " + ex);
            var dispatcher = Deployment.Current.Dispatcher;
            dispatcher.BeginInvoke(() =>
            {
                ex.IsHandled = false; //if reused exception, make sure ishandled starts out as false
                if (failureCallback != null)
                {
                    failureCallback(ex);
                }
                if (!ex.IsHandled)
                {
                    if (UnhandledExceptionHandler != null)
                    {
                        UnhandledExceptionHandler(ex);
                    }
                }
                if (finallyCallback != null)
                {
                    finallyCallback(service);
                }
            });
        }

        protected bool AuthenticateIfRequired<T>(T service, Action<ServiceException> failureCallback, Action<T> finallyCallback, Action<T> successCallback) where T : BaseService
        {
            if (service.IsAuthenticationRequired && (_currentToken == null || _currentToken.NeedsToBeRefreshed()))
            {
                if (!_loginInProgress)
                {
                    _loginInProgress = true;
                    PrepareAuthentication((ex) =>
                    {
                        //Authentication failure
                        ex.IsHandled = true; //don't propagate original exception up

                        var sce = ex as ServerErrorException;
                        var code = sce.Response.StatusCode;

                        if (code == HttpStatusCode.BadRequest && sce.Response.Content.Contains("incorrect_client_credentials"))
                        {
                            HandleFailure(service,new AuthenticationException(sce.Response),failureCallback,finallyCallback);
                        } else
                        {
                            HandleFailure(service,ex,failureCallback,finallyCallback);
                        }
                    });
                }
                _pendingServiceCalls.Enqueue(
                    () => ExecuteService<T>(service, successCallback, failureCallback, finallyCallback));
                return true;
            }
            return false;
        }



        protected void PrepareAuthentication(Action<ServiceException> authFailed)
        {
            Action<ServiceException> failureHandler = (ex) =>
            {
                Debug.WriteLine("Auth Failed");
                _pendingServiceCalls.Clear();
                _loginInProgress = false;
                authFailed(ex);
            };

            if (_grantToken == null)
            {
                ExecuteService(new FetchGrantService(_clientString, _clientId, _username, _password), fgs =>
                {
                    _grantToken = fgs.Result.AccessToken;
                    ExecuteService(new FetchTokenService(_grantToken), fts =>
                    {
                        _currentToken = fts.Result;
                        _authenticator = new ECollegeClientAuthenticator(_currentToken.AccessToken);
                        ResumeServices();
                    }, failureHandler);
                }, failureHandler);
            }
            else
            {
                ExecuteService(new FetchTokenService(_grantToken), fts =>
                {
                    _currentToken = fts.Result;
                    _authenticator = new ECollegeClientAuthenticator(_currentToken.AccessToken);
                    ResumeServices();
                }, failureHandler);
            }
        }

        protected void ResumeServices()
        {
            _loginInProgress = false;
            while (_pendingServiceCalls.Count > 0)
            {
                var pendingService = _pendingServiceCalls.Dequeue();
                pendingService();
            }
        }

        private class ECollegeClientAuthenticator : IAuthenticator
        {
            private readonly string _token;

            public ECollegeClientAuthenticator(string token)
            {
                _token = token;
            }

            public void Authenticate(RestClient client, RestRequest request)
            {
                request.AddHeader("X-Authorization", "Access_Token access_token=" + _token);
            }

        }

    }
}
