using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente un noeud dans l'organigramme de l'entreprise
    /// </summary>
    internal class Noeud
    {
        string nom;
        string poste;
        Noeud[] enfants;


        #region  constructeurs 
        public Noeud(string nom, string poste, params Noeud[] enfants)
        {
            this.nom = nom;
            this.poste = poste;
            this.enfants = enfants;
        }
        #endregion


        #region propriétés 
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Poste
        {
            get { return poste; }
            set { poste = value; }
        }

        public Noeud[] Enfants
        {
            get { return enfants; }
            set { enfants = value; }
        }
        #endregion

    }
}
