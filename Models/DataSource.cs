using System.Runtime.Serialization;

namespace EntrepreneurCommon.Client
{
    public enum DataSource
    {
        [EnumMember(Value = "tranquility")] Tranquility,
        [EnumMember(Value = "singularity")] Singularity
    }
}
