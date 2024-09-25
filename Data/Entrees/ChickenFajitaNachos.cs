﻿namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the ChickenFajitaNachos class
    /// </summary>
    public class ChickenFajitaNachos : Nachos
    {
        /// <summary>
        /// The name of the chicken fajita nachos instance
        /// </summary>
        public override string Name { get; } = "Chicken Fajita Nachos";

        /// <summary>
        /// The description of these nachos
        /// </summary>
        public override string Description { get; } = "Chicken fajitas but with chips";

        /// <summary>
        /// The price of these nachos
        /// </summary>
        public override decimal Price
        {
            get
            {
                decimal price = 10.99m;
                IngredientItem i;

                if (AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && i.Included)
                {
                    price += 1m;
                }

                return price;
            }
        }

        /// <summary>
        /// The total number of calories in these nachos
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 650;
                IngredientItem i;

                if (AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!) && !i.Included)
                {
                    cals -= 150;
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!) && !i.Included)
                {
                    cals -= 110;
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!) && !i.Included)
                {
                    cals -= 20;
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!) && !i.Included)
                {
                    cals -= 100;
                }
                if (SalsaType == Salsa.None)
                {
                    cals -= 20;
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && i.Included)
                {
                    cals += 150;
                }

                return cals;
            }
        }

        /// <summary>
        /// Information for the preparation of these nachos
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();
                IngredientItem i;

                if (AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!) && !i.Included)
                {
                    instructions.Add("Hold Chicken");
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!) && !i.Included)
                {
                    instructions.Add("Hold Veggies");
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!) && !i.Included)
                {
                    instructions.Add("Hold Queso");
                }

                if (SalsaType == Salsa.None)
                {
                    instructions.Add("Hold Salsa");
                }
                else if (SalsaType == Salsa.Mild)
                {
                    instructions.Add("Swap Mild Salsa");
                }
                else if (SalsaType == Salsa.Hot)
                {
                    instructions.Add("Swap Hot Salsa");
                }
                else if (SalsaType == Salsa.Green)
                {
                    instructions.Add("Swap Green Salsa");
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && i.Included)
                {
                    instructions.Add("Add Guacamole");
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!) && !i.Included)
                {
                    instructions.Add("Hold Sour Cream");
                }

                return instructions;
            }
        }

        /// <summary>
        /// Adds the default additional ingredients to the dictionary and updates their included and default properties
        /// </summary>
        public override void BaseAdditionalIngredientsIncludedAndDefault()
        {
            AdditionalIngredients.Clear();
            IngredientItem i;

            //starts containing these
            AdditionalIngredients.Add(Ingredient.Chicken, new IngredientItem(Ingredient.Chicken));
            AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!);
            i.Included = true;
            i.Default = true;

            AdditionalIngredients.Add(Ingredient.Queso, new IngredientItem(Ingredient.Queso));
            AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = true;
            i.Default = true;

            AdditionalIngredients.Add(Ingredient.SourCream, new IngredientItem(Ingredient.SourCream));
            AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!);
            i.Included = true;
            i.Default = true;

            AdditionalIngredients.Add(Ingredient.Veggies, new IngredientItem(Ingredient.Veggies));
            AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = true;
            i.Default = true;

            //starts without containing these
            AdditionalIngredients.Add(Ingredient.Guacamole, new IngredientItem(Ingredient.Guacamole));
            AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = false;
            i.Default = false;
        }
    }
}