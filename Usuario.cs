public class Usuario
{

    public int IdUsuario { get; set; }
    public int Dni { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Mail { get; set; }
    public string Clave { get; set; }
    public int IntentosFallidos { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsBloqueado { get; set; }

    private List<CajaDeAhorro> cajas;
    private List<PlazoFijo> plazosFijos;
    private List<Tarjeta> tarjetas;
    private List<Pago> pagos;




    public Usuario(int id, int dni, string nombre, string apellido, string mail, string clave, bool isAdmin, bool isBloqueado)
    {
        IdUsuario = id;
        Dni = dni;
        Nombre = nombre;
        Apellido = apellido;
        Mail = mail;
        Clave = clave;
        IsAdmin = isAdmin;
        IsBloqueado = isBloqueado;
        cajas = new List<CajaDeAhorro>();
        tarjetas = new List<Tarjeta>();
        pagos = new List<Pago>();
        plazosFijos = new List<PlazoFijo>();

    }

    public void AddPago(Pago pago)
    {

        this.pagos.Add(pago);

    }

    public void RemovePago(Pago pago)
    {

        this.pagos.Remove(pago);

    }

    public void AddPlazoFijo(PlazoFijo plazoFijo)
    {

        this.plazosFijos.Add(plazoFijo);



    }
    public void RemovePlazoFijo(PlazoFijo plazoFijo)
    {

        this.plazosFijos.Remove(plazoFijo);

    }

    public void AddCajaDeAhorro(CajaDeAhorro cajaDeAhorro)
    {

        this.cajas.Add(cajaDeAhorro);

    }

    public void RemoveCajaDeAhorro(CajaDeAhorro cajaDeAhorro)
    {

        this.cajas.Remove(cajaDeAhorro);

    }


    public void AddTarjeta(Tarjeta tarjeta)
    {

        this.tarjetas.Add(tarjeta);

    }

    public void RemoveTarjeta(Tarjeta tarjeta)
    {

        this.tarjetas.Remove(tarjeta);

    }

    public List<Pago> GetPagos()
    {
        return pagos.ToList();
    }

    public List<CajaDeAhorro> GetCajas()
    {
        return cajas.ToList();
    }


    public List<PlazoFijo> GetPlazos()
    {
        return plazosFijos.ToList();
    }


    public List<Tarjeta> GetTarjetas()
    {
        return tarjetas.ToList();
    }

    public Tarjeta? BuscarTarjetaPorId(int Id)
    {
        Tarjeta? resultado = null;
        foreach (Tarjeta tarjeta in tarjetas)
        {

            if (tarjeta.IdTarjeta.Equals(Id))
            {
                resultado = tarjeta;

            }
        }
        return resultado;
    }

    public CajaDeAhorro? BuscarCajaPorId(int id)
    {
        CajaDeAhorro? resultado = null;
        foreach (CajaDeAhorro caja in cajas)
        {

            if (caja.IdCajaAhorro == id)
            {
                resultado = caja;

            }
        }
        return resultado;
    }

    public Pago? BuscarPagoPorId(int id)
    {
        Pago? resultado = null;
        foreach (Pago pago in pagos)
        {

            if (pago.IdPago == id)
            {
                resultado = pago;

            }
        }
        return resultado;
    }


    public bool BajaTarjetaCredito(int id)
    {
        bool resultado = false;

        foreach (Tarjeta tarjeta in tarjetas)
        {
            if (tarjeta.IdTarjeta == id && tarjeta.Consumos == 0)
            {

                this.RemoveTarjeta(tarjeta);

                resultado = true;
                break;
            }

        }

        return resultado;
    }
}
