using System;
using System.Collections.Generic;
using System.IO;


namespace Parcial2_GrupoInternet_Batch
{
    public class Program
    {
        static List<Cliente> LeerClientesDeCSV(string path)
        {
            List<Cliente> clientes = new List<Cliente>();

            string[] registros = File.ReadAllLines(path);

            for (int i = 0; i < registros.Length; i++)
            {

                string[] campos = registros[i].Split(";");

                Cliente p = GenerarCliente(campos);

                if (p == null)
                {
                    Console.WriteLine("Error");
                }
                else
                {
                    clientes.Add(p);
                }
            }
            return clientes;
        }

        private static Cliente GenerarCliente(string[] campos)
        {
            if (campos == null)
            {
                return null;
            }
            if (campos.Length != 6)
            {                
                return null;
            }

            Cliente resultado = new Cliente();

            string Nro = campos[0];
            string DNI = campos[1];
            string Nombre = campos[2];
            string Localidad = campos[3];
            string NombrePLan = campos[4];
            decimal PrecioPlan = decimal.Parse(campos[5]);

            resultado.Nro = Nro;
            resultado.DNI = DNI;
            resultado.Nombre = Nombre;
            resultado.Localidad = Localidad;
            resultado.NombrePLan = NombrePLan;
            resultado.PrecioPlan = PrecioPlan;

            return resultado;
        }

        static void Main(string[] args)
        {
            if (!File.Exists(args[0]))
            {
                return;
            }

            List<Cliente> clientes = LeerClientesDeCSV(args[0]);

            decimal TotalRecaudado = CalcularTotalDeRecaudacion(clientes);

            decimal ValorPromedio = CalcularValorPromedioAbono(clientes);

            decimal TotalBuenosAires = CalcularTotalBuenosAires(clientes);

            string[] reporte = EscribirReporte(TotalRecaudado,ValorPromedio,TotalBuenosAires);

            File.WriteAllLines("Reporte", reporte);

        }
    }
}
