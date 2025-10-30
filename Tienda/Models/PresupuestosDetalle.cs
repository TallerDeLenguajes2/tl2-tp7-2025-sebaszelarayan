namespace  Tienda.Models;
public class PresupuestosDetalle
{
    private Productos? producto;
    private int cantidad;

    public PresupuestosDetalle()
    {
        cantidad = 0;
        producto = new Productos();
    }

    public Productos? Producto { get => producto; set => producto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
}