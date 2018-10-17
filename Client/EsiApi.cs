using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using EntrepreneurCommon.Api.SystemModels;
using EntrepreneurCommon.Models.Esi;
using Newtonsoft.Json;
using RestSharp;

namespace EntrepreneurCommon.Api
{
    public partial class EsiApiClient
    {
        public RestClient RestClient { get; set; }
        //public EsiAuthClient AuthClient { get; }

        public EsiApiClient()
        {
            RestClient = new RestClient("https://esi.tech.ccp.is");
        }

        public EsiApiClient( string basePath ) //, string clientId, string secretKey, string callbackUrl )
        {
            RestClient = new RestClient(basePath);
        }

        /// <summary>
        /// Layer between RestClient.Execute() and EsiAppClient.Execute().
        /// Used for processing exceptions as part of a shared codeand propagating them.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        private void PropagateErrors( IRestRequest request, IRestResponse response )
        {
            if (!response.IsSuccessful) {
                switch (response.StatusCode) {
                    case (HttpStatusCode.InternalServerError):
                        var _internal = EsiErrors.GetInternalServerError(response.Content);
                        throw new EsiException((int)response.StatusCode, $"{_internal.Error}");
                    case (HttpStatusCode.Forbidden):
                        var _forbidden = EsiErrors.GetForbidden(response.Content);
                        throw new EsiException((int)response.StatusCode, $"{_forbidden.SsoStatus}: {_forbidden.Error}");
                    default:
                        throw new EsiException((int)response.StatusCode, $"Unsuccessful status code for request {request.Resource}: {response.StatusCode} - {response.ErrorMessage} - {response.Content}\n{JsonConvert.SerializeObject(request)}");
                }
            }
        }

        /// <summary>
        /// A passtrhough for RestClient.Execute, with error propagation;
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IRestResponse Execute( IRestRequest request )
        {
            var response = RestClient.Execute(request);
            PropagateErrors(request, response);
            return response;
        }
        /// <summary>
        /// A typed passthrough for RestClient.Execute, with error propagation;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public IRestResponse<T> Execute<T>( IRestRequest request ) where T : new()
        {
            var response = RestClient.Execute<T>(request);
            PropagateErrors(request, response);
            return response;
        }
        /// <summary>
        /// Return a paginated result for objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public EsiPaginatedResponse<T> ExecutePaginated<T>( IRestRequest request ) where T : new()
        {
            List<T> itemCollection = new List<T>();
            List<IRestResponse<List<T>>> responseCollection = new List<IRestResponse<List<T>>>();
            var response = Execute<List<T>>(request);

            responseCollection.Add(response);
            itemCollection.AddRange(response.Data);

            if (response.Headers.Any(x => x.Name == "X-Pages")) {
                int pages = Convert.ToInt32(
                    (string)response.Headers.First(x => x.Name == "X-Pages").Value);
                if (pages > 1) {
                    for (int i = 2; i <= pages; i++) {
                        var rq = new RestRequest(request.Resource, request.Method);
                        foreach (var r in request.Parameters) { rq.AddParameter(r); }
                        rq.AddParameter("page", i);
                        var subresponse = Execute<List<T>>(rq);

                        responseCollection.Add(subresponse);
                        itemCollection.AddRange(subresponse.Data);
                    }
                }
            }

            return new EsiPaginatedResponse<T>()
            {
                Items = itemCollection,
                Responses = responseCollection
            };
        }
       
        /// <summary>
        /// Untyped Execute method
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <param name="token"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public IRestResponse Execute( string resource, Method method = Method.GET, Parameter[] parameters = null, string token = null, object body = null )
        {
            var request = new RestRequest(resource, method);
            if (parameters != null)
                foreach (var param in parameters) { request.AddParameter(param); }
            if (token != null) { request.AddHeader("Authorization", $"Bearer {token}"); }
            if (body != null) { request.AddJsonBody(body); }

            var response = RestClient.Execute(request);
            PropagateErrors(request, response);
            return response;
        }
        /// <summary>
        /// Typed Execute method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource"></param>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <param name="token"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public IRestResponse<T> Execute<T>( string resource, Method method = Method.GET, Parameter[] parameters = null, string token = null, object body = null ) where T : new()
        {
            var request = new RestRequest(resource, method);
            if (parameters != null)
                foreach (var param in parameters) { request.AddParameter(param); }
            if (token != null) { request.AddHeader("Authorization", $"Bearer {token}"); }
            if (body != null) { request.AddJsonBody(body); }

            var response = RestClient.Execute<T>(request);
            PropagateErrors(request, response);
            return response;
        }
        /// <summary>
        /// Paginated Execute method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource"></param>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <param name="token"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public EsiPaginatedResponse<T> ExecutePaginated<T>( string resource, Method method = Method.GET, Parameter[] parameters = null, string token = null, object body = null ) where T : new()
        {
            List<Parameter> pars = new List<Parameter>();
            if (parameters != null) { pars.AddRange(parameters); }

            var response = Execute<List<T>>(resource, method, parameters, token, body);

            List<T> itemCollection = new List<T>();
            List<IRestResponse<List<T>>> responseCollection = new List<IRestResponse<List<T>>>();

            itemCollection.AddRange(JsonConvert.DeserializeObject<List<T>>(response.Content));
            responseCollection.Add(response);

            string _pages;
            int _xpages = 1;

            if (response.Headers.Any(x => x.Name == "X-Pages")) {
                int pages = Convert.ToInt32(
                    (string)response.Headers.First(x => x.Name == "X-Pages").Value);
                if (pages > 1) {
                    for (int i = 2; i <= pages; i++) {
                        var rq = new RestRequest(resource, method);
                        foreach (var p in parameters) { rq.AddParameter(p); }
                        if (token != null) { rq.AddParameter("token", token, ParameterType.QueryString); }
                        var subresponse = Execute<List<T>>(rq);

                        itemCollection.AddRange(subresponse.Data);
                        responseCollection.Add(subresponse);
                    }
                }
            }

            //try {
            //    bool isPaginated = response.Headers.Any(x => x.Name == "X-Pages");
            //    // If the record IS paginated...
            //    if (isPaginated) {
            //        _pages = (string)response.Headers.FirstOrDefault(x => x.Name == "X-Pages").Value;
            //        _xpages = Convert.ToInt32(_pages);
            //        if (_xpages > 1) {
            //            for (int i = 2; i <= _xpages; i++) {
            //                List<Parameter> _pars = new List<Parameter>();
            //                if (parameters != null) { _pars.AddRange(parameters); }
            //                _pars.Add(new Parameter { Name = "page", Value = i });

            //                var subResponse = Execute<List<T>>(resource, method, parameters, token, body);
            //                objects.AddRange(JsonConvert.DeserializeObject<List<T>>(subResponse.Content));
            //                responses.Add(subResponse);
            //            }
            //        }
            //    }
            //}
            //catch (Exception e) { throw new Exception(e.Message); }

            return new EsiPaginatedResponse<T>() { Items = itemCollection, Responses = responseCollection };
        }
        
        /// <summary>
        /// Helper method, used to convert IRestResponse's raw data into an object by using Json Deserialization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public T ParseResponse<T>( IRestResponse response )
        {
            T data = JsonConvert.DeserializeObject<T>(response.Content);
            return data;
        }

    }

    public partial class EsiApiClient
    {





    }
}
