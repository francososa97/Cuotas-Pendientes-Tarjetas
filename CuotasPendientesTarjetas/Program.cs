using CuotasPendientesTarjetas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static CuotasPendientesTarjetas.Models.Cuota;
using static CuotasPendientesTarjetas.Models.Resumen;

namespace CuotasPendientesTarjetas
{
    class Program
    {

        static void Main(string[] args)
        {

            //Resumen de ejmplo para poder correr el rpograma
            List<Cuota> ResumenEjemplo = new List<Cuota>()
            {
                new Cuota("WWW MAXIMUS COM AR",10,12 ,1021.43m,Cuota.TipoMarca.Visa),
                new Cuota("CODERHOUSE CURSO",10,12 ,4466.66m,Cuota.TipoMarca.Visa),
                new Cuota("GARBARINO",4 ,6 ,1333.16m,Cuota.TipoMarca.Visa),
                new Cuota("YENNY PLAZA OESTE",2 ,6,1091.56m,Cuota.TipoMarca.Visa),
                new Cuota("WWWSOMMIERCENTERCOM",12,18,2077.66m, Cuota.TipoMarca.Visa),
                
            };


            var ResumenInicial = new Resumen(ResumenEjemplo, DateTime.Now);
            var CuotasGrabar = BuildCuotas(ResumenInicial);
            bool operacionExitosa = GrabarArchivo(CuotasGrabar);

            string resultado = operacionExitosa ? "Se logro grabar el archivo " : "No logro grabar el archivo";
            Console.WriteLine(resultado);
                

        }

        /// <summary>
        /// Obteiene el mes actual de tu resumen haciendo una validacion previa
        /// </summary>
        /// <param name="tipoMes"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        public static TipoMes ObtenerMes(TipoMes tipoMes, int mes)
        {
            int ValorMes= (int)tipoMes;
            if ((ValorMes + mes) > 12)
            {
                int mesActual = (ValorMes + mes) - 12;
                return (TipoMes)mesActual;
            }
            else
            {
                int mesActual = ValorMes + mes;
                return (TipoMes)mesActual;

            }
        }

        /// <summary>
        /// Metodo que crear el string a grabar en el archivo txt, recibiendo el modelo de resumen
        /// </summary>
        /// <param name="resumen"></param>
        /// <returns></returns>
        public static StringBuilder BuildCuotas(Resumen resumen)
        {
            StringBuilder registro = new StringBuilder();
            int cuotaMayorPlazo = resumen.CuotaMasLongeba;
            int indice = 0;

            var cuotasVisa = resumen.Cuotas.Where(x=> x.MarcaCuota == Cuota.TipoMarca.Visa).ToList();
            var cuotasMaster = resumen.Cuotas.Where(x => x.MarcaCuota == Cuota.TipoMarca.MasterCard).ToList();
            var cuotasAmex= resumen.Cuotas.Where(x => x.MarcaCuota == Cuota.TipoMarca.AmericanExpres).ToList();
            cuotasVisa.OrderByDescending(x => x.CuotaActual);

            cuotasVisa = (from cuota in cuotasVisa
                         orderby cuota.CuotaActual
                         select cuota).ToList();

            cuotasMaster = (from cuota in cuotasMaster
                            orderby cuota.CuotaActual
                          select cuota).ToList();

            cuotasAmex = (from cuota in cuotasAmex
                          orderby cuota.CuotaActual
                          select cuota).ToList();

            while (cuotaMayorPlazo > 0)
            {
                if (cuotasVisa.Count > 0 && cuotasVisa.Any(x => x.CuotaActual - indice > 0))
                {
                    MostrarCuotasMensuales(registro, cuotasVisa, resumen.MesActual, indice, TipoMarca.Visa);
                }
                if (cuotasMaster.Count > 0 && cuotasMaster.Any(x=> x.CuotaActual-indice > 0))
                {
                    MostrarCuotasMensuales(registro, cuotasMaster, resumen.MesActual, indice, TipoMarca.MasterCard);
                }
                if (cuotasAmex.Count > 0 && cuotasAmex.Any(x => x.CuotaActual - indice > 0))
                {
                    MostrarCuotasMensuales(registro, cuotasAmex, resumen.MesActual, indice, TipoMarca.AmericanExpres);
                }

                cuotaMayorPlazo--;
                indice++;
            }

            return registro;

        }

        /// <summary>
        /// Graba las cuotas mensuales en el string builder
        /// </summary>
        /// <param name="registro"></param>
        /// <param name="cuotas"></param>
        /// <param name="mesActual"></param>
        /// <param name="indice"></param>
        /// <param name="marca"></param>
        public static void MostrarCuotasMensuales(StringBuilder registro, List<Cuota> cuotas, TipoMes mesActual, int indice, TipoMarca marca)
        {
            List<decimal> TotalResumen = new List<decimal>();
            GuardarMostrarContenido(registro, $"-------------------- Cuotas de {marca} de mes {ObtenerMes(mesActual, indice)} --------------------\n Nombre                  |  Cuota actual            |  Cuota Total         |   Movimientos \n");

            cuotas.ForEach(cuota =>
            {
                int pocicionCuota = cuota.CuotaActual - indice;
                if (pocicionCuota > 0)
                {
                    TotalResumen.Add(cuota.ValorCuota);
                    GuardarMostrarContenido(registro, $"{cuota.Nombre + "    "}|  {pocicionCuota + " "}      |  {cuota.CuotaTotal + " "}       |  {cuota.ValorCuota}");
                }
            });
            GuardarMostrarContenido(registro, $"Total mensual: {TotalResumen.Sum()}");
        }


        /// <summary>
        /// Guarda informmacion en el string builder pasada por parametro y muestra por consola
        /// </summary>
        /// <param name="textoGrabar"></param>
        /// <param name="texto"></param>
        public static void GuardarMostrarContenido(StringBuilder textoGrabar,string texto)
        {
            textoGrabar.AppendLine(texto);
            Console.WriteLine($"\n{texto}");
        }


        /// <summary>
        /// Metodo que graba el archivo txt de tu resumen recibe el string
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <returns></returns>
        public static bool GrabarArchivo(StringBuilder stringBuilder)
        {
            try
            {
                using (StreamWriter Archivo = new StreamWriter("C:/Users/usuario/Desktop/Tarjeta.txt", true, Encoding.UTF8))
                {
                    Archivo.WriteLine(stringBuilder.ToString());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
