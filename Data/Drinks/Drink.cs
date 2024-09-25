using System.ComponentModel;
using Windows.Graphics.Display;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// This represents an abstract Drink class
    /// </summary>
    public abstract class Drink : IMenuItem
    {
        /// <summary>
        /// An event indicating that a property has changed
        /// </summary>
        public virtual event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Helper method to indicate that a property has changed
        /// </summary>
        /// <param name="propertyName">The property that has changed</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The name of the abstract drink
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The description of the abstract drink
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Private backing field for SizeType
        /// </summary>
        private Size _drinkSize = Size.Medium;

        /// <summary>
        /// Property representing the size of the fries
        /// </summary>
        public virtual Size DrinkSize
        {
            get
            {
                return _drinkSize;
            }
            set
            {
                _drinkSize = value;

                OnPropertyChanged(nameof(DrinkSize));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// The price of the abstract drink
        /// </summary>
        public abstract decimal Price { get; }

        /// <summary>
        /// The calories in the abstract drink
        /// </summary>
        public abstract uint Calories { get; }

        /// <summary>
        /// Information for the preparation of the abstract drink
        /// </summary>
        public abstract IEnumerable<string> PreparationInformation { get; }

        /// <summary>
        /// Returns the name of the drink
        /// </summary>
        /// <returns>The name of the drink</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
