using AlarmeApplication.Models;
using AlarmeApplication.Services;
using AlarmeApplication.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AlarmeApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OcorrenciaController : ControllerBase
    {

        private readonly IOcorrenciaService _ocorrenciaService;
        private readonly IMapper _mapper;

        public OcorrenciaController(IOcorrenciaService ocorrenciaService, IMapper mapper)
        {
            _ocorrenciaService = ocorrenciaService;
            _mapper = mapper;
        }

        //[HttpGet]
        //[Authorize(Roles = "operador,oficial,supervisor")]
        //public ActionResult<IEnumerable<OcorrenciaViewModel>> Get()
        //{
        //    var lista = _ocorrenciaService.ListarOcorrencias();
        //    var viewModelList = _mapper.Map<IEnumerable<OcorrenciaViewModel>>(lista);

        //    return Ok(viewModelList);
        //}

        [HttpGet]
        [Authorize(Roles = "operador,oficial,supervisor")]
        public ActionResult<IEnumerable<OcorrenciaPaginacaoViewModel>> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {
            var ocorrencias = _ocorrenciaService.ListarOcorrenciasUltimaReferencia(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<OcorrenciaViewModel>>(ocorrencias);

            var viewModel = new OcorrenciaPaginacaoViewModel
            {
                Ocorrencias = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().OcorrenciaId
            };

            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "operador,oficial,usuario,supervisor")]
        public ActionResult<OcorrenciaViewModel>Get(int id)
        {
            var ocorrenciaExistente = _ocorrenciaService.ObterOcorrenciaPorId(id);
            if (ocorrenciaExistente == null)
                return NotFound();

            var viewModel = _mapper.Map<OcorrenciaViewModel>(ocorrenciaExistente);
            return Ok(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "operador,oficial,supervisor")]
        public ActionResult Post([FromBody] OcorrenciaViewModel model)
        {
            var ocorrencia = _mapper.Map<OcorrenciaModel>(model);

            try
            {
                _ocorrenciaService.CriarOcorrencia(ocorrencia);
                return CreatedAtAction(nameof(Get), new { id = ocorrencia.OcorrenciaId }, ocorrencia);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "operador,supervisor")]
        public ActionResult Put(int id, [FromBody] OcorrenciaViewModel model)
        {
            var ocorrenciaExistente = _ocorrenciaService.ObterOcorrenciaPorId(id);
            if (ocorrenciaExistente == null)
                return NotFound();

            _mapper.Map(model, ocorrenciaExistente);
            _ocorrenciaService.AtualizarOcorrencia(ocorrenciaExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "operador")]
        public ActionResult Delete(int id)
        {
            _ocorrenciaService.DeletarOcorrencia(id);
            return NoContent();
        }
    }
}
