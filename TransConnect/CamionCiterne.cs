using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente un Camion Citerne, un type spécifique de camion utilisé pour transporter des liquides en vrac comme de l'eau, du carburant ou des produits chimiques
    /// </summary>
    sealed internal class CamionCiterne : Camion
    {
        /// <summary>
        /// Coût par kilomètre pour un Camion Citerne
        /// </summary>
        public const double coutKmCamionCiterne = 0.6;

        /// <summary>
        /// Calcule le tarif pour un Camion Citerne en fonction du kilométrage
        /// </summary>
        /// <param name="kilometrage"> nombre de kilomètres parcourus </param>
        /// <returns> tarif calculé pour le Camion Citerne </returns>
        public override decimal CalculerTarif(int kilometrage)
        {
            return base.CalculerTarif(kilometrage);
        }

    }
}
