using Microsoft.Data.Sqlite;
using Tienda.Models;

namespace Tienda.Repository;

public class ProductosRepository
{
    private string cadenaConnection = "Data source = Db/Tienda.db";
    private List<Productos> _productos = new List<Productos>();

    public void Alta(Productos producto)
    {
        
        _productos.Add(producto);
    }
    public List<Productos> GetAll()
    {
        string query = "SELECT * FROM productos";
        var connection = new SqliteConnection(cadenaConnection);
        connection.Open();
        var command = new SqliteCommand(query, connection);
        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var producto = new Productos
            {
                IdProducto = Convert.ToInt32(reader["idproducto"]),
                Descripcion = reader["descripcion"].ToString(),
                Precio=Convert.ToDouble(reader["precio"])
            };
            _productos.Add(producto);
        }
        return _productos;
    }
    public Productos DetallesProductosID(int id)
    {

        return _productos.FirstOrDefault(p => p.IdProducto == id) ?? new Productos();
    }
    public void Modificar(Productos entidad, int id)
    {
        var productoModificar = _productos.FirstOrDefault(p => p.IdProducto == id);
        if (productoModificar == null)
        {
            throw new InvalidOperationException("Producto que se intenta modificar no existe.");
        }
        productoModificar.Precio = entidad.Precio;
        productoModificar.Descripcion = entidad.Descripcion;
    }

    public bool Eliminar(int id)
    {
        var producto = _productos.FirstOrDefault(p => p.IdProducto == id);
        if (producto == null)
        {
            return false;
            throw new InvalidOperationException("Producto que se intenta eliminar no existe.");
        }
        _productos.Remove(producto);
        return true;
    }

}

