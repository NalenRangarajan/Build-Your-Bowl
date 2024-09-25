namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the GreenChickenBowl class
    /// </summary>
    public class GreenChickenBowl : Bowl
    {
        /// <summary>
        /// The name of the green chicken bowl instance
        /// </summary>
        /// <remarks>
        /// This is an example of an get-only autoproperty with a default value
        /// </remarks>
        public override string Name { get; } = "Green Chicken Bowl";

        /// <summary>
        /// The description of this bowl
        /// </summary>
        /// <remarks>
        /// This is also a get-only autoproperty, but it was declared using lambda syntax
        /// </remarks>
        public override string Description => "Rice bowl with chicken and green things";

        /// <summary>
        /// Private backing field for SalsaType
        /// </summary>
        private Salsa _salsaType = Salsa.Green;

        /// <summary>
        /// A virtual property representing the type of salsa to be included in the entree
        /// </summary>
        public override Salsa SalsaType
        {
            get
            {
                return _salsaType;
            }
            set
            {
                _salsaType = value;

                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(PreparationInformation));
                OnPropertyChanged(nameof(SalsaType));
            }
        }

        /// <summary>
        /// The price of this bowl
        /// </summary>
        public override decimal Price => 9.99m;

        /// <summary>
        /// The total number of calories in this bowl
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 890;
                IngredientItem i;

                if (AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!) && !i.Included)
                {
                    cals -= 150;
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.BlackBeans, out i!) && !i.Included)
                {
                    cals -= 130;
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
                if (AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && !i.Included)
                {
                    cals -= 150;
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

                if (AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!) && !i.Included)
                {
                    instructions.Add("Hold Chicken");
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.BlackBeans, out i!) && !i.Included)
                {
                    instructions.Add("Hold Black Beans");
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!) && !i.Included)
                {
                    instructions.Add("Hold Queso");
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!) && !i.Included)
                {
                    instructions.Add("Hold Veggies");
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!) && !i.Included)
                {
                    instructions.Add("Hold Sour Cream");
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
                else if (SalsaType == Salsa.Medium)
                {
                    instructions.Add("Swap Medium Salsa");
                }

                if (AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && !i.Included)
                {
                    instructions.Add("Hold Guacamole");
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
            AdditionalIngredients.Add(Ingredient.Chicken, new IngredientItem(Ingredient.Chicken));
            AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!);
            i.Included = true;
            i.Default = true;

            AdditionalIngredients.Add(Ingredient.Queso, new IngredientItem(Ingredient.Queso));
            AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = true;
            i.Default = true;

            AdditionalIngredients.Add(Ingredient.BlackBeans, new IngredientItem(Ingredient.BlackBeans));
            AdditionalIngredients.TryGetValue(Ingredient.BlackBeans, out i!);
            i.Included = true;
            i.Default = true;

            AdditionalIngredients.Add(Ingredient.Veggies, new IngredientItem(Ingredient.Veggies));
            AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = true;
            i.Default = true;

            AdditionalIngredients.Add(Ingredient.Guacamole, new IngredientItem(Ingredient.Guacamole));
            AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = true;
            i.Default = true;

            AdditionalIngredients.Add(Ingredient.SourCream, new IngredientItem(Ingredient.SourCream));
            AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!);
            i.Included = true;
            i.Default = true;
        }
    }
}