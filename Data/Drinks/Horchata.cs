using System.ComponentModel;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the Horchata class
    /// </summary>
    public class Horchata : Drink
    {
        /// <summary>
        /// The name of the horchata instance
        /// </summary>
        public override string Name { get; } = "Horchata";

        /// <summary>
        /// The description of this drink
        /// </summary>
        public override string Description { get; } = "Milky drink with cinnamon";

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
                decimal price = 3.50m;

                if (DrinkSize == Size.Kids)
                {
                    price -= 1.00m;
                }
                else if (DrinkSize == Size.Small)
                {
                    price -= 0.50m;
                }
                else if (DrinkSize == Size.Large)
                {
                    price += 0.75m;
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
                uint cals = 280;

                double scaledAmount = 1.0;

                if (!Ice)
                {
                    cals += 30;
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
        /// Redefines the Equals method to compare the properties of horchata
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is Horchata horchata1 && obj is Horchata horchata2)
            {
                if (horchata1.Name == horchata2.Name && horchata1.Description == horchata2.Description && horchata1.Ice == horchata2.Ice)
                {
                    if ( horchata1.DrinkSize == horchata2.DrinkSize && horchata1.Price == horchata2.Price && horchata1.Calories == horchata2.Calories)
                    {
                        foreach (string s in horchata1.PreparationInformation)
                        {
                            if (!horchata2.PreparationInformation.Contains<string>(s))
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
