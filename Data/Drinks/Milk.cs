using System.ComponentModel;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the Milk class
    /// </summary>
    public class Milk : Drink
    {
        /// <summary>
        /// The name of the milk instance
        /// </summary>
        public override string Name { get; } = "Milk";

        /// <summary>
        /// The description of this drink
        /// </summary>
        public override string Description { get; } = "Creamy beverage in plain or chocolate";

        /// <summary>
        /// Private backing field for Chocolate
        /// </summary>
        private bool _chocolate = false;

        /// <summary>
        /// Whether the milk is chocolate
        /// </summary>
        public bool Chocolate
        {
            get
            {
                return _chocolate;
            }
            set
            {
                _chocolate = value;

                OnPropertyChanged(nameof(Chocolate));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// A private backing field for DrinkSize
        /// </summary>
        private Size _size = Size.Kids;

        /// <summary>
        /// A property representing the size of this drink
        /// </summary>
        public override Size DrinkSize
        {
            get
            {
                return _size;
            }
        }

        /// <summary>
        /// A property representing the price of this drink
        /// </summary>
        public override decimal Price
        {
            get
            {
                decimal price = 2.50m;

                return price;
            }
        }

        /// <summary>
        /// The total number of calories in this drink
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 200;

                if (Chocolate)
                {
                    cals = 270;
                }

                return cals;
            }
        }

        /// <summary>
        /// Information for the preparation of this drink
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();

                instructions.Add("Kids");
                if (Chocolate)
                {
                    instructions.Add("Chocolate");
                }

                return instructions;
            }
        }

        /// <summary>
        /// A new definition of the getHashCode method
        /// </summary>
        /// <returns>The hashcode of the object</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Redefines the Equals method to compare the properties of milk
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is Milk milk1 && obj is Milk milk2)
            {
                if (milk1.Name == milk2.Name && milk1.Description == milk2.Description && milk1.Chocolate == milk2.Chocolate && milk1.DrinkSize == milk2.DrinkSize)
                {
                    if (milk1.Price == milk2.Price && milk1.Calories == milk2.Calories)
                    {
                        foreach (string s in milk1.PreparationInformation)
                        {
                            if (!milk2.PreparationInformation.Contains<string>(s))
                            {
                                return false;
                            }
                        }

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
