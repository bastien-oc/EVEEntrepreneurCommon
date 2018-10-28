using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EntrepreneurCommon.Authentication;

namespace EntrepreneurCommon.Util
{
    public class EsiTokenContainerCollection : ICollection<IEsiTokenContainer>
    {
        public List<IEsiTokenContainer> Items;

        public EsiTokenContainerCollection()
        {
            Items = new List<IEsiTokenContainer>();
        }
        public int Count => Items.Count;
        public bool IsReadOnly => false;
        /// <summary>
        /// Adds an EsiTokenContainer to a collection if another token doesn't duplicate the scopes for the given character.
        /// </summary>
        /// <param name="item"></param>
        public void AddUnique(IEsiTokenContainer item)
        {
            if (!Items.Any(a => a.CharacterId == item.CharacterId && a.Scopes == item.Scopes))
                Items.Add(item);
            else
                throw new SufficientTokenExistsException(
                    "A token with identical character ID and scopes already exists. No need to add another.");
        }
        /// <summary>
        /// Returns true if another token with matching CharacterId and Scopes is found in the collection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ContainsComparable(IEsiTokenContainer item)
        {
            if (!Items.Any(a => a.CharacterId == item.CharacterId && a.Scopes == item.Scopes))
                return true;
            return false;
        }
        public void Add(IEsiTokenContainer item)
        {
            Items.Add(item);
        }
        public void Clear()
        {
            Items.Clear();
        }
        public bool Contains(IEsiTokenContainer item)
        {
            return Items.Contains(item);
            
        }
        /// <summary>
        /// TODO Implement.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void CopyTo(IEsiTokenContainer[] array,
            int arrayIndex) => throw new NotImplementedException();
        public IEnumerator<IEsiTokenContainer> GetEnumerator() => ((IEnumerable<EsiTokenContainer>) Items).GetEnumerator();
        public bool Remove(IEsiTokenContainer item) => Items.Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<EsiTokenContainer>)Items).GetEnumerator();
    }

    internal class SufficientTokenExistsException : Exception
    {
        public SufficientTokenExistsException(string message) : base(message) { }
    }
}