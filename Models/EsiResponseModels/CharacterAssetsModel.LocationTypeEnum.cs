using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EntrepreneurCommon.Models.EsiResponseModels
{
    public partial class CharacterAssetsModel
    {
        /// <summary>
        /// location_type string
        /// </summary>
        /// <value>location_type string</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum LocationTypeEnum
        {

            /// <summary>
            /// Enum Station for value: station
            /// </summary>
            [EnumMember(Value = "station")]
            Station = 1,

            /// <summary>
            /// Enum Solarsystem for value: solar_system
            /// </summary>
            [EnumMember(Value = "solar_system")]
            Solarsystem = 2,

            /// <summary>
            /// Enum Other for value: other
            /// </summary>
            [EnumMember(Value = "other")]
            Other = 3
        }
    }
}