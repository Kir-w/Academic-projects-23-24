using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Réprésente un Camion, un des types de véhicule
    /// </summary>
    abstract internal class Camion : Vehicule   
    {
        /// <summary>
        /// Coût par kilomètre pour un Camion
        /// </summary>
        public const double coutKmCamion = 0.5;

        /// <summary>
        /// Constructeur de la classe Camion
        /// </summary>
        public Camion() : base(coutKmCamion) { }

        /// <summary>
        /// Calcule le tarif pour un Camion en fonction du kilométrage
        /// </summary>
        /// <param name="kilometrage"> le kilométrage parcouru </param>
        /// <returns> le tarif calculé en fonction du kilométrage </returns>
        public override decimal CalculerTarif(int kilometrage)
        {
            return base.CalculerTarif(kilometrage);
        }

    }
}
