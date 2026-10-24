using System;

public class CalculadoraCostos
{
    private const decimal COSTO_POR_KILO = 4000m;
    private const decimal IVA = 0.19m;
    private const decimal AUMENTO_ESPECIAL = 0.03m;
    private const decimal UTILIDAD = 0.40m;

    public static decimal CalcularCosto(int kilos, string tipoRopa)
    {
        if (kilos < 5 || kilos > 40)
        {
            throw new ArgumentOutOfRangeException("kilos", "La cantidad de kilos debe estar entre 5 y 40.");
        }

        if (string.IsNullOrWhiteSpace(tipoRopa))
        {
            throw new ArgumentException("El tipo de ropa es obligatorio.", "tipoRopa");
        }

        decimal costo = kilos * COSTO_POR_KILO;

        if (tipoRopa.Equals("Blanca", System.StringComparison.OrdinalIgnoreCase)
            || tipoRopa.Equals("Algodón", System.StringComparison.OrdinalIgnoreCase)
            || tipoRopa.Equals("Tennis", System.StringComparison.OrdinalIgnoreCase))
        {
            costo *= (1 + AUMENTO_ESPECIAL);
        }

        return costo;
    }

    public static decimal CalcularCostoConIVA(decimal costo)
    {
        if (costo < 0)
        {
            throw new ArgumentOutOfRangeException("costo", "El costo no puede ser negativo.");
        }

        return costo * (1 + IVA);
    }

    public static decimal CalcularUtilidad(decimal costo)
    {
        if (costo < 0)
        {
            throw new ArgumentOutOfRangeException("costo", "El costo no puede ser negativo.");
        }

        return costo * UTILIDAD;
    }
}
