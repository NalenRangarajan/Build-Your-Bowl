namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the CarnitasBowl class
    /// </summary>
    public class CarnitasBowl : Bowl
    {
        /// <summary>
        /// The name of the carnitas bowl instance
        /// </summary>
        public override string Name { get; } = "Carnitas Bowl";

        /// <summary>
        /// The description of this bowl
        /// </summary>
        public override string Description { get; } = "Rice bowl with carnitas and extras";
        
        /// <summary>
        /// The price of this bowl
        /// </summary>
        public override decimal Price
        {
            get
            {
                decimal price = 9.99m;

                IngredientItem i;

                if (AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && i.Included)
                {
                    price += 1m;
                }

                return price;
            }
        }

        /// <summary>
        /// The total number of calories in this bowl
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 680;

                IngredientItem i;

                if (AdditionalIngredients.TryGetValue(Ingredient.Carnitas, out i!) && !i.Included)
                {
                    cals -= 210;
                }

                if (AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!) && !i.Included)
                {
                    cals -= 110;
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!) && i.Included)
                {
                    cals += 20;
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!) && i.Included)
                {
                    cals += 100;
                }
                if (SalsaType == Salsa.None)
                {
                    cals -= 20;
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.PintoBeans, out i!) && !i.Included)
                {
                    cals -= 130;
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && i.Included)
                {
                    cals += 150;
                }

                return cals;
            }
        }

        /// <summary>
        /// Information for the preparation of this bowl
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();
                IngredientItem i;

                if (AdditionalIngredients.TryGetValue(Ingredient.Carnitas, out i!) && !i.Included)
                {
                    instructions.Add("Hold Carnitas");
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!) && i.Included)
                {
                    instructions.Add("Add Veggies");
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!) && !i.Included)
                {
                    instructions.Add("Hold Queso");
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.PintoBeans, out i!) && !i.Included)
                {
                    instructions.Add("Hold Pinto Beans");
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
                if (AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!) && i.Included)
                {
                    instructions.Add("Add Sour Cream");
                }

                return instructions;
            }
        }

        /// <summary>
        /// Adds the default additional ingredients to the dictionary and updates their Included and Default properties
        /// </summary>
        public override void BaseAdditionalIngredientsIncludedAndDefault()
        {
            AdditionalIngredients.Clear();
            IngredientItem i;

            //starts containing these
            AdditionalIngredients.Add(Ingredient.Carnitas, new IngredientItem(Ingredient.Carnitas));
            AdditionalIngredients.TryGetValue(Ingredient.Carnitas, out i!);
            i.Included = true;
            i.Default = true;

            AdditionalIngredients.Add(Ingredient.Queso, new IngredientItem(Ingredient.Queso));
            AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = true;
            i.Default = true;

            AdditionalIngredients.Add(Ingredient.PintoBeans, new IngredientItem(Ingredient.PintoBeans));
            AdditionalIngredients.TryGetValue(Ingredient.PintoBeans, out i!);
            i.Included = true;
            i.Default = true;

            //starts without containing these
            AdditionalIngredients.Add(Ingredient.Veggies, new IngredientItem(Ingredient.Veggies));
            AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = false;
            i.Default = false;

            AdditionalIngredients.Add(Ingredient.Guacamole, new IngredientItem(Ingredient.Guacamole));
            AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = false;
            i.Default = false;

            AdditionalIngredients.Add(Ingredient.SourCream, new IngredientItem(Ingredient.SourCream));
            AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!);
            i.Included = false;
            i.Default = false;
        }

        public CarnitasBowl()
        {
            BaseAdditionalIngredientsIncludedAndDefault();
        }
    }
}
