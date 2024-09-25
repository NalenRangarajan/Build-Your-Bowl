using System.ComponentModel;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the AguaFresca class
    /// </summary>
    public class AguaFresca : Drink
    {
        /// <summary>
        /// The name of the Agua Fresca instance
        /// </summary>
        public override string Name { get; } = "Agua Fresca";

        /// <summary>
        /// The description of this drink
        /// </summary>
        public override string Description { get; } = "Refreshing lightly sweetened fruit drink";

        /// <summary>
        /// A private backing field for DrinkFlavor
        /// </summary>
        private Flavor _drinkFlavor = Flavor.Limonada;

        /// <summary>
        /// A property representing the flavor of this drink
        /// </summary>
        public Flavor DrinkFlavor
        {
            get
            {
                return _drinkFlavor;
            }
            set
            {
                _drinkFlavor = value;

                OnPropertyChanged(nameof(DrinkFlavor));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// Private backing field for Ice
        /// </summary>
        private bool _ice = true;

        /// <summary>
        /// Whether this drink has ice
        /// </summary>
        public bool Ice
        {
            get
            {
                return _ice;
            }
            set
            {
                _ice = value;

                OnPropertyChanged(nameof(Ice));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// The price of this drink
        /// </summary>
        public override decimal Price
        {
            get
            {
                decimal price = 3.00m;

                if (DrinkSize == Size.Kids)
                {
                    price = 2.00m;
                }
                else if (DrinkSize == Size.Small)
                {
                    price = 2.50m;
                }
                else if (DrinkSize == Size.Large)
                {
                    price = 3.75m;
                }

                if (DrinkFlavor == Flavor.Tamarind)
                {
                    price += 0.50m;
                }

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
                uint cals;

                double scaledAmount = 1.0;

                if (DrinkFlavor == Flavor.Limonada || DrinkFlavor == Flavor.Lime)
                {
                    cals = 125;
                }
                else if (DrinkFlavor == Flavor.Tamarind || DrinkFlavor == Flavor.Strawberry)
                {
                    cals = 150;
                }
                else //Flavor is cucumber
                {
                    cals = 75;
                }

                if (!Ice)
                {
                    cals += 10;
                }

                if (DrinkSize == Size.Kids)
                {
                    scaledAmount = 0.60;
                }
                else if (DrinkSize == Size.Small)
                {
                    scaledAmount = 0.75;
                }
                else if (DrinkSize == Size.Large)
                {
                    scaledAmount = 1.50;
                }

                cals = (uint)(cals * scaledAmount);

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

                instructions.Add($"{DrinkSize}");
                instructions.Add($"{DrinkFlavor}");
                
                if (!Ice)
                {
                    instructions.Add("Hold Ice");
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
        /// Redefines the Equals method to compare the properties of AguaFresca
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is AguaFresca aguaFresca1 && obj is AguaFresca aguaFresca2)
            {
                if (aguaFresca1.Name == aguaFresca2.Name && aguaFresca1.Description == aguaFresca2.Description && aguaFresca1.DrinkFlavor == aguaFresca2.DrinkFlavor)
                {
                    if (aguaFresca1.DrinkSize == aguaFresca2.DrinkSize && aguaFresca1.Price == aguaFresca2.Price && aguaFresca1.Calories == aguaFresca2.Calories)
                    {
                        foreach (string s in aguaFresca1.PreparationInformation)
                        {
                            if (!aguaFresca2.PreparationInformation.Contains<string>(s))
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
