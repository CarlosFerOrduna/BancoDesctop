using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WinFormsApp1;
using System.Data;
using System.Data.SqlClient;


public class Banco
{

    private List<Usuario> usuarios;
    private List<CajaDeAhorro> cajas;
    private List<PlazoFijo> plazos;
    private List<Tarjeta> tarjetas;
    private List<Pago> pagos;
    private List<Movimiento> movimientos;
    private DAL DB;
    private List<CajaAhorroUsuario> cajasUsuario;

    public Usuario? UsuarioActual { get; set; }

    public Banco()
    {
        usuarios = new List<Usuario>();
        cajas = new List<CajaDeAhorro>();
        plazos = new List<PlazoFijo>();
        tarjetas = new List<Tarjeta>();
        pagos = new List<Pago>();
        movimientos = new List<Movimiento>();
        cajasUsuario = new List<CajaAhorroUsuario>();
        DB = new DAL();
        InicializarAtributos();
    }

    // MOSTRAR DATOS

    public List<Usuario> MostrarUsuarios()
    {
        return usuarios.ToList();
    }

    public List<Usuario> MostrarUsuariosBloqueados()
    {
        List<Usuario> salida = new List<Usuario>();

        foreach (Usuario usuario in usuarios)
        {
            if (usuario.IsBloqueado)
            {
                salida.Add(usuario);
            }
        }

        return salida.ToList();
    }

    public List<CajaDeAhorro> MostrarCajasDeAhorroUsuarioActual()
    {
        return UsuarioActual.GetCajas().ToList();
    }

    public List<CajaDeAhorro> MostrarCajasDeAhorro()
    {
        return cajas.ToList();
    }

    public List<CajaDeAhorro> MostrarCajasDeAhorroByIdUsuario(int IdTitular)
    {
        List<CajaDeAhorro>? resultado = new List<CajaDeAhorro>();

        foreach (CajaDeAhorro caja in cajas)
        {
            if (caja.ContieneTitular(IdTitular))
            {
                resultado.Add(caja);
            }
        }

        return resultado;
    }

    public List<Movimiento> MostrarMovimientosUsuarioActual(int IdCajaAhorro)
    {
        foreach (CajaDeAhorro caja in UsuarioActual.GetCajas())
        {
            if (caja.IdCajaAhorro == IdCajaAhorro)
            {

                return caja.GetMovimientos().ToList();
            }
        }

        return null;
    }

    public List<Movimiento> MostrarMovimientos(int IdCajaAhorro)
    {
        foreach (CajaDeAhorro caja in UsuarioActual.GetCajas())
        {
            if (caja.IdCajaAhorro == IdCajaAhorro)
            {

                return caja.GetMovimientos().ToList();
            }
        }

        return null;
    }

    public List<Pago> MostrarPagosPendiente()
    {
        List<Pago> random = new List<Pago>();

        foreach (Pago pago in pagos)
        {
            if (!pago.IsPagado)
            {
                random.Add(pago);
            }
        }

        return random;
    }

    public List<Pago> MostrarPagosRealizado()
    {
        List<Pago> random = new List<Pago>();

        foreach (Pago pago in pagos)
        {
            if (pago.IsPagado)
            {
                random.Add(pago);
            }
        }

        return random;
    }

    public List<Pago> MostrarPagosPendienteByIdUsuario(int IdUsuario)
    {
        List<Pago> resultado = new List<Pago>();

        foreach (Pago pago in pagos)
        {
            if (!pago.IsPagado && pago.IdUsuario == IdUsuario)
            {
                resultado.Add(pago);
            }
        }

        return resultado;
    }

    public List<Pago> MostrarPagosRealizadoByIdUsuario(int IdUsuario)
    {
        List<Pago> resultado = new List<Pago>();

        foreach (Pago pago in pagos)
        {
            if (pago.IsPagado && pago.IdUsuario == IdUsuario)
            {
                resultado.Add(pago);
            }
        }

        return resultado;
    }

    public List<Pago> MostrarPagosPendienteUsuarioActual()
    {
        List<Pago> random = new List<Pago>();

        foreach (Pago pago in UsuarioActual.GetPagos())
        {
            if (!pago.IsPagado)
            {
                random.Add(pago);
            }
        }

        return random;
    }

    public List<Pago> MostrarPagosRealizadoUsuarioActual()
    {
        List<Pago> random = new List<Pago>();

        foreach (Pago pago in UsuarioActual.GetPagos())
        {
            if (pago.IsPagado)
            {
                random.Add(pago);
            }
        }

        return random;
    }

    public List<PlazoFijo> MostrarPlazosFijosUsuarioActual()
    {

        return UsuarioActual.GetPlazos().ToList();
    }

    public List<PlazoFijo> MostrarPlazosFijos()
    {

        return plazos.ToList();
    }

    public List<PlazoFijo> MostrarPlazosFijosByIdUsuario(int IdUsuario)
    {
        List<PlazoFijo> resultado = new List<PlazoFijo>();

        foreach (PlazoFijo plazoFijo in plazos)
        {
            if (plazoFijo.IdUsuario == IdUsuario)
            {
                resultado.Add(plazoFijo);
            }
        }

        return resultado;
    }

