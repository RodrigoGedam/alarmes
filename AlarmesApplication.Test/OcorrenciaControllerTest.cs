using AlarmeApplication.Controllers;
using AlarmeApplication.Data.Contexts;
using AlarmeApplication.Models;
using AlarmeApplication.Services;
using AlarmeApplication.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AlarmesApplication.Test
{
    public class OcorrenciaControllerTest
    {
        private readonly Mock<DatabaseContext> _mockContext;
        private readonly OcorrenciaController _ocorrenciaController;
        private readonly DbSet<OcorrenciaModel> _mockSet;

        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IOcorrenciaService> _mockOcorrenciaService;

        public OcorrenciaControllerTest()
        {
            _mockContext = new Mock<DatabaseContext>();

            _mockSet = MockDbSet();

            _mockContext.Setup(x => x.Ocorrencias).Returns(_mockSet);

            _mockOcorrenciaService = new Mock<IOcorrenciaService>();

            _mockMapper = new Mock<IMapper>();

            _ocorrenciaController = new OcorrenciaController( _mockOcorrenciaService.Object , _mockMapper.Object);
        }

        private DbSet<OcorrenciaModel> MockDbSet()
        {
            // Lista de clientes para simular dados no banco de dados
            var data = new List<OcorrenciaModel>
            {
                new OcorrenciaModel { OcorrenciaId = 10, Prioridade = 0, Localizacao = "ZZ", Descricao = "Invasao", Status = "Concluido", Data = DateTime.Now },
                new OcorrenciaModel { OcorrenciaId = 11, Prioridade = 10, Localizacao = "ZZ1", Descricao = "Invasao1", Status = "Concluido1", Data = DateTime.Now }
            }.AsQueryable();

            // Cria o mock do DbSet
            var mockSet = new Mock<DbSet<OcorrenciaModel>>();

            // Configura o comportamento do mock DbSet para simular uma consulta ao banco de dados
            mockSet.As<IQueryable<OcorrenciaModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<OcorrenciaModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<OcorrenciaModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<OcorrenciaModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Retorna o DbSet mock
            return mockSet.Object;
        }

        [Fact]
        public void GetAll_ReturnsWithOcorrencias()
        {
            // Arrange
            var ocorrencias = new List<OcorrenciaModel>
            {
                new OcorrenciaModel { OcorrenciaId = 10, Prioridade = 0, Localizacao = "ZZ", Descricao = "Invasao", Status = "Concluido", Data = DateTime.Now },
                new OcorrenciaModel { OcorrenciaId = 11, Prioridade = 10, Localizacao = "ZZ1", Descricao = "Invasao1", Status = "Concluido1", Data = DateTime.Now }
            };
            _mockOcorrenciaService.Setup(s => s.ListarOcorrencias()).Returns(ocorrencias);

            // Act
            var result = _ocorrenciaController.Get();

            // Assert
            Assert.Equal(2, ocorrencias.Count());
        }

        [Fact]
        public void ReturnsEmptyList_WhenNoOcorrenciasExists()
        {
            _mockOcorrenciaService.Setup(s => s.ListarOcorrencias()).Returns(new List<OcorrenciaModel>());
            var result = _ocorrenciaController.Get();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<OcorrenciaViewModel>>>(result);
            var model = Assert.IsAssignableFrom<ActionResult<IEnumerable<OcorrenciaViewModel>>>(actionResult);
        }

        [Fact]
        public void ThrowsException_WhenDatabaseFails()
        {
            _mockOcorrenciaService.Setup(s => s.ListarOcorrencias()).Throws(new System.Exception("Database Error"));
            Assert.Throws<System.Exception>(() => _ocorrenciaController.Get());
        }
    }
}
