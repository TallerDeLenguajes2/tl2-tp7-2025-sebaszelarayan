
using Microsoft.AspNetCore.Mvc;
using Tienda.Models;
using Tienda.Repository;

namespace Tarea.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private ProductosRepository _productosRepository;
    public ProductosController()
    {
        _productosRepository = new ProductosRepository();
    }
    //A partir de aquí van todos los Action Methods (Get, Post,etc.)


    [HttpPost("api/AltaProducto")]
    public ActionResult<string> AltaProducto(Productos nuevoProducto)
    {
        _productosRepository.Alta(nuevoProducto);
        return Ok("Producto dado de alta exitosamente");
    }

    [HttpGet("api/Productos")]
    public ActionResult<List<Productos>> GetProductos()
    {
        List<Productos> listProductos;
        listProductos = _productosRepository.GetAll();
        return Ok(listProductos);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteProducto(int id)
    {
        bool eliminado = _productosRepository.Eliminar(id);
        if (eliminado)
        {
            return NoContent();// HTTP 204 (Eliminación exitosa)
        }
        else
        {
            return NotFound($"No se encontró el producto con ID {id} para eliminar.");
        }
    }
}