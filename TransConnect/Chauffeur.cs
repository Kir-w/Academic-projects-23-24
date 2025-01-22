using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente un Chauffeur, un type spécifique de Salarié
    /// </summary>
    sealed internal class Chauffeur : Salarie 
    {
        bool livraisonJour; //Un chauffeur ne peut faire qu'une seule livraison par jour
        bool libre; //Il ne peut livrer que s'il est libre
        int livraisonsEffectuees;
        static List<Chauffeur> listeChauffeurs  = new List<Chauffeur>();


        #region constructeur 
        /// <summary>
        /// Constructeur de la classe Chauffeur
        /// </summary>
        /// <param name="SS"> numéro de sécurité sociale du chauffeur </param>
        /// <param name="nom"> nom du chauffeur </param>
        /// <param name="prenom"> prénom du chauffeur </param>
        /// <param name="dateNaissance"> date de naissance du chauffeur </param>
        /// <param name="adressePostale"> adresse postale du chauffeur </param>
        /// <param name="adresseMail">< adresse mail du chauffeur /param>
        /// <param name="numTelephone"> numéro de téléhpone du chauffeur </param>
        /// <param name="dateEntree"> date d'entrée du chauffeur dans l'entreprise </param>
        /// <param name="poste"> poste occupé </param>
        /// <param name="salaire"> salaire du chauffeur </param>
        public Chauffeur(int SS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string numTelephone, DateTime dateEntree, string poste, string salaire)
            : base(SS, nom, prenom, dateNaissance, adressePostale, adresseMail, numTelephone, dateEntree, poste, salaire)
        {
            livraisonJour = false; // Par défaut, le chauffeur n'a pas de livraison pour la journée
            libre = true; // Par défaut, le chauffeur est disponible
            livraisonsEffectuees = 0; // Par défaut, le chauffeur n'a effectué aucune livraison
            ListeChauffeurs.Add(this);
        }
        #endregion


        #region propriétés 

        /// <summary>
        /// Propriété indiquant si le chauffeur est disponible pour effectuer une livraison
        /// </summary>
        public bool Libre
        {
            get { return libre; }
            set { libre = value; }
        }

        /// <summary>
        /// Propriété indiquant si le chauffeur a une livraison à effectuer dans la journée
        /// </summary>
        public bool LivraisonJour
        {
            get { return livraisonJour; }
            set { livraisonJour = value; }
        }

        /// <summary>
        /// Propriété représentant le nombre de livraisons effectuées par le chauffeur
        /// </summary>
        public int LivraisonsEffectuees
        {
            get { return livraisonsEffectuees; }
            set { livraisonsEffectuees = value; }
        }

        /// <summary>
        /// Propriété représentant la liste de tous les chauffeurs de l'entreprise
        /// </summary>
        public static List<Chauffeur> ListeChauffeurs
        {
            get { return listeChauffeurs; }
            set {  listeChauffeurs = value;}
        }

        #endregion




    }
}
