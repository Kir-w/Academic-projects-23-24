using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// /// Représente l'organigramme de l'entreprise sous forme d'un arbre n-aire
    /// </summary>
    internal class Organigramme
    {
        Noeud racine;
        List<Chauffeur> listeChauffeurs; //Liste des chauffeurs


        #region constructeur 
        /// <summary>
        /// Constructeur de la classe Organigramme
        /// </summary>
        /// <param name="racine"> noeud racine de l'organigramme </param>
        public Organigramme(Noeud racine)
        {
            this.racine = racine;
            listeChauffeurs = new List<Chauffeur>(); // Initialisation de la liste
        }

        /// <summary>
        /// Constructeur de la classe Organigramme
        /// </summary>
        /// <param name="racine">boeud racine de l'organigramme </param>
        /// <param name="chauf"> liste des chauffeurs de l'entreprise </param>
        public Organigramme(Noeud racine, List<Chauffeur> chauf)
        {
            this.racine = racine;
            listeChauffeurs = chauf; 
        }
        #endregion




        #region ajouter un nouveau salarié dans l'organigramme 

        /// <summary>
        /// Ajoute un nouveau salarié dans l'organigramme
        /// </summary>
        /// <param name="nom"> nom du nouveau salarié </param>
        /// <param name="poste"> poste occupé par le nouveau salarié </param>
        public void Embaucher(string nom, string poste)
        {
            Noeud nouveauSalarié = new Noeud(nom, poste);

            // Rechercher le nœud parent le plus approprié pour ajouter le nouveau salarié
            Noeud noeudParent = RechercherNoeudParent(racine, poste);

            // Ajouter le nouveau salarié comme enfant du nœud parent
            noeudParent.Enfants = noeudParent.Enfants.Append(nouveauSalarié).ToArray();
        }
        #endregion


        #region enlever un salarié licencié de l'organigramme 

        /// <summary>
        /// Supprime un salarié de l'organigramme
        /// </summary>
        /// <param name="nom"> nom du salarié à licencier </param>
        /// <param name="poste"> poste occupé par le salarié à licencier </param>
        public void Licencier(string nom, string poste)
        {
            // Rechercher le nœud parent dans l'organigramme en fonction du poste
            Noeud parent = RechercherNoeudParent(racine, poste);

            if (parent != null)
            {
                //Suppression du nœud correspondant au salarié à licencier (c'est une maj)
                parent.Enfants = parent.Enfants.Where(e => e.Nom != nom).ToArray();
                Salarie.TotalSalaries -= 1;
            }
            else
            { Console.WriteLine("Erreur.");}
        }
        #endregion




        #region rechercher un noeud, i.e. un salarié 

        /// <summary>
        /// Recherche un noeud (salarié) dans l'organigramme par son nom
        /// </summary>
        /// <param name="noeud"> noeud à partir duquel on effectue la recherche </param>
        /// <param name="nom"> nom du salarié à rechercher </param>
        /// <returns> le nœud correspondant au salarié recherché, sinon null s'il n'est pas trouvé </returns>
        private Noeud RechercherNoeud(Noeud noeud, string nom)
        {
            if (noeud.Nom == nom)
            { return noeud;}

            foreach (var enfant in noeud.Enfants)
            {
                var resultatRecherche = RechercherNoeud(enfant, nom);
                if (resultatRecherche != null)
                {
                    return resultatRecherche;
                }
            }
            return null;
        }
        #endregion


        #region rechercher le parent d'un noeud, i.e. le N+1 d'un salarié 

        /// <summary>
        /// recherche le noeud parent le plus approprié pour un nouveau salarié en fonction de son poste
        /// </summary>
        /// <param name="noeud"> noeud à partir duquel effectuer la recherche </param>
        /// <param name="poste"> poste occupé par le nouveau salarié </param>
        /// <returns> le noeud parent le plus approprié pour le nouveau salarié </returns>
        private Noeud RechercherNoeudParent(Noeud noeud, string poste)
        {
            Noeud nouveau = null;
            int minEnfants = int.MaxValue;

            if (poste == "Commercial" || poste == "Commerciale")
            {
                if (noeud.Poste == "Directrice Commerciale")
                {
                    return noeud;
                }
            }
            else if (poste == "Chauffeur")
            {
                if (noeud.Poste == "Chef d'Equipe")
                {
                    //Ajout dans la branche (donc dans l'équipe) la moins remplie
                    if (noeud.Enfants.Length < minEnfants) 
                    {
                        nouveau = noeud;
                        minEnfants = noeud.Enfants.Length;
                    }
                }
            }
            else if (poste == "Comptable")
            {
                if (noeud.Poste == "Direction comptable")
                { return noeud;}
            }
            else
            {
                if (noeud.Poste == "Directrice des RH")
                { return noeud;}
            }

            // Sinon, rechercher le nœud parent le plus approprié dans les sous-branches
            foreach (var enfant in noeud.Enfants)
            {
                var resultatRecherche = RechercherNoeudParent(enfant, poste);
                if (resultatRecherche != null)
                {
                    return resultatRecherche;
                }
            }
            // Si aucun nœud parent n'a été trouvé, retourner le chef d'équipe le moins chargé
            return nouveau;
        }
        #endregion




        #region afficher l'organigramme
        /// <summary>
        /// Affiche l'organigramme dans la console
        /// </summary>
        public void Afficher()
        {
            Afficher(racine, 1);
        }

        /// <summary>
        /// Affiche récursivement les noeuds de l'organigramme avec leur niveau d'indentation
        /// </summary>
        /// <param name="noeud"> noeud à afficher </param>
        /// <param name="niveau"> niveau d'indentation du noeud </param>
        private void Afficher(Noeud noeud, int niveau)
        {
            string indentation = new string(' ', niveau * 5); //5 espaces par niveau de profondeur

            Console.WriteLine($"{indentation} - {noeud.Nom} / {noeud.Poste}");
            foreach (var enfant in noeud.Enfants)
            {
                Afficher(enfant, niveau + 1);
            }
        }
        #endregion





        #region ajouter un chauffeur à la liste des chauffeurs de l'entreprise 
        /// <summary>
        /// Ajoute un chauffeur à la liste des chauffeurs de l'entrepris
        /// </summary>
        /// <param name="chauffeur"> chauffeur à ajouter </param>
        public void AjouterChauffeur(Chauffeur chauffeur)
        {
            listeChauffeurs.Add(chauffeur);
        }
        #endregion


        #region trouver un chauffeur disponble pour effectuer une livraison 
        /// <summary>
        /// Trouve un chauffeur disponible pour effectuer une livraison
        /// </summary>
        /// <returns> Un chauffeur disponible, ou null si aucun chauffeur n'est disponible </returns>
        public Chauffeur TrouverChauffeurDisponible()
        {
            foreach (Chauffeur chauffeur in Chauffeur.ListeChauffeurs)
            {
                if (!chauffeur.LivraisonJour && chauffeur.Libre)
                {
                    return chauffeur;
                }
            }
            return null; // Aucun chauffeur disponible
        }
        #endregion


    }
}
