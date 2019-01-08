using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EntrepreneurCommon.Api.SystemModels;
using EntrepreneurCommon.Common;
using EntrepreneurCommon.Common.Attributes;
using EntrepreneurCommon.ExtensionMethods;
using EntrepreneurCommon.Logging;
using EntrepreneurCommon.Models.EsiResponseModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Extensions;
using JsonSerializer = EntrepreneurCommon.Common.JsonSerializer;

namespace EntrepreneurCommon.Client
{
    public class EsiRestClient : RestClient, IEsiRestClient
    {
        private readonly EsiConfiguration config;
        private readonly JsonSerializer serializer = new JsonSerializer();
        private readonly ILogger logger;

        public EsiRestClient(EsiConfiguration configuration,
                             ILogger logger = null)
        {
            config = configuration;
            this.logger = logger;
            
            BaseUrl = new Uri(config.BaseUrl);

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings() {
                ContractResolver = new DefaultContractResolver() {NamingStrategy = new SnakeCaseNamingStrategy()}
            };
            
            AddHandler("application/json", serializer);
            AddHandler("text/json",        serializer);
            AddHandler("text/x-json",      serializer);
            AddHandler("text/javascript",  serializer);
            AddHandler("*+json",           serializer);
        }

        public override IRestResponse Execute(IRestRequest request)
        {
            request.SetDatasource(config.DataSource);
            var response = base.Execute(request);
            ErrorsCheck(request, response);
            return response;
        }

        public override IRestResponse<T> Execute<T>(IRestRequest request)
        {
            request.SetDatasource(config.DataSource);
            var responseRaw = base.Execute(request);
            var response    = responseRaw.ToAsyncResponse<T>();
            response.Request = request;
            ErrorsCheck(request, response);
            response.Data = JsonConvert.DeserializeObject<T>(response.Content);

            // If response data is of IEsiResponseModel, map request parameters to response data
            var esiData = response.Data as IEsiResponseModel;
            esiData?.AssignAnnotationFields(request);

            return response;
        }

        public override async Task<IRestResponse> ExecuteTaskAsync(IRestRequest request)
        {
            request.SetDatasource(config.DataSource);
            var response = await base.ExecuteTaskAsync(request);
            ErrorsCheck(request, response);
            return response;
        }

        public override async Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            request.SetDatasource(config.DataSource);
            var responseRaw = await base.ExecuteTaskAsync(request);
            var response    = responseRaw.ToAsyncResponse<T>();

            // If response data is of IEsiResponseModel, map request parameters to response data
            response.Request = request;
            ErrorsCheck(request, response);
            response.Data = JsonConvert.DeserializeObject<T>(response.Content);

            var esiData = response.Data as IEsiResponseModel;
            esiData?.AssignAnnotationFields(request);

            return response;
        }

        /// <summary>
        ///     Check for timeout errors.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        private static void ErrorsCheck(IRestRequest request, IRestResponse response)
        {
            if (response.StatusCode == 0) {
                throw new TimeoutException("The request timed out!");
            }

            if (response.IsSuccessful == false) {
                switch (response.StatusCode) {
                    case HttpStatusCode.BadRequest:
                        throw new BadRequestException(response.ErrorMessage);
                    default:
                        throw new EsiException((int) response.StatusCode, response);
                }
            }
        }

        /// <summary>
        ///     Layer between base.Execute() and EsiAppClient.Execute().
        ///     Used for processing exceptions as part of a shared codeand propagating them.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        private static void PropagateErrors(IRestRequest request, IRestResponse response)
        {
            if (!response.IsSuccessful) {
                switch (response.StatusCode) {
                    case HttpStatusCode.InternalServerError:
                        var _internal = EsiErrorsHelper.GetInternalServerError(response.Content);
                        throw new EsiException((int) response.StatusCode, response);
                    case HttpStatusCode.Forbidden:
                        var _forbidden = EsiErrorsHelper.GetForbidden(response.Content);
                        throw new EsiException((int) response.StatusCode, response);
                    default:
                        throw new EsiException((int) response.StatusCode, response);
                }
            }
        }

        /// <summary>
        ///     Return a paginated result for objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public EsiPaginatedResponse<T> ExecutePaginated<T>(IRestRequest request) where T : new()
        {
            var itemCollection     = new List<T>();
            var responseCollection = new List<IRestResponse<List<T>>>();
            var response           = Execute<List<T>>(request);

            responseCollection.Add(response);
            itemCollection.AddRange(response.Data);

            if (response.Headers.Any(x => x.Name == "X-Pages")) {
                var pages = Convert.ToInt32(
                                            (string) response.Headers.First(x => x.Name == "X-Pages").Value);
                if (pages > 1) {
                    for (var i = 2; i <= pages; i++) {
                        var rq = new RestRequest(request.Resource, request.Method);
                        foreach (var r in request.Parameters) {
                            rq.AddParameter(r);
                        }

                        rq.AddParameter("page", i);
                        var subresponse = Execute<List<T>>(rq);

                        responseCollection.Add(subresponse);
                        itemCollection.AddRange(subresponse.Data);
                    }
                }
            }

            // If type is an IEsiResponseModel, assign request parameters to applicable properties in entry.
            foreach (var entry in itemCollection) {
                var model = entry as IEsiResponseModel;
                model?.AssignAnnotationFields(request);
            }

            return new EsiPaginatedResponse<T> {
                Items = itemCollection,
                Responses = responseCollection
            };
        }

