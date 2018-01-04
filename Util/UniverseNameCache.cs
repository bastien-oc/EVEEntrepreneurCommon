using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntrepreneurEsiApi.Api;
using EntrepreneurEsiApi.Models.Esi;
using Newtonsoft.Json;

namespace EntrepreneurEsiApi.Util
{
    public class UniverseNameCache:IEnumerable
    {
        public List<UniverseNameResponse> NamesCache = new List<UniverseNameResponse>();
        private UniverseApi Api = new UniverseApi(new EsiApiClient());

        public string PersistentFilePath { get; set; }

        public void AddNameToCache( UniverseNameResponse universeName )
        {
            // If the item exists in the list, ignore.
            foreach (var un in NamesCache) { if (un.ID == universeName.ID) { return; } }
            // If you're still executing, this means the name is not in the list. Add it.
            NamesCache.Add(universeName);
        }
        public IEnumerable<string> GetNames( int[] ids )
        {
            List<int> resolvedIds = new List<int>();
            var results = from n in NamesCache
                          where ids.Contains(n.ID)
                          select n;
            foreach (var r in results) {
                resolvedIds.Add(r.ID);
                yield return r.Name;
            }

            // Retrieve all items from ids list that re not on the resolvedIds list.
            var unresolved = from id in ids
                             where !resolvedIds.Contains(id)
                             select id;

            var response = Api.GetUniverseNamesResponse(unresolved.ToArray());
            foreach (var r in response.Data) {
                yield return r.Name;
            }
        }
        public string GetName( int id )
        {
            var result = (from n in NamesCache
                          where n.ID == id
                          select n).FirstOrDefault();
            // Id exists in the table, return it.
            if (result != null) {
                return result.Name;
            }

            // Id does not exist, have to request.
            var response = Api.GetUniverseNamesResponse(new int[] { id });
            if (response.Data.Count != 0) {
                return response.Data.First().Name;
            } else
                return "";

        }

        public void SaveToFile()
        {
            if (PersistentFilePath != null) {
                Directory.CreateDirectory(Path.GetDirectoryName(PersistentFilePath));
                File.WriteAllText(PersistentFilePath,JsonConvert.SerializeObject(NamesCache));
            }
        }

        public void LoadFromFile()
        {
            if (PersistentFilePath != null) {
                NamesCache = JsonConvert.DeserializeObject<List<UniverseNameResponse>>(File.ReadAllText(PersistentFilePath));
            }
        }

        public IEnumerator GetEnumerator() => ((IEnumerable)NamesCache).GetEnumerator();
    }
}
