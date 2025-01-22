using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente un Camion Frigorifique, un type spécifique de camion utilisé pour transporter des produits réfrigérés comme des aliments frais ou des médicaments
    /// </summary>
    sealed internal class CamionFrigorifique : Camion
    {
        /// <summary>
        /// Coût par kilomètre pour un Camion Frigorifique
        /// </summary>
        public const double coutKmCamionFrigorifique = 0.7;

        /// <summary>
        /// Calcul du tarif pour un Camion Frigorifique en fonction du kilométrage
        /// </summary>
        /// <param name="kilometrage"></param>
        /// <returns> tarif calculé pour le Camion Frigorifique </returns>
        public override decimal CalculerTarif(int kilometrage)
        {
            return base.CalculerTarif(kilometrage);
        }

    }
}
