using System.ComponentModel;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// This represents an abstract Entree class
    /// </summary>
    public abstract class Entree : IMenuItem
    {
        /// <summary>
        /// An event indicating that a property has changed
        /// </summary>
        public virtual event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Helper method to indicate that a property has changed
        /// </summary>
        /// <param name="propertyName">The property that has changed</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The name of the abstract entree
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The description of the abstract entree
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// A virtual property representing the base ingredient in the entree
        /// </summary>
        public virtual IngredientItem BaseIngredient { get; } = new IngredientItem(Ingredient.Rice) { Included = true, Default = true};

        /// <summary>
        /// Private backing field for SalsaType
        /// </summary>
        private Salsa _salsaType = Salsa.Medium;

        /// <summary>
        /// A virtual property representing the type of salsa to be included in the entree
        /// </summary>
        public virtual Salsa SalsaType
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
        /// Additional ingredients that can be included in the entree
        /// </summary>
        public virtual Dictionary<Ingredient, IngredientItem> AdditionalIngredients { get; } = new Dictionary<Ingredient, IngredientItem>();

        /// <summary>
        /// A virtual property representing the price of an entree
        /// </summary>
        public virtual decimal Price
        {
            get
            {
                decimal price = 7.99m;

                foreach (KeyValuePair<Ingredient, IngredientItem> ingredient in AdditionalIngredients)
                {
                    if (ingredient.Value.Included)
                    {
                        price += ingredient.Value.UnitCost;
                    }
                }

                return price;
            }
        }

        /// <summary>
        /// A virtual property representing the calories in an entree
        /// </summary>
        public virtual uint Calories
        {
            get
            {
                uint cals = 0;

                cals += BaseIngredient.Calories;

                if(SalsaType != Salsa.None)
                {
                    cals += 20;
                }

                foreach(KeyValuePair<Ingredient, IngredientItem> ingredient in AdditionalIngredients)
                {
                    if (ingredient.Value.Included)
                    {
                        cals += ingredient.Value.Calories;
                    }
                }

                return cals;
            }
        }

        /// <summary>
        /// A virtual property representing the information for the preparation of this entree
        /// </summary>
        public virtual IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();
                if (SalsaType == Salsa.Green)
                {
                    instructions.Add("Swap Green Salsa");
                }
                else if (SalsaType == Salsa.Hot)
                {
                    instructions.Add("Swap Hot Salsa");
                }
                else if (SalsaType == Salsa.Mild)
                {
                    instructions.Add("Swap Mild Salsa");
                }
                else if (SalsaType == Salsa.None)
                {
                    instructions.Add("Hold Salsa");
                }

                foreach (KeyValuePair<Ingredient, IngredientItem> ingredient in AdditionalIngredients)
                {
                    if (ingredient.Value.Included)
                    {
                        instructions.Add($"Add {ingredient.Value.Name}");
                    }
                }

                return instructions;
            }
        }

        /// <summary>
        /// A method that adds the additional ingredients to the dictionary and appropriately sets it's Included and Default properties
        /// </summary>
        public virtual void BaseAdditionalIngredientsIncludedAndDefault()
        {
            AdditionalIngredients.Clear();
            IngredientItem i;

            //starts without containing these
            AdditionalIngredients.Add(Ingredient.Chicken, new IngredientItem(Ingredient.Chicken));
            AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!);
            i.Included = false;
            i.Default = false;

            AdditionalIngredients.Add(Ingredient.Steak, new IngredientItem(Ingredient.Steak));
            AdditionalIngredients.TryGetValue(Ingredient.Steak, out i!);
            i.Included = false;
            i.Default = false;

            AdditionalIngredients.Add(Ingredient.Carnitas, new IngredientItem(Ingredient.Carnitas));
            AdditionalIngredients.TryGetValue(Ingredient.Carnitas, out i!);
            i.Included = false;
            i.Default = false;

            AdditionalIngredients.Add(Ingredient.Queso, new IngredientItem(Ingredient.Queso));
            AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = false;
            i.Default = false;

            AdditionalIngredients.Add(Ingredient.PintoBeans, new IngredientItem(Ingredient.PintoBeans));
            AdditionalIngredients.TryGetValue(Ingredient.PintoBeans, out i!);
            i.Included = false;
            i.Default = false;

            AdditionalIngredients.Add(Ingredient.BlackBeans, new IngredientItem(Ingredient.BlackBeans));
            AdditionalIngredients.TryGetValue(Ingredient.BlackBeans, out i!);
            i.Included = false;
            i.Default = false;

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

        /// <summary>
        /// A method that allows an ingredient to be added
        /// </summary>
        /// <param name="ingredient">The ingredient to add</param>
        public virtual void EditOrderAdd(Ingredient ingredient)
        {
            IngredientItem i;

            if (AdditionalIngredients.TryGetValue(ingredient, out i!) && !i.Included)
            {
                i.Included = true;
            }

            i.PropertyChanged += HandleChange!;

            OnPropertyChanged(nameof(Calories));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(PreparationInformation));
        }

        /// <summary>
        /// A method that allows an ingredient to be removed
        /// </summary>
        /// <param name="ingredient">The ingredient to remove</param>
        public virtual void EditOrderRemove(Ingredient ingredient)
        {
            IngredientItem i;

            if(AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included)
            {
                i.Included = false;
            }

            i.PropertyChanged -= HandleChange!;

            OnPropertyChanged(nameof(Calories));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(PreparationInformation));
        }

        /// <summary>
        /// Returns the name of the entree
        /// </summary>
        /// <returns>The name of the entree</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Handler that updates the properties of the entree
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        private void HandleChange(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Calories));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(PreparationInformation));
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
        /// Redefines the Equals method to compare the properties of entrees
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is Entree entree1 && obj is Entree entree2)
            {
                if (entree1.Name == entree2.Name && entree1.Description == entree2.Description && entree1.SalsaType == entree2.SalsaType)
                {
                    if(entree1.BaseIngredient.Name == entree2.BaseIngredient.Name && entree1.BaseIngredient.Included == entree2.BaseIngredient.Included && entree1.BaseIngredient.Default == entree2.BaseIngredient.Default)
                    {
                        foreach (KeyValuePair<Ingredient, IngredientItem> key in entree1.AdditionalIngredients)
                        {
                            if (!entree2.AdditionalIngredients.TryGetValue(key.Key, out IngredientItem? value))
                            {
                                return false;
                            }
                            else if (entree2.AdditionalIngredients.TryGetValue(key.Key, out IngredientItem? value2))
                            {
                                if ((key.Value.Included != value2.Included || key.Value.Default != value2.Default || key.Value.Name != value2.Name))
                                {
                                    return false;
                                }
                            }
                        }

                        if (entree1.Price == entree2.Price && entree1.Calories == entree2.Calories)
                        {
                            foreach (string s in entree1.PreparationInformation)
                            {
                                if (!entree2.PreparationInformation.Contains<string>(s))
                                {
                                    return false;
                                }
                            }

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public Entree()
        {
            BaseAdditionalIngredientsIncludedAndDefault();
        }
    }
}
