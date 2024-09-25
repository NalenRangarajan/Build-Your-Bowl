namespace BuildYourBowl.Data
{
    /// <summary>
    /// A mock drink class for testing
    /// </summary>
    public class MockDrink : Drink
    {
        /// <summary>
        /// The name of the mock drink
        /// </summary>
        public override string Name { get; } = "Mock Drink";

        /// <summary>
        /// The description of the mock drink
        /// </summary>
        public override string Description { get; } = "A Mock Drink";

        /// <summary>
        /// The price of the mock drink
        /// </summary>
        public override decimal Price
        {
            get
            {
                if (DrinkSize == Size.Small)
                {
                    return 0.50m;
                }
                else if (DrinkSize == Size.Medium)
                {
                    return 1.00m;
                }
                else if (DrinkSize == Size.Large)
                {
                    return 1.50m;
                }

                return 0.00m;
            }
        }

        /// <summary>
        /// The calories in the mock drink
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 300;

                double scaledAmount = 1.0;


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
        /// Instructions to create the mock drink
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();
                instructions.Add($"{DrinkSize}");

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
        /// Redefines the Equals method to compare the properties of mock drinks
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is MockDrink drink1 && obj is MockDrink drink2)
            {
                if (drink1.Name == drink2.Name && drink1.Description == drink2.Description && drink1.DrinkSize == drink2.DrinkSize)
                {
                    if (drink1.Price == drink2.Price && drink1.Calories == drink2.Calories)
                    {
                        foreach (string s in drink1.PreparationInformation)
                        {
                            if (!drink2.PreparationInformation.Contains<string>(s))
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
