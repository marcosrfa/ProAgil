using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        public IProAgilRepository _repo { get; }
        public PalestranteController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{palestranteId}")]
        public async Task<IActionResult> Get(int palestranteId)
        {
            try
            {
                var result = await _repo.GetPalestranteById(palestranteId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!!");
            }
            
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                var result = await _repo.GetPalestrantesByNomeAsync(nome, true);
                return  Ok(result);
            }
            catch (System.Exception)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!!");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repo.Add(model);
                if(await _repo.SaveChangesAsync()){
                    return  Created($"/api/palestrante/{model.Id}", model);
                }
                
                return BadRequest();
            }
            catch (System.Exception)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!!");
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> Put(int palestranteId, Palestrante model)
        {
            try
            {
                // var palestrante = await _repo.GetPalestranteById(palestranteId, false);
                var palestrante = await _repo.GetPalestranteById(model.Id, false);

                if(palestrante == null) return NotFound();

                _repo.Update(model);
                if(await _repo.SaveChangesAsync()){
                    return  Created($"/api/palestrante/{model.Id}", model);
                }
                
                return BadRequest();
            }
            catch (System.Exception)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!!!!");
            }
            
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int palestranteId)
        {
            try
            {
                var palestrante = await _repo.GetPalestranteById(palestranteId, false);

                if(palestrante == null) return NotFound();

                _repo.Delete(palestrante);

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
    }
}