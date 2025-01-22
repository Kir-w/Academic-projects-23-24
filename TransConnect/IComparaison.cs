using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_KylieWU_IlanZINI_grpL
{
    /// <summary>
    /// /// Interface générique IComparaison permettant de comparer des objets de type T
    /// </summary>
    /// <typeparam name="T"> objet à comparer </typeparam>
    internal interface IComparaison<T> 
    {
        bool Egalite1(T item);
    }
}
