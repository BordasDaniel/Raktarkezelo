using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raktárkezelő.Models;

namespace Raktárkezelő.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetByPagination(int PageNum, int PageSize)
        {
            try
            {
                using (var context = new RaktarContext())
                {
                    var products = context.Termekeks.Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    return Ok(products);
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
                    var product = context.Termekeks.Find(id);
                    if (product == null) return NotFound();

                    return Ok(product);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost()]
        public IActionResult Add(Termekek termek)
        {
            try
            {
                using (var context = new RaktarContext())
                {
                    if (!context.Termekeks.Any())
                    {
                        context.Termekeks.Add(termek);
                        context.SaveChanges();
                        return Ok(termek);
                    }
                    else
                    {
                        return BadRequest("A termék már létezik.");
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Termekek termek)
        {
            try
            {
                using (var context = new RaktarContext())
                {
                    var existingProduct = context.Termekeks.Find(id);
                    if (existingProduct == null) return NotFound();
                    context.Entry(existingProduct).CurrentValues.SetValues(termek);
                    context.SaveChanges();
                    return Ok(existingProduct);
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
                    var existingProduct = context.Termekeks.Find(id);
                    if (existingProduct == null) return NotFound();

                    context.Termekeks.Remove(existingProduct);
                    context.SaveChanges();
                    return Ok(existingProduct);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
