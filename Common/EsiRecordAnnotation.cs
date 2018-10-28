using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace EntrepreneurCommon.Common
{

    /// <summary>
    /// An attribute that marks the property that is meant to take a value of RestSharp.Parameter object of corresponding name.
    /// </summary>
    public class EsiRecordAnnotationAttribute : Attribute
    {
        public string Name;

        public EsiRecordAnnotationAttribute(string name)
        {
            this.Name = name;
        }
    }

    /// <summary>
    /// An interface used as a marker to let the code know that the class contains custom annotation fields. Use reflection to check for all EsiRecordAnnotationAttribute and assign appropriate values at runtime.
    /// </summary>
    public interface IEsiAnnotatedRecord { }

    public static class AnnotatedRecordExtension
    {
        /// <summary>
        /// Assigns matching Parameters to properties with matching EsiRecordAnnotationAttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="parameters"></param>
        public static void AssignAnnotationFields<T>(this T record, params RestSharp.Parameter[] parameters)
            where T : IEsiAnnotatedRecord
        {
            // 1. Iterate through all the members (more precisely, properties) of the record and "recognize" all the members that are attributed with EsiRecordAnnotationAttribute
            // 2. Iterate through all the parameters...
            // 3. ... and assign their values to the properties with matching names.

            var typeInfo = record.GetType();
            var propertyInfos = typeInfo.GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(EsiRecordAnnotationAttribute)));
            // Iterate through the annotated properties
            foreach (var prop in propertyInfos) {
                // Get the attribute
                var attr = (EsiRecordAnnotationAttribute) prop.GetCustomAttribute(typeof(EsiRecordAnnotationAttribute),
                    false);
                // Check if we have a parameter of matching name
                Parameter parameter = parameters.FirstOrDefault(par => par.Name == attr.Name);
                if (parameter != null) {
                    prop.SetValue(record, Convert.ChangeType(parameter.Value, prop.PropertyType), null);
                }
            }
        }
    }
}