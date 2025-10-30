using Tienda.Models;

namespace Tienda.Repository;

public class PresupuestosRepository
{
    private List<Presupuestos> _presupuestos = new List<Presupuestos>();

    public void Crear(Presupuestos presupuesto)
    {
        _presupuestos.Add(presupuesto);
    }
    public List<Presupuestos> ListarPresupuestos()
    {
        return _presupuestos;
    }
    public Presupuestos DetallesPresupuestosID(int id)
    {

        return _presupuestos.FirstOrDefault(p => p.IdPresupuesto == id) ?? new Presupuestos();
    }
    public void Modificar(Presupuestos entidad, int id)
    {
        var presupuestoModificar = _presupuestos.FirstOrDefault(p => p.IdPresupuesto == id);
        if (presupuestoModificar == null)
        {
            throw new InvalidOperationException("Presupuesto que se intenta modificar no existe.");
        }
        presupuestoModificar.Detalle = entidad.Detalle;
        presupuestoModificar.NombreDestinatario = entidad.NombreDestinatario;
        presupuestoModificar.FechaCreacion = entidad.FechaCreacion;
    }

    public void Eliminar(int id)
    {
        var presupuesto = _presupuestos.FirstOrDefault(p => p.IdPresupuesto == id);
        if (presupuesto == null)
        {
            throw new InvalidOperationException("Presupuesto que se intenta eliminar no existe.");
        }
        _presupuestos.Remove(presupuesto);
    }

}

