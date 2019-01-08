using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using EntrepreneurCommon.ExtensionMethods;
using EntrepreneurCommon.Models.DatabaseModels;
using EveEntrepreneurWebPersistency3.Models;
using Newtonsoft.Json;

namespace EntrepreneurCommon.DbLayer
{
    public class JsonTokenStorageProvider: ITokenStorageProvider
    {
        public ICollection<DbTokenWrapper> Tokens { get; set; } = new List<DbTokenWrapper>();
        // public string Name { get; set; } = "Json Storage Provider";

        private readonly string _fileName;

        public JsonTokenStorageProvider(string fileName)
        {
            _fileName = fileName;
            ReloadFile();
        }

        private void SaveFile()
        {
            var strObject = JsonConvert.SerializeObject(Tokens, Formatting.Indented);
            File.WriteAllText(_fileName, strObject);
        }

        private void ReloadFile()
        {
            var fileExists = File.Exists(_fileName);
            if (fileExists) {
                Tokens = JsonConvert.DeserializeObject<List<DbTokenWrapper>>(File.ReadAllText(_fileName));
            }
            else {
                using (File.CreateText(_fileName)) {}
                Tokens.Clear();
            }
        }
        
        #region ITokenStorageProvider
        
        public IDbTokenWrapper GetToken(Func<IDbTokenWrapper, bool> predicate)
        {
            return Tokens.FirstOrDefault(predicate);
        }

        public void SaveToken(IDbTokenWrapper token)
        {
            if (Tokens.Any(t => t.Uuid == token.Uuid)) {
                var existing = Tokens.FirstOrDefault(t => t.Uuid == token.Uuid);
                existing.AssignTokenResponse(token);
                existing.AssignTokenVerification(token);
            }
            else {
                Tokens.Add(token as DbTokenWrapper);
            }
            SaveFile();
        }
        
        #endregion
    }
}
