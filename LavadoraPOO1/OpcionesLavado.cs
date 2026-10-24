public static class OpcionesLavado
{
    public static string ObtenerRecomendacion(string tipoRopa)
    {
        if (string.IsNullOrWhiteSpace(tipoRopa))
        {
            return "Seleccione un tipo de ropa para recibir una recomendación.";
        }

        switch (tipoRopa.Trim().ToLowerInvariant())
        {
            case "blanca":
            case "toallas":
            case "sábanas":
            case "sabanas":
            case "acolchados":
            case "cortinas":
                return "Ropa blanca y textiles resistentes: use agua tibia y revise la etiqueta antes de elegir una temperatura alta.";

            case "jeans":
            case "camperas":
                return "Jeans y prendas gruesas: lávelos del revés con agua fría o tibia para cuidar el color.";

            case "colores":
            case "algodón":
            case "algodon":
            case "lycra":
            case "sedas":
            case "delicadas":
                return "Prendas de color o delicadas: use agua fría y un ciclo suave; siga siempre la etiqueta de cuidado.";

            case "tennis":
                return "Calzado: revise la etiqueta; si es apto para lavadora, use agua fría y una bolsa protectora.";

            default:
                return "No hay recomendación específica para este tipo de ropa.";
        }
    }
}
