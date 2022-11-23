using WinFormsApp1;

public class PlazoFijo
{
    public int IdPlazoFijo { get; set; }
    public double Monto { get; set; }
    public DateTime FechaIni { get; set; }
    public DateTime FechaFin { get; set; }
    public double Tasa { get; set; }
    public bool IsPagado { get; set; }
    public int IdUsuario { get; set; }


    public PlazoFijo(int id, double monto, DateTime fechaIni, DateTime fechaFin, double tasa, bool isPagado, int idUsuario)
    {
        IdPlazoFijo = id;
        Monto = monto;
        Tasa = tasa;
        FechaIni = fechaIni;
        FechaFin = fechaFin;
        IsPagado = isPagado;
        IdUsuario = idUsuario;
    }
}