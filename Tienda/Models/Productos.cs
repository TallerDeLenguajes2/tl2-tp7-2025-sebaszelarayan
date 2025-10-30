namespace  Tienda.Models;
public class Productos
{
    private int idProducto;
    private string? descripcion;
    private double precio;

    public Productos()
    {
        IdProducto = 0;
        Descripcion = "";
        precio = 0;
    }

    public double Precio { get => precio; set => precio = value; }
    public int IdProducto { get => idProducto; set => idProducto = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }
}