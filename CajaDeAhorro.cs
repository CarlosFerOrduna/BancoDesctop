using WinFormsApp1;

public class CajaDeAhorro
{
    public int IdCajaAhorro { get; set; }
    public int Cbu { get; set; }
    public double Saldo { get; set; }

    public List<Usuario> Titulares { get; set; }
    public List<Movimiento> Movimientos { get; set; }

    public CajaDeAhorro(int id, int cbu, double saldo)
    {
        IdCajaAhorro = id;
        Cbu = cbu;
        Saldo = saldo;
        Titulares = new List<Usuario>();
        Movimientos = new List<Movimiento>();
    }

    public void AddMovimiento(Movimiento nuevoMovimiento)
    {
        Movimientos.Add(nuevoMovimiento);
    }


    public List<Movimiento> GetMovimientos()
    {
        return Movimientos.ToList();
    }

    public void AddTitular(Usuario nuevoTitular)
    {
        Titulares.Add(nuevoTitular);
    }

    public List<Usuario> GetTitular()
    {
        return Titulares.ToList();
    }

    public void RemoveTitular(Usuario titular)
    {
        Titulares.Remove(titular);
    }

    public bool ContieneTitular(int IdTitular)
    {
        bool resultado = false;

        foreach (Usuario titular in Titulares)
        {
            if (titular.IdUsuario == IdTitular)
            {
                resultado = true;
            }
        }
        return resultado;
    }
}