    public List<Tarjeta> MostrarTarjetasDeCreditoUsuarioActual()
    {

        return UsuarioActual.GetTarjetas().ToList();
    }

    public List<Tarjeta> MostrarTarjetasDeCredito()
    {

        return tarjetas.ToList();
    }

    public List<Tarjeta> MostrarTarjetasDeCreditoByIdUsuario(int IdUsuario)
    {
        List<Tarjeta> resultado = new List<Tarjeta>();

        foreach (Tarjeta tarjeta in tarjetas)
        {
            if (tarjeta.IdUsuario == IdUsuario)
            {
                resultado.Add(tarjeta);
            }
        }

        return resultado;
    }


    // METODOS COMPLEMENTARIOS 

    private Usuario? BuscarUsuarioPorDni(int Dni)
    {
        Usuario? resultado = null;

        foreach (Usuario usuario in usuarios)
        {

            if (usuario.Dni.Equals(Dni))
            {

                resultado = usuario;
            }
        }

        return resultado;
    }

    public Usuario? BuscarUsuarioPorId(int Id)
    {
        Usuario? resultado = null;
        foreach (Usuario usuario in usuarios)
        {

            if (usuario.IdUsuario.Equals(Id))
            {

                resultado = usuario;
            }
        }

        return resultado;
    }

    private bool ExisteEmail(string mail)
    {
        bool resultado = true;
        foreach (Usuario usuario in usuarios)
        {

            if (usuario.Dni.Equals(mail))
            {

                resultado = false;
            }
        }

        return resultado;
    }

    public CajaDeAhorro BuscarCajaPorIdUsuarioActual(int Id)
    {
        CajaDeAhorro resultado = null;
        foreach (CajaDeAhorro caja in UsuarioActual.GetCajas())
        {

            if (caja.IdCajaAhorro == Id)
            {

                resultado = caja;
            }
        }

        return resultado;
    }

    public CajaDeAhorro? BuscarCajaPorId(int Id)
    {
        CajaDeAhorro? resultado = null;
        foreach (CajaDeAhorro caja in cajas)
        {

            if (caja.IdCajaAhorro == Id)
            {

                resultado = caja;
            }
        }

        return resultado;
    }

    public CajaDeAhorro? BuscarCajaPorCbu(int Cbu)
    {
        CajaDeAhorro? resultado = null;
        foreach (CajaDeAhorro caja in cajas)
        {

            if (caja.Cbu == Cbu)
            {

                resultado = caja;
            }
        }

        return resultado;
    }

    public PlazoFijo? BuscarPlazoPorIdUsuarioActual(int id)
    {
        PlazoFijo? resultado = null;

        foreach (PlazoFijo plazo in UsuarioActual.GetPlazos())
        {
            if (plazo.IdPlazoFijo == id)
            {

                resultado = plazo;
            }
        }

        return resultado;
    }

    public PlazoFijo? BuscarPlazoPorId(int id)
    {
        PlazoFijo? resultado = null;

        foreach (PlazoFijo plazo in plazos)
        {
            if (plazo.IdPlazoFijo == id)
            {

                resultado = plazo;
            }
        }

        return resultado;
    }

    private Tarjeta? BuscarTarjetaPorId(int id)
    {
        Tarjeta? resultado = null;

        foreach (Tarjeta tarjeta in UsuarioActual.GetTarjetas())
        {
            if (tarjeta.IdTarjeta == id)
            {

                resultado = tarjeta;
            }
        }

        return resultado;
    }

    public Pago? BuscarPagoPorId(int id)
    {
        Pago? resultado = null;
        foreach (Pago pago in UsuarioActual.GetPagos())
        {
            if (pago.IdPago == id)
            {

                resultado = pago;
            }
        }

        return resultado;
    }

    public Usuario BuscarUsuarioBloqueado(string Nombre, int Dni)
    {
        Usuario? resultado = null;

        foreach (Usuario usuario in usuarios)
        {
            if (usuario.Nombre.Equals(Nombre) && usuario.Dni == Dni)
            {
                resultado = usuario;
            }
        }

        return resultado;
    }

    //OPERACIONES DEL USUARIO

    public Usuario IniciarSesion(string Usuario, int Dni, string Clave)
    {
        Usuario? usuarioResultado = null;

        foreach (Usuario usuario in usuarios)
        {
            if (usuario.Nombre.Equals(Usuario))
            {
                if (usuario.Dni == Dni)
                {
                    if (!usuario.IsBloqueado)
                    {
                        if (usuario.Clave.Equals(Clave))
                        {
                            UsuarioActual = usuario;
                            usuario.IntentosFallidos = 0;

                            usuarioResultado = usuario;
                        }
                        else
                        {
                            usuario.IntentosFallidos++;
                            if (usuario.IntentosFallidos == 3)
                            {
                                usuario.IsBloqueado = true;
                                DB.BloquearUsuario(usuario.IdUsuario);
                            }
                        }
                    }
                }
            }
        }
        return usuarioResultado;
    }

