using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente une Camionnette, un type spécifique de véhicule utilisé pour le transport de petits volumes
    /// </summary>
    sealed internal class Camionnette : Vehicule
    {
        /// <summary>
        /// Coût par kilomètre pour une Camionnette
        /// </summary>
        public const double coutKmCamionnette = 0.4;

        /// <summary>
        /// Constructeur de la classe Camionnette
        /// </summary>
        public Camionnette() : base(coutKmCamionnette) { }

        /// <summary>
        /// Calcule le tarif pour une Camionnette en fonction du kilométrage
        /// </summary>
        /// <param name="kilometrage"> le kilométrage parcouru </param>
        /// <returns> le tarif calculé en fonction du kilométrage </returns>
        public override decimal CalculerTarif(int kilometrage)
        {
            return base.CalculerTarif(kilometrage);
        }

    }
}
