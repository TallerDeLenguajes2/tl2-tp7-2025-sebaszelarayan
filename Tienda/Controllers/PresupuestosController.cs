
using Microsoft.AspNetCore.Mvc;
using Tienda.Models;
using Tienda.Repository;

namespace Tarea.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresupuestosController : ControllerBase
{
    private PresupuestosRepository _presupuestosRepository;
    public PresupuestosController()
    {
        _presupuestosRepository = new PresupuestosRepository();
    }
    //A partir de aquí van todos los Action Methods (Get, Post,etc.)


    [HttpPost("api/Presupuesto")]
    public ActionResult<string> AltaPresupuesto(string? nombreDestinatario, DateTime fechaCreacion, List<PresupuestosDetalle>? detalle)
    {
        var nuevoPresupuesto = new Presupuestos(nombreDestinatario, fechaCreacion, detalle);
        _presupuestosRepository.Crear(nuevoPresupuesto);
        return Ok("Presupuesto dado de alta exitosamente");
    }

    [HttpPost("api/Presupuesto/{id}/ProductoDetalle")]

    public ActionResult AgregarProductoDetalle(int id, int idProducto, int cantidad)
    {
        var _productosRepositoy = new ProductosRepository();

        var detalle = new PresupuestosDetalle
        {
            Cantidad = cantidad,
            Producto = _productosRepositoy.DetallesProductosID(idProducto)
        };

        _presupuestosRepository.AgregarProducto(id, detalle);
        return Ok("Producto agregado al presupuesto exitosamente.");

    }
    [HttpGet("/api/Presupuesto/{id}")]
    public ActionResult<Presupuestos> GetDescricionPresupuesto(int id)
    {
        var presupuesto = _presupuestosRepository.DetallesPresupuestosID(id);
        if (presupuesto == null)
        {
            return NotFound($"No se encontró el producto con ID {id}.");
        }
        return Ok(presupuesto);
    }

    [HttpGet("api/presupuesto")]
    public ActionResult<List<Presupuestos>> GetPresupuestos()
    {
        List<Presupuestos> listPresupestos;
        listPresupestos = _presupuestosRepository.ListarPresupuestos();
        return Ok(listPresupestos);
    }

    [HttpDelete("api/Presupuesto/{id}")]
    public ActionResult DeleteProducto(int id)
    {
        bool eliminado = _presupuestosRepository.Eliminar(id);
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