using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente un Véhicule utilisé pour effectuer des livraisons
    /// </summary>
    internal class Vehicule
    {
        public float kilometrage;
        public double tarifService = 20.0;
        public double coutKilometre;


        #region constructeur 
        public Vehicule(double coutKilometre)
        {
            this.coutKilometre = coutKilometre;
        }
        #endregion


        public virtual decimal CalculerTarif(int kilometrage)
        {
            // Calcul du tarif en fonction du kilométrage et du type de véhicule
            return kilometrage * (decimal)coutKilometre + (decimal)tarifService; ;
        }

    }
    //Pour un poids lourd (20 tonnes): 
    //1.75€/l d'après https://www.europe-camions.com/news/oil-price et
    //30 litres pour 100 km environ d'après https://www.ecolow.fr/consommation-camion/#:~:text=Quelle%20autonomie%20pour%20un%20camion,40%20Litres%20pour%20100%20km.
    //
    //si on estime le tarif par : tarif = coût au km (0.5€/km) + service (20€ par heure)
}
