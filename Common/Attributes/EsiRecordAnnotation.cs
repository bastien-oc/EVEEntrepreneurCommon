using System;
using System.Linq;
using System.Reflection;
using EntrepreneurCommon.ExtensionMethods;
using RestSharp;

namespace EntrepreneurCommon.Common.Attributes
{
    /// <summary>
    ///     An attribute that marks the property that is meant to take a value of RestSharp.Parameter object of corresponding
    ///     name.
    /// </summary>
    public class RestParameterMappingAttribute : Attribute
    {
        public string ParameterName;

        public RestParameterMappingAttribute() { }

        public RestParameterMappingAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }
    }

    /// <summary>
    ///     An interface used as a marker to let the code know that the class contains custom annotation fields. Use reflection
    ///     to check for all EsiRecordAnnotationAttribute and assign appropriate values at runtime.
    /// </summary>
    public interface IEsiAnnotatedRecord { }

    public static class AnnotatedRecordExtension
    {
        /// <summary>
        ///     Assigns matching Parameters to properties with matching EsiRecordAnnotationAttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="parameters"></param>
        public static void AssignAnnotationFields<T>(this T record, params Parameter[] parameters)
            where T : IEsiAnnotatedRecord
        {
            // 1. Iterate through all the members (more precisely, properties) of the record and "recognize" all the members that are attributed with EsiRecordAnnotationAttribute
            // 2. Iterate through all the parameters...
            // 3. ... and assign their values to the properties with matching names.

            // If Name value of EsiRecordAnnotationAttribute is empty, use snake_case name of the property it attributes.

            var typeInfo = record.GetType();
            var propertyInfos = typeInfo.GetProperties()
                                        .Where(p => Attribute.IsDefined(p, typeof(RestParameterMappingAttribute)));
            // Iterate through the annotated properties
            foreach (var prop in propertyInfos) {
                // Get the attribute
                var attr =
                    (RestParameterMappingAttribute) prop.GetCustomAttribute(typeof(RestParameterMappingAttribute),
                                                                            false);

                // Check if we have a parameter of matching name
                // If EsiRecordAnnotation.Name is Null of Empty, use case_snake name value of the property it attributes.
                var annotationName = !string.IsNullOrEmpty(attr.ParameterName)
                                         ? attr.ParameterName
                                         : prop.Name.ToSnakeCase();

                var parameter = parameters.FirstOrDefault(par => par.Name == annotationName);
                if (parameter != null) {
                    prop.SetValue(record, Convert.ChangeType(parameter.Value, prop.PropertyType), null);
                }
            }
        }

        /// <summary>
        ///     Assigns matching Parameters to properties with matching EsiRecordAnnotationAttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="request"></param>
        public static void AssignAnnotationFields<T>(this T record, IRestRequest request) where T : IEsiAnnotatedRecord
        {
            AssignAnnotationFields(record, request.Parameters.ToArray());
        }
    }
}
