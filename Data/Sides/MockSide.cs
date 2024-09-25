namespace BuildYourBowl.Data
{
    /// <summary>
    /// A mock side class for testing
    /// </summary>
    public class MockSide : Side
    {
        /// <summary>
        /// The name of the mock side
        /// </summary>
        public override string Name { get; } = "Mock Side";

        /// <summary>
        /// The description of the mock side
        /// </summary>
        public override string Description { get; } = "A Mock Side";

        /// <summary>
        /// The price of the mock side
        /// </summary>
        public override decimal Price
        {
            get
            {
                if (SizeType == Size.Small)
                {
                    return 0.50m;
                }
                else if (SizeType == Size.Medium)
                {
                    return 1.00m;
                }
                else if (SizeType == Size.Large)
                {
                    return 1.50m;
                }

                return 0.00m;
            }
        }

        /// <summary>
        /// The calories in the mock side
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 300;

                double scaledAmount = 1.0;


                if (SizeType == Size.Kids)
                {
                    scaledAmount = 0.60;
                }
                else if (SizeType == Size.Small)
                {
                    scaledAmount = 0.75;
                }
                else if (SizeType == Size.Large)
                {
                    scaledAmount = 1.50;
                }

                cals = (uint)(cals * scaledAmount);

                return cals;
            }
        }

        /// <summary>
        /// The instructions to create the mock side
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();
                instructions.Add($"{SizeType}");

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
        /// Redefines the Equals method to compare the properties of mock sides
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is MockSide side1 && obj is MockSide side2)
            {
                if (side1.Name == side2.Name && side1.Description == side2.Description && side1.SizeType == side2.SizeType)
                {
                    if (side1.Price == side2.Price && side1.Calories == side2.Calories)
                    {
                        foreach (string s in side1.PreparationInformation)
                        {
                            if (!side2.PreparationInformation.Contains<string>(s))
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
