using System;
using Logica;
using Entidad;

namespace Presentacion
{
    class Program
    {
        private static readonly LiquidacionService liquidacionService = new LiquidacionService();
        static void Main(string[] args)
        {
            char seguir = 'S';
            do
            {
                int opcion = Menu();
                switch (opcion)
                {
                    case 1: Guardar();
                        break;
                    case 2:ConsultarRegistros();
                        break;
                    case 3: Eliminar();
                        break;
                    case 4:seguir = 'N';
                        break;
                }
            } while (seguir == 'S');


        }

        public static int Menu()
        {
            int opcion;
            Console.Clear();
            Console.WriteLine("-----Liquidaciones de Impuestos---");
            Console.WriteLine("1.Registrar Establecimiento");
            Console.WriteLine("2.Consultar");
            Console.WriteLine("3.Eliminar");
            Console.WriteLine("4.Salir");
            Console.WriteLine();
            Console.Write("Seleccione su opcion->");
            do
            {
              opcion = int.Parse(Console.ReadLine());
            } while (opcion < 1 && opcion > 3);

            return opcion;
        }
        private static void Guardar()
        {
            Console.Clear();
            Console.WriteLine("---Registro de Usuario---");
            var persona = RegistrarDatos();
            string mensaje = liquidacionService.Guarda(persona);
            Console.WriteLine($"     { mensaje}");
            Console.Write("          Pulse una tecla para salir "); Console.ReadKey();
        }

        public static LiquidacionImpuesto RegistrarDatos()
        {
            Console.Clear();
            long identificacion = ValidarIdentificacion();
            Console.Write("Nombre del Establecimiento         :"); 
            string nombreEstablecimiento = Console.ReadLine();
            Console.Write("Valor Ingreso Anual                :");
            decimal valorIngresoAnual = decimal.Parse(Console.ReadLine());
            Console.Write("Valor Gasto   Anual                :"); 
            decimal valorGastosAnual = decimal.Parse(Console.ReadLine());
            Console.Write("Tiempo de Funcionamiento           :");
            int tiempoFuncionamiento = int.Parse(Console.ReadLine());
            string tipoResponsabilidad = ValidarResponsabilidad();
            var liquidacion= CrearLiquidacion(tipoResponsabilidad);
            liquidacion.Identificacion = identificacion;
            liquidacion.NombreEstablecimiento = nombreEstablecimiento;
            liquidacion.ValorIngresoAnual = valorIngresoAnual;
            liquidacion.ValorGastoAnual = valorGastosAnual;
            liquidacion.TipoResponsabilidad = tipoResponsabilidad;
            liquidacion.TiempoFuncionamiento = tiempoFuncionamiento;
            liquidacion.CalcularLiquidacion();
            return liquidacion;
        }
        public static LiquidacionImpuesto CrearLiquidacion(string tipoResponsabilidad)
        {
            LiquidacionImpuesto liquidacion;
            if (tipoResponsabilidad.Equals("CON IVA"))
            {
                liquidacion = new ResponsableIVA();
            }
            else if (tipoResponsabilidad.Equals("SIN IVA"))
            {
                liquidacion = new NoResponsableIVA();
            }
            else
            {
                liquidacion = new RegimenSimpleTributacion();
            }
            return liquidacion;
        }
       
        public static long ValidarIdentificacion()
        {
            long identificacion;
            string respuesta;
            do
            {
                Console.Write("Identificacion del Establecimiento :"); identificacion = long.Parse(Console.ReadLine());
                respuesta=liquidacionService.Busca(identificacion);
                Console.WriteLine(respuesta);
            } while (!respuesta.Equals("Identificacion Generada correctamente"));
          
            return identificacion;
        }
        public static string ValidarResponsabilidad()
        {
            int opcion;
            string tipoResponsabilidad;
            do
            {
                Console.Write("Tipo de Responsabilidad    ( 1.RESPONSABLE IVA    2.NO RESPONSABLE IVA 3.REGIMEN TRIBUTARIO  ) :");
                opcion = int.Parse(Console.ReadLine());
            } while (opcion < 1 && opcion > 3);

            if (opcion == 1)
            {
                tipoResponsabilidad = "CON IVA";
            }
            else if(opcion == 2)
            {
                tipoResponsabilidad = "SIN IVA";
            }
            else
            {
                tipoResponsabilidad = "RST";
            }
            return tipoResponsabilidad;
        }
        public static void ConsultarRegistros()
        {
            Console.Clear();
            Console.WriteLine("---------CONSULTA REGISTROS----------------");
            Console.WriteLine();
            var respuesta = liquidacionService.Consultar();
            if (respuesta.Error)
            {
                Console.WriteLine($"     { respuesta.Mensaje}");
            }
            else
            {
                foreach (var item in respuesta.Persona)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.Write("Pulse una tecla para salir "); Console.ReadKey();
            }
        }
 
        public static void Eliminar()
        {
            Console.Clear();
            Console.WriteLine("------Eliminar por Identificacion --------");
            Console.WriteLine();
            Console.Write(" Nro Liquidacion :"); long numeroIdentificacion = long.Parse(Console.ReadLine());
            string mensajeEliminacion = liquidacionService.Eliminar(numeroIdentificacion);
            Console.WriteLine($"    { mensajeEliminacion} ");
            Console.WriteLine();
            Console.Write("   Pulse una tecla para salir "); Console.ReadKey();
        }

    }
}