        /// <summary>
        ///     Helper method, used to convert IRestResponse's raw data into an object by using Json Deserialization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public T ParseResponse<T>(IRestResponse response)
        {
            var data = JsonConvert.DeserializeObject<T>(response.Content);
            return data;
        }

        ///// <summary>
        ///// Paginated Execute method
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="resource"></param>
        ///// <param name="method"></param>
        ///// <param name="parameters"></param>
        ///// <param name="token"></param>
        ///// <param name="body"></param>
        ///// <returns></returns>
        //public EsiPaginatedResponse<T> ExecutePaginated<T>(string resource, Method method = Method.GET,
        //    Parameter[] parameters = null, string token = null, object body = null) where T : new()
        //{
        //    List<Parameter> pars = new List<Parameter>();
        //    if ( parameters != null )
        //    {
        //        pars.AddRange(parameters);
        //    }
        //    var response = Execute<List<T>>(resource, method, parameters, token, body);
        //    List<T> itemCollection = new List<T>();
        //    List<IRestResponse<List<T>>> responseCollection = new List<IRestResponse<List<T>>>();
        //    itemCollection.AddRange(JsonConvert.DeserializeObject<List<T>>(response.Content));
        //    responseCollection.Add(response);
        //    // string _pages;
        //    // int _xpages = 1;
        //    if ( response.Headers.Any(x => x.Name == "X-Pages") )
        //    {
        //        int pages = Convert.ToInt32(
        //            (string) response.Headers.First(x => x.Name == "X-Pages").Value);
        //        if ( pages > 1 )
        //        {
        //            for ( int i = 2 ; i <= pages ; i++ )
        //            {
        //                var rq = new RestRequest(resource, method);
        //                foreach ( var p in parameters )
        //                {
        //                    rq.AddParameter(p);
        //                }
        //                if ( token != null )
        //                {
        //                    rq.AddParameter("token", token, ParameterType.QueryString);
        //                }
        //                var subresponse = Execute<List<T>>(rq);
        //                itemCollection.AddRange(subresponse.Data);
        //                responseCollection.Add(subresponse);
        //            }
        //        }
        //    }
        //    //try {
        //    //    bool isPaginated = response.Headers.Any(x => x.Name == "X-Pages");
        //    //    // If the record IS paginated...
        //    //    if (isPaginated) {
        //    //        _pages = (string)response.Headers.FirstOrDefault(x => x.Name == "X-Pages").Value;
        //    //        _xpages = Convert.ToInt32(_pages);
        //    //        if (_xpages > 1) {
        //    //            for (int i = 2; i <= _xpages; i++) {
        //    //                List<Parameter> _pars = new List<Parameter>();
        //    //                if (parameters != null) { _pars.AddRange(parameters); }
        //    //                _pars.Add(new Parameter { Name = "page", Value = i });
        //    //                var subResponse = Execute<List<T>>(resource, method, parameters, token, body);
        //    //                objects.AddRange(JsonConvert.DeserializeObject<List<T>>(subResponse.Content));
        //    //                responses.Add(subResponse);
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception e) { throw new Exception(e.Message); }
        //    return new EsiPaginatedResponse<T>() { Items = itemCollection, Responses = responseCollection };
        //}
        
        
        ///// <summary>
        ///// Untyped Execute method
        ///// </summary>
        ///// <param name="resource"></param>
        ///// <param name="method"></param>
        ///// <param name="parameters"></param>
        ///// <param name="token"></param>
        ///// <param name="body"></param>
        ///// <returns></returns>
        //public IRestResponse Execute(string resource, Method method = Method.GET, Parameter[] parameters = null,
        //    string token = null, object body = null)
        //{
        //    var request = new RestRequest(resource, method);
        //    if ( parameters != null )
        //        foreach ( var param in parameters )
        //        {
        //            request.AddParameter(param);
        //        }
        //    if ( token != null )
        //    {
        //        request.AddHeader("Authorization", $"Bearer {token}");
        //    }
        //    if ( body != null )
        //    {
        //        request.AddJsonBody(body);
        //    }
        //    var response = base.Execute(request);
        //    PropagateErrors(request, response);
        //    return response;
        //}
        ///// <summary>
        ///// Typed Execute method
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="resource"></param>
        ///// <param name="method"></param>
        ///// <param name="parameters"></param>
        ///// <param name="token"></param>
        ///// <param name="body"></param>
        ///// <returns></returns>
        //public IRestResponse<T> Execute<T>(string resource, Method method = Method.GET, Parameter[] parameters = null,
        //    string token = null, object body = null) where T : new()
        //{
        //    var request = new RestRequest(resource, method);
        //    if ( parameters != null )
        //        foreach ( var param in parameters )
        //        {
        //            request.AddParameter(param);
        //        }
        //    if ( token != null )
        //    {
        //        request.AddHeader("Authorization", $"Bearer {token}");
        //    }
        //    if ( body != null )
        //    {
        //        request.AddJsonBody(body);
        //    }
        //    var response = base.Execute<T>(request);
        //    PropagateErrors(request, response);
        //    return response;
        //}
    }
}
