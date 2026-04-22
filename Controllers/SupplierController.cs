using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raktárkezelő.Models;

namespace Raktárkezelő.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetByPagination(int PageNum, int PageSize)
        {
            try
            {
                using (var context = new RaktarContext())
                {
                    var suppliers = context.Beszallitoks.Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    return Ok(suppliers);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                using (var context = new RaktarContext())
                {
                    var supplier = context.Beszallitoks.Find(id);
                    if (supplier == null) return NotFound();

                    return Ok(supplier);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost()]
        public IActionResult Add(Beszallitok beszallito)
        {
            try
            {
                using(var context = new RaktarContext())
                {
                    if(!context.Beszallitoks.Any(b => b.Id == beszallito.Id))
                    {
                        context.Beszallitoks.Add(beszallito);
                        context.SaveChanges();
                        return Ok(beszallito);
                    }
                    else
                    {
                        return BadRequest("Már van ilyen elem a beszállítók között.");
                    }
                }

            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Beszallitok beszallito)
        {
            try
            {
                using (var context = new RaktarContext())
                {
                    var existingSupplier = context.Beszallitoks.Find(id);
                    if (existingSupplier == null) return NotFound();

                    context.Entry(existingSupplier).CurrentValues.SetValues(beszallito);
                    context.SaveChanges();
                    return Ok(beszallito);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (var context = new RaktarContext())
                {
                    var existingSupplier = context.Beszallitoks.Find(id);
                    if (existingSupplier == null) return NotFound();
                    context.Beszallitoks.Remove(existingSupplier);
                    context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
