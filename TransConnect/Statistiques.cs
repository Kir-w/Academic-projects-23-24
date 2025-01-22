using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente différentes options de statistiques pour les livraisons, les chauffeurs ou les commandes
    /// </summary>
    internal class Statistiques
    {

        #region les livraisons effectuées par chauffeur 
        public static void AfficherLivraisonsParChauffeur(List<Chauffeur> chauffeurs)
        {
            foreach (Chauffeur c in chauffeurs)
            {
                Console.WriteLine($"  {c.Nom} a {c.LivraisonsEffectuees} livraisons effectuées");
            }
        }
        #endregion


        #region choix d'afficher les commandes par jour, mois ou année
        public static void AfficherCommandesParPeriode(List<Commande> commandes)
        {
            string formatDate = "jj/mm/aaaa";
            string dateCommande = ""; //variable pour stocker

            bool retour = false;
            while (!retour)
            {
                Console.WriteLine("  Afficher les commandes par : jour ? mois ? année ? (jour/mois/année/retour): ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "jour":
                        AfficherCommandesParJour(commandes);
                        break;
                    case "mois":
                        AfficherCommandesParMois(commandes);
                        break;
                    case "année":
                        AfficherCommandesParAnnee(commandes);
                        break;
                    case "retour":
                        retour = true;
                        break;
                    default:
                        Console.WriteLine("  Choix non valide. Veuillez réessayer.");
                        break;
                }
            }
        }
        #endregion


        #region commandes par jour 
        private static void AfficherCommandesParJour(List<Commande> commandes)
        {
            while (true)
            {
                Console.Write("  Entrez la date (jj/mm/aaaa) ou 'retour' pour revenir au menu précédent : ");
                string saisie = Console.ReadLine();

                if (saisie == "retour")
                    return;
                else
                {
                    if (DateTime.TryParseExact(saisie, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                    {
                        AfficherCommandesPourPeriode(commandes, date, date);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Format de date invalide. Veuillez réessayer.");
                    }
                }
            }
        }
        #endregion


        #region commandes par mois 
        private static void AfficherCommandesParMois(List<Commande> commandes)
        {
            bool moisValide = false;

            while (!moisValide)
            {
                Console.Write("  Entrez le mois et l'année (mm/aaaa) ou 'retour' pour revenir au menu précédent: ");
                string saisie = Console.ReadLine();

                if (saisie == "retour")
                    return;
                else
                {
                    if (DateTime.TryParseExact(saisie, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateDebut))
                    {
                        DateTime dateFin = new DateTime(dateDebut.Year, dateDebut.Month, DateTime.DaysInMonth(dateDebut.Year, dateDebut.Month));
                        moisValide = true;

                        AfficherCommandesPourPeriode(commandes, dateDebut, dateFin);
                    }
                    else
                    {
                        Console.WriteLine("Format de date invalide. Veuillez réessayer.");
                    }
                }
            }
        }
        #endregion


        #region commandes par année
        private static void AfficherCommandesParAnnee(List<Commande> commandes)
        {
            bool anneeValide = false;

            while (!anneeValide)
            {
                Console.Write("  Entrez l'année (aaaa) ou 'retour' pour revenir au menu précédent : ");
                string saisie = Console.ReadLine();

                if (saisie == "retour")
                    return;
                else
                {
                    DateTime dateDebut = new DateTime(int.Parse(saisie), 1, 1);
                    DateTime dateFin = new DateTime(int.Parse(saisie), 12, 31);
                    anneeValide = true;

                    AfficherCommandesPourPeriode(commandes, dateDebut, dateFin);
                }
            }
        }
        #endregion


        #region commandes réalisées pour une période donnée 
        private static void AfficherCommandesPourPeriode(List<Commande> commandes, DateTime dateDebut, DateTime dateFin)
        {
            List<Commande> commandesPeriode = commandes.Where(c => c.Date >= dateDebut && c.Date <= dateFin).ToList();

            if (commandesPeriode.Count > 0)
            {
                Console.WriteLine("  Commandes pour la période :");
                foreach (Commande commande in commandesPeriode)
                {
                    Console.WriteLine($"  Numéro : {commande.num}, Client : {commande.client.Nom}, Date : {commande.Date}, Prix : {commande.Prix}");
                }
            }
            else
            {
                Console.WriteLine("  Aucune commande pour cette période.");
            }
        }
        #endregion



        #region les commandes par client 
        public static void AfficherCommandesParClient(List<Commande> commandes, List<Client> clients)
        {
            Console.Write("  Entrez le nom du client : ");
            string nomClient = Console.ReadLine();

            Client client = null;
            foreach (Client c in clients)
            {
                if (c.Nom == nomClient)
                {
                    client = c;
                    break;
                }
            }

            if (client != null)
            {
                List<Commande> commandesClient = new List<Commande>();
                foreach (Commande cmd in commandes)
                {
                    if (cmd.Client == client)
                    {
                        commandesClient.Add(cmd);
                    }
                }

                if (commandesClient.Count > 0)
                {
                    Console.WriteLine($"  Commandes pour le client {client.Nom} :");
                    foreach (Commande commande in commandesClient)
                    {
                        Console.WriteLine($"    Numéro : {commande.num}, Date : {commande.Date}, Prix : {commande.Prix}");
                    }
                }
                else
                {
                    Console.WriteLine($"  Aucune commande pour le client {client.Nom}.");
                }
            }
            else
            {
                Console.WriteLine("  Client non trouvé.");
            }
        }
        #endregion


    }
}
