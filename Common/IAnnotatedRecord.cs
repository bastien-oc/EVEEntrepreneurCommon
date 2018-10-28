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
    /// An interface used as a marker to let the code know that the class contains custom annotation fields. Use reflection to check for all EsiRecordAnnotationAttribute and assign appropriate values at runtime.
    /// </summary>
    public interface IAnnotatedRecord { }

    [EsiEndpoint("/v4/characters/{character_id}/wallet/journal/", true)]
    public abstract class MockClass : IAnnotatedRecord
    {
        [Column("character_id", Order = 0), Key, Index("UNIQUE", IsUnique = true, Order = 0)]
        [EsiRecordAnnotation("character_id")]
        public int CharacterId { get; set; }
    }

    public static class AnnotatedRecordExtension
    {
        public static void AssignFields<T>(this T record, params RestSharp.Parameter[] parameters) where T : IAnnotatedRecord
        {
            // 1. Iterate through all the members (more precisely, properties) of the record and "recognize" all the members that are attributed with EsiRecordAnnotationAttribute
            // 2. Iterate through all the parameters...
            // 3. ... and assign their values to the properties with matching names.

            var typeInfo = record.GetType();
            var propertyInfos = typeInfo.GetProperties().Where( p => Attribute.IsDefined(p, typeof(EsiRecordAnnotationAttribute)));
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
