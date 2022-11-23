<h3>#todolist</h3>
<ul>
<li>VerDetalle(CajaAhorro) falta filtrar cambiar equals y ver que pasa</li>

<li>PagarPago(Pago)</li>
<li>EliminarPago(Pago)</li>
<li>Pagar(PlazoFijo), Lo dejamos para el final con la interfaz del usuario adm </li>
<li>Pagar, Modificar(TarjetaCredito)</li>
</ul>


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
                    /*
                    if (command.ExecuteNonQuery() == 1)
                    {
                        command = new SqlCommand(queryTrans2, connection);
                        command.Parameters.Add(new SqlParameter("@idCajaOrigen", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Real));
                        command.Parameters["@idCajaOrigen"].Value = IdCajaDestino;
                        command.Parameters["@monto"].Value = Monto;

                        return command.ExecuteNonQuery();
                    }
                    */
                }
                catch
                {

                    return 0;

                }
            }
        }