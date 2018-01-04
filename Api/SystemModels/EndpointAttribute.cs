using System;

namespace EntrepreneurEsiApi.Api.SystemModels
{
    public class EndpointAttribute:Attribute
    {
        private string name;
        public string Name { get => name; set => name = value; }
        public EndpointAttribute( string fieldName ) { this.Name = fieldName; }
    }
}
