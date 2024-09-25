using BuildYourBowl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BuildYourBowl.PointOfSale
{
    /// <summary>
    /// Event Args for a Ingredient change
    /// </summary>
    public class IngredientEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Property that says whether the ingredient should be added or removed
        /// </summary>
        public bool Add { get; }

        /// <summary>
        /// The ingredient that is being changed
        /// </summary>
        public Ingredient Ingredient { get; }

        /// <summary>
        /// The constructor for ingredient event args that contains info about the event
        /// </summary>
        /// <param name="i">The ingredient that is passed with the event</param>
        /// <param name="add">The bool that is passed with the event</param>
        public IngredientEventArgs(Ingredient i, bool add)
        {
            Ingredient = i;
            Add = add;
        }
    }
}
