using Microsoft.AspNetCore.Mvc;
using LAB14.Models;

namespace LAB14.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private static List<Producto> _productos = new()
    {
        new Producto { Id = 1, Nombre = "Laptop", Precio = 1500.00m, Stock = 10 },
        new Producto { Id = 2, Nombre = "Mouse", Precio = 25.00m, Stock = 50 },
        new Producto { Id = 3, Nombre = "Teclado", Precio = 75.00m, Stock = 30 }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Producto>> GetAll()
    {
        return Ok(_productos);
    }

    [HttpGet("{id}")]
    public ActionResult<Producto> GetById(int id)
    {
        var producto = _productos.FirstOrDefault(p => p.Id == id);
        if (producto == null) return NotFound();
        return Ok(producto);
    }

    [HttpPost]
    public ActionResult<Producto> Create(Producto producto)
    {
        producto.Id = _productos.Max(p => p.Id) + 1;
        _productos.Add(producto);
        return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
    }

    [HttpGet("health")]
    public ActionResult Health()
    {
        return Ok(new { 
            status = "OK", 
            timestamp = DateTime.UtcNow,
            version = "1.0.0",
            ambiente = "Producción"
        });
    }
}