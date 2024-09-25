namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the SpicySteakBowl class
    /// </summary>
    public class SpicySteakBowl : Bowl
    {
        /// <summary>
        /// The name of the spicy steak bowl instance
        /// </summary>
        public override string Name { get; } = "Spicy Steak Bowl";

        /// <summary>
        /// The description of this bowl
        /// </summary>
        public override string Description { get; } = "Spicy rice bowl with steak and fajita toppings";

        /// <summary>
        /// Private backing field for SalsaType
        /// </summary>
        private Salsa _salsaType = Salsa.Hot;

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
        /// The total number of calories in this bowl
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 620;
                IngredientItem i;
                
                if (AdditionalIngredients.TryGetValue(Ingredient.Steak, out i!) && !i.Included)
                {
                    cals -= 180;
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!) && !i.Included)
                {
                    cals -= 110;
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!) && i.Included)
                {
                    cals += 20;
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
        /// Information for the preparation of this bowl
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();
                IngredientItem i;

                if (AdditionalIngredients.TryGetValue(Ingredient.Steak, out i!) && !i.Included)
                {
                    instructions.Add("Hold Steak");
                }
                if (AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!) && i.Included)
                {
                    instructions.Add("Add Veggies");
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
                else if (SalsaType == Salsa.Medium)
                {
                    instructions.Add("Swap Medium Salsa");
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
            AdditionalIngredients.Add(Ingredient.Steak, new IngredientItem(Ingredient.Steak));
            AdditionalIngredients.TryGetValue(Ingredient.Steak, out i!);
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

            //starts without containing these
            AdditionalIngredients.Add(Ingredient.Veggies, new IngredientItem(Ingredient.Veggies));
            AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = false;
            i.Default = false;

            AdditionalIngredients.Add(Ingredient.Guacamole, new IngredientItem(Ingredient.Guacamole));
            AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = false;
            i.Default = false;
        }
    }
}
