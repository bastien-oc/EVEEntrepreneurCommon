using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    [Table("structure_info")]
    public class DbStructureInfo
    {
        [Key]
        [Column("structure_id")]
        public Int64 StructureId { get; set; }

        [Column("name")] public string Name { get; set; }
        [Column("solar_system_id")] public Int32 SolarSystemId { get; set; }
        [Column("type_id")] public Int32 TypeId { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
