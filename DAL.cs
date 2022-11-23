using System.Data;
using System.Data.SqlClient;

namespace WinFormsApp1
{
    public class DAL
    {

        private string connectionString;
        public DAL()
        {
            connectionString = Properties.Resources.ConnectionStr;
        }

        public List<Usuario> InicializarUsuarios()
        {
            List<Usuario> misUsuarios = new List<Usuario>();

            string queryString = "SELECT * from [dbo].[usuario];";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    Usuario aux;

                    while (reader.Read())
                    {
                        aux = new Usuario(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetBoolean(6), reader.GetBoolean(7));
                        misUsuarios.Add(aux);
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return misUsuarios;
        }

        public List<CajaDeAhorro> InicializarCajasAhorro()
        {
            List<CajaDeAhorro> cajas = new List<CajaDeAhorro>();

            string queryString = "SELECT * from [dbo].[caja_de_ahorro];";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    CajaDeAhorro aux;

                    while (reader.Read())
                    {
                        aux = new CajaDeAhorro(reader.GetInt32(0), reader.GetInt32(1), reader.GetDouble(2));
                        cajas.Add(aux);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return cajas;
        }

        public List<PlazoFijo> InicializarPlazosFijo()
        {
            List<PlazoFijo> plazos = new List<PlazoFijo>();

            string queryString = "SELECT * from [dbo].[plazo_fijo];";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    PlazoFijo aux;

                    while (reader.Read())
                    {
                        aux = new PlazoFijo(reader.GetInt32(0), reader.GetDouble(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetDouble(4), reader.GetBoolean(5), reader.GetInt32(6));
                        plazos.Add(aux);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return plazos;
        }

        public List<Pago> InicializarPagos()
        {
            List<Pago> pagos = new List<Pago>();

            string queryString = "SELECT * from [dbo].[pago];";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    Pago aux;

                    while (reader.Read())
                    {
                        aux = new Pago(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetBoolean(3), reader.GetString(4), reader.GetInt32(5));
                        pagos.Add(aux);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return pagos;
        }

        public List<Tarjeta> InicializarTarjetas()
        {
            List<Tarjeta> tarjetas = new List<Tarjeta>();

            string queryString = "SELECT * from [dbo].[tarjeta_de_credito];";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    Tarjeta aux;

                    while (reader.Read())
                    {
                        aux = new Tarjeta(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt16(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetInt32(5));
                        tarjetas.Add(aux);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return tarjetas;
        }

        public List<Movimiento> InicializarMovimientos()
        {
            List<Movimiento> movimientos = new List<Movimiento>();

            string queryString = "SELECT * from [dbo].[movimiento];";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    Movimiento aux;

                    while (reader.Read())
                    {
                        aux = new Movimiento(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetDateTime(3), reader.GetInt32(4));
                        movimientos.Add(aux);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return movimientos;
        }

        public List<CajaAhorroUsuario> InicializarCajaAhorroUsuarios()
        {
            List<CajaAhorroUsuario> cajaAhorroUsuarios = new List<CajaAhorroUsuario>();

            string queryString = "SELECT * from [dbo].[usuario_caja_de_ahorro];";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    CajaAhorroUsuario aux;

                    while (reader.Read())
                    {
                        aux = new CajaAhorroUsuario(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                        cajaAhorroUsuarios.Add(aux);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return cajaAhorroUsuarios;
        }

        //devuelve el ID del usuario agregado a la base, si algo falla devuelve -1
        public int AltaUsuario(int Dni, string Nombre, string Apellido, string Mail, string Clave, bool Admin, bool Bloqueado)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            int idNuevoUsuario = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[usuario] ([dni],[nombre],[apellido],[email],[clave],[is_admin],[is_bloqueado]) VALUES (@dni,@nombre,@apellido,@mail,@clave,@admin,@bloqueado);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@mail", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@clave", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@admin", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@bloqueado", SqlDbType.Bit));
                command.Parameters["@dni"].Value = Dni;
                command.Parameters["@nombre"].Value = Nombre;
                command.Parameters["@apellido"].Value = Apellido;
                command.Parameters["@mail"].Value = Mail;
                command.Parameters["@clave"].Value = Clave;
                command.Parameters["@admin"].Value = Admin;
                command.Parameters["@bloqueado"].Value = Bloqueado;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();

                    //*******************************************
                    //Ahora hago esta query para obtener el ID
                    string ConsultaID = "SELECT MAX([id_usuario]) FROM [dbo].[usuario]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoUsuario = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return idNuevoUsuario;
                }
                return idNuevoUsuario;
            }
        }

        //devuelve la cantidad de elementos modificados en la base (debería ser 1 si anduvo bien)
        public int EliminarUsuario(int Id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[usuario] WHERE id_usuario=@id";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = Id;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        //devuelve la cantidad de elementos modificados en la base (debería ser 1 si anduvo bien)
        public int ModificarUsuario(int Id, int Dni, string Nombre, string Apellido, string Mail, string Password, bool EsADM, bool Bloqueado)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "UPDATE [dbo].[usuario] SET dni=@dni, nombre=@nombre, apellido=@apellido, email=@mail, clave=@password, is_admin=@esadm, is_bloqueado=@bloqueado WHERE id_usuario=@id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@mail", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@esadm", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@bloqueado", SqlDbType.Bit));
                command.Parameters["@id"].Value = Id;
                command.Parameters["@dni"].Value = Dni;
                command.Parameters["@nombre"].Value = Nombre;
                command.Parameters["@apellido"].Value = Apellido;
                command.Parameters["@mail"].Value = Mail;
                command.Parameters["@password"].Value = Password;
                command.Parameters["@esadm"].Value = EsADM;
                command.Parameters["@bloqueado"].Value = Bloqueado;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int BloquearUsuario(int Id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "UPDATE [dbo].[usuario] SET is_bloqueado=1 WHERE id_usuario=@id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = Id;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int DesbloquearUsuario(int Id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "UPDATE [dbo].[usuario] SET is_bloqueado=0 WHERE id_usuario=@id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = Id;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int AltaCajaAhorro(int Cbu, double Saldo)
        {
            int resultadoQuery;
            int idNuevaCaja = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[caja_de_ahorro] ([cbu],[saldo]) VALUES (@cbu,@saldo);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@cbu", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@saldo", SqlDbType.Real));
                command.Parameters["@cbu"].Value = Cbu;
                command.Parameters["@saldo"].Value = Saldo;
                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();

                    string ConsultaID = "SELECT MAX([id_caja_de_ahorro]) FROM [dbo].[caja_de_ahorro]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevaCaja = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return idNuevaCaja;
                }
                return idNuevaCaja;
            }
        }

        public int EliminarCajaAhorro(int Id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[caja_de_ahorro] WHERE id_caja_de_ahorro=@id";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = Id;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int ModificarCajaAhorro(int Id, int Cbu, double Saldo)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "UPDATE [dbo].[caja_de_ahorro] SET cbu=@cbu saldo=@saldo WHERE id_caja_de_ahorro=@id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@cbu", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@saldo", SqlDbType.Real));
                command.Parameters["@id"].Value = Id;
                command.Parameters["@cbu"].Value = Cbu;
                command.Parameters["@saldo"].Value = Saldo;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int CrearCajaAhorro(int IdUsuario, int cbu, double Saldo)
        {
            int resultadoQuery;
            int idNuevaCaja = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[caja_de_ahorro] ([cbu],[saldo]) VALUES (@cbu,@saldo);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@cbu", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@saldo", SqlDbType.Real));
                command.Parameters["@dni"].Value = IdUsuario;
                command.Parameters["@cbu"].Value = cbu;
                command.Parameters["@saldo"].Value = Saldo;
                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();

