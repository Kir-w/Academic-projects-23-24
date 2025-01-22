using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente une personne avec ses informations personnelles
    /// </summary>
    
    abstract internal class Personne
    {
        int SS;
        protected string nom;
        public string prenom;
        DateTime dateNaissance;
        protected string adressePostale;
        string adresseMail;
        string numTelephone;


        #region constructeur 

        /// <summary>
        /// Initialisation d'une nouvelle instance de la classe Personne
        /// </summary>
        /// <param name="SS"> le numéro de sécurité sociale de la personne </param>
        /// <param name="nom"> le nom de la personne </param>
        /// <param name="prenom"> le prénom de la personne </param>
        /// <param name="dateNaissance"> la date de naissance de la personne </param>
        /// <param name="adressePostale"> l'adresse postale de la personne </param>
        /// <param name="adresseMail"> l'adresse mail de la personne </param>
        /// <param name="numTelephone"> le numéro de téléphone de la personne </param>
        public Personne(int SS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string numTelephone)
        {
            this.SS = SS;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.adressePostale = adressePostale;
            this.adresseMail = adresseMail;
            this.numTelephone = numTelephone;
        }
        #endregion


        #region propriétés 

        /// <summary>
        /// Obtenir ou définir le Nom de la personne
        /// </summary>
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        /// <summary>
        /// Obtenir ou définir l'adresse postale de la personne
        /// </summary>
        public string AdressePostale
        {
            get { return adressePostale; }
            set { adressePostale = value; }
        }

        /// <summary>
        /// Obtenir ou définir l'email de la personne
        /// </summary>
        public string AdresseMail
        {
            get { return adresseMail; }
            set { adresseMail = value; }
        }

        /// <summary>
        /// Obtenir ou définir le numéro de téléphone de la personne
        /// </summary>
        public string NumTelephone
        {
            get { return numTelephone; }
            set { numTelephone = value; }
        }
        #endregion

    }
}
