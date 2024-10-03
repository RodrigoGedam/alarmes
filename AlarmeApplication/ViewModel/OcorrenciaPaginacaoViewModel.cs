namespace AlarmeApplication.ViewModel
{
    public class OcorrenciaPaginacaoViewModel
    {
        public IEnumerable<OcorrenciaViewModel> Ocorrencias { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/Ocorrencia?referencia={Ref}&amp;tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/Ocorrencia?referencia={NextRef}&amp;tamanho={PageSize}" : "";
    }
}
