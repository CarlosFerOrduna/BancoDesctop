public class Movimiento
{
    public int IdMovimiento { get; set; }
    public string Detalle { get; set; }
    public double Monto { get; set; }
    public DateTime Fecha { get; set; }
    public int IdCajaAhorro { get; set; }

    public Movimiento(int idMovimiento, string detalle, double monto, DateTime fecha, int idCaja)
    {
        IdMovimiento = idMovimiento;
        Detalle = detalle;
        Monto = monto;
        Fecha = fecha;
        IdCajaAhorro = idCaja;
    }
}