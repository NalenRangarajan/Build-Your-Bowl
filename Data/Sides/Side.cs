using System.ComponentModel;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// This represents an abstract Side class
    /// </summary>
    public abstract class Side : IMenuItem
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
        /// The name of the abstract side
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The description of the abstract side
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// The price of the abstract side
        /// </summary>
        public abstract decimal Price { get; }

        /// <summary>
        /// Private backing field for SizeType
        /// </summary>
        private Size _sizeType = Size.Medium;

        /// <summary>
        /// Property representing the size of the fries
        /// </summary>
        public virtual Size SizeType
        {
            get
            {
                return _sizeType;
            }
            set
            {
                _sizeType = value;

                OnPropertyChanged(nameof(SizeType));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// The calories in the abstract side
        /// </summary>
        public abstract uint Calories { get; }

        /// <summary>
        /// Information for the preparation of the abstract side
        /// </summary>
        public abstract IEnumerable<string> PreparationInformation { get; }

        /// <summary>
        /// Returns the name of the side
        /// </summary>
        /// <returns>The name of the side</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
