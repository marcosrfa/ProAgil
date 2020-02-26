using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebAPI.DTOS;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        public IProAgilRepository _repo { get; }
        public IMapper _mapper { get; }

        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _repo.GetAllEventosAsync(true);

                var results = _mapper.Map<EventoDTO[]>(eventos);

                return Ok(results);
            }
            catch (System.Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou!!!! Erro: {ex.Message}");
            }
            
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var evento = await _repo.GetEventoById(eventoId, true);

                var result = _mapper.Map<EventoDTO>(evento);
                return  Ok(result);
            }
            catch (System.Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou!!!! Erro: {ex.Message}");
            }
            
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var result = await _repo.GetEventosByTemaAsync(tema, true);
                return  Ok(result);
            }
            catch (System.Exception)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!!");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDTO model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);

                _repo.Add(evento);

                if(await _repo.SaveChangesAsync()){
                    return  Created($"/api/evento/{model.Id}",  _mapper.Map<EventoDTO>(evento));
                }
                
                return BadRequest();
            }
            catch (System.Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou!!!! Erro: {ex.Message}");
            }
            
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, EventoDTO model)
        {
            try
            {
                var evento = await _repo.GetEventoById(eventoId, false);
                if(evento == null) return NotFound();

                _mapper.Map(model, evento);

                _repo.Update(evento);

                if(await _repo.SaveChangesAsync()){
                    return  Created($"/api/evento/{model.Id}", _mapper.Map<EventoDTO>(evento));
                }
                
                return BadRequest();
            }
            catch (System.Exception)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!!");
            }
            
        }

        [HttpDelete("{eventoId}")]
        public async Task<IActionResult> Delete(int eventoId)
        {
            try
            {
                var evento = await _repo.GetEventoById(eventoId, false);

                if(evento == null) return NotFound();

                _repo.Delete(evento);

                if(await _repo.SaveChangesAsync()){
                    return  Ok();
                }
                
                return BadRequest();
            }
            catch (System.Exception)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!!");
            }
            
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if(file.Length > 0){
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString(); // Tostring foi por minha conta para não dar problema 
                    var fullPath = Path.Combine(pathToSave, fileName.Replace("\"", "").Trim());

                    using(var stream = new FileStream(fullPath, FileMode.Create)){
                        file.CopyTo(stream);
                    }
                }

                return Ok();
            }
            catch (System.Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar realizar upload de arquivo! Erro: {ex.Message}");
            }

            return this.BadRequest("Erro ao tentar realizar upload de arquivo");
        }
    }
}