using System.ComponentModel;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the CornDogBitesMeal class
    /// </summary>
    public class CornDogBitesMeal : KidsMeal
    {
        /// <summary>
        /// The name of the Corn Dog Bites Kids Meal instance
        /// </summary>
        public override string Name { get; } = "Corn Dog Bites Kids Meal";

        /// <summary>
        /// The description of this kids meal
        /// </summary>
        public override string Description { get; } = "Mini corn dogs with side and drink";

        /// <summary>
        /// The type of item in the Corn Dog Bites meal
        /// </summary>
        public override string ItemType
        {
            get
            {
                return "Bites";
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
                uint cals = 50 * ItemCount;

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
                    instructions.Add($"{ItemCount} Bites");
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
        /// Redefines the Equals method to compare the properties of CornDogBitesMeal
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is CornDogBitesMeal bites1 && obj is CornDogBitesMeal bites2)
            {
                if (bites1.Name == bites2.Name && bites1.Description == bites2.Description && bites1.ItemType == bites2.ItemType && bites1.ItemCount == bites2.ItemCount)
                {
                    if ((bites1.DrinkChoice as Drink).Equals(bites2.DrinkChoice) && (bites1.SideChoice as Side).Equals(bites2.SideChoice) && bites1.Price == bites2.Price && bites1.Calories == bites2.Calories)
                    {
                        foreach (string s in bites1.PreparationInformation)
                        {
                            if (!bites2.PreparationInformation.Contains<string>(s))
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
