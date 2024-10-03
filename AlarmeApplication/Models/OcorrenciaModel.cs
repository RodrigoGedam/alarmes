using System.Runtime.InteropServices;

namespace AlarmeApplication.Models
{
    public class OcorrenciaModel
    {
        public int OcorrenciaId { get; set; }
        public int Prioridade { get; set; }
        public string Localizacao { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public DateTime Data { get; set; }
    }
}
