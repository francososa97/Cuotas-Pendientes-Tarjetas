using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuotasPendientesTarjetas.Models
{
    class Resumen
    {
        /// <summary>
        /// Lista de cuotas de tu resumen 
        /// </summary>
        public List<Cuota> Cuotas { get; set; }
        /// <summary>
        // monto de tu resumen mensual
        /// </summary>
        public decimal MontoTotal { get; set; }
        /// <summary>
        /// Mes actual desde donde se obtiene el resumen
        /// </summary>
        public int MesResumenInicial { get; set; }
        /// <summary>
        /// Tipo de mes actual - enumerable que representa el mes actual del resumen
        /// </summary>
        public TipoMes MesActual { get; set; }
        /// <summary>
        /// Numero de cantidads de cuotas restantes mas longeba
        /// </summary>
        public int CuotaMasLongeba { get; set; }
        /// <summary>
        /// Fecha inicial del resumen que se obtiene en el consturctor
        /// </summary>
        public DateTime FechaResumenInicial { get; set; }
 
        /// <summary>
        /// Unico Constructor obtiene informacion de tu resumen para cacluclar las cuotas mensuales
        /// </summary>
        /// <param name="Cuotas"></param>
        /// <param name="ResumenInicial"></param>
        public Resumen(List<Cuota> Cuotas, DateTime ResumenInicial) 
        { 
            this.Cuotas = Cuotas;
            this.MontoTotal = Cuotas.Sum(Cuota => Cuota.ValorCuota);
            FechaResumenInicial = ResumenInicial;
            this.MesResumenInicial = ResumenInicial.Month;
            this.MesActual = (TipoMes)ResumenInicial.Month;
            this.CuotaMasLongeba = Cuotas.Max(x => x.CuotaActual);
        }
        /// <summary>
        /// Enumerable que representa los meses de tu resumen
        /// </summary>
        public enum TipoMes
        {
            Enero = 1,
            Febrero = 2,
            Marzo = 3,
            Abril = 4,
            Mayo = 5,
            Junio = 6,
            Julio = 7,
            Agosto = 8,
            Septiembre = 9,
            Octubre = 10,
            Noviembre = 11,
            Diciembre =12
        }

    }
}
