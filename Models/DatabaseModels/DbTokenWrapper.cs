using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntrepreneurCommon.Authentication;
using EveEntrepreneurWebPersistency3.Models;

namespace EntrepreneurCommon.Models.DatabaseModels
{
    /// <summary>
    ///     Database adjusted Token Container
    /// </summary>
    [Table("app_esi_tokens")]
    public class DbTokenWrapper : EsiTokenContainer, IDbTokenWrapper
    {
        public DbTokenWrapper(IEsiTokenContainer baseKey) : base(baseKey, baseKey)
        {
            // Assign Unique Identifier
            AssignUuid();
        }

        public DbTokenWrapper() : base(null)
        {
            // Assign Unique Identifier
            AssignUuid();
        }

        [Key] public string Uuid { get; set; }

        public void AssignUuid()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }
}
