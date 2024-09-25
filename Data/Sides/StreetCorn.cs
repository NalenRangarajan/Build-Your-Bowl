using System.ComponentModel;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the StreetCorn class
    /// </summary>
    public class StreetCorn : Side
    {
        /// <summary>
        /// The name of the street corn instance
        /// </summary>
        public override string Name { get; } = "Street Corn";

        /// <summary>
        /// The description of this side
        /// </summary>
        public override string Description { get; } = "The zestiest corn out there";

        /// <summary>
        /// Private backing field for CheddarCheese
        /// </summary>
        private bool _cotijaCheese = true;

        /// <summary>
        /// Whether the corn has cotija cheese
        /// </summary>
        public bool CotijaCheese
        {
            get
            {
                return _cotijaCheese;
            }
            set
            {
                _cotijaCheese = value;
                OnPropertyChanged(nameof(CotijaCheese));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// Private backing field for Cilantro
        /// </summary>
        private bool _cilantro = true;

        /// <summary>
        /// Whether the corn has cilantro
        /// </summary>
        public bool Cilantro
        {
            get
            {
                return _cilantro;
            }
            set
            {
                _cilantro = value;
                OnPropertyChanged(nameof(Cilantro));
                OnPropertyChanged(nameof(Calories));
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
                decimal price = 4.50m;

                if (SizeType == Size.Kids)
                {
                    price -= 1.25m;
                }
                else if (SizeType == Size.Small)
                {
                    price -= 0.75m;
                }
                else if (SizeType == Size.Large)
                {
                    price += 1.00m;
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
                uint cals = 300;

                double scaledAmount = 1.0;

                if (!CotijaCheese)
                {
                    cals -= 80;
                }
                if (!Cilantro)
                {
                    cals -= 5;
                }

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
                if (!CotijaCheese)
                {
                    instructions.Add("Hold Cotija Cheese");
                }
                if (!Cilantro)
                {
                    instructions.Add("Hold Cilantro");
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
        /// Redefines the Equals method to compare the properties of corn
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is StreetCorn corn1 && obj is StreetCorn corn2)
            {
                if (corn1.Name == corn2.Name && corn1.Description == corn2.Description && corn1.CotijaCheese == corn2.CotijaCheese && corn1.Cilantro == corn2.Cilantro)
                {
                    if ( corn1.SizeType == corn2.SizeType && corn1.Price == corn2.Price && corn1.Calories == corn2.Calories)
                    {
                        foreach (string s in corn1.PreparationInformation)
                        {
                            if (!corn2.PreparationInformation.Contains<string>(s))
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
