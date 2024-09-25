using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BuildYourBowl.PointOfSale.EventArgs
{
    /// <summary>
    /// Event Args for a Count change
    /// </summary>
    public class CountChangedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Field that represents the new count
        /// </summary>
        public uint NewCount { get; }

        /// <summary>
        /// Constructor for a CountEventArgs that contains a count
        /// </summary>
        /// <param name="count">The count of items</param>
        public CountChangedEventArgs(uint count)
        {
            NewCount = count;
        }
    }
}