    public bool DesbloquearUsuario(int IdUsuario)
    {
        bool resultado = false;

        if (DB.DesbloquearUsuario(IdUsuario) > 0)
        {
            resultado = true;
        }

        return resultado;
    }

    public void CerrarSesion()
    {
        this.UsuarioActual = null;
    }

    public List<Movimiento> BuscarMovimiento(int id, string? detalle, DateTime? fecha, double? monto)
    {//ver, como filtra con or si pones varias cosas como fecha y monto, si uno falla te lo trae igual

        List<Movimiento> resultado = new List<Movimiento>();
        CajaDeAhorro cajaDeAhorro = BuscarCajaPorIdUsuarioActual(id);

        foreach (Movimiento movimiento in cajaDeAhorro.GetMovimientos())
        {
            if (movimiento.Detalle.Equals(detalle) || movimiento.Fecha == fecha || movimiento.Monto == monto)
            {
                resultado.Add(movimiento);
            }
        }

        return resultado;
    }



    public bool CrearCajaAhorro(int IdUsuario, double Saldo)
    {

        int Cbu = CrearCbu();
        Usuario? usuario = BuscarUsuarioPorId(IdUsuario);

        foreach (CajaDeAhorro caja in cajas)
        {
            if (caja.Cbu != Cbu)
            {

                int idNuevaCajaDeUsuario = DB.CrearCajaAhorro(IdUsuario, Cbu, Saldo);
                if (idNuevaCajaDeUsuario != -1)
                {

                    CajaDeAhorro nuevaCaja = new CajaDeAhorro(idNuevaCajaDeUsuario, Cbu, Saldo);
                    cajas.Add(nuevaCaja);
                    usuario.AddCajaDeAhorro(nuevaCaja);
                    return true;

                }

            }
        }
        return false;
    }


    //----------------------------------- TP 2 ----------------------------------------

