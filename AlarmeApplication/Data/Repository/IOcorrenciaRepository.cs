using AlarmeApplication.Models;

namespace AlarmeApplication.Data.Repository
{
    public interface IOcorrenciaRepository
    {
        IEnumerable<OcorrenciaModel> GetAll();
        IEnumerable<OcorrenciaModel> GetAllReference(int lastReference, int size);
        OcorrenciaModel GetById(int id);
        void Add(OcorrenciaModel ocorrencia);
        void Update(OcorrenciaModel ocorrencia);
        void Delete(OcorrenciaModel ocorrencia);
    }
}