                    string ConsultaID = "SELECT MAX([id_caja_de_ahorro]) FROM [dbo].[caja_de_ahorro]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevaCaja = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return idNuevaCaja;
                }
                return idNuevaCaja;
            }
        }

        public int Transferir(int IdCajaOrigen, int IdCajaDestino, double Monto)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryTrans = "EXEC dbo.transferir @idCajaOrigen, @idCajaDestino, @monto;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryTrans, connection);
                command.Parameters.Add(new SqlParameter("@idCajaOrigen", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idCajaDestino", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Real));
                command.Parameters["@idCajaOrigen"].Value = IdCajaOrigen;
                command.Parameters["@idCajaDestino"].Value = IdCajaDestino;
                command.Parameters["@monto"].Value = Monto;

                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return 0;
                }
            }
        }

        public int Depositar(int IdCajaDestino, double Monto)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryTrans = "UPDATE [dbo].[caja_de_ahorro] SET saldo = saldo + @saldo WHERE id_caja_de_ahorro=@idCajaDestino;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryTrans, connection);
                command.Parameters.Add(new SqlParameter("@idCajaDestino", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@saldo", SqlDbType.Real));
                command.Parameters["@idCajaDestino"].Value = IdCajaDestino;
                command.Parameters["@saldo"].Value = Monto;

                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return 0;
                }
            }
        }

        public int Retirar(int IdCajaDestino, double Monto)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryTrans = "UPDATE [dbo].[caja_de_ahorro] SET saldo = saldo - @saldo WHERE id_caja_de_ahorro=@idCajaDestino;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryTrans, connection);
                command.Parameters.Add(new SqlParameter("@idCajaDestino", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@saldo", SqlDbType.Real));
                command.Parameters["@idCajaDestino"].Value = IdCajaDestino;
                command.Parameters["@saldo"].Value = Monto;

                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return 0;
                }
            }
        }


        public int EliminarTitularCajaAhorro(int IdUsuario, int IdCajaAhorro)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE [dbo].[usuario_caja_de_ahorro] WHERE id_usuario=@idUsuario AND id_caja_de_ahorro=@idUsuarioCajaAhorro;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idUsuarioCajaAhorro", SqlDbType.Int));

                command.Parameters["@idUsuario"].Value = IdCajaAhorro;
                command.Parameters["@idCajaAhorro"].Value = IdUsuario;

                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int AgregarTitularCajaAhorro(int IdUsuario, int IdCajaAhorro)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[usuario_caja_de_ahorro] ([id_usuario],[id_caja_de_ahorro]) VALUES (@idUsuario,@idCajaAhorro);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idCajaAhorro", SqlDbType.Int));
                command.Parameters["@idUsuario"].Value = IdUsuario;
                command.Parameters["@idCajaAhorro"].Value = IdCajaAhorro;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return 0;
                }
            }
        }

        public int AltaPlazoFijo(double Monto, DateTime Fecha_inicio, DateTime Fecha_fin, double Tasa, bool IsPagado, int IdUsuario)
        {

            int resultadoQuery;
            int idNuevoPlazoFijo = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[plazo_fijo] ([monto],[fecha_inicio],[fecha_fin],[tasa],[pagado],[id_usuario]) VALUES (@monto,@fecha_inicio,@fecha_fin,@tasa,@pagado,@idUsuario);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@fecha_inicio", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@fecha_fin", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@tasa", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@pagado", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                command.Parameters["@monto"].Value = Monto;
                command.Parameters["@fecha_inicio"].Value = Fecha_inicio;
                command.Parameters["@fecha_fin"].Value = Fecha_fin;
                command.Parameters["@tasa"].Value = Tasa;
                command.Parameters["@pagado"].Value = IsPagado;
                command.Parameters["@idUsuario"].Value = IdUsuario;
                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();


                    string ConsultaID = "SELECT MAX([id_plazo_fijo]) FROM [dbo].[plazo_fijo]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoPlazoFijo = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return idNuevoPlazoFijo;
                }
                return idNuevoPlazoFijo;
            }
        }

        public int BajaPlazoFijo(int Id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[plazo_fijo] WHERE id_plazo_fijo=@id";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = Id;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int ModificarPlazoFijo(int Id, double Monto, DateTime Fecha_inicio, DateTime Fecha_fin, double Tasa, bool IsPagado)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "UPDATE [dbo].[plazo_fijo] SET monto=@monto, fecha_inicio=@fecha_inicio, fecha_fin=@fecha_fin, tasa=@tasa, pagado=@pagado WHERE id_plazo_fijo=@id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@fecha_inicio", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@fecha_fin", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@tasa", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@pagado", SqlDbType.Bit));
                command.Parameters["@id"].Value = Id;
                command.Parameters["@monto"].Value = Monto;
                command.Parameters["@fecha_inicio"].Value = Fecha_inicio;
                command.Parameters["@fecha_fin"].Value = Fecha_fin;
                command.Parameters["@tasa"].Value = Tasa;
                command.Parameters["@pagado"].Value = IsPagado;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int AltaPago(string Detalle, double Monto, bool IsPagado, string Metodo, int IdUsuario)
        {

            int resultadoQuery;
            int idNuevoPago = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[pago] ([detalle],[monto],[pagado],[metodo],[id_usuario]) VALUES (@detalle,@monto,@pagado,@metodo,@idUsuario);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@detalle", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@pagado", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@metodo", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                command.Parameters["@detalle"].Value = Detalle;
                command.Parameters["@monto"].Value = Monto;
                command.Parameters["@pagado"].Value = IsPagado;
                command.Parameters["@metodo"].Value = Metodo;
                command.Parameters["@idUsuario"].Value = IdUsuario;
                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();


                    string ConsultaID = "SELECT MAX([id_pago]) FROM [dbo].[pago]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoPago = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return idNuevoPago;
                }
                return idNuevoPago;
            }
        }

        public int BajaPago(int Id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[pago] WHERE id_pago=@id";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = Id;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int ModificarPago(int Id, string Detalle, double Monto, bool Pagado, string Metodo)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "UPDATE [dbo].[pago] SET detalle=@detalle, monto=@monto, pagado=@pagado, metodo=@metodo WHERE id_pago=@id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@detalle", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@pagado", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@metodo", SqlDbType.NVarChar));
                command.Parameters["@id"].Value = Id;
                command.Parameters["@detalle"].Value = Detalle;
                command.Parameters["@monto"].Value = Monto;
                command.Parameters["@pagado"].Value = Pagado;
                command.Parameters["@metodo"].Value = Metodo;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int PagarPagoConCajaAhorro(int IdCajaAhorro, int IdPago, double Monto)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryTrans = "EXEC dbo.pagar_pago_con_caja_de_ahorro @idCajaAhorro, @idPago, @monto;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryTrans, connection);
                command.Parameters.Add(new SqlParameter("@idCajaAhorro", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idPago", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Real));
                command.Parameters["@idCajaAhorro"].Value = IdCajaAhorro;
                command.Parameters["@idPago"].Value = IdPago;
                command.Parameters["@monto"].Value = Monto;

                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return 0;
                }
            }
        }

        public int PagarPagoConTarjeta(int IdTarjeta, int IdPago, double Monto)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryTrans = "EXEC dbo.pagar_pago_con_tarjeta @idTarjeta, @idPago, @monto;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryTrans, connection);
                command.Parameters.Add(new SqlParameter("@idTarjeta", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idPago", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Real));
                command.Parameters["@idTarjeta"].Value = IdTarjeta;
                command.Parameters["@idPago"].Value = IdPago;
                command.Parameters["@monto"].Value = Monto;

                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return 0;
                }
            }
        }

        public int AltaTarjetaCredito(int Numero, int CodigoV, double Limite, double Consumos, int IdUsuario)
        {

            int resultadoQuery;
            int idNuevaTarjeta = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[tarjeta_de_credito] ([numero],[codigoV],[limite],[consumos],[id_usuario]) VALUES (@numero,@codigoV,@limite,@consumos,@idUsuario);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@numero", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@codigoV", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@limite", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@consumos", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                command.Parameters["@numero"].Value = Numero;
                command.Parameters["@codigoV"].Value = CodigoV;
                command.Parameters["@limite"].Value = Limite;
                command.Parameters["@consumos"].Value = Consumos;
                command.Parameters["@idUsuario"].Value = IdUsuario;
                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();


                    string ConsultaID = "SELECT MAX([id_tarjeta]) FROM [dbo].[tarjeta_de_credito]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevaTarjeta = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return idNuevaTarjeta;
                }
                return idNuevaTarjeta;
            }
        }

        public int BajaTarjetaCredito(int Id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[tarjeta_de_credito] WHERE id_tarjeta=@id";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = Id;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int ModificarTarjetaCredito(int Id, int Numero, int CodigoV, double Limite, double Consumos)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "UPDATE [dbo].[tarjeta_de_credito] SET numero=@numero, codigoV=@codigoV, limite=@limite, consumos=@consumos WHERE id_tarjeta=@id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@numero", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@codigoV", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@limite", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@consumos", SqlDbType.Real));
                command.Parameters["@id"].Value = Id;
                command.Parameters["@numero"].Value = Numero;
                command.Parameters["@codigoV"].Value = CodigoV;
                command.Parameters["@limite"].Value = Limite;
                command.Parameters["@consumos"].Value = Consumos;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int PagarTarjeta(int IdTarjeta, int IdCajaAhorro, double Monto)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryTrans = "EXEC dbo.pagar_tarjeta @idTarjeta, @idCajaAhorro, @monto;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryTrans, connection);
                command.Parameters.Add(new SqlParameter("@idTarjeta", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idCajaAhorro", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Real));
                command.Parameters["@idTarjeta"].Value = IdTarjeta;
                command.Parameters["@idCajaAhorro"].Value = IdCajaAhorro;
                command.Parameters["@monto"].Value = Monto;

                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return 0;
                }
            }

        }

        public int AltaMovimiento(string Detalle, double Monto, DateTime Fecha, int IdCajaAhorro)
        {

            int resultadoQuery;
            int idNuevoMovimiento = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[movimiento] ([detalle],[monto],[fecha],[id_caja_de_ahorro]) VALUES (@detalle,@monto,@fecha,@idCajaAhorro);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@detalle", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@idCajaAhorro", SqlDbType.Int));
                command.Parameters["@detalle"].Value = Detalle;
                command.Parameters["@monto"].Value = Monto;
                command.Parameters["@fecha"].Value = Fecha;
                command.Parameters["@idCajaAhorro"].Value = IdCajaAhorro;
                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();

                    string ConsultaID = "SELECT MAX([id_movimiento]) FROM [dbo].[movimiento]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoMovimiento = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return idNuevoMovimiento;
                }
                return idNuevoMovimiento;
            }
        }

        public int EliminarMovimiento(int Id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[movimiento] WHERE id_movimiento=@id";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = Id;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int ModificarMovimiento(int Id, string Detalle, double Monto, DateTime Fecha)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "UPDATE [dbo].[movimiento] SET detalle=@detalle, monto=@monto, fecha=@fecha WHERE id_movimiento=@id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@detalle", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime));
                command.Parameters["@id"].Value = Id;
                command.Parameters["@detalle"].Value = Detalle;
                command.Parameters["@monto"].Value = Monto;
                command.Parameters["@fecha"].Value = Fecha;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int AltaUsuarioCajaAhorro(int IdUsuario, int IdCajaAhorro)
        {
            int resultadoQuery;
            int idNuevoUsuarioCaja = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[usuario_caja_de_ahorro] ([id_usuario],[id_caja_de_ahorro]) VALUES (@idUsuario,@idCajaAhorro);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idCajaAhorro", SqlDbType.Int));
                command.Parameters["@idUsuario"].Value = IdUsuario;
                command.Parameters["@idCajaAhorro"].Value = IdCajaAhorro;
                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();

                    string ConsultaID = "SELECT MAX([id_usuario_caja_de_ahorro]) FROM [dbo].[usuario_caja_de_ahorro]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoUsuarioCaja = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return idNuevoUsuarioCaja;
                }
                return idNuevoUsuarioCaja;
            }
        }

        public int EliminarUsuarioCajaAhorro(int Id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[usuario_caja_de_ahorro] WHERE id_usuario_caja_de_ahorro=@id";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = Id;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int ModificarUsuarioCajaAhorro(int Id, int IdUsuario, int IdCajaAhorro)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "UPDATE [dbo].[usuario_caja_de_ahorro] SET id_usuario=@idUsuario, id_caja_de_ahorro=@idCajaAhorro WHERE id_usuario_caja_de_ahorro=@id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idCajaAhorro", SqlDbType.Int));
                command.Parameters["@id"].Value = Id;
                command.Parameters["@idUsuario"].Value = IdUsuario;
                command.Parameters["@idCajaAhorro"].Value = IdCajaAhorro;
                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int GetMaxCbu()
        {
            int idNuevaCaja = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string ConsultaID = "SELECT MAX([cbu]) FROM [dbo].[caja_de_ahorro]";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(ConsultaID, connection);
                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevaCaja = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return idNuevaCaja;
                }
                return idNuevaCaja;
            }
        }
        public int GetMaxNumeroTarjeta()
        {
            int idNuevaCaja = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string ConsultaID = "SELECT MAX([numero]) FROM [dbo].[tarjeta_de_credito]";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(ConsultaID, connection);
                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevaCaja = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return idNuevaCaja;
                }
                return idNuevaCaja;
            }
        }
    }
}
