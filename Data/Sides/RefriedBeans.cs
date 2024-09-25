using System.ComponentModel;
using System.Security.Authentication;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the RefriedBeans class
    /// </summary>
    public class RefriedBeans : Side
    {
        /// <summary>
        /// The name of the refried beans instance
        /// </summary>
        public override string Name { get; } = "Refried Beans";

        /// <summary>
        /// The description of this side
        /// </summary>
        public override string Description { get; } = "Beans fried not just once but twice";

        /// <summary>
        /// Private backing field for CheddarCheese
        /// </summary>
        private bool _cheddarCheese = true;

        /// <summary>
        /// Whether the refried beans have cheddar cheese
        /// </summary>
        public bool CheddarCheese
        {
            get
            {
                return _cheddarCheese;
            }
            set
            {
                _cheddarCheese = value;
                OnPropertyChanged(nameof(CheddarCheese));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// Private backing field for Onions
        /// </summary>
        private bool _onions = true;

        /// <summary>
        /// Whether the refied beans have onions
        /// </summary>
        public bool Onions
        {
            get
            {
                return _onions;
            }
            set
            {
                _onions = value;
                OnPropertyChanged(nameof(Onions));
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
                decimal price = 3.75m;

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
                uint cals = 300;

                double scaledAmount = 1.0;

                if (!CheddarCheese)
                {
                    cals -= 90;
                }
                if (!Onions)
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

                if (!CheddarCheese)
                {
                    instructions.Add("Hold Cheddar Cheese");
                }
                if (!Onions)
                {
                    instructions.Add("Hold Onions");
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
        /// Redefines the Equals method to compare the properties of beans
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is RefriedBeans beans1 && obj is RefriedBeans beans2)
            {
                if (beans1.Name == beans2.Name && beans1.Description == beans2.Description && beans1.CheddarCheese == beans2.CheddarCheese && beans1.Onions == beans2.Onions)
                {
                    if (beans1.SizeType == beans2.SizeType && beans1.Price == beans2.Price && beans1.Calories == beans2.Calories)
                    {
                        foreach (string s in beans1.PreparationInformation)
                        {
                            if (!beans2.PreparationInformation.Contains<string>(s))
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
