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
    public Productos(string? descripcion,double precio)
    {
        IdProducto = 0;
        this.descripcion = descripcion;
        this.precio = precio;
    }

    public double Precio { get => precio; set => precio = value; }
    public int IdProducto { get => idProducto; set => idProducto = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }
}