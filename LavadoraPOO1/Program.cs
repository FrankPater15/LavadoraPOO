using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        int clientesAtendidos = 0;
        decimal totalGanancias = 0m;

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("==========================================");
        Console.WriteLine("          LAVANDERÍA - SERVICIO");
        Console.WriteLine("==========================================");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para iniciar o ESC para salir.");
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                break;
            }

            Console.WriteLine();
            try
            {
                string nombreCliente = LeerTexto("Nombre del cliente: ");
                int kilos = LeerEntero("Kilos de ropa (5-40): ", 5, 40);
                List<string> tiposDeRopa = Lavadora.ObtenerTiposDeRopa();
                string tipoRopa = SeleccionarTipoDeRopa(tiposDeRopa);

                using (Lavadora lavadora = new Lavadora(kilos, tipoRopa))
                {
                    lavadora.ReproducirSonido("Encendido.wav");
                    Console.WriteLine();
                    Console.WriteLine(OpcionesLavado.ObtenerRecomendacion(tipoRopa));

                    if (LeerSiNo("¿Desea fijar un tiempo de lavado personalizado? (S/N): "))
                    {
                        int minutos = LeerEntero(
                            "Tiempo de lavado en minutos (1-" + Lavadora.TiempoLavadoMaximoMinutos + "): ",
                            1,
                            Lavadora.TiempoLavadoMaximoMinutos);
                        lavadora.FijarTiempoLavado(minutos);
                        lavadora.ReproducirSonido("OpcionesSonido.wav");
                    }

                    if (lavadora.CicloTerminado())
                    {
                        decimal costo = CalculadoraCostos.CalcularCosto(kilos, tipoRopa);
                        decimal costoConIVA = CalculadoraCostos.CalcularCostoConIVA(costo);
                        decimal utilidad = CalculadoraCostos.CalcularUtilidad(costo);

                        Console.WriteLine();
                        Console.WriteLine("============= LAVADO FINALIZADO =============");
                        Console.WriteLine("Costo sin IVA: {0:C2}", costo);
                        Console.WriteLine("Costo con IVA: {0:C2}", costoConIVA);
                        Console.WriteLine("Utilidad:      {0:C2}", utilidad);
                        Console.WriteLine("Cliente:       " + nombreCliente);
                        Console.WriteLine("Fecha y hora:  " + DateTime.Now.ToString("f", CultureInfo.CurrentCulture));

                        totalGanancias += utilidad;
                        clientesAtendidos++;
                    }
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine();
                Console.WriteLine("Se recibió el fin de la entrada. Cerrando el programa.");
                break;
            }
        }

        Console.WriteLine();
        Console.WriteLine("================= RESUMEN ==================");
        Console.WriteLine("Clientes atendidos: {0}", clientesAtendidos);
        Console.WriteLine("Ganancias totales:  {0:C2}", totalGanancias);
        Console.WriteLine("============================================");
    }

    private static string SeleccionarTipoDeRopa(List<string> tiposDeRopa)
    {
        Console.WriteLine();
        Console.WriteLine("Seleccione el tipo de ropa:");
        for (int i = 0; i < tiposDeRopa.Count; i++)
        {
            Console.WriteLine("{0}. {1}", i + 1, tiposDeRopa[i]);
        }

        int opcion = LeerEntero("Opción: ", 1, tiposDeRopa.Count);
        return tiposDeRopa[opcion - 1];
    }

    private static string LeerTexto(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string valor = Console.ReadLine();
            if (valor == null)
            {
                throw new EndOfStreamException();
            }

            valor = valor.Trim();
            if (valor.Length > 0)
            {
                return valor;
            }

            Console.WriteLine("El valor no puede estar vacío.");
        }
    }

    private static int LeerEntero(string mensaje, int minimo, int maximo)
    {
        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine();
            if (entrada == null)
            {
                throw new EndOfStreamException();
            }

            int valor;
            if (int.TryParse(entrada, out valor) && valor >= minimo && valor <= maximo)
            {
                return valor;
            }

            Console.WriteLine("Ingrese un número entero entre {0} y {1}.", minimo, maximo);
        }
    }

    private static bool LeerSiNo(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string respuesta = Console.ReadLine();
            if (respuesta == null)
            {
                throw new EndOfStreamException();
            }

            respuesta = respuesta.Trim();
            if (respuesta.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (respuesta.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            Console.WriteLine("Responda S para sí o N para no.");
        }
    }
}
