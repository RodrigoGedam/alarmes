using AlarmeApplication.Data.Contexts;
using AlarmeApplication.Data.Repository;
using AlarmeApplication.Models;
using Microsoft.EntityFrameworkCore;

public class OcorrenciaRepository : IOcorrenciaRepository
{
    private readonly DatabaseContext _context;

    public OcorrenciaRepository(DatabaseContext context)
    {
        _context = context;
    }

    public IEnumerable<OcorrenciaModel> GetAll() => [.. _context.Ocorrencias];

    public IEnumerable<OcorrenciaModel> GetAllReference(int lastReference, int size)
    {
        var ocorrencias = _context.Ocorrencias.Where(o => o.OcorrenciaId > lastReference).OrderBy(o => o.OcorrenciaId).Take(size).AsNoTracking().ToList();

        return ocorrencias;
    }

    public OcorrenciaModel GetById(int id) => _context.Ocorrencias.Find(id);

    public void Add(OcorrenciaModel ocorrencia)
    {
        _context.Add(ocorrencia);
        _context.SaveChanges(); ;
    }

    public void Update(OcorrenciaModel ocorrencia)
    {
        _context.Update(ocorrencia);
        _context.SaveChanges();
    }

    public void Delete(OcorrenciaModel ocorrencia)
    {
        _context.Ocorrencias.Remove(ocorrencia);
        _context.SaveChanges();
    }
}
