using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Data.Common;


namespace GEPARK.Models
{
    public static class Funcion
    {
        public static string select = "";
        public static string orden = "";
        public static string filtro = "";
        public static int top = 100000;
        public static int skip = 0;
        public static string DevolverString(string conexion, string stComando)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = conexion;

            SqlCommand command = new SqlCommand(stComando, connection);

            string stSalida = "";
            try
            {
                command.CommandTimeout = 18000;
                command.Connection.Open();
                stSalida = (string)command.ExecuteScalar();
                if (stSalida == null)
                {
                    stSalida = "";
                }
            }
            catch (System.Exception ex)
            {
                command.Connection.Close();
                return ex.ToString();
            }
            command.Connection.Close();
            return stSalida;
        }

        public static int DevolverInt(string conexion, string stComando)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = conexion;

            SqlCommand command = new SqlCommand(stComando, connection);

            int stSalida = 0;
            try
            {
                command.CommandTimeout = 18000;
                command.Connection.Open();
                stSalida = (int)command.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                command.Connection.Close();
                return -1;
            }
            command.Connection.Close();
            return stSalida;
        }

        public static bool DevolverBool(string conexion, string stComando)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = conexion;

            SqlCommand command = new SqlCommand(stComando, connection);

            bool stSalida = false;
            try
            {
                command.CommandTimeout = 18000;
                command.Connection.Open();
                stSalida = (bool)command.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                command.Connection.Close();
                return false;
            }
            command.Connection.Close();
            return stSalida;
        }

        public static double DevolverDouble(string conexion, string stComando)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = conexion;

            SqlCommand command = new SqlCommand(stComando, connection);

            double stSalida = 0;
            try
            {
                command.CommandTimeout = 18000;
                command.Connection.Open();
                stSalida = Convert.ToDouble(command.ExecuteScalar().ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                command.Connection.Close();
                return -1;
            }
            command.Connection.Close();
            return stSalida;
        }


        public static string EjecutarSql(string conexion, string stComando)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = conexion;

            SqlCommand command = new SqlCommand(stComando, connection);

            try
            {
                command.CommandTimeout = 18000;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                command.Connection.Close();
                return "Error - " + ex.ToString();

            }
            command.Connection.Close();
            return "OK";
        }


        static public DataTable DevolverLista(string conexion, string stProcedimiento)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = conexion;

            DataSet dsArticulo = new DataSet();
            SqlDataAdapter myData = new SqlDataAdapter(stProcedimiento, conexion);
            myData.TableMappings.Add("Tabla", "Persona");

            dsArticulo.Reset();
            dsArticulo.Clear();
            try
            {               
                myData.SelectCommand.CommandTimeout = 18000;
                myData.Fill(dsArticulo);
                return (DataTable)dsArticulo.Tables[0];
            }
            catch
            {
                return new DataTable();
            }
        }

    }

}
