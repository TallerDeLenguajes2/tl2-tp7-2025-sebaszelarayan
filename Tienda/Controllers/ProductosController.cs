
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


    [HttpPost("api/Producto")]
    public ActionResult<string> AltaProducto(string descripcion, double precio)
    {
        var nuevoProducto = new Productos(descripcion, precio);
        _productosRepository.Alta(nuevoProducto);
        return Ok("Producto dado de alta exitosamente");
    }
    [HttpPut("/api/Producto/{id}")]
    public ActionResult<string> ModificarProducto(int id, string descripcion, double precio)
    {
        _productosRepository.Modificar(id, descripcion, precio);
        return Ok("Producto Modificado exitosamente");
    }
    [HttpGet("/api/Producto/{id}")]
    public ActionResult<Productos> GetDescricionProducto(int id)
    {
        var Producto = _productosRepository.DetallesProductosID(id);
        if (Producto == null)
    {
        return NotFound($"No se encontró el producto con ID {id}.");
    }
        return Ok(Producto);
    }

    [HttpGet("api/Productos")]
    public ActionResult<List<Productos>> GetProductos()
    {
        List<Productos> listProductos;
        listProductos = _productosRepository.GetAll();
        return Ok(listProductos);
    }

    [HttpDelete("/api/Producto/{id}")]
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