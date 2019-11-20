using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //GERAL
         void Add<T> (T entity) where T : class;
         void Update<T> (T entity) where T : class;
         void Delete<T> (T entity) where T : class;
         Task<bool> SaveChangesAsync();

         // Eventos
         Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
         Task<Evento[]> GetEventosByTemaAsync(string tema, bool includePalestrantes);
         Task<Evento> GetEventoById(int eventoId, bool includePalestrantes);
         // Palestrantes
        Task<Palestrante[]> GetPalestrantesByNomeAsync(string nome, bool includeEventos);
         Task<Palestrante> GetPalestranteById(int palestranteId, bool includeEventos);
    }
}