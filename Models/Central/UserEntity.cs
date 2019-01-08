using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EntrepreneurCommon.Models.Central
{
    [DataContract]
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserName    { get; set; }
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "User needs to authenticate with valid EVE Online character.")]
        public int MainCharacter { get; set; }

        private string RegisteredCharacters { get; set; }

        [NotMapped]
        public IEnumerable<string> RegisteredCharactersA {
            get => RegisteredCharacters.Split(';');
            set => RegisteredCharacters = string.Join(";", value);
        }
    }
}
