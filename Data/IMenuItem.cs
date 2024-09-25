using System.ComponentModel;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// An interface providing properties for menu items
    /// </summary>
    public interface IMenuItem : INotifyPropertyChanged
    {
        /// <summary>
        /// The name of the menu item
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The description of the menu item
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The price of the menu item
        /// </summary>

        decimal Price { get; }

        /// <summary>
        /// The number of calories in the menu item
        /// </summary>

        uint Calories { get; }

        /// <summary>
        /// Information for the preparation of this menu item
        /// </summary>
        IEnumerable<string> PreparationInformation { get; }
    }
}
