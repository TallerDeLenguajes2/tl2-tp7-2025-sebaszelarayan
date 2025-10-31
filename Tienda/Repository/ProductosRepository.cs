using Microsoft.Data.Sqlite;
using SQLitePCL;
using Tienda.Models;

namespace Tienda.Repository;

public class ProductosRepository
{
    private string cadenaConnection = "Data source = Db/Tienda.db";
    private List<Productos> _productos = new List<Productos>();

    public void Alta(Productos producto)
    {
        string query = "INSERT INTO productos(descripcion,precio) VALUES (@descripcion,@precio)";
        var connection = new SqliteConnection(cadenaConnection);
        connection.Open();
        var command = new SqliteCommand(query, connection);
        command.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
        command.Parameters.Add(new SqliteParameter("@precio", producto.Precio));
        command.ExecuteNonQuery();

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
                Precio = Convert.ToDouble(reader["precio"])
            };
            _productos.Add(producto);
        }
        return _productos;
    }
    public Productos? DetallesProductosID(int id)
    {
        Productos? producto = null;

        string query = "SELECT * FROM productos WHERE idproducto=@idproducto";
        var connection = new SqliteConnection(cadenaConnection);
        connection.Open();

        var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@idproducto", id);


        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            producto = new Productos
            {
                IdProducto = Convert.ToInt32(reader["idproducto"]),
                Descripcion = reader["descripcion"].ToString(),
                Precio = Convert.ToDouble(reader["precio"])
            };
        }

        return producto;
    }
    public void Modificar(int id, string descripcion, double precio)
    {

        using var connection = new SqliteConnection(cadenaConnection);
        connection.Open();
        string sql = "UPDATE Productos SET descripcion=@descripcion,precio=@precio WHERE idproducto = @id";
        using var command = new SqliteCommand(sql, connection);

        command.Parameters.Add(new SqliteParameter("@descripcion", descripcion));
        command.Parameters.Add(new SqliteParameter("@precio", precio));
        command.Parameters.Add(new SqliteParameter("@id", id));

        command.ExecuteNonQuery();
        /*
        var productoModificar = _productos.FirstOrDefault(p => p.IdProducto == id);
            if (productoModificar == null)
            {
                throw new InvalidOperationException("Producto que se intenta modificar no existe.");
    }
    productoModificar.Precio = precio;
            productoModificar.Descripcion = descripcion;*/
    }

    public bool Eliminar(int id)
    {
        using var connection = new SqliteConnection(cadenaConnection);
        connection.Open();
        string sql = "DELETE FROM Productos WHERE idproducto = @id";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.Add(new SqliteParameter("@id", id));
        command.ExecuteNonQuery();
        return true;
        /*
        var producto = _productos.FirstOrDefault(p => p.IdProducto == id);
        if (producto == null)
        {
            return false;
            throw new InvalidOperationException("Producto que se intenta eliminar no existe.");
        }
        _productos.Remove(producto);
        return true;
        */
    }

}

