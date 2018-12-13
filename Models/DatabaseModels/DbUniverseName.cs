using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Models.EsiResponseModels;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    [Table("universe_names")]
    public class DbUniverseName
    {
        public DbUniverseName() { }

        public DbUniverseName(UniverseNameResponse fromResponse)
        {
            Id = fromResponse.ID;
            Name = fromResponse.Name;
            Category = fromResponse.Name;
        }

        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("name")]     public string Name     { get; set; }
        [Column("category")] public string Category { get; set; }
    }
}
