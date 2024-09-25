using System.ComponentModel;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the Ingredient class
    /// </summary>
    public class IngredientItem : INotifyPropertyChanged
    {
        /// <summary>
        /// An event indicating that a property has changed
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Helper method to indicate that a property has changed
        /// </summary>
        /// <param name="propertyName">The property that has changed</param>
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The type of the ingredient
        /// </summary>
        public Ingredient IngredientType { get; }

        /// <summary>
        /// The name of the ingredient
        /// </summary>
        public string Name
        {
            get
            {
                if (IngredientType == Ingredient.BlackBeans)
                {
                    return "Black Beans";
                }
                else if (IngredientType == Ingredient.PintoBeans)
                {
                    return "Pinto Beans";
                }
                else if (IngredientType == Ingredient.Queso)
                {
                    return "Queso";
                }
                else if (IngredientType == Ingredient.Veggies)
                {
                    return "Veggies";
                }
                else if (IngredientType == Ingredient.SourCream)
                {
                    return "Sour Cream";
                }
                else if (IngredientType == Ingredient.Guacamole)
                {
                    return "Guacamole";
                }
                else if(IngredientType == Ingredient.Chicken)
                {
                    return "Chicken";
                }
                else if (IngredientType == Ingredient.Steak)
                {
                    return "Steak";
                }
                else if(IngredientType == Ingredient.Carnitas)
                {
                    return "Carnitas";
                }
                else if(IngredientType == Ingredient.Rice)
                {
                    return "Rice";
                }
                else //IngredientType is Chips
                {
                    return "Chips";
                }
            }
        }

        /// <summary>
        /// The number of calories in the ingredient
        /// </summary>
        public uint Calories
        {
            get
            {
                if (IngredientType == Ingredient.BlackBeans || IngredientType == Ingredient.PintoBeans)
                {
                    return 130;
                }
                else if (IngredientType == Ingredient.Queso)
                {
                    return 110;
                }
                else if (IngredientType == Ingredient.Veggies)
                {
                    return 20;
                }
                else if (IngredientType == Ingredient.SourCream)
                {
                    return 100;
                }
                else if (IngredientType == Ingredient.Guacamole || IngredientType == Ingredient.Chicken)
                {
                    return 150;
                }
                else if (IngredientType == Ingredient.Steak)
                {
                    return 180;
                }
                else if (IngredientType == Ingredient.Carnitas)
                {
                    return 210;
                }
                else if (IngredientType == Ingredient.Rice)
                {
                    return 210;
                }
                else //IngredientType is Chips
                {
                    return 250;
                }
            }
        }

        /// <summary>
        /// The cost of the ingredient
        /// </summary>
        public decimal UnitCost 
        {
            get
            {
                if(IngredientType == Ingredient.Steak || IngredientType == Ingredient.Chicken || IngredientType == Ingredient.Carnitas)
                {
                    return 2.00m;
                }
                else if (IngredientType == Ingredient.BlackBeans || IngredientType == Ingredient.PintoBeans || IngredientType == Ingredient.Queso || IngredientType == Ingredient.Guacamole)
                {
                    return 1.00m;
                }
                else
                {
                    return 0.00m;
                }
            }
        }

        /// <summary>
        /// Private backing field for Included
        /// </summary>
        private bool _included = false;

        /// <summary>
        /// Whether this ingredient is currently included in its containing menu item
        /// </summary>
        public bool Included
        {
            get
            {
                return _included;
            }
            set
            {
                _included = value;
                OnPropertyChanged(nameof(Included));
            }
        }

        /// <summary>
        /// Whether this ingredient is included in its containing menu item by default
        /// </summary>
        public bool Default { get; set; } = false;

        /// <summary>
        /// Constructs an ingredient item
        /// </summary>
        /// <param name="ingredientType">The type of ingredient to construct</param>
        public IngredientItem (Ingredient ingredientType) 
        { 
            IngredientType = ingredientType;
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
        /// Redefines the Equals method to compare the properties of IngredientItem
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>A bool determining whether the objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (this is IngredientItem ingredient1 && obj is IngredientItem ingredient2)
            {
                if (ingredient1.Name == ingredient2.Name && ingredient1.IngredientType == ingredient2.IngredientType && ingredient1.Included == ingredient2.Included)
                {
                    if (ingredient1.Default == ingredient2.Default && ingredient1.UnitCost == ingredient2.UnitCost && ingredient1.Calories == ingredient2.Calories)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
