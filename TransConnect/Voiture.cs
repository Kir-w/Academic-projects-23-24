using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente une Voiture, un type spécifique de Véhicule utilisé pour le transport de passagers
    /// </summary>
    sealed internal class Voiture : Vehicule
    {
        public int nbPassagers;
        public const double coutKmVoiture = 0.3;


        public Voiture() : base(coutKmVoiture) { } 


        public override decimal CalculerTarif(int kilometrage)
        {
            return base.CalculerTarif(kilometrage);
        }


    }
}
