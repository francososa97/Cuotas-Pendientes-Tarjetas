using System;
using System.Collections.Generic;
using System.Text;

namespace CuotasPendientesTarjetas.Models
{
    class Cuota
    {
        /// <summary>
        /// Nombre de la cuota pendiente de tu resumen
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// numero de cuotas restantes de la cuota actual
        /// </summary>
        public int CuotaActual{ get; set; }
        /// <summary>
        /// Cantidad de cuotas totales de tu movimiento
        /// </summary>
        public int CuotaTotal { get; set; }
        /// <summary>
        /// Monto de la cuota actual
        /// </summary>
        public decimal ValorCuota { get; set; }
        /// <summary>
        /// Enumerable de tipo marca de cuota, esto representa si la cuota es de una tarjeta visa,mastercard o american express
        /// </summary>
        public TipoMarca MarcaCuota { get; set; }

        /// <summary>
        /// Constructor cuota simple sin marca de tarjeta
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="CuotaActual"></param>
        /// <param name="CuotaTotal"></param>
        /// <param name="ValorCuota"></param>
        public Cuota(string Nombre,int CuotaActual, int CuotaTotal,decimal ValorCuota)
        {
            this.Nombre = Nombre;
            this.CuotaActual = CuotaActual;
            this.CuotaTotal = CuotaTotal;
            this.ValorCuota = ValorCuota;
        }
        /// <summary>
        /// Constructor de cuota que filtra por tipo de marca
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="CuotaActual"></param>
        /// <param name="CuotaTotal"></param>
        /// <param name="ValorCuota"></param>
        /// <param name="tipo"></param>
        public Cuota(string Nombre, int CuotaActual, int CuotaTotal, decimal ValorCuota, TipoMarca tipo)
        {
            this.Nombre = Nombre;
            this.CuotaActual = CuotaActual;
            this.CuotaTotal = CuotaTotal;
            this.ValorCuota = ValorCuota;
            this.MarcaCuota = tipo;
        }
        /// <summary>
        /// Enumerable que representa el tipo de tarjeta a la cual esta asociada la cuota
        /// </summary>
        public enum TipoMarca
        {
            Visa = 0,
            MasterCard = 1,
            AmericanExpres = 2,
        }
    }
}
