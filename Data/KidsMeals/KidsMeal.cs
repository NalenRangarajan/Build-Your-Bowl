using System.ComponentModel;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// This represents an abstract KidsMeal class
    /// </summary>
    public abstract class KidsMeal : IMenuItem
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
        /// Handler that updates the properties of the kids meal's side and drink
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        private void HandleChange(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Calories));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(PreparationInformation));
        }

        /// <summary>
        /// The name of the abstract kids meal
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The description of the abstract kids meal
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// The type of item in the meal
        /// </summary>
        public abstract string ItemType { get; }

        /// <summary>
        /// The default number of items in the meal
        /// </summary>
        private uint _defaultCount = 5;

        /// <summary>
        /// Private backing field for Count.
        /// </summary>
        private uint _itemCount = 5;

        /// <summary>
        /// A property representing the amount of items in this meal
        /// </summary>
        public virtual uint ItemCount
        {
            get
            {
                return _itemCount;
            }
            set
            {
                if (value <= 8 && value >= _defaultCount)
                {
                    _itemCount = value;

                    OnPropertyChanged(nameof(ItemCount));
                    OnPropertyChanged(nameof(Price));
                    OnPropertyChanged(nameof(Calories));
                    OnPropertyChanged(nameof(PreparationInformation));
                }
            }
        }

        /// <summary>
        /// A property that represents the price of this kids meal
        /// </summary>
        public virtual decimal Price
        {
            get
            {
                decimal price = 5.99m;

                uint extraBites = ItemCount - _defaultCount;

                price += 0.75m * extraBites;

                if (DrinkChoice.DrinkSize == Size.Small)
                {
                    price += 0.50m;
                }
                else if (DrinkChoice.DrinkSize == Size.Medium)
                {
                    price += 1.00m;
                }
                else if (DrinkChoice.DrinkSize == Size.Large)
                {
                    price += 1.50m;
                }

                if (SideChoice.SizeType == Size.Small)
                {
                    price += 0.50m;
                }
                else if (SideChoice.SizeType == Size.Medium)
                {
                    price += 1.00m;
                }
                else if (SideChoice.SizeType == Size.Large)
                {
                    price += 1.50m;
                }

                return price;
            }
        }

        /// <summary>
        /// A private backing field for DrinkChoice
        /// </summary>
        private Drink _drinkChoice = new Milk();

        /// <summary>
        /// A property representing the drink to be included with this meal
        /// </summary>
        public virtual Drink DrinkChoice
        {
            get
            {
                return _drinkChoice;
            }
            set
            {
                _drinkChoice.PropertyChanged -= HandleChange!;
                _drinkChoice = value;
                _drinkChoice.PropertyChanged += HandleChange!;

                OnPropertyChanged(nameof(DrinkChoice));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// Private backing field for SideChoice
        /// </summary>
        private Side _sideChoice = new Fries() { SizeType = Size.Kids};

        /// <summary>
        /// A property representing the side to be included with this meal
        /// </summary>
        public virtual Side SideChoice
        {
            get
            {
                return _sideChoice;
            }
            set
            {
                _sideChoice.PropertyChanged -= HandleChange!;
                _sideChoice = value;
                _sideChoice.PropertyChanged += HandleChange!;

                OnPropertyChanged(nameof(SideChoice));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// The total number of calories in this abstract meal
        /// </summary>
        public abstract uint Calories { get; }

        /// <summary>
        /// Information for the preparation of the abstract meal
        /// </summary>
        public abstract IEnumerable<string> PreparationInformation { get; }

        /// <summary>
        /// Returns the name of the kids meal
        /// </summary>
        /// <returns>The name of the kids meal</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
