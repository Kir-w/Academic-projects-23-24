using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TransConnect_KylieWU_IlanZINI_grpL.Organigramme;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// Représente une Livraison entre deux villes, avec des fonctions pour calculer le chemin le plus court et le tarif correspondant
    /// </summary>
    internal class Livraison
    {
        public string villeDepart;
        public string villeArrivee;
        float tarif;


        #region variables pour représenter sous forme de matrice d'adjacence les distances entre les villes
        // Graphe pondéré représenté sous forme de matrice d'adjacence pour les km

        int[,] poids_km = {
        //string[] Sommets = { "Angers", "Avignon", "Biarritz",
        //"Bordeaux", "La Rochelle", "Lyon", "Marseille", "Monaco", "Montpellier",
        //"Nimes", "Paris", "Pau", "Rouen", "Toulon", "Toulouse" };

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



        #region constructeur 
        public Livraison(string villeDepart, string villeArrivee)
        {
            this.villeDepart = villeDepart;
            this.villeArrivee = villeArrivee;
        }
        #endregion


        #region propriétés 
        public string VilleDepart
        {
            get { return villeDepart; }
            set { villeDepart = value; }
        }

        public string VilleArrivee
        {
            get { return villeArrivee; }
            set { villeArrivee = value; }
        }
        #endregion



        #region fonction qui renvoie des informations sur le chemin le plus court entre 2 villes
        public static string CheminLePlusCourt(string villeDepart, string villeArrivee, int[,] poids_km, int[,] poids_temps)
        {
            string[] Sommets = { "Angers", "Avignon", "Biarritz", "Bordeaux", "La Rochelle", "Lyon", "Marseille", "Monaco", "Montpellier", "Nimes", "Paris", "Pau", "Rouen", "Toulon", "Toulouse" };

            int[] distances_km = new int[Sommets.Length];
            int[] parents_km = new int[Sommets.Length];

            // Initialisation des distances à l'infini et des parents à -1
            for (int u = 0; u < Sommets.Length; u++)
            {
                distances_km[u] = int.MaxValue;
                parents_km[u] = -1;
            }

            // Fonction pour trouver l'indice d'un élément dans un tableau de chaînes
            static int TrouverIndex(string[] array, string value)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == value)
                    {
                        return i;
                    }
                }
                return -1; // Si l'élément n'est pas trouvé
            }

            // Utilisation de la fonction pour trouver les indices de départ et d'arrivée
            int sommetDepart = TrouverIndex(Sommets, villeDepart);
            int sommetArrivee = TrouverIndex(Sommets, villeArrivee);

            // On applique l'algorithme de Dijkstra pour les km
            Dijkstra(poids_km, poids_temps, sommetDepart, out distances_km, out parents_km);

            // On récupère le chemin le plus court pour les km
            List<int> chemin_km = ConstruireChemin(sommetArrivee, parents_km);

            // Calcul du temps total correspondant au chemin en termes de kilomètres
            int tempsTotal_km = 0;
            for (int i = 0; i < chemin_km.Count - 1; i++)
            {
                int u = chemin_km[i];
                int v = chemin_km[i + 1];
                tempsTotal_km += poids_temps[u, v];
            }


            // Construction et affichage du résultat
            string resultat = $"  Chemin le plus court de {villeDepart} à {villeArrivee} :\n";
            resultat += $"  Distance totale : {distances_km[sommetArrivee]} km\n";
            resultat += "  Parcours :\n";
            foreach (int sommet in chemin_km)
            {
                resultat += Sommets[sommet] + "\n";
            }
            resultat += $"  Temps total de trajet : {tempsTotal_km} minutes";

            return resultat;
        }
        #endregion


        #region fonction pour renvoyer la distance du chemin le plus court entre 2 villes
        public static int CheminLePlusCourtTarif(string villeDepart, string villeArrivee, int[,] poids_km, int[,] poids_temps)
        {
            string[] Sommets = { "Angers", "Avignon", "Biarritz", "Bordeaux", "La Rochelle", "Lyon", "Marseille", "Monaco", "Montpellier", "Nimes", "Paris", "Pau", "Rouen", "Toulon", "Toulouse" };

            int[] distances_km = new int[Sommets.Length];
            int[] parents_km = new int[Sommets.Length];

            // Initialisation des distances à l'infini et des parents à -1
            for (int u = 0; u < Sommets.Length; u++)
            {
                distances_km[u] = int.MaxValue;
                parents_km[u] = -1;
            }

            // Fonction pour trouver l'indice d'un élément dans un tableau de chaînes
            static int TrouverIndex(string[] array, string value)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == value)
                    {
                        return i;
                    }
                }
                return -1; // Si l'élément n'est pas trouvé
            }

            // Utilisation de la fonction pour trouver les indices de départ et d'arrivée
            int sommetDepart = TrouverIndex(Sommets, villeDepart);
            int sommetArrivee = TrouverIndex(Sommets, villeArrivee);

            // On applique l'algorithme de Dijkstra pour les km
            Dijkstra(poids_km, poids_temps, sommetDepart, out distances_km, out parents_km);

            // On récupère le chemin le plus court pour les km
            List<int> chemin_km = ConstruireChemin(sommetArrivee, parents_km);


            // Construction et affichage du résultat
            int resultat = distances_km[sommetArrivee];
            return resultat;
        }
        #endregion




        #region fonction de relâchement (non utilisé) 
        //void Relachement(int u, int v, int[,] poids)
        //    {
        //        if (distances_km[v] > distances_km[u] + poids[u, v])
        //        {
        //            distances_km[v] = distances_km[u] + poids[u, v];
        //            parents_km[v] = u;
        //        }
        //    }
        #endregion



        #region Dijkstra
        public static void Dijkstra(int[,] G, int[,] temps, int r, out int[] distances, out int[] parents)
        {
            int n = G.GetLength(0);

            // Initialisation des distances et des parents
            distances = new int[n];
            parents = new int[n];
            for (int u = 0; u < n; u++)
            {
                distances[u] = int.MaxValue;
                parents[u] = -1;
            }
            
            distances[r] = 0;
            
            // Liste des sommets non traités
            List<int> nonTraites = new List<int>();
            for (int u = 0; u < n; u++)
            {
                nonTraites.Add(u);
            }
            
            
            while (nonTraites.Count > 0)
            {
                // On veut le sommet non traité avec la plus petite distance
                int minDistance = int.MaxValue;
                int u = -1;

                foreach (int vertex in nonTraites) // en s'inspirant du cours (diapo 37)
                {
                    if (distances[vertex] < minDistance)
                    {
                        minDistance = distances[vertex];
                        u = vertex;
                    }
                }

                // On retire u de la liste des sommets non traités
                nonTraites.Remove(u);

                // On parcourt les voisins de u
                for (int v = 0; v < n; v++)
                {
                    if (G[u, v] != 0)
                    {
                    // On met à jour la distance et le parent
                    int newDistance = distances[u] + G[u, v];
                        if (newDistance < distances[v])
                        {
                            distances[v] = newDistance;
                            parents[v] = u;
                        }
                    }
                }
            }
        }
        #endregion




        public static List<int> ConstruireChemin(int arrivee, int[] parents)
        {
            List<int> chemin = new List<int>();
            int sommet = arrivee;
            while (sommet != -1)
            {
                chemin.Add(sommet);
                sommet = parents[sommet];
            }
            chemin.Reverse();
            return chemin;
        }


    }
}
