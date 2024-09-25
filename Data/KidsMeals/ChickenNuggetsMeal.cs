using System.ComponentModel;
using System.Diagnostics;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the ChickenNuggetsMeal class
    /// </summary>
    public class ChickenNuggetsMeal : KidsMeal
    {
        /// <summary>
        /// The name of the Chicken Nuggets Kids Meal instance
        /// </summary>
        public override string Name { get; } = "Chicken Nuggets Kids Meal";

        /// <summary>
        /// The description of this kids meal
        /// </summary>
        public override string Description { get; } = "Chicken nuggets with side and drink";

        /// <summary>
        /// The type of item in the Chicken Nuggets Kids meal
        /// </summary>
        public override string ItemType
        {
            get
            {
                return "Nuggets";
            }
        }

        /// <summary>
        /// The default number of items in the meal
        /// </summary>
        private uint _defaultCount = 5;

        /// <summary>
        /// The total number of calories in this meal
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 60 * ItemCount;

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
                    instructions.Add($"{ItemCount} Nuggets");
                }

                instructions.Add($"Side: {SideChoice.Name}");
                foreach(string item in SideChoice.PreparationInformation)
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
        /// Redefines the Equals method to compare the properties of ChickenNuggetsMeal
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is ChickenNuggetsMeal nuggets1 && obj is ChickenNuggetsMeal nuggets2)
            {
                if (nuggets1.Name == nuggets2.Name && nuggets1.Description == nuggets2.Description && nuggets1.ItemType == nuggets2.ItemType && nuggets1.ItemCount == nuggets2.ItemCount)
                {
                    if ((nuggets1.DrinkChoice as Drink).Equals(nuggets2.DrinkChoice) && (nuggets1.SideChoice as Side).Equals(nuggets2.SideChoice) && nuggets1.Price == nuggets2.Price && nuggets1.Calories == nuggets2.Calories)
                    {
                        foreach (string s in nuggets1.PreparationInformation)
                        {
                            if (!nuggets2.PreparationInformation.Contains<string>(s))
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
