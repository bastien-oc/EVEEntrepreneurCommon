using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntrepreneurCommon.Models {
    public class NameCache
    {
        public string Name { get; set; }

        [Key, Index("UNIQUE", IsUnique = true), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Category { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}