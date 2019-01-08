using System;
using System.Collections.Generic;
using System.Linq;
using EntrepreneurCommon.Models.DatabaseModels;
using EveEntrepreneurWebPersistency3.Models;

namespace EntrepreneurCommon.DbLayer
{
    public interface ITokenStorageProvider
    {
        ICollection<DbTokenWrapper> Tokens { get; set; }

        // string Name { get; set; }

        IDbTokenWrapper GetToken(Func<IDbTokenWrapper, bool> predicate);
        void SaveToken(IDbTokenWrapper token);
    }
}
