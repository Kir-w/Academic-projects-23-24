using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente un Client, une personne qui effectue des achats auprès de l'entreprise
    /// </summary>
    sealed internal class Client : Personne
    {
        string ville;
        double montantAchats;


        #region constructeur 
        public Client(string ville, double montantAchats, int SS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string numTelephone)
            : base(SS, nom, prenom, dateNaissance, adressePostale, adresseMail, numTelephone)
        {
            this.ville = ville;
            this.montantAchats = montantAchats; 
        }
        #endregion


        #region propriétés 
        public string Ville
        {
            get { return ville; }
            set { ville = value; }
        }

        public double MontantAchats
        {
            get { return montantAchats; }
            set { montantAchats = value; }
        }
        #endregion




        #region afficher les clients par ordre alphabétique

        /// <summary>
        /// Affiche les clients par ordre alphabétique de leur nom
        /// </summary>
        /// <param name="clients"> Liste des clients à afficher </param>
        public static void AfficherClientsAlphabetique(List<Client> clients)
        {
            // Trier les clients par nom
            var clientsTries = clients.OrderBy(c => c.Nom).ToList();

            // Afficher les clients triés
            foreach (var client in clientsTries)
            {
                Console.WriteLine($"{client.nom}, {client.prenom}");
            }
        }
        #endregion


        #region afficher les clients par ville
        public static void AfficherClientsParVille(List<Client> clients)
        {
            // Trier les clients par ville
            var clientsTries = clients.OrderBy(c => c.Ville).ToList();

            // Afficher les clients triés
            foreach (var client in clientsTries)
            {
                Console.WriteLine($"{client.nom}, {client.prenom} - {client.Ville}");
            }
        }
        #endregion


        #region afficher les clients par montant d'achats cumulés 
        public static void AfficherClientsParMontantAchatsCumule(List<Client> clients)
        {
            // Trier les clients par montant des achats cumulé
            var clientsTries = clients.OrderByDescending(c => c.MontantAchats).ToList();

            // Afficher les clients triés
            foreach (var client in clientsTries)
            {
                Console.WriteLine($"{client.nom}, {client.prenom} - Montant des achats cumulé : {client.MontantAchats}");
            }
        }
        #endregion
 


        #region le directeur de l'entreprise choisit comment il veut voir afficher ses clients parmi les 3 possibilités 
        public static void AfficherCombinaisonSimultanee(List<Client> clients)
        {
            // Afficher les options disponibles pour la combinaison personnalisée
            Console.WriteLine();
            Console.WriteLine("  Combinaison en simultané :");
            Console.WriteLine();
            Console.WriteLine("  3.4.1. Par ordre alphabétique");
            Console.WriteLine("  3.4.2. Par ville");
            Console.WriteLine("  3.4.3. Par montant des achats cumulé");
            Console.WriteLine("  3.4.4. Retour");
            Console.WriteLine();
            Console.WriteLine("Choix (par exemple, 123 pour afficher par ordre alphabétique, par ville et par montant des achats cumulé) : ");
            string choix = Console.ReadLine();

            if (choix.Contains("1"))
            {
                AfficherClientsAlphabetique(clients);
            }
            if (choix.Contains("2"))
            {
                AfficherClientsParVille(clients);
            }
            if (choix.Contains("3"))
            {
                AfficherClientsParMontantAchatsCumule(clients);
            }
        }
        #endregion




        #region ajout d'un nouveau client dans la liste des clients de l'entreprise 
        public static void AjouterClient(List<Client> clients)
        {
            // Demander les informations sur le nouveau client
            Console.Write("Nom du client : ");
            string nom = Console.ReadLine();

            Console.Write("Prénom du client : ");
            string prenom = Console.ReadLine();

            Console.Write("Adresse du client : ");
            string adresse = Console.ReadLine();

            Console.Write("Ville du client : ");
            string ville = Console.ReadLine();

            Console.Write("Montant des achats cumulé du client : ");
            double montantAchatsCumule = double.Parse(Console.ReadLine());

            
            Client nouveauClient = new Client(ville, montantAchatsCumule, 13275, nom, prenom, DateTime.Now, "", "", "");

            //Ajout du client à la liste des clients
            clients.Add(nouveauClient);
        }
        #endregion


        #region suppression d'un client de la liste des clients de l'entreprise 
        public static void SupprimerClient(List<Client> clients)
        {
            Console.Write("Nom du client à supprimer : ");
            string nomClient = Console.ReadLine();

            // Rechercher le client dans la liste
            Client clientASupprimer = clients.FirstOrDefault(c => c.Nom == nomClient);

            // Si le client existe, le supprimer
            if (clientASupprimer != null)
            {
                clients.Remove(clientASupprimer);
                Console.WriteLine("Client supprimé.");
            }
            else
            {
                Console.WriteLine("Client non trouvé.");
            }
        }
        #endregion


        #region modification des informations d'un client de l'entreprise 
        public static void ModifierClient(List<Client> clients)
        {
            Console.Write("Nom du client à modifier : ");
            string nomClient = Console.ReadLine();

            // Rechercher le client dans la liste
            Client clientAModifier = clients.FirstOrDefault(c => c.Nom == nomClient);

            // Si le client existe, demander les nouvelles informations
            if (clientAModifier != null)
            {
                Console.Write("Nouveau nom du client : ");
                string nouveauNom = Console.ReadLine();

                Console.Write("Nouveau prénom du client : ");
                string nouveauPrenom = Console.ReadLine();

                Console.Write("Nouvelle adresse du client : ");
                string nouvelleAdresse = Console.ReadLine();

                Console.Write("Nouvelle ville du client : ");
                string nouvelleVille = Console.ReadLine();

                Console.Write("Nouveau montant des achats cumulé du client : ");
                double nouveauMontantAchatsCumule = double.Parse(Console.ReadLine());

                // Mettre à jour les informations du client
                clientAModifier.nom = nouveauNom;
                clientAModifier.prenom = nouveauPrenom;
                clientAModifier.AdressePostale = nouvelleAdresse;
                clientAModifier.Ville = nouvelleVille;
                clientAModifier.MontantAchats = nouveauMontantAchatsCumule;

                Console.WriteLine("Client modifié.");
            }

            else
            {
                Console.WriteLine("Client non trouvé.");
            }
        }
        #endregion


    }
}
