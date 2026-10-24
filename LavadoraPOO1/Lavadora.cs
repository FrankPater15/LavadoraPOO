using System;
using System.Collections.Generic;
using System.Media;
using System.IO;
using System.Threading;

public class Lavadora : IDisposable
{
    private static readonly List<string> TiposDeRopa = new List<string>
    {
        "Blanca", "Colores", "Algodón", "Lycra", "Sedas", "Jeans", "Tennis"
    };

    public const int TiempoLavadoMaximoMinutos = 1440;

    private readonly int kilos;
    private readonly string tipoRopa;
    private int tiempoLavado = 30;
    private bool cicloCompleto;
    private SoundPlayer reproductorActual;

    public Lavadora(int kilos, string tipoRopa)
    {
        if (kilos < 5 || kilos > 40)
        {
            throw new ArgumentException("La cantidad de kilos debe estar entre 5 y 40.");
        }

        string tipoValido = TiposDeRopa.Find(
            tipo => string.Equals(tipo, tipoRopa, StringComparison.OrdinalIgnoreCase));
        if (tipoValido == null)
        {
            throw new ArgumentException("Tipo de ropa no válido.");
        }

        this.kilos = kilos;
        this.tipoRopa = tipoValido;
    }

    public void ReproducirSonido(string nombreArchivo, bool esperarFinalizacion = false)
    {
        string rutaArchivo = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Sonidos",
            nombreArchivo);

        if (!File.Exists(rutaArchivo))
        {
            Console.WriteLine("Aviso: no se encontró el sonido '{0}'.", rutaArchivo);
            return;
        }

        SoundPlayer reproductor = new SoundPlayer(rutaArchivo);
        try
        {
            reproductor.Load();
            if (esperarFinalizacion)
            {
                reproductor.PlaySync();
            }
            else
            {
                reproductor.Play();
            }
        }
        catch (IOException ex)
        {
            reproductor.Dispose();
            Console.WriteLine("Aviso: no se pudo cargar el sonido: {0}", ex.Message);
            return;
        }
        catch (InvalidOperationException ex)
        {
            reproductor.Dispose();
            Console.WriteLine("Aviso: no se pudo reproducir el sonido: {0}", ex.Message);
            return;
        }

        if (reproductorActual != null)
        {
            reproductorActual.Dispose();
        }

        reproductorActual = reproductor;
    }

    public static List<string> ObtenerTiposDeRopa()
    {
        return new List<string>(TiposDeRopa);
    }

    public void FijarTiempoLavado(int minutos)
    {
        if (minutos <= 0 || minutos > TiempoLavadoMaximoMinutos)
        {
            throw new ArgumentOutOfRangeException(
                "minutos",
                "El tiempo de lavado debe estar entre 1 y " + TiempoLavadoMaximoMinutos + " minutos.");
        }

        this.tiempoLavado = minutos;
    }

    private void LlenadoDeAgua()
    {
        for (int i = 0; i < 3; i++)
        {
            Console.Write("Iniciando el llenado");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }

            Console.WriteLine();
        }

        ReproducirSonido("LLenado.wav");
        Thread.Sleep(7000);
    }

    private void Lavado()
    {
        Console.WriteLine("Lavando {0} kg de ropa tipo {1} durante {2} minutos.", kilos, tipoRopa, tiempoLavado);
        Console.WriteLine("Simulación acelerada: cada minuto de lavado dura un segundo.");
        ReproducirSonido("Lavando.wav");
        Thread.Sleep(TimeSpan.FromSeconds(tiempoLavado));
    }

    private void Enjuague()
    {
        Console.WriteLine("Enjuagando...");
        ReproducirSonido("Lavando.wav");
        Thread.Sleep(5000);
    }

    private void Secado(bool secar)
    {
        if (secar)
        {
            Console.WriteLine("Secando...");
            ReproducirSonido("Secando.wav");
            Thread.Sleep(10000);
        }
        else
        {
            Console.WriteLine("Ciclo de secado omitido.");
        }
    }

    public bool CicloTerminado()
    {
        if (!cicloCompleto)
        {
            DateTime inicio = DateTime.Now;
            LlenadoDeAgua();
            Lavado();
            Enjuague();

            bool secar = LeerSiNo("¿Desea secar las prendas? (S/N): ");
            Secado(secar);

            cicloCompleto = true;
            ReproducirSonido("Finalizado.wav", true);

            TimeSpan duracion = DateTime.Now - inicio;
            Console.WriteLine(
                "Tiempo total del ciclo: {0} minutos y {1} segundos",
                (int)duracion.TotalMinutes,
                duracion.Seconds);
        }

        return cicloCompleto;
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

    public void Dispose()
    {
        if (reproductorActual != null)
        {
            reproductorActual.Dispose();
            reproductorActual = null;
        }
    }
}
