using Tienda.Models;
using Microsoft.Data.Sqlite;
namespace Tienda.Repository;

public class PresupuestosRepository
{
    private string cadenaConnection = "Data source = Db/Tienda.db";
    private List<Presupuestos> _presupuestos = new List<Presupuestos>();

    public void Crear(Presupuestos presupuesto)
    {
        string query = "INSERT INTO presupuestos(NombreDestinatario,FechaCreacion) VALUES (@NombreDestinatario,@FechaCreacion)";
        var connection = new SqliteConnection(cadenaConnection);
        connection.Open();
        var command = new SqliteCommand(query, connection);
        command.Parameters.Add(new SqliteParameter("@NombreDestinatario", presupuesto.NombreDestinatario));
        command.Parameters.Add(new SqliteParameter("@FechaCreacion", presupuesto.FechaCreacion));
        command.ExecuteNonQuery();
        _presupuestos.Add(presupuesto);
    }
    public List<Presupuestos> ListarPresupuestos()
    {
        string query = "SELECT * FROM presupuestos";
        var connection = new SqliteConnection(cadenaConnection);
        connection.Open();
        var command = new SqliteCommand(query, connection);
        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var presupuesto = new Presupuestos
            {
                IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]),
                NombreDestinatario = reader["NombreDestinatario"].ToString(),
                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
            };
            _presupuestos.Add(presupuesto);
        }

        return _presupuestos;
    }
    public Presupuestos? DetallesPresupuestosID(int id)
    {

        Presupuestos? presupuesto = null;

        string query = "SELECT * FROM presupuestos WHERE idPresupuesto=@idPresupuesto";
        var connection = new SqliteConnection(cadenaConnection);
        connection.Open();

        var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@idPresupuesto", id);

        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            presupuesto = new Presupuestos
            {
                IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]),
                NombreDestinatario = reader["NombreDestinatario"].ToString(),
                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
            };
        }
        if (presupuesto == null) return null;

        string queryDetalle = @"
                SELECT pd.cantidad, p.idProducto, p.descripcion, p.precio 
                FROM PresupuestosDetalle pd
                JOIN Productos p ON pd.idProducto = p.idProducto
                WHERE pd.idPresupuesto = @id;";

            var cmdDetalle = new SqliteCommand(queryDetalle, connection);
            cmdDetalle.Parameters.AddWithValue("@id", id);
            
            using (var readerDetalle = cmdDetalle.ExecuteReader())
            {
                while (readerDetalle.Read())
                {
                    var producto = new Productos
                    {
                        IdProducto = Convert.ToInt32(readerDetalle["idProducto"]),
                        Descripcion = readerDetalle["descripcion"].ToString(),
                        Precio = Convert.ToDouble(readerDetalle["precio"])
                    };

                    var detalle = new PresupuestosDetalle
                    {
                        Cantidad = Convert.ToInt32(readerDetalle["cantidad"]),
                        Producto = producto,
                        
                    };
                    presupuesto.Detalle.Add(detalle);
                }
            }
        return presupuesto;
    }
    
    public bool Eliminar(int id)
    {

        using var connection = new SqliteConnection(cadenaConnection);
        connection.Open();
        string sql = "DELETE FROM presupuestos WHERE idPresupuesto = @idPresupuesto;";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.Add(new SqliteParameter("@idPresupuesto", id));
        command.ExecuteNonQuery();
        return true;

    }

    public void AgregarProducto(int idPresupuesto, PresupuestosDetalle detalle)
{
    // Verificamos que el objeto producto no sea nulo y tenga un ID válido.
    if (detalle.Producto == null || detalle.Producto.IdProducto <= 0)
    {
        throw new ArgumentException("El producto proporcionado no es válido o no tiene un ID.");
    }

    string query = @"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, cantidad) 
                     VALUES (@idPresupuesto, @idProducto, @cantidad);";

    using (var connection = new SqliteConnection(cadenaConnection))
    {
        connection.Open();
        var command = new SqliteCommand(query, connection);

        // Aquí está la "traducción":
        // 1. Tomamos el idPresupuesto que viene como parámetro.
        command.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
        // 2. Extraemos el idProducto desde el objeto 'producto' dentro de 'detalle'.
        command.Parameters.AddWithValue("@idProducto", detalle.Producto.IdProducto);
        // 3. Tomamos la cantidad directamente de 'detalle'.
        command.Parameters.AddWithValue("@cantidad", detalle.Cantidad);

        command.ExecuteNonQuery();
    }
}
}


