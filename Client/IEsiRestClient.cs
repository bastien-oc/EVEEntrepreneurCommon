using System;
using System.Threading.Tasks;
using EntrepreneurCommon.Models.EsiResponseModels;
using RestSharp;

namespace EntrepreneurCommon.Client
{
    public interface IEsiRestClient
    {
        IRestResponse Execute(IRestRequest request);
        IRestResponse<T> Execute<T>(IRestRequest request) where T : new();
        Task<IRestResponse> ExecuteTaskAsync(IRestRequest request);
        Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request);
        
        EsiPaginatedResponse<T> ExecutePaginated<T>(IRestRequest request) where T : new();
        T ParseResponse<T>(IRestResponse response);

        Uri BuildUri(IRestRequest request);
        // EsiPaginatedResponse<T> ExecutePaginated<T>(string resource, Method method = Method.GET, Parameter[] parameters = null, string token = null, object body = null) where T : new();
    }
}