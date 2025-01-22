using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static TransConnect_KylieWU_IlanZINI_grpL.Salarie;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    class Program
    {

        static void Main(string[] args)
        {
            // Organigramme initial de l'entreprise : 
            Organigramme organigramme = InitialisationOrganigramme();


           
            #region initialisation d'une liste de chauffeurs, d'une liste de clients et d'une liste de commandes
            Chauffeur Romu = new Chauffeur(161616, "M. Romu", "Marc", new DateTime(1985, 7, 25), "10 rue de Paris", "marc.romu@gmail.com", "+33612345678", new DateTime(2015, 10, 3), "Chauffeur", "30000");
            Chauffeur Romi = new Chauffeur(171717, "Mme Romi", "Sophie", new DateTime(1992, 3, 18), "8 rue Henri Barbusse", "sophie.romi@gmail.com", "+33623456789", new DateTime(2018, 6, 22), "Chauffeur", "32000");
            Chauffeur Roma = new Chauffeur(181818, "M. Roma", "Thomas", new DateTime(1988, 9, 7), "15 rue Notre Dame des Champs", "thomas.roma@gmail.com", "+33635556780", new DateTime(2019, 11, 14), "Chauffeur", "31000");
            Chauffeur Rome = new Chauffeur(191919, "Mme Rome", "Julie", new DateTime(1994, 1, 30), "20 Avenue Montaigne", "julie.rome@gmail.com", "+33745678901", new DateTime(2020, 8, 5), "Chauffeur", "33000");
            Chauffeur Rimou = new Chauffeur(202020, "M. Rimou", "Paul", new DateTime(1983, 11, 22), "11 Boulevard de Stalingrad", "paul.rimou@gmail.com", "+33656749012", new DateTime(2022, 5, 18), "Chauffeur", "29000");

            List<Chauffeur> chauffeurs = new List<Chauffeur>();
            chauffeurs.Add(Romu);
            chauffeurs.Add(Romi);
            chauffeurs.Add(Roma);
            chauffeurs.Add(Rome);
            chauffeurs.Add(Rimou);


            List<Client> clients = new List<Client>();

            List<Commande> commandes = new List<Commande>();
            #endregion



            #region variables pour représenter sous forme de matrice d'adjacence les distances entre les villes
            int[,] poids_km = {
            //car avec le fichier Distances.csv on a: 
            //Paris Rouen   133 1h45
            //Paris   Lyon    464 4h55
            //Paris   Angers  294 3h11
            //Angers  La Rochelle 187 2h20
            //La Rochelle Bordeaux    183 1h38
            //Bordeaux    Biarritz    202 1h47
            //Biarritz    Toulouse    309 2h39
            //Pau Toulouse    193 1h41
            //Toulouse    Nimes   289 2h26
            //Montpellier Nimes   52  35mn
            //Nimes   Marseilles  126 1h13
            //Marseille   Avignon 99  1h
            //Monaco  Marseille   224 2h3
            //Toulon  Monaco  169 1h35

            //string[] Sommets = { "Angers", "Avignon", "Biarritz",
            //"Bordeaux", "La Rochelle", "Lyon", "Marseille", "Monaco", "Montpellier",
            //"Nimes", "Paris", "Pau", "Rouen", "Toulon", "Toulouse" };

             //Graphe pondéré représenté sous forme de matrice d'adjacence pour les distances entre les villes : 

                {0, 0, 0, 0, 187, 0, 0, 0, 0, 0, 294, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 99, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 202, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 309},
                {0, 0, 202, 0, 183, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {187, 0, 0, 183, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 464, 0, 0, 0, 0},
                {0, 99, 0, 0, 0, 0, 0, 224, 0, 126, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 224, 0, 0, 0, 0, 0, 0, 169, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 52, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 126, 0, 52, 0, 0, 0, 0, 0, 289},
                {294, 0, 0, 0, 0, 464, 0, 0, 0, 0, 0, 0, 133, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 193},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 133, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 169, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 309, 0, 0, 0, 0, 0, 0, 289, 0, 193, 0, 0, 0}
            };

            int[,] poids_temps = { //en minutes
                { 0, 0, 0, 0, 140, 0, 0, 0, 0, 0, 191, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 60, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 107, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 159},
                {0, 0, 107, 0, 98, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {140, 0, 0, 98, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 295, 0, 0, 0, 0},
                {0, 60, 0, 0, 0, 0, 0, 150, 0, 73, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 150, 0, 0, 0, 0, 0, 0, 95, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 35, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 73, 0, 35, 0, 0, 0, 0, 0, 146},
                {191, 0, 0, 0, 0, 295, 0, 0, 0, 0, 0, 0, 105, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 101},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 105, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 95, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 159, 0, 0, 0, 0, 0, 0, 146, 0, 101, 0, 0, 0}
            };
            #endregion



            # region Menu principal 
            bool quitter = false;
            while (!quitter)
            {
                AfficherMenuPrincipal();
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        organigramme.Afficher();
                        break;
                    case "2":
                        GererSalaries(organigramme);
                        break;
                    case "3":
                        GererClients();
                        break;
                    case "4":
                        GererCommandes(clients, organigramme, poids_km, poids_temps, chauffeurs);
                        break; 
                    case "5":
                        AfficherAutres(commandes, clients, chauffeurs);
                        break;
                    case "6":
                        quitter = true;
                        break;
                    default:
                        Console.WriteLine("  Choix non valide. Réessayer.");
                        break;
                }
            }
            #endregion 

        }


        #region fonction pour retourner l'organigramme initial des salariés de l'entreprise 
        static Organigramme InitialisationOrganigramme()
        {
            Salarie Dupond = new Salarie(111111, "M. Dupond", "Jean", new DateTime(1990, 5, 15), "22 avenue de Fontainebleau", "jean.dupont@gmail.com", "+33782828388", new DateTime(2010, 8, 1), "Directeur Général", "84000");

            Salarie Fiesta = new Salarie(222222, "Mme Fiesta", "Anna", new DateTime(1985, 9, 23), "5 avenue des Jasmins", "sophie.fiesta@gmail.com", "+33699999388", new DateTime(2008, 10, 15), "Directrice Commerciale", "75500");
            Salarie Forge = new Salarie(333333, "M. Forge", "Pierre", new DateTime(1988, 2, 8), "2 boulevard Voltaire", "pierre.forge@gmail.com", "+33642153230", new DateTime(2012, 3, 20), "Commercial", "57000");
            Salarie Fermi = new Salarie(444444, "Mme Fermi", "Marie", new DateTime(1992, 11, 30), "24 rue du Cardinal Lemoine", "marie.fermi@gmail.com", "+33601020928", new DateTime(2015, 6, 10), "Commerciale", "54800");

            Salarie Fetard = new Salarie(555555, "M. Fetard", "Nicolas", new DateTime(1983, 7, 3), "8 rue Saint-Honoré", "nicolas.fetard@gmail.com", "+33600938894", new DateTime(2009, 9, 5), "Directeur des opérations", "66200");
            Salarie Royal = new Salarie(777777, "M. Royal", "Ben", new DateTime(1987, 8, 9), "12 boulevard de la Citadelle", "thomas.royal@gmail.com", "+33609938388", new DateTime(2013, 2, 25), "Chef d'Equipe", "57200");
            Chauffeur Romu = new Chauffeur(161616, "M. Romu", "Marc", new DateTime(1985, 7, 25), "10 rue de Paris", "marc.romu@gmail.com", "+33612345678", new DateTime(2015, 10, 3), "Chauffeur", "30000");
            Chauffeur Romi = new Chauffeur(171717, "Mme Romi", "Sophie", new DateTime(1992, 3, 18), "8 rue Henri Barbusse", "sophie.romi@gmail.com", "+33623456789", new DateTime(2018, 6, 22), "Chauffeur", "32000");
            Chauffeur Roma = new Chauffeur(181818, "M. Roma", "Thomas", new DateTime(1988, 9, 7), "15 rue Notre Dame des Champs", "thomas.roma@gmail.com", "+33635556780", new DateTime(2019, 11, 14), "Chauffeur", "31000");

            Salarie Prince = new Salarie(888888, "Mme Prince", "Laura", new DateTime(1984, 6, 21), "30 avenue Lavoisier", "laura.prince@gmail.com", "+33689052345", new DateTime(2011, 5, 18), "Chef d'Equipe", "54900");
            Chauffeur Rome = new Chauffeur(191919, "Mme Rome", "Julie", new DateTime(1994, 1, 30), "20 avenue Montaigne", "julie.rome@gmail.com", "+33745678901", new DateTime(2020, 8, 5), "Chauffeur", "33000");
            Chauffeur Rimou = new Chauffeur(202020, "M. Rimou", "Paul", new DateTime(1983, 11, 22), "11 boulevard de Stalingrad", "paul.rimou@gmail.com", "+33656749012", new DateTime(2022, 5, 18), "Chauffeur", "29000");

            Salarie Joyeuse = new Salarie(666666, "Mme Joyeuse", "Laurie", new DateTime(1991, 4, 17), "25 rue de Verdun", "julie.joyeuse@gmail.com", "+33 6 67890123", new DateTime(2014, 12, 1), "Directrice des RH", "65300");
            Salarie Couleur = new Salarie(101010, "Mme Couleur", "Alice", new DateTime(1993, 10, 27), "77 Avenue de la Liberté", "alice.couleur@gmail.com", "+33 6 01234567", new DateTime(2017, 7, 8), "Formation", "45800");
            Salarie ToutleMonde = new Salarie(121212, "Mme ToutleMonde", "Jeanne", new DateTime(1989, 2, 14), "8 avenue de l'Avenir", "julie.toutleMonde@gmail.com", "+33 6 23456789", new DateTime(2019, 10, 20), "Contrats", "47400");

            Salarie GripSous = new Salarie(999999, "M. GripSous", "Kevin", new DateTime(1995, 3, 12), "38 rue de la République", "kevin.gripSous@gmail.com", "+33 6 90123456", new DateTime(2016, 11, 12), "Directeur Financier", "76800");
            Salarie Picsou = new Salarie(111111, "M. Picsou", "Antoine", new DateTime(1986, 12, 5), "62 rue Saint-Louis", "nicolas.picsou@gmail.com", "+33 6 12345678", new DateTime(2018, 4, 3), "Direction comptable", "66500");
            Salarie Fournier = new Salarie(141414, "Mme Fournier", "Juliette", new DateTime(1983, 8, 20), "10 rue Avaulée", "laura.fournier@gmail.com", "+33 6 45678901", new DateTime(2021, 11, 28), "Comptable", "55000");
            Salarie Gautier = new Salarie(131313, "Mme Gautier", "Aurélie", new DateTime(1994, 6, 8), "14 boulevard des Sabliers", "aurélie.gautier@gmail.com", "+33 6 34567890", new DateTime(2020, 9, 15), "Comptable", "57000");

            Salarie GrosSous = new Salarie(151515, "M. GrosSous", "Donald", new DateTime(1990, 11, 11), "59 avenue du Richelieu", "nicolas.grosSous@gmail.com", "+33 6 56789012", new DateTime(2021, 12, 15), "Contrôleur de Gestion", "55000");



            Organigramme organigramme = new Organigramme(
                new Noeud(Dupond.Nom, Dupond.Poste,

                    new Noeud(Fiesta.Nom, Fiesta.Poste,
                        new Noeud(Forge.Nom, Forge.Poste),
                        new Noeud(Fermi.Nom, Fermi.Poste)),

                    new Noeud(Fetard.Nom, Fetard.Poste,
                        new Noeud(Royal.Nom, Royal.Poste,
                            new Noeud(Romu.Nom, Romu.Poste),
                            new Noeud(Romi.Nom, Romi.Poste),
                            new Noeud(Roma.Nom, Roma.Poste)),

                        new Noeud(Prince.Nom, Prince.Poste,
                            new Noeud(Rome.Nom, Rome.Poste),
                            new Noeud(Rimou.Nom, Rimou.Poste))),

                    new Noeud(Joyeuse.Nom, Joyeuse.Poste,
                        new Noeud(Couleur.Nom, Couleur.Poste),
                        new Noeud(ToutleMonde.Nom, ToutleMonde.Poste)),

                    new Noeud(GripSous.Nom, GripSous.Poste,
                        new Noeud(Picsou.Nom, Picsou.Poste,
                            new Noeud(Fournier.Nom, Fournier.Poste),
                            new Noeud(Gautier.Nom, Gautier.Poste)),

                        new Noeud(GrosSous.Nom, GrosSous.Poste))
            ));


            return organigramme;
        }
        #endregion




        #region afficher le menu principal 
        static void AfficherMenuPrincipal()
        {
            Console.WriteLine();
            Console.WriteLine("  SOCIETE TRANSCONNECT - MENU PRINCIPAL");
            Console.WriteLine();
            Console.WriteLine("  1. Afficher l'organigramme");
            Console.WriteLine("  2. Gérer les salariés");
            Console.WriteLine("  3. Gérer les clients");
            Console.WriteLine("  4. Gérer les commandes");
            Console.WriteLine("  5. Autres");
            Console.WriteLine("  6. Quitter");
            Console.WriteLine();
            Console.Write("  Choix (1/2/3/4/5/6) : ");
        }
        #endregion



        #region si le directeur veut s'occuper de ses salariés 
        static void GererSalaries(Organigramme organigramme)
        {
            bool retour = false;
            while (!retour)
            {
                AfficherMenuSalaries();
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        EmbaucherSalarie(organigramme);
                        break;
                    case "2":
                        LicencierSalarie(organigramme);
                        break;
                    case "3":
                        retour = true;
                        break;
                    default:
                        Console.WriteLine("  Choix non valide. Réessayer.");
                        break;
                }
            }
        }


        static void AfficherMenuSalaries()
        {
            Console.WriteLine();
            Console.WriteLine("  Menu Gestion des Salariés");
            Console.WriteLine();
            Console.WriteLine("  2.1. Embaucher un salarié");
            Console.WriteLine("  2.2. Licencier un salarié");
            Console.WriteLine("  2.3. Retour");
            Console.WriteLine();
            Console.Write("  Choix (1/2/3) : ");
        }


        static void EmbaucherSalarie(Organigramme organigramme)
        {
            Console.Write("Nom du salarié à embaucher : ");
            string nom = Console.ReadLine();
            Console.Write("Poste : ");
            string poste = Console.ReadLine();

            organigramme.Embaucher(nom, poste);
        }


        static void LicencierSalarie(Organigramme organigramme)
        {
            Console.Write("Nom du salarié à licencier : ");
            string nom = Console.ReadLine();
            Console.Write("Poste : ");
            string poste = Console.ReadLine();

            organigramme.Licencier(nom, poste);
        }
        #endregion



        #region si le directeur veut s'occuper de ses clients 
        static void GererClients()
        {
            List<Client> clients = new List<Client>();

            //Exemples de clients : 
            Client client1 = new Client("Paris", 100, 010200, "Donne", "John", new DateTime(2000, 2, 1), "1 rue de Paris", "john.donne@gmail.com", "0102030405");
            Client client2 = new Client("Normandie", 200, 020300, "Grant", "Mira", new DateTime(2000, 3, 2), "10 rue de Normandie", "mira.grant@gmail.com", "0607080900");
            Client client3 = new Client("Marseille", 300, 030400, "Camus", "Alfred", new DateTime(2000, 4, 3), "20 rue de Marseille", "alfred.camus@gmail.com", "0708090102");

            clients.Add(client1);
            clients.Add(client2);
            clients.Add(client3);

            bool retour = false;
            while (!retour)
            {
                AfficherMenuClients();
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Client.AfficherClientsAlphabetique(clients);
                        break;
                    case "2":
                        Client.AfficherClientsParVille(clients);
                        break;
                    case "3":
                        Client.AfficherClientsParMontantAchatsCumule(clients);
                        break;
                    case "4":
                        Client.AfficherCombinaisonSimultanee(clients);
                        break;
                    case "5":
                        Client.AjouterClient(clients);
                        break;
                    case "6":
                        Client.SupprimerClient(clients);
                        break;
                    case "7":
                        Client.ModifierClient(clients);
                        break;
                    case "8":
                        retour = true;
                        break;
                    default:
                        Console.WriteLine("  Choix non valide. Réessayer.");
                        break;
                }
            }
        }


        static void AfficherMenuClients()
        {
            Console.WriteLine();
            Console.WriteLine("  Menu Affichage Clients");
            Console.WriteLine();
            Console.WriteLine("  3.1. Afficher par ordre alphabétique");
            Console.WriteLine("  3.2. Afficher par ville");
            Console.WriteLine("  3.3. Afficher par montant des achats cumulés");
            Console.WriteLine("  3.4. Afficher une combinaison simultanée");
            Console.WriteLine("  3.5. Ajouter un nouveau client");
            Console.WriteLine("  3.6. Supprimer un client");
            Console.WriteLine("  3.7. Modifier un client");
            Console.WriteLine("  3.8. Retour");
            Console.WriteLine();
            Console.Write("  Choix (1/2/3/4/5/6/7/8) : ");
        }
        #endregion



        #region si le directeur veut s'occuper de ses commandes 
        static void GererCommandes(List<Client> clients, Organigramme organigramme, int[,] poids_km, int[,] poids_temps, List<Chauffeur> chauffeurs)
        {
            List<Commande> commandes = new List<Commande>();

            bool retour = false;
            while (!retour)
            {
                AfficherMenuCommandes();
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Commande.AfficherCommandes(commandes);
                        break;
                    case "2":
                        Commande.CreerNouvelleCommande(clients, commandes, organigramme, poids_km, poids_temps);
                        break;
                    case "3":
                        Commande.ModifierCommande(commandes);
                        break;
                    case "4":
                        AfficherStatistiques(commandes, clients, chauffeurs);
                        break;
                    case "5":
                        retour = true;
                        break;
                    default:
                        Console.WriteLine("  Choix non valide. Réessayer.");
                        break;
                }
            }
        }

        static void AfficherMenuCommandes()
        {
            Console.WriteLine();
            Console.WriteLine("  Menu Affichage Commandes");
            Console.WriteLine();
            Console.WriteLine("  4.1. Afficher les commandes");
            Console.WriteLine("  4.2. Créer une nouvelle commande");
            Console.WriteLine("  4.3. Modifier une commande existante");
            Console.WriteLine("  4.4. Afficher les statistiques des commandes ");
            Console.WriteLine("  4.5. Retour");
            Console.WriteLine();
            Console.Write("  Choix (1/2/3/4/5) : ");
        }
        #endregion




        #region si le directeur veut consulter des statistiques sur les commandes 

        //Afficher par chauffeur le nombre de livraisons effectuées 
        //Afficher les commandes selon une période de temps 
        //Afficher la moyenne des prix des commandes 
        //Afficher la moyenne des comptes clients 
        //Afficher la liste des commandes pour un client
        static void AfficherStatistiques(List<Commande> commandes, List<Client> clients, List<Chauffeur> chauffeurs)
        {
            bool retour = false;
            while (!retour)
            {
                AfficherMenuStatistiques();
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Statistiques.AfficherLivraisonsParChauffeur(chauffeurs);
                        break;
                    case "2":
                        Statistiques.AfficherCommandesParPeriode(commandes);
                        break;
                    case "3":
                        Commande.AfficherMoyennePrixCommandes(commandes);
                        break;
                    case "4":
                        Commande.AfficherMoyenneComptesClients(clients);
                        break;
                    case "5":
                        Statistiques.AfficherCommandesParClient(commandes, clients);
                        break;
                    case "6":
                        retour = true;
                        break;
                    default:
                        Console.WriteLine("  Choix non valide. Réessayer.");
                        break;
                }
            }
        }


        static void AfficherMenuStatistiques()
        {
            Console.WriteLine();
            Console.WriteLine("  Menu Statistiques");
            Console.WriteLine();
            Console.WriteLine("  4.4.1. Afficher le nombre de livraisons par chauffeur");
            Console.WriteLine("  4.4.2. Afficher les commandes selon une période de temps");
            Console.WriteLine("  4.4.3. Afficher la moyenne des prix des commandes");
            Console.WriteLine("  4.4.4. Afficher la moyenne des comptes clients");
            Console.WriteLine("  4.4.5. Afficher la liste des commandes pour un client");
            Console.WriteLine("  4.4.6. Retour");
            Console.WriteLine();
            Console.Write("  Choix (1/2/3/4/5/6) : ");
        }
        #endregion





        #region si le directeur veut consulter des informations supplémentaires sur son entreprise 

        //1- Nombre de salariés dans l'entreprise 
        //2- Nombre total de commandes reçues 
        //3- Comparaison du salaire de 2 chauffeurs 
        //4- Tri des chauffeurs selon les noms 
        //5- Tri des chauffeurs selon les salaires 
        static void AfficherAutres(List<Commande> commandes, List<Client> clients, List<Chauffeur> chauffeurs) //A MODIFIER ????????
        {
            bool retour = false;
            while (!retour)
            {
                AfficherMenuAutres();
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Console.WriteLine($"  L'entreprise compte {Salarie.TotalSalaries} salariés.");
                        break;
                    case "2":
                        Console.WriteLine($"  L'entreprise a reçu {Commande.TotalCommandes} commandes au total");
                        break;
                    case "3":
                        ComparaisonSalaireChauffeurs(chauffeurs); 
                        break;
                    case "4":
                        Console.WriteLine("Tri des chauffeurs selon les noms");
                        chauffeurs.Sort(chauffeurs[0]); // on utilise l'interface IComparer<T>
                        foreach (Chauffeur c in chauffeurs) { Console.WriteLine(c); }
                        break;
                    case "5":
                        Console.WriteLine("Tri des chauffeurs selon les salaires");
                        chauffeurs.Sort(); // on utilise l'interface IComparable
                        foreach (Chauffeur c in chauffeurs) { Console.WriteLine(c); }
                        break;
                    case "6":
                        retour = true;
                        break;
                    default:
                        Console.WriteLine("  Choix non valide. Réessayer.");
                        break;
                }
            }
        }


        static void AfficherMenuAutres()
        {
            Console.WriteLine();
            Console.WriteLine("  Menu Autres");
            Console.WriteLine();
            Console.WriteLine("  5.1. Nombre de salariés dans l'entreprise");
            Console.WriteLine("  5.2. Nombre total de commandes reçues par l'entreprise");
            Console.WriteLine("  5.3. Comparaison du salaire de 2 chauffeurs");
            Console.WriteLine("  5.4. Tri des chauffeurs selon les noms");
            Console.WriteLine("  5.5. Tri des chauffeurs selon les salaires");
            Console.WriteLine("  5.6. Retour");
            Console.WriteLine();
            Console.Write("  Choix (1/2/3/4/5/6) : ");
        }


        static void ComparaisonSalaireChauffeurs(List<Chauffeur> chauffeurs)
        {
            Console.WriteLine($"  Entrez 2 chauffeurs de l'entreprise dont vous voulez comparer le salaire: ");
            string c1 = Console.ReadLine();
            string c2 = Console.ReadLine();
            Chauffeur chauf1 = chauffeurs[0]; //juste pour initialiser
            Chauffeur chauf2 = chauffeurs[1]; //juste pour initialiser

            foreach (Chauffeur c in chauffeurs) //pour trouver les bons chauffeurs parmi tous 
            { 
                if (c.Nom == c1) chauf1 = c;
                else if (c.Nom == c2) chauf2 = c;
            }

            if (chauf1.Egalite1(chauf2)) Console.WriteLine($"Les salaires des chauffeurs {chauf1.Nom} et {chauf2.Nom} sont égaux."); 
            else Console.WriteLine($"Les salaires des chauffeurs {chauf1.Nom} et {chauf2.Nom} ne sont PAS égaux.");
        }

        #endregion
        


    }
}