    private void InicializarAtributos()
    {
        usuarios = DB.InicializarUsuarios();
        cajas = DB.InicializarCajasAhorro();
        plazos = DB.InicializarPlazosFijo();
        tarjetas = DB.InicializarTarjetas();
        pagos = DB.InicializarPagos();
        movimientos = DB.InicializarMovimientos();
        cajasUsuario = DB.InicializarCajaAhorroUsuarios();

        foreach (Tarjeta tarjeta in tarjetas)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.IdUsuario == tarjeta.IdUsuario)
                {
                    usuario.AddTarjeta(tarjeta);
                }
            }
        }

        foreach (PlazoFijo plazoFijo in plazos)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.IdUsuario == plazoFijo.IdUsuario)
                {
                    usuario.AddPlazoFijo(plazoFijo);
                }
            }
        }

        foreach (Pago pago in pagos)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.IdUsuario == pago.IdUsuario)
                {
                    usuario.AddPago(pago);
                }
            }
        }

        foreach (Movimiento movimiento in movimientos)
        {
            foreach (CajaDeAhorro cajaDeAhorro in cajas)
            {
                if (cajaDeAhorro.IdCajaAhorro == movimiento.IdCajaAhorro)
                {
                    cajaDeAhorro.AddMovimiento(movimiento);
                }
            }
        }

        foreach (CajaAhorroUsuario cajaDeAhorroUsuario in cajasUsuario)
        {
            foreach (CajaDeAhorro cajaDeAhorro in cajas)
            {
                foreach (Usuario usuario in usuarios)
                {
                    if (cajaDeAhorroUsuario.IdCajaAhorro == cajaDeAhorro.IdCajaAhorro && cajaDeAhorroUsuario.IdUsuario == usuario.IdUsuario)
                    {
                        cajaDeAhorro.AddTitular(usuario);
                        usuario.AddCajaDeAhorro(cajaDeAhorro);
                    }
                }
            }
        }
    }

    public bool AltaDeUsuario(int Dni, string Nombre, string Apellido, string Mail, string Clave, bool IsAdmin, bool Bloqueado)
    {
        //comprobación para que no me agreguen usuarios con DNI duplicado
        bool esValido = true;

        foreach (Usuario u in usuarios)
        {
            if (u.Dni == Dni)
                esValido = false;
        }
        if (esValido)
        {

            int idNuevoUsuario = DB.AltaUsuario(Dni, Nombre, Apellido, Mail, Clave, IsAdmin, Bloqueado);
            if (idNuevoUsuario != -1)
            {
                //Ahora sí lo agrego en la lista
                Usuario nuevo = new Usuario(idNuevoUsuario, Dni, Nombre, Apellido, Mail, Clave, IsAdmin, Bloqueado);
                usuarios.Add(nuevo);
                return true;
            }
            else
            {
                //algo salió mal con la query porque no generó un id válido
                return false;
            }
        }
        else
            return false;
    }

    public bool ModificarUsuarioAdmin(int Id, int Dni, string Nombre, string Apellido, string Mail, string Clave, bool Admin, bool Bloqueado)
    {
        //primero me aseguro que lo pueda agregar a la base
        if (DB.ModificarUsuario(Id, Dni, Nombre, Apellido, Mail, Clave, Admin, Bloqueado) == 1)
        {
            try
            {
                //Ahora sí lo MODIFICO en la lista
                for (int i = 0; i < usuarios.Count; i++)
                    if (usuarios[i].IdUsuario == Id)
                    {
                        usuarios[i].Dni = Dni;
                        usuarios[i].Nombre = Nombre;
                        usuarios[i].Apellido = Apellido;
                        usuarios[i].Mail = Mail;
                        usuarios[i].Clave = Clave;
                        usuarios[i].IsAdmin = Admin;

                    }
                    else if (usuarios[i].IdUsuario == Id && usuarios[i].IsAdmin == true)
                    {
                        usuarios[i].IsBloqueado = Bloqueado;

                    }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            //algo salió mal con la query porque no generó 1 registro
            return false;
        }
    }

    public bool ModificarUsuarioUsuarioActual(int Dni, string Nombre, string Apellido, string Mail, string Clave)
    {
        int Id = UsuarioActual.IdUsuario;
        bool Admin = UsuarioActual.IsAdmin;
        bool Bloqueado = UsuarioActual.IsBloqueado;

        if (Dni == 0)
        {
            Dni = UsuarioActual.Dni;
        }

        if (Nombre.Equals(""))
        {
            Nombre = UsuarioActual.Nombre;
        }

        if (Apellido.Equals(""))
        {
            Apellido = UsuarioActual.Apellido;
        }

        if (Mail.Equals(""))
        {
            Mail = UsuarioActual.Mail;
        }

        if (Clave.Equals(""))
        {
            Clave = UsuarioActual.Clave;
        }

        //primero me aseguro que lo pueda agregar a la base
        if (DB.ModificarUsuario(Id, Dni, Nombre, Apellido, Mail, Clave, Admin, Bloqueado) == 1)
        {
            try
            {
                //Ahora sí lo MODIFICO en la lista
                for (int i = 0; i < usuarios.Count; i++)
                    if (usuarios[i].IdUsuario == Id)
                    {
                        usuarios[i].Dni = Dni;
                        usuarios[i].Nombre = Nombre;
                        usuarios[i].Apellido = Apellido;
                        usuarios[i].Mail = Mail;
                        usuarios[i].Clave = Clave;
                        usuarios[i].IsAdmin = Admin;

                    }
                    else if (usuarios[i].IdUsuario == Id && usuarios[i].IsAdmin == true)
                    {
                        usuarios[i].IsBloqueado = Bloqueado;

                    }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            //algo salió mal con la query porque no generó 1 registro
            return false;
        }
    }

    public bool EliminarUsuarioAdmin(int Id)
    {
        bool resultado = false;
        bool resultado1 = false;
        bool resultado2 = false;
        bool resultado3 = false;
        bool resultado4 = false;
        Usuario? usuario = BuscarUsuarioPorId(Id);

        //primero me aseguro que lo pueda eliminar a la base
        if (DB.EliminarUsuario(Id) == 1)
        {
            try
            {
                foreach (CajaDeAhorro caja in usuario.GetCajas())
                {
                    if (usuario.GetCajas().Count == 0)
                    {
                        BajaCajaAhorro(caja.IdCajaAhorro);
                        resultado1 = true;
                    }
                }

                foreach (PlazoFijo plazoFijos in usuario.GetPlazos())
                {
                    if (usuario.GetPlazos().Count == 0)
                    {
                        BajaPlazoFijoUsuarioActual(plazoFijos.IdPlazoFijo);
                        resultado2 = true;
                    }
                }

                foreach (Tarjeta tarjetas in usuario.GetTarjetas())
                {
                    if (usuario.GetTarjetas().Count == 0)
                    {
                        BajaTarjetaCredito(tarjetas.IdTarjeta);
                        resultado3 = true;
                    }
                }

                foreach (Pago pagos in usuario.GetPagos())
                {
                    if (usuario.GetPagos().Count == 0)
                    {
                        BajaPagoUsuarioActual(pagos.IdPago);
                        resultado4 = true;
                    }
                }

                if (resultado1 && resultado2 && resultado3 && resultado4)
                {
                    usuarios.Remove(usuario);
                    resultado = true;
                }

            }
            catch (Exception)
            {
                return resultado;
            }
        }
        return resultado;
    }

    public bool EliminarUsuarioUsuarioActual()
    {
        bool resultado = false;
        bool resultado1 = false;
        bool resultado2 = false;
        bool resultado3 = false;
        bool resultado4 = false;

        //primero me aseguro que lo pueda eliminar a la base
        if (DB.EliminarUsuario(UsuarioActual.IdUsuario) == 1)
        {
            try
            {
                foreach (CajaDeAhorro caja in UsuarioActual.GetCajas())
                {
                    if (UsuarioActual.GetCajas().Count == 0)
                    {
                        BajaCajaAhorro(caja.IdCajaAhorro);
                        resultado1 = true;
                    }
                    resultado1 = false;
                }

                foreach (PlazoFijo plazoFijos in UsuarioActual.GetPlazos())
                {
                    if (UsuarioActual.GetPlazos().Count == 0)
                    {
                        BajaPlazoFijoUsuarioActual(plazoFijos.IdPlazoFijo);
                        resultado2 = true;
                    }
                    resultado2 = false;
                }

                foreach (Tarjeta tarjetas in UsuarioActual.GetTarjetas())
                {
                    if (UsuarioActual.GetTarjetas().Count == 0)
                    {
                        BajaTarjetaCredito(tarjetas.IdTarjeta);
                        resultado3 = true;
                    }
                    resultado3 = false;
                }

                foreach (Pago pagos in UsuarioActual.GetPagos())
                {
                    if (UsuarioActual.GetPagos().Count == 0)
                    {
                        BajaPagoUsuarioActual(pagos.IdPago);
                        resultado4 = true;
                    }
                    resultado4 = false;
                }

                if (resultado1 && resultado2 && resultado3 && resultado4)
                {
                    usuarios.Remove(UsuarioActual);
                    resultado = true;
                }

                return false;
            }
            catch (Exception)
            {
                return resultado;
            }
        }
        return resultado;
    }

    public bool AltaCajaAhorro()
    {
        double Saldo = 0;
        int cbuNuevaCaja = CrearCbu();
        int idNuevoCaja = DB.AltaCajaAhorro(cbuNuevaCaja, Saldo);

        if (idNuevoCaja != -1)
        {

            CajaDeAhorro nuevaCaja = new CajaDeAhorro(idNuevoCaja, cbuNuevaCaja, Saldo);
            cajas.Add(nuevaCaja);
            UsuarioActual.AddCajaDeAhorro(nuevaCaja);

            int idCajaAhorroUsuario = DB.AltaUsuarioCajaAhorro(UsuarioActual.IdUsuario, nuevaCaja.IdCajaAhorro);

            CajaAhorroUsuario cajaAhorroUsuario = new CajaAhorroUsuario(idCajaAhorroUsuario, UsuarioActual.IdUsuario, nuevaCaja.IdCajaAhorro);
            cajasUsuario.Add(cajaAhorroUsuario);

            return true;
        }

        return false;
    }

    public bool ModificarCajaAhorro(int DniUsuario, int IdCajaAhorro, string accion)
    {
        bool resultado = false;

        CajaDeAhorro? caja = BuscarCajaPorId(IdCajaAhorro);
        Usuario? titular = BuscarUsuarioPorDni(DniUsuario);

        int IdUsuario = titular.IdUsuario;

        if (accion.Equals("agregar") && DB.AgregarTitularCajaAhorro(IdUsuario, IdCajaAhorro) == 1)
        {

            caja.AddTitular(titular);
            titular.AddCajaDeAhorro(caja);

            resultado = true;
        }
        else if (accion.Equals("eliminar") && caja.GetTitular().Count > 1 && DB.EliminarTitularCajaAhorro(IdUsuario, IdCajaAhorro) == 1)
        {
            caja.RemoveTitular(titular);
            titular.RemoveCajaDeAhorro(caja);

            resultado = true;
        }
        return resultado;
    }

    public bool BajaCajaAhorro(int Id)
    {
        //primero me aseguro que lo pueda eliminar a la base
        CajaDeAhorro? caja = BuscarCajaPorIdUsuarioActual(Id);

        if (DB.EliminarCajaAhorro(Id) == 1)
        {
            try
            {
                //Ahora sí lo elimino en la lista
                for (int i = 0; i < usuarios.Count; i++)
                {
                    if (usuarios[i].IdUsuario == Id)
                    {

                        if (caja.Saldo == 0)
                        {
                            cajas.Remove(caja);
                            UsuarioActual.RemoveCajaDeAhorro(caja);
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            //algo salió mal con la query porque no generó 1 registro
            return false;
        }
    }

    public bool AltaPagoUsuarioActual(string Detalle, double Monto, bool IsPagado, string Metodo)
    {
        Usuario? usuario = UsuarioActual;
        bool resultado = false;

        int idNuevoPago = DB.AltaPago(Detalle, Monto, IsPagado, Metodo, UsuarioActual.IdUsuario);
        if (idNuevoPago != -1)
        {
            //Ahora sí lo agrego en la lista
            Pago nuevo = new Pago(idNuevoPago, Detalle, Monto, IsPagado, Metodo, usuario.IdUsuario);
            pagos.Add(nuevo);
            UsuarioActual.AddPago(nuevo);
            resultado = true;
        }
        return resultado;

    }

    public bool AltaPago(int IdUsuario, string Detalle, double Monto, bool IsPagado, string Metodo)
    {
        bool resultado = false;
        Usuario? usuario = BuscarUsuarioPorId(IdUsuario);

        int idNuevoPago = DB.AltaPago(Detalle, Monto, IsPagado, Metodo, IdUsuario);
        if (idNuevoPago != -1)
        {
            //Ahora sí lo agrego en la lista
            Pago nuevo = new Pago(idNuevoPago, Detalle, Monto, IsPagado, Metodo, IdUsuario);
            pagos.Add(nuevo);
            usuario.AddPago(nuevo);
            resultado = true;
        }

        return resultado;
    }

    public bool ModificarPago(int IdPago, string Detalle, double Monto, bool IsPagado, string Metodo)
    {
        //primero me aseguro que lo pueda agregar a la base
        if (DB.ModificarPago(IdPago, Detalle, Monto, IsPagado, Metodo) == 1)
        {
            try
            {
                //Ahora sí lo MODIFICO en la lista
                for (int i = 0; i < pagos.Count; i++)
                    if (pagos[i].IdPago == IdPago)
                    {
                        pagos[i].IsPagado = IsPagado;

                    }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            //algo salió mal con la query porque no generó 1 registro
            return false;
        }
    }


    public bool BajaPagoUsuarioActual(int Id)
    {

        bool resultado = false;

        if (DB.BajaPago(Id) == 1)
        {
            foreach (Pago pago in UsuarioActual.GetPagos())
            {
                if (pago.IdPago == Id)
                {
                    if (pago.IsPagado)
                    {
                        UsuarioActual.RemovePago(pago);
                        pagos.Remove(pago);
                        resultado = true;
                    }
                }
            }
        }

        return resultado;
    }

    public bool BajaPago(int IdPago)
    {

        bool resultado = false;

        if (DB.BajaPago(IdPago) == 1)
        {
            foreach (Pago pago in pagos)
            {
                if (pago.IdPago == IdPago)
                {
                    if (pago.IsPagado)
                    {
                        pagos.Remove(pago);
                        resultado = true;
                    }
                }
            }
        }
        return resultado;
    }

    public bool AltaPlazoFijoUsuarioActual(int IdCajaAhorro, double Monto, DateTime FechaIni, DateTime FechaFin, double Tasa, bool IsPagado)
    {
        bool resultado = false;
        string DetalleMovimiento = "Alta Plazo Fijo";
        int idPlazoFijo = DB.AltaPlazoFijo(Monto, FechaIni, FechaFin, Tasa, IsPagado, UsuarioActual.IdUsuario);

        PlazoFijo plazoFijo;
        CajaDeAhorro? cajaAPagar = BuscarCajaPorIdUsuarioActual(IdCajaAhorro);

        if (cajaAPagar.Saldo >= Monto && idPlazoFijo != -1)
        {
            plazoFijo = new PlazoFijo(idPlazoFijo, Monto, FechaFin, FechaFin, Tasa, IsPagado, UsuarioActual.IdUsuario);

            plazos.Add(plazoFijo);
            UsuarioActual.AddPlazoFijo(plazoFijo);

            Movimiento movimiento = AltaMovimiento(DetalleMovimiento, Monto, cajaAPagar.IdCajaAhorro);

            cajaAPagar.Saldo -= Monto;

            cajaAPagar.AddMovimiento(movimiento);

            resultado = true;
        }

        return resultado;
    }

    public bool AltaPlazoFijo(int Id, double Monto, DateTime FechaIni, DateTime FechaFin, double Tasa, bool IsPagado, int IdUsuario)
    {
        bool resultado = false;
        string DetalleMovimiento = "Alta Plazo Fijo";
        int idPlazoFijo = DB.AltaPlazoFijo(Monto, FechaIni, FechaFin, Tasa, IsPagado, IdUsuario);

        PlazoFijo plazoFijo;
        CajaDeAhorro? cajaAPagar = BuscarCajaPorIdUsuarioActual(Id);

        if (cajaAPagar.Saldo >= Monto && idPlazoFijo != -1)
        {
            plazoFijo = new PlazoFijo(idPlazoFijo, Monto, FechaFin, FechaFin, Tasa, IsPagado, IdUsuario);

            plazos.Add(plazoFijo);
            UsuarioActual.AddPlazoFijo(plazoFijo);

            Movimiento movimiento = AltaMovimiento(DetalleMovimiento, Monto, cajaAPagar.IdCajaAhorro);

            cajaAPagar.Saldo -= Monto;

            cajaAPagar.AddMovimiento(movimiento);

            resultado = true;
        }

        return resultado;
    }

    public bool BajaPlazoFijoUsuarioActual(int Id)
    {
        bool resultado = false;
        PlazoFijo? plazoFijo = BuscarPlazoPorIdUsuarioActual(Id);

        if (plazoFijo.IsPagado)
        {

            TimeSpan comparacionDeFechas = DateTime.Now.Subtract(plazoFijo.FechaFin);

            if (comparacionDeFechas.Days >= 30)
            {

                if (DB.BajaPlazoFijo(Id) == 1)
                {
                    plazos.Remove(plazoFijo);
                    UsuarioActual.RemovePlazoFijo(plazoFijo);

                    resultado = true;

                }
            }
        }

        return resultado;
    }

    public bool BajaPlazoFijo(int Id)
    {
        bool resultado = false;
        PlazoFijo? plazoFijo = BuscarPlazoPorId(Id);

        if (plazoFijo.IsPagado)
        {

            TimeSpan comparacionDeFechas = DateTime.Now.Subtract(plazoFijo.FechaFin);

            if (comparacionDeFechas.Days >= 30)
            {

                if (DB.BajaPlazoFijo(Id) == 1)
                {
                    plazos.Remove(plazoFijo);
                    UsuarioActual.RemovePlazoFijo(plazoFijo);

                    resultado = true;

                }
            }
        }

        return resultado;
    }

    public bool AltaTarjetaCreditoUsuarioActual()
    {
        bool resultado = false;
        Random codigoV = new Random();
        int numeroTarjeta = CrearNumeroTarjeta();
        int codigoTarjeta = codigoV.Next(100, 999);
        double limiteTarjeta = 25000;
        double consumoTarjeta = 0;

        int idTarjeta = DB.AltaTarjetaCredito(numeroTarjeta, codigoTarjeta, limiteTarjeta, consumoTarjeta, UsuarioActual.IdUsuario);

        if (idTarjeta != -1)
        {

            Tarjeta nuevaTarjeta = new Tarjeta(idTarjeta, numeroTarjeta, codigoTarjeta, limiteTarjeta, consumoTarjeta, UsuarioActual.IdUsuario);
            UsuarioActual.AddTarjeta(nuevaTarjeta);
            tarjetas.Add(nuevaTarjeta);

            resultado = true;
        }

        return resultado;
    }

    public bool AltaTarjetaCredito(int IdUsuario)
    {
        bool resultado = false;
        Random codigoV = new Random();
        int numeroTarjeta = CrearNumeroTarjeta();
        int codigoTarjeta = codigoV.Next(100, 999);
        double limiteTarjeta = 25000;
        double consumoTarjeta = 0;

        int idTarjeta = DB.AltaTarjetaCredito(numeroTarjeta, codigoTarjeta, limiteTarjeta, consumoTarjeta, IdUsuario);

        if (idTarjeta != -1)
        {

            Tarjeta nuevaTarjeta = new Tarjeta(idTarjeta, numeroTarjeta, codigoTarjeta, limiteTarjeta, consumoTarjeta, IdUsuario);
            UsuarioActual.AddTarjeta(nuevaTarjeta);
            tarjetas.Add(nuevaTarjeta);

            resultado = true;
        }

        return resultado;
    }

    public bool ModificarTarjetaCredito(int IdTarjeta, double NuevoLimite)
    {
        bool resultado = false;
        Tarjeta? tarjeta = BuscarTarjetaPorId(IdTarjeta);

        if (DB.ModificarTarjetaCredito(tarjeta.IdTarjeta, tarjeta.Numero, tarjeta.CodigoV, NuevoLimite, tarjeta.Consumos) == 1)
        {
            tarjeta.Limite = NuevoLimite;

            resultado = true;
        }

        return resultado;
    }

    public bool BajaTarjetaCredito(int Id)
    {
        bool resultado = false;
        Tarjeta? tarjeta = BuscarTarjetaPorId(Id);

        if (UsuarioActual.BajaTarjetaCredito(Id) && DB.BajaTarjetaCredito(Id) == 1)
        {

            tarjetas.Remove(tarjeta);

            resultado = true;
        }

        return resultado;
    }

    public bool Depositar(int Id, double Monto)
    {

        bool resultado = false;
        CajaDeAhorro? cajaUsuario = BuscarCajaPorIdUsuarioActual(Id);
        string detalleMovimiento = "Deposito";

        if (Monto > 0)
        {
            if (DB.Depositar(cajaUsuario.IdCajaAhorro, Monto) == 1)
            {

                cajaUsuario.Saldo += Monto;

                Movimiento movimiento = AltaMovimiento(detalleMovimiento, Monto, cajaUsuario.IdCajaAhorro);
                movimientos.Add(movimiento);
                cajaUsuario.AddMovimiento(movimiento);

                resultado = true;
            }
        }

        return resultado;
    }

    public bool Retirar(int Id, double Monto)
    {
        bool resultado = false;
        CajaDeAhorro? cajaUsuario = BuscarCajaPorIdUsuarioActual(Id);
        string detalleMovimiento = "Retiro";

        if (cajaUsuario.Saldo >= Monto)
        {
            if (DB.Retirar(cajaUsuario.IdCajaAhorro, Monto) == 1)
            {

                cajaUsuario.Saldo -= Monto;

                Movimiento movimiento = AltaMovimiento(detalleMovimiento, Monto, cajaUsuario.IdCajaAhorro);
                movimientos.Add(movimiento);
                cajaUsuario.AddMovimiento(movimiento);

                resultado = true;
            }
        }

        return resultado;
    }

    public bool Transferir(int IdCajaOrigen, int CbuCajaDestino, double Monto)
    {
        bool resultado = false;
        CajaDeAhorro? cajaOrigen = BuscarCajaPorIdUsuarioActual(IdCajaOrigen);
        CajaDeAhorro? cajaDestino = BuscarCajaPorCbu(CbuCajaDestino);
        string detalleMovimiento = "Transferir";
        Movimiento movimientoCajaOrigen = AltaMovimiento(detalleMovimiento, Monto, cajaOrigen.IdCajaAhorro);
        Movimiento movimientoCajaDestino = AltaMovimiento(detalleMovimiento, Monto, cajaDestino.IdCajaAhorro);

        if (cajaDestino != null && cajaOrigen.Saldo >= Monto)
        {
            if (DB.Transferir(cajaOrigen.IdCajaAhorro, cajaDestino.IdCajaAhorro, Monto) == 2)
            {
                cajaOrigen.Saldo -= Monto;
                cajaDestino.Saldo += Monto;

                cajaOrigen.AddMovimiento(movimientoCajaOrigen);
                cajaDestino.AddMovimiento(movimientoCajaDestino);
                movimientos.Add(movimientoCajaOrigen);
                movimientos.Add(movimientoCajaDestino);

                resultado = true;
            }
        }

        return resultado;
    }

    public bool PagarTarjeta(int IdTarjeta, int IdCajaAhorro)
    {

        bool resultado = false;

        CajaDeAhorro caja = UsuarioActual.BuscarCajaPorId(IdCajaAhorro);
        Tarjeta tarjeta = UsuarioActual.BuscarTarjetaPorId(IdTarjeta);

        string detalleMovimiento = "Pago Tarjeta";

        Movimiento movimiento = AltaMovimiento(detalleMovimiento, tarjeta.Consumos, caja.IdCajaAhorro);

        if (caja.Saldo >= tarjeta.Consumos)
        {
            if (DB.PagarTarjeta(IdTarjeta, IdCajaAhorro, tarjeta.Consumos) == 2)
            {
                caja.Saldo -= tarjeta.Consumos;
                tarjeta.Consumos = 0;

                caja.AddMovimiento(movimiento);
                movimientos.Add(movimiento);

                resultado = true;
            }
        }

        return resultado;
    }

    public bool PagarPago(int IdCajaAhorro, int IdTarjetaCredito, int IdPagoActual)
    {
        bool resultado = false;
        CajaDeAhorro? cajaActual = UsuarioActual.BuscarCajaPorId(IdCajaAhorro);
        Pago PagoActual = UsuarioActual.BuscarPagoPorId(IdPagoActual);
        Tarjeta? tarjeta = UsuarioActual.BuscarTarjetaPorId(IdTarjetaCredito);



        if (IdTarjetaCredito != 0 && tarjeta.Limite >= (tarjeta.Consumos + PagoActual.Monto))
        {
            if (DB.PagarPagoConTarjeta(tarjeta.IdTarjeta, PagoActual.IdPago, PagoActual.Monto) == 2)
            {
                string detalle = "Tarjeta";

                ModificarPago(PagoActual.IdPago, PagoActual.Detalle, PagoActual.Monto, true, detalle);

                tarjeta.Consumos += PagoActual.Monto;

                resultado = true;
            }
        }
        else if (IdCajaAhorro != 0 && cajaActual.Saldo >= PagoActual.Monto)
        {
            if (DB.PagarPagoConCajaAhorro(cajaActual.IdCajaAhorro, PagoActual.IdPago, PagoActual.Monto) == 2)
            {
                string detalle = "Caja de Ahorro";

                ModificarPago(PagoActual.IdPago, PagoActual.Detalle, PagoActual.Monto, true, detalle);

                cajaActual.Saldo -= PagoActual.Monto;

                string detalleMovimiento = "Pagar pago";
                Movimiento movimiento = AltaMovimiento(detalleMovimiento, PagoActual.Monto, cajaActual.IdCajaAhorro);
                cajaActual.AddMovimiento(movimiento);
                movimientos.Add(movimiento);

                resultado = true;
            }
        }


        return resultado;
    }

    public int CrearCbu()
    {
        int cbu = 100;

        if (DB.GetMaxCbu() > 99)
        {
            cbu = DB.GetMaxCbu() + 1;
        }

        return cbu;
    }

    public int CrearNumeroTarjeta()
    {
        int numeroTarjeta = 10000000;

        if (DB.GetMaxNumeroTarjeta() > 9999999)
        {
            numeroTarjeta = DB.GetMaxNumeroTarjeta() + 1;
        }

        return numeroTarjeta;
    }

    public Movimiento AltaMovimiento(string DetalleMovimiento, double Monto, int IdCajaAhorro)
    {
        DateTime FechaMovimiento = DateTime.Now.Date;

        int IdMovimiento = DB.AltaMovimiento(DetalleMovimiento, Monto, FechaMovimiento, IdCajaAhorro);

        Movimiento nuevoMovimiento = new Movimiento(IdMovimiento, DetalleMovimiento, Monto, FechaMovimiento, IdCajaAhorro);

        movimientos.Add(nuevoMovimiento);

        return nuevoMovimiento;
    }


}