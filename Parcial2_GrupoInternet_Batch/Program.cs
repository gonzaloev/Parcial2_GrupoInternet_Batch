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

        static decimal CalcularTotalDeRecaudacion(List<Cliente> clientes)
        {
            decimal total = 0;
            foreach(Cliente c in clientes)
            {
                total += c.PrecioPlan;
            }
            return total;
        }
        static decimal CalcularValorPromedioAbono(List<Cliente> clientes)
        {
            decimal total = 0;
            foreach (Cliente c in clientes)
            {
                total += c.PrecioPlan;
            }

            decimal promedio = total / clientes.Count;

            return promedio;
        }
        static decimal CalcularTotalBuenosAires(List<Cliente> clientes)
        {
            decimal total = 0;
            foreach (Cliente c in clientes)
            {
                if(c.Localidad == "Buenos Aires")
                {
                    total += c.PrecioPlan;
                }
            }

            return total;
        }
        static int CantidadDeClientesEnBsAs(List<Cliente> clientes)
        {
            int contador = 0;
            foreach (Cliente c in clientes)
            {
                if(c.Localidad == "Buenos Aires")
                {
                    contador++;
                }
            }
            return contador;
        }
        static string[] EscribirReporte(decimal totalRecaudado, decimal valorPromedio, decimal totalBuenosAires, int CantidadDeClientes)
        {
            string[] reporte = new string[4];

            reporte[0] = $"El total de dinero recaudado por mes es de: {totalRecaudado:0.00}";
            reporte[1] = $"El valor promedio del abono es de: {valorPromedio:0.00}";
            reporte[2] = $"El total de dinero recaudado por mes en Buenos Aires es: {totalBuenosAires:0.00}";
            reporte[3] = $"La cantidad de clientes en Buenos Aires es: {CantidadDeClientes}";

            return reporte;
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

            int CantidadDeClientes = CantidadDeClientesEnBsAs(clientes);

            string[] reporte = EscribirReporte(TotalRecaudado,ValorPromedio,TotalBuenosAires,CantidadDeClientes);

            File.WriteAllLines("Reporte.txt", reporte);

        }

        
    }
}
