using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BirthdayAPI.Data;
using BirthdayAPI.Models;
using System;

namespace BirthdayAPI.Controllers
{
    [Route("v1/birthdays")]
    public class BirthdaysController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Birthday>>> Get([FromServices] DataContext context)
        {
            var birthdayPersons = await context.BirthdayPersons.AsNoTracking().ToListAsync();
            return birthdayPersons;
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Birthday>> GetById(
            int id,
            [FromServices]DataContext context)
        {
            var birthdayPerson = await context.BirthdayPersons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            
            if(birthdayPerson == null)
                return NotFound(new { message = "Não foi possível encontrar aniversariante."});

            return Ok(birthdayPerson);
        }
        
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Birthday>>> Post(
            [FromBody]Birthday model,
            [FromServices]DataContext context)
        {
            if(ModelState.IsValid)
            {
                try 
                {
                    context.BirthdayPersons.Add(model);
                    await context.SaveChangesAsync();
                        
                    return Ok(model);
                }
                catch
                {
                    return BadRequest(new { message = "Não foi possível cadastrar aniversariante."});
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Birthday>>> Put(
            int id, 
            [FromBody]Birthday model, 
            [FromServices]DataContext context)
        {
            if(id != model.Id)
                return NotFound(new { message = "Aniversariante não encontrado." });

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try 
            {
                context.Entry<Birthday>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                    
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Este registro já foi atualizado." });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar informações de aniversariante." });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Birthday>>> Delete(
            int id, 
            [FromServices]DataContext context)
        {
            var birthdayPerson = await context.BirthdayPersons.FirstOrDefaultAsync(x => x.Id == id);
            
            if(birthdayPerson == null)
                return NotFound(new { message = "Aniversariante não encontrado." });

            try 
            {
                context.BirthdayPersons.Remove(birthdayPerson);
                await context.SaveChangesAsync();

                return Ok(new { message = "Aniversariante removido com sucesso!" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o aniversariante." });
            }
        }
    }
}
