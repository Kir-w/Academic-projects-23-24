using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente un Salarié, qui hérite de la classe Personne et implémente les interfaces IEgalite, IComparaison<Salarie>, IComparable et IComparer<Salarie>
    /// </summary>
    internal class Salarie : Personne, IEgalite, IComparaison<Salarie>, IComparable, IComparer<Salarie>
    {
        DateTime dateEntree;
        string poste;
        string salaire;
        static int totalSalaries;


        #region constructeur 
        public Salarie(int SS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string numTelephone, DateTime dateEntree, string poste, string salaire)
            : base(SS, nom, prenom, dateNaissance, adressePostale, adresseMail, numTelephone)
        {
            this.dateEntree = dateEntree;
            this.poste = poste;
            this.salaire = salaire;
            totalSalaries += 1;
        }
        #endregion


        #region propriétés
        public string Poste
        {
            get { return poste; }
            set { poste = value; }
        }

        public string Salaire
        {
            get { return salaire; }
            set { salaire = value; }
        }

        public static int TotalSalaries
        {
            get { return totalSalaries; }
            set { totalSalaries = value; }
        }
        #endregion


        #region vérifier si les salaires de 2 salariés sont égaux 
        public bool Egalite(string val)
        {
            return salaire == val;
        }

        public bool Egalite1(Salarie s)
        { 
            return this.Egalite(s.salaire); 
        }
        #endregion


        #region trier les salariés par leur salaire (interface IComparable)
        public int Compare(Salarie s, Salarie s1)
        {
            return s.nom.CompareTo(s1.nom);
        }
        #endregion 


        #region trier les salariés par leur nom (interface IComparer<T>)
        public int CompareTo(object s)
        {
            return this.salaire.CompareTo(((Salarie)s).salaire);
        }
        #endregion



        public override string ToString()
        {
            return "Nom = " + nom + ", prenom = " + prenom + ", salaire = " + salaire + " euros";
        }

    }
}
