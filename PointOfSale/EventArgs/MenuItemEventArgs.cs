using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace BuildYourBowl.Data
{
    /// <summary>
    /// Event Args for a Menu Item change
    /// </summary>
    public class MenuItemEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// The menu item that is being changed
        /// </summary>
        public IMenuItem MenuItem { get; init; }

        /// <summary>
        /// The constructor for menu item event args that contains info about the event
        /// </summary>
        /// <param name="item">The item that is passed with the event</param>
        public MenuItemEventArgs(IMenuItem item)
        {
            MenuItem = item;
        }
    }
}
