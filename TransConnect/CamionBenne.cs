using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente un Camion Benne, un type spécifique de camion utilisé pour transporter des matériaux en vrac comme du sable, de la terre ou du gravier
    /// </summary>
    sealed internal class CamionBenne : Camion
    {
        /// <summary>
        /// Coût par kilomètre pour un Camion Benne (fixé par nous)
        /// </summary>
        public const double coutKmCamionBenne = 0.6;

        /// <summary>
        /// Calcule le tarif pour un Camion Benne en fonction du kilométrage
        /// </summary>
        /// <param name="kilometrage"> nombre de kilomètres parcourus </param>
        /// <returns> Tarif calculé pour le Camion Benne </returns>
        public override decimal CalculerTarif(int kilometrage)
        {
            return base.CalculerTarif(kilometrage);
        }

    }
}
