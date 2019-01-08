using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Models.EsiResponseModels;
using Newtonsoft.Json;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    [Table("character_roles")]
    public class CharacterRolesDb
    {
        public CharacterRolesDb() { }

        public CharacterRolesDb(CharacterRolesModel characterRoles)
        {
            CharacterId = characterRoles.CharacterId;
            Roles = characterRoles.Roles;
        }

        [Key]
        [Column("character_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CharacterId { get; set; }

        [Column("roles")] public string _roles { get; set; }

        /// <summary>
        /// Wrapper around string-based roles data
        /// </summary>
        [NotMapped]
        public IEnumerable<RolesEnum> Roles {
            get => JsonConvert.DeserializeObject<IEnumerable<RolesEnum>>(_roles);
            set => _roles = JsonConvert.SerializeObject(value);
        }
    }
}
