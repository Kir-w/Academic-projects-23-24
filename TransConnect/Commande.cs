using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente une Commande, qui comprend un client, une livraison, un véhicule, un chauffeur et un prix
    /// </summary>
    internal class Commande : IMoyenne
    {
        public int num;
        public Client client;
        Livraison livraison;
        decimal prix;
        Vehicule vehicule;
        Chauffeur chauffeur;
        DateTime date;

        static Chauffeur chauffeurDisponible;

        static int totalCommandes;



        #region constructeur
        public Commande(int num, Client client, Livraison livraison, decimal prix, Vehicule vehicule, DateTime date, Chauffeur chauffeur = null)
        {
            this.num = num;
            this.Client = client;
            this.Livraison = livraison;
            this.Prix = prix;
            this.Vehicule = vehicule;
            this.Date = date;
            this.Chauffeur = chauffeur;
            totalCommandes += 1;
        }
        #endregion


        #region propriétés

        public Client Client
        {
            get { return client; }
            set { client = value; }
        }

        public Livraison Livraison
        {
            get { return livraison; }
            set { livraison = value; }
        }

        public decimal Prix
        {
            get { return prix; }
            set { prix = value; }
        }

        public Vehicule Vehicule
        {
            get { return vehicule; }
            set { vehicule = value; }
        }

        public Chauffeur Chauffeur
        {
            get { return chauffeur; }
            set { chauffeur = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public Chauffeur ChauffeurDisponible
        {
            get { return chauffeurDisponible; }
            set { chauffeurDisponible = value; }
        }

        public static int TotalCommandes 
        {
            get { return totalCommandes; }
        }

        #endregion




        #region afficher les commandes faites 
        public static void AfficherCommandes(List<Commande> commandes)
        {
            if (commandes.Count == 0)
            {
                Console.WriteLine("  La liste des commandes est vide.");
            }
            else
            {
                Console.WriteLine("  Liste des commandes :");
                foreach (var commande in commandes)
                {
                    Console.WriteLine($"  Numéro : {commande.num}, Client : {commande.Client.Nom}, Date : {commande.Date}");
                }
            }
        }
        #endregion



        #region créer une nouvelle commande
        public static void CreerNouvelleCommande(List<Client> clients, List<Commande> commandes, Organigramme organigramme, int[,] poids_km, int[,] poids_temps)
        {
            Client nouveauClient = null;
            Console.Write("  Nom du client : ");
            string nomClient = Console.ReadLine();
            Console.WriteLine();

            // Vérifier si le client existe déjà dans la base
            Client clientExistant = null;
            foreach (Client client in clients)
            {
                if (client.Nom == nomClient)
                {
                    clientExistant = client;
                    break;
                }
            }

            if (clientExistant != null)
            {
                // Le client existe déjà, pas besoin de le créer
                Console.WriteLine("  Client trouvé.");
            }
            else
            {
                // Le client n'existe pas, créer un nouveau client
                Console.WriteLine("  Client non trouvé.");
                Console.WriteLine("  Création d'un nouveau client...");
                Console.WriteLine();
                Console.Write("  Ville du client : ");
                string villeClient = Console.ReadLine();

                Console.Write("  Montant des achats cumulés du client (en euros): ");
                double montantAchats = double.Parse(Console.ReadLine());

                // Créer un nouveau client et l'ajouter à la liste des clients
                nouveauClient = new Client(villeClient, montantAchats, 14793, nomClient, "", DateTime.Now, "", "", "");
                clients.Add(nouveauClient);

                Console.WriteLine("  Nouveau client créé.");
            }
            Console.WriteLine(); //pour aérer

            int i = commandes.Count + 1; //Numéro de commande

            Console.WriteLine($"  Pour la commande n°{i}, saisir la ville de départ : ");
            string villeDepart = Console.ReadLine();

            Console.WriteLine($"  Pour la commande n°{i}, saisir la ville d'arrivée : ");
            string villeArrivee = Console.ReadLine();

            string cheminlpc = Livraison.CheminLePlusCourt(villeDepart, villeArrivee, poids_km, poids_temps);
            int distance = Livraison.CheminLePlusCourtTarif(villeDepart, villeArrivee, poids_km, poids_temps);

            Console.WriteLine();//pour aérer
            Console.WriteLine($"  Un chemin a été généré : {cheminlpc}");

            Client clientCommande = clientExistant ?? nouveauClient;
            Commande nouvelleCommande = new Commande(i, clientCommande, new Livraison(villeDepart, villeArrivee), 0, new Vehicule(0.5), DateTime.Now, null);

            // Ajouter la nouvelle commande à la liste des commandes
            commandes.Add(nouvelleCommande);
            Console.WriteLine(); //pour aérer
            Console.WriteLine($"  Nouvelle commande ajoutée pour le client {clientCommande.Nom}.");

            Console.WriteLine(); //pour aérer

            Console.WriteLine("  Recherche d'un chauffeur disponible...");

            Chauffeur chauffeurDisponible = organigramme.TrouverChauffeurDisponible();

            if (chauffeurDisponible != null)
            { 
                Console.WriteLine($"  Le chauffeur suivant est disponible : {chauffeurDisponible.Nom}");
                nouvelleCommande.Chauffeur = chauffeurDisponible;
                chauffeurDisponible.LivraisonJour = true; // Marquer le chauffeur comme ayant une livraison pour la journée
                chauffeurDisponible.Libre = false; // Marquer le chauffeur comme non disponible
                chauffeurDisponible.LivraisonsEffectuees++; // Incrémenter le nombre de livraisons effectuées
            }

            else Console.WriteLine("  Aucun chauffeur n'est disponible pour le moment, réessayez plus tard.");


            Console.WriteLine(); //pour aérer
            Console.Write("  Transport de passagers ou de produits ? (passagers/produits)");
            string reponse = Console.ReadLine();

            Vehicule vehicule = null;

            if (reponse == "passagers")
            {
                Console.Write("  Nombre de passagers ?");
                int nbPassagers = int.Parse(Console.ReadLine());
                vehicule = new Voiture() { nbPassagers = nbPassagers };
                Console.WriteLine($"  Une voiture est disponible pour {nbPassagers} passagers.");
            }

            else if (reponse == "produits")
            {
                Console.Write("  Choix du produit transporté (verres/liquide/gaz/sable/terre/gravier/marchandises périssables) : ");
                string produit = Console.ReadLine();

                if (produit == "verres")
                {
                    vehicule = new Camionnette();
                }
                else if (produit == "liquide" || produit == "gaz")
                {
                    vehicule = new CamionCiterne();
                }
                else if (produit == "sable" || produit == "terre" || produit == "gravier")
                {
                    vehicule = new CamionBenne();
                }
                else if (produit == "marchandises périssables")
                {
                    vehicule = new CamionFrigorifique();
                }
                Console.WriteLine($"  Ce vehicule : {vehicule} est disponible.");
            }

            decimal tarif = vehicule.CalculerTarif(distance); //CalculerTarif provenant de Vehicule.cs

            Console.WriteLine($"  Le tarif s'élève à {tarif} euros");
        }
        #endregion



        #region modifier une commande
        public static void ModifierCommande(List<Commande> commandes)
        {
            Console.WriteLine("  Veuillez entrer le numéro de commande que vous souhaitez modifier : ");
            int numeroCommande = int.Parse(Console.ReadLine());

            Commande commandeAModifier = null;

            foreach (Commande cmd in commandes)
            {
                if (cmd.num == numeroCommande)
                {
                    commandeAModifier = cmd;
                    break;
                }
            }

            if (commandeAModifier != null)
            {
                Console.WriteLine("  Commande trouvée.");
                Console.WriteLine();

                Console.WriteLine("  Quelles informations souhaitez-vous modifier ?");
                Console.WriteLine("  1. Ville de départ");
                Console.WriteLine("  2. Ville d'arrivée");
                Console.WriteLine("  3. Nom du client");
                Console.WriteLine("  4. Suppression de commande");
                Console.WriteLine("  5. Retour");

                Console.Write("  Votre choix (1/2/3/4/5) : ");

                bool retour = false;
                while (!retour)
                {
                    string choix = Console.ReadLine();
                    switch (choix)
                    {
                        case "1":
                            Console.Write("  Nouvelle ville de départ : ");
                            commandeAModifier.livraison.villeDepart = Console.ReadLine();
                            break;
                        case "2":
                            Console.Write("  Nouvelle ville d'arrivée : ");
                            commandeAModifier.livraison.villeArrivee = Console.ReadLine();
                            break;
                        case "3":
                            Console.Write("  Nouveau nom du client : ");
                            commandeAModifier.client.Nom = Console.ReadLine();
                            break;
                        case "4":
                            SupprimerCommande(commandes);
                            retour = true;
                            break;
                        case "5":
                            retour = true;
                            break;
                        default:
                            Console.WriteLine("  Choix invalide.");
                            break;
                    }
                    if (!retour)
                        Console.WriteLine("  Commande modifiée avec succès.");
                }
            }
            else
            {
                Console.WriteLine("  Aucune commande trouvée avec ce numéro.");
            }

        }
        #endregion



        #region supprimer une commande
        public static void SupprimerCommande(List<Commande> commandes)
        {
            Console.WriteLine("  Veuillez entrer le numéro de commande que vous souhaitez supprimer : ");
            int numeroCommande = int.Parse(Console.ReadLine());

            Commande commandeASupprimer = commandes.Find(cmd => cmd.num == numeroCommande);

            if (commandeASupprimer != null)
            {
                Console.WriteLine($"  Commande n°{commandeASupprimer.num} pour le client {commandeASupprimer.client.Nom}");
                Console.Write("  Êtes-vous sûr de vouloir supprimer cette commande ? (Oui/Non) ");
                string confirmation = Console.ReadLine().ToUpper();

                if (confirmation == "Oui")
                {
                    commandes.Remove(commandeASupprimer);
                    Console.WriteLine("  Commande supprimée avec succès.");
                }
                else
                {
                    Console.WriteLine("  Suppression annulée.");
                }
            }
            else
            {
                Console.WriteLine("  Aucune commande trouvée avec ce numéro.");
            }

        }
        #endregion




        #region la moyenne du prix des commandes effectuées 
        public float Moyenne()
        {
            return (float)Prix;
        }

        public static void AfficherMoyennePrixCommandes(List<Commande> commandes)
        {
            if (commandes.Count > 0)
            {
                float totalPrix = commandes.Sum(c => c.Moyenne());
                float moyennePrix = totalPrix / commandes.Count;
                Console.WriteLine($"  La moyenne des prix des commandes s'élève à {moyennePrix} euros");
            }
            else
            {
                Console.WriteLine("  Aucune commande disponible.");
            }
        }
        #endregion


        #region la moyenne des montants des comptes des clients 
        public static void AfficherMoyenneComptesClients(List<Client> clients)
        {
            if (clients.Count > 0)
            {
                double totalComptes = clients.Sum(c => c.MontantAchats);
                double moyenneComptes = totalComptes / clients.Count;
                Console.WriteLine($"  Moyenne des comptes clients : {moyenneComptes} euros");
            }
            else
            {
                Console.WriteLine("  Aucun client disponible.");
            }
        }
        #endregion



    }
}
