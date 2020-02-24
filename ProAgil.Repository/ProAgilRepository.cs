using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository {
    public class ProAgilRepository : IProAgilRepository {
        public ProAgilContext _context { get; }
        public ProAgilRepository (ProAgilContext context) {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T> (T entity) where T : class {
            _context.Add(entity);
        }

        public void Update<T> (T entity) where T : class {
            _context.Update(entity);
        }
        public void Delete<T> (T entity) where T : class {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync () {
            return (await _context.SaveChangesAsync()) > 0;
        }

        // Eventos
        public async Task<Evento[]> GetAllEventosAsync (bool includePalestrantes = false) {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(r => r.RedesSociais);

            if(includePalestrantes)
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);

            query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoById (int eventoId, bool includePalestrantes = false) {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(r => r.RedesSociais)
                .Where(e => e.Id == eventoId);

            if(includePalestrantes)
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);

            query.OrderByDescending(e => e.DataEvento);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetEventosByTemaAsync (string tema, bool includePalestrantes = false) {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(r => r.RedesSociais)
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            if(includePalestrantes)
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);

            query.OrderByDescending(e => e.DataEvento);

            return await query.ToArrayAsync();  
        }

        public async Task<Palestrante> GetPalestranteById (int palestranteId, bool includeEventos = false) {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(r => r.RedesSociais)
                .Where(p => p.Id == palestranteId);

            if(includeEventos)
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(e => e.Evento);

            query.OrderBy(p => p.Nome);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetPalestrantesByNomeAsync (string nome, bool includeEventos = false) {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(r => r.RedesSociais)
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            if(includeEventos)
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(e => e.Evento);

            query.OrderBy(p => p.Nome);

            return await query.ToArrayAsync();
        }
    }
}