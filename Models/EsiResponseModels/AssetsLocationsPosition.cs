using System.Runtime.Serialization;

namespace EntrepreneurCommon.Models.EsiResponseModels {
    [DataContract]
    public class AssetsLocationsPosition {
        /// <summary>
        ///     x number
        /// </summary>
        /// <value>x number</value>
        [DataMember(Name = "x", EmitDefaultValue = false)]
        public double? X { get; set; }

        /// <summary>
        ///     y number
        /// </summary>
        /// <value>y number</value>
        [DataMember(Name = "y", EmitDefaultValue = false)]
        public double? Y { get; set; }

        /// <summary>
        ///     z number
        /// </summary>
        /// <value>z number</value>
        [DataMember(Name = "z", EmitDefaultValue = false)]
        public double? Z { get; set; }
    }
}
