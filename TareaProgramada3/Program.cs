using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TareaProgramada3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new TP3Context())
            {
                bool agregarOtroProducto = true;

                while (agregarOtroProducto)
                {
                    Console.Write("Escriba el nombre del producto: ");
                    string nombreProducto = Console.ReadLine();

                    int cantidad;
                    do
                    {
                        Console.Write("Escriba la cantidad de números aleatorios a generar: ");
                    } while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad <= 0);

                    bool permitirRepetidos = true;
                    if (cantidad <= 100)
                    {
                        Console.Write("¿Los números se pueden repetir? (s/n): ");
                        string respuestaRepetidos = Console.ReadLine().ToLower();
                        permitirRepetidos = respuestaRepetidos == "s";
                    }

                    var producto = new Producto
                    {
                        Nombre = nombreProducto,
                        FechaHora = DateTime.Now
                    };

                    var numerosGenerados = GenerarNumeros(cantidad, permitirRepetidos);
                    int orden = 1;
                    foreach (int num in numerosGenerados)
                    {
                        producto.Numeros.Add(new Numero
                        {
                            Orden = orden++,
                            Num = num
                        });
                    }

                    context.Productos.Add(producto);

                    Console.Write("¿Desea agregar otro producto? (s/n): ");
                    string respuestaOtro = Console.ReadLine().ToLower();
                    agregarOtroProducto = respuestaOtro == "s";

                    if (!agregarOtroProducto)
                    {
                        context.SaveChanges();

                        var productos = context.Productos.Include(p => p.Numeros).OrderBy(p => p.FechaHora).ToList();

                        foreach (var p in productos)
                        {
                            Console.WriteLine($"{p.ProductoId}. {p.Nombre} - {p.FechaHora}");
                            foreach (var n in p.Numeros.OrderBy(n => n.Orden))
                            {
                                Console.WriteLine($"\t{n.NumeroId}. [{n.Orden}] {n.Num}");
                            }
                        }

                        Console.WriteLine("Programa finalizado. Presione la tecla enter para terminar.");
                        Console.ReadLine();
                    }
                }
            }
        }

        static List<int> GenerarNumeros(int cantidad, bool permitirRepetidos)
        {
            Random random = new Random();
            HashSet<int> unicos = new HashSet<int>();
            List<int> resultado = new List<int>();

            while (resultado.Count < cantidad)
            {
                int numero = random.Next(0, 100);
                if (permitirRepetidos || unicos.Add(numero))
                {
                    resultado.Add(numero);
                }
            }

            return resultado;
        }
    }
}
