using AlarmeApplication.Data.Repository;
using AlarmeApplication.Models;

namespace AlarmeApplication.Services
{
    public class OcorrenciaService : IOcorrenciaService
    {
        private readonly IOcorrenciaRepository _repository;

        public OcorrenciaService(IOcorrenciaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<OcorrenciaModel> ListarOcorrencias() => _repository.GetAll();

        public IEnumerable<OcorrenciaModel> ListarOcorrenciasUltimaReferencia(int ultimoId = 0, int tamanho = 10) => _repository.GetAllReference(ultimoId, tamanho);

        public OcorrenciaModel ObterOcorrenciaPorId(int id) => _repository.GetById(id);

        public void CriarOcorrencia(OcorrenciaModel ocorrencia) => _repository.Add(ocorrencia);

        public void AtualizarOcorrencia(OcorrenciaModel ocorrencia) => _repository.Update(ocorrencia);

        public void DeletarOcorrencia(int id)
        {
            var ocorrencia = _repository.GetById(id);
            if (ocorrencia != null)
            {
                _repository.Delete(ocorrencia);
            }
        }
    }
}
