using System.Collections.Generic;
using System;

public class CalculadoraEnergia
{
    private const decimal POTENCIA_KW = 0.2m;

    private static readonly Dictionary<int, decimal> PRECIO_KWH = new Dictionary<int, decimal>
    {
        {2, 867.8m},
        {3, 737.6m},
        {4, 867.8m},
        {5, 1041.0m}
    };

    public static decimal CalcularConsumoMensual(int horasDiarias, int dias)
    {
        if (horasDiarias < 0 || horasDiarias > 24)
        {
            throw new ArgumentOutOfRangeException("horasDiarias", "Las horas diarias deben estar entre 0 y 24.");
        }

        if (dias < 1 || dias > 31)
        {
            throw new ArgumentOutOfRangeException("dias", "La cantidad de días debe estar entre 1 y 31.");
        }

        return POTENCIA_KW * horasDiarias * dias;
    }

    public static decimal CalcularCostoEnergia(decimal consumoKWH, int estrato)
    {
        if (consumoKWH < 0)
        {
            throw new ArgumentOutOfRangeException("consumoKWH", "El consumo no puede ser negativo.");
        }

        if (!PRECIO_KWH.ContainsKey(estrato))
        {
            throw new ArgumentException("Estrato no válido.");
        }

        return consumoKWH * PRECIO_KWH[estrato];
    }
}
