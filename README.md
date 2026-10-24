# Lavadora POO

Aplicación de consola en C# que simula el servicio de una lavandería: recibe los datos del cliente, configura y ejecuta un ciclo de lavado, reproduce sonidos y calcula el costo del servicio.

> Este es un proyecto académico que tenía guardado de mi etapa en el SENA. Lo comparto como parte de mi proceso de aprendizaje y como ejercicio de programación orientada a objetos. No es un proyecto oficial del SENA.

## ¿Qué hace?

- Registra el nombre del cliente y la cantidad de ropa.
- Permite elegir entre ropa blanca, de colores, algodón, lycra, seda, jeans y tenis.
- Muestra recomendaciones de cuidado según el tipo de prenda.
- Permite personalizar el tiempo de lavado.
- Simula el llenado, lavado, enjuague y secado opcional.
- Reproduce los sonidos incluidos en `LavadoraPOO1/Sonidos`.
- Muestra el costo antes y después del IVA, la utilidad estimada y un resumen de clientes atendidos.

La simulación acepta cargas de **5 a 40 kg**. El lavado predeterminado dura 30 minutos simulados; se puede configurar entre 1 y 1440 minutos. Para que el ciclo no tome todo ese tiempo en la vida real, cada minuto de lavado equivale a un segundo. El llenado, el enjuague y el secado también usan pausas breves de simulación.

## Requisitos

- Windows.
- .NET Framework 4.7.2 o su paquete de desarrollo (Targeting Pack).
- Visual Studio con las herramientas de desarrollo de escritorio para .NET, o un SDK de .NET compatible para compilar el proyecto.

## Ejecutar

Clona el repositorio:

```powershell
git clone https://github.com/FrankPater15/LavadoraPOO.git
cd LavadoraPOO
```

Compila la solución:

```powershell
dotnet build .\LavadoraPOO1.sln
```

Inicia la aplicación compilada:

```powershell
.\LavadoraPOO1\bin\Debug\LavadoraPOO1.exe
```

También puedes abrir `LavadoraPOO1.sln` en Visual Studio y ejecutar con **Ctrl + F5**. Sigue las instrucciones de la consola y presiona **ESC** para salir.

## Cálculos

- Tarifa base: **$4.000 por kg**.
- Recargo especial del **3 %** para ropa blanca, algodón y tenis.
- IVA calculado: **19 %**.
- Utilidad estimada: **40 %** del costo base del servicio.

El proyecto incluye además una calculadora de consumo y costo energético por estrato. Sus tarifas son valores de ejemplo definidos en el código; no necesariamente representan precios vigentes.

## Estructura

```text
LavadoraPOO/
├── LavadoraPOO1.sln
└── LavadoraPOO1/
    ├── Program.cs
    ├── Lavadora.cs
    ├── OpcionesLavado.cs
    ├── CalculadoraCostos.cs
    ├── CalculadoraEnergia.cs
    └── Sonidos/
```

## Tecnologías

- C#
- .NET Framework 4.7.2
- Aplicación de consola
- Sonidos WAV con `System.Media.SoundPlayer`
