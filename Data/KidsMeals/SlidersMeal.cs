using System.ComponentModel;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the SlidersMeal class
    /// </summary>
    public class SlidersMeal : KidsMeal
    {
        /// <summary>
        /// The name of the Sliders Kids Meal instance
        /// </summary>
        public override string Name { get; } = "Sliders Kids Meal";

        /// <summary>
        /// The description of this kids meal
        /// </summary>
        public override string Description { get; } = "Sliders with side and drink";

        /// <summary>
        /// The type of item in the Sliders Kids meal
        /// </summary>
        public override string ItemType
        {
            get
            {
                return "Sliders";
            }
        }

        /// <summary>
        /// The default number of items in the meal
        /// </summary>
        private uint _defaultCount = 2;

        /// <summary>
        /// Private backing field for Count.
        /// </summary>
        private uint _itemCount = 2;

        /// <summary>
        /// A property representing the amount of sliders in this meal
        /// </summary>
        public override uint ItemCount
        {
            get
            {
                return _itemCount;
            }
            set
            {
                if (value <= 4 && value >= _defaultCount)
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
        /// Private backing field for AmericanCheese
        /// </summary>
        private bool _americanCheese = true;

        /// <summary>
        /// Whether the sliders contain American Cheese
        /// </summary>
        public bool AmericanCheese
        {
            get
            {
                return _americanCheese;
            }
            set
            {
                _americanCheese = value;

                OnPropertyChanged(nameof(AmericanCheese));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// The price of this meal
        /// </summary>
        public override decimal Price
        {
            get
            {
                decimal price = 5.99m;

                uint extraSliders = ItemCount - _defaultCount;

                price += 2.00m * extraSliders;

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
        /// The total number of calories in this meal
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 150 * ItemCount;

                if (!AmericanCheese)
                {
                    cals -= 40 * ItemCount;
                }

                cals += DrinkChoice.Calories;

                cals += SideChoice.Calories;

                return cals;
            }
        }

        /// <summary>
        /// Information for the preparation of this meal
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();
                if (ItemCount != _defaultCount)
                {
                    instructions.Add($"{ItemCount} Sliders");
                }

                if (!AmericanCheese)
                {
                    instructions.Add("Hold American Cheese");
                }

                instructions.Add($"Side: {SideChoice.Name}");
                foreach (string item in SideChoice.PreparationInformation)
                {
                    instructions.Add("\t" + item);
                }
                instructions.Add($"Drink: {DrinkChoice.Name}");
                foreach (string item in DrinkChoice.PreparationInformation)
                {
                    instructions.Add("\t" + item);
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
        /// Redefines the Equals method to compare the properties of SlidersMeal
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is SlidersMeal sliders1 && obj is SlidersMeal sliders2)
            {
                if (sliders1.Name == sliders2.Name && sliders1.Description == sliders2.Description && sliders1.ItemType == sliders2.ItemType && sliders1.ItemCount == sliders2.ItemCount && sliders1.AmericanCheese == sliders2.AmericanCheese)
                {
                    if ((sliders1.DrinkChoice as Drink).Equals(sliders2.DrinkChoice) && (sliders1.SideChoice as Side).Equals(sliders2.SideChoice) && sliders1.Price == sliders2.Price && sliders1.Calories == sliders2.Calories)
                    {
                        foreach (string s in sliders1.PreparationInformation)
                        {
                            if (!sliders2.PreparationInformation.Contains<string>(s))
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
