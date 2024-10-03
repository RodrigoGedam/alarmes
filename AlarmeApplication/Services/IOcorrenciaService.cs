using AlarmeApplication.Models;

namespace AlarmeApplication.Services
{
    public interface IOcorrenciaService
    {
        IEnumerable<OcorrenciaModel> ListarOcorrencias();
        IEnumerable<OcorrenciaModel> ListarOcorrenciasUltimaReferencia(int ultimoId = 0, int tamanho = 10);
        OcorrenciaModel ObterOcorrenciaPorId(int id);
        void CriarOcorrencia(OcorrenciaModel ocorrencia);
        void AtualizarOcorrencia(OcorrenciaModel ocorrencia);
        void DeletarOcorrencia(int id);
    }
}
