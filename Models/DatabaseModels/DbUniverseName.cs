using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Models.Esi;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    [Table("universe_names")]
    public class DbUniverseName
    {
        public DbUniverseName()
        {
        }
        public DbUniverseName( UniverseNameResponse fromResponse )
        {
            this.Id = fromResponse.ID;
            this.Name = fromResponse.Name;
            this.Category = fromResponse.Name;
        }

        [Column("id")] [Key] public Int32 Id { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("category")] public string Category { get; set; }
    }
}
