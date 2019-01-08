using EntrepreneurCommon.Authentication;

namespace EveEntrepreneurWebPersistency3.Models
{
    public interface IDbTokenWrapper : IEsiTokenContainer
    {
        string Uuid { get; set; }

        void AssignUuid();
    }
}