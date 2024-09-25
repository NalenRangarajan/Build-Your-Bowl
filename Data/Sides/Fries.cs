using System.ComponentModel;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the Fries class
    /// </summary>
    public class Fries : Side
    {

        /// <summary>
        /// The name of the fries instance
        /// </summary>
        public override string Name { get; } = "Fries";

        /// <summary>
        /// The description of this side
        /// </summary>
        public override string Description { get; } = "Crispy salty sticks of deliciousness";

        /// <summary>
        /// Private backing field for Curly
        /// </summary>
        private bool _curly = false;

        /// <summary>
        /// Whether these fries are curly
        /// </summary>
        public bool Curly
        {
            get
            {
                return _curly;
            }
            set
            {
                _curly = value;
                OnPropertyChanged(nameof(Curly));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// The price of this side
        /// </summary>
        public override decimal Price
        {
            get
            {
                decimal price = 3.50m;

                if (SizeType == Size.Kids)
                {
                    price -= 1.00m;
                }
                else if (SizeType == Size.Small)
                {
                    price -= 0.50m;
                }
                else if (SizeType == Size.Large)
                {
                    price += 0.75m;
                }

                return price;
            }
        }

        /// <summary>
        /// The total number of calories in this side
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 350;

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
        /// Information for the preparation of this side
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();
                instructions.Add($"{SizeType}");
                if (Curly)
                {
                    instructions.Add("Curly");
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
        /// Redefines the Equals method to compare the properties of fries
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is Fries fries1 && obj is Fries fries2)
            {
                if (fries1.Name == fries2.Name && fries1.Description == fries2.Description && fries1.Curly == fries2.Curly && fries1.SizeType == fries2.SizeType)
                {
                    if (fries1.Price == fries2.Price && fries1.Calories == fries2.Calories)
                    {
                        foreach (string s in fries1.PreparationInformation)
                        {
                            if (!fries2.PreparationInformation.Contains<string>(s))
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
