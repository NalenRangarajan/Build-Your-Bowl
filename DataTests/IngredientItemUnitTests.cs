using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the IngredientItem class
    /// </summary>
    public class IngredientItemUnitTests
    {
        /// <summary>
        /// Tests that ingredients are not included and not default by default
        /// </summary>
        /// <param name="i">The ingredient to make an item out of</param>
        [Theory]
        [InlineData(Ingredient.BlackBeans)]
        [InlineData(Ingredient.PintoBeans)]
        [InlineData(Ingredient.Queso)]
        [InlineData(Ingredient.Veggies)]
        [InlineData(Ingredient.SourCream)]
        [InlineData(Ingredient.Guacamole)]
        [InlineData(Ingredient.Chicken)]
        [InlineData(Ingredient.Steak)]
        [InlineData(Ingredient.Carnitas)]
        [InlineData(Ingredient.Rice)]
        [InlineData(Ingredient.Chips)]
        public void IngredientItemIncludedAndDefaultIsFalse(Ingredient i)
        {
            IngredientItem ingredient = new IngredientItem(i);

            Assert.False(ingredient.Included);
            Assert.False(ingredient.Default);
        }

        /// <summary>
        /// Tests that the name of the ingredient is correct
        /// </summary>
        /// <param name="i">The ingredient to make an item out of</param>
        /// <param name="expectedName">The expected name of the ingredient</param>
        /// 
        [Theory]
        [InlineData(Ingredient.BlackBeans, "Black Beans")]
        [InlineData(Ingredient.PintoBeans, "Pinto Beans")]
        [InlineData(Ingredient.Queso, "Queso")]
        [InlineData(Ingredient.Veggies, "Veggies")]
        [InlineData(Ingredient.SourCream, "Sour Cream")]
        [InlineData(Ingredient.Guacamole, "Guacamole")]
        [InlineData(Ingredient.Chicken, "Chicken")]
        [InlineData(Ingredient.Steak, "Steak")]
        [InlineData(Ingredient.Carnitas, "Carnitas")]
        [InlineData(Ingredient.Rice, "Rice")]
        [InlineData(Ingredient.Chips, "Chips")]
        public void IngredientItemName(Ingredient i, string expectedName)
        {
            IngredientItem ingredient = new IngredientItem(i);

            Assert.Matches(expectedName, ingredient.Name);
        }

        /// <summary>
        /// Tests that the calories in an ingredient is correct
        /// </summary>
        /// <param name="i">The ingredient to make an item out of</param>
        /// <param name="expectedCalories">The expected calories the ingredient</param>
        /// 
        [Theory]
        [InlineData(Ingredient.BlackBeans, 130)]
        [InlineData(Ingredient.PintoBeans, 130)]
        [InlineData(Ingredient.Queso, 110)]
        [InlineData(Ingredient.Veggies, 20)]
        [InlineData(Ingredient.SourCream, 100)]
        [InlineData(Ingredient.Guacamole, 150)]
        [InlineData(Ingredient.Chicken, 150)]
        [InlineData(Ingredient.Steak, 180)]
        [InlineData(Ingredient.Carnitas, 210)]
        [InlineData(Ingredient.Rice, 210)]
        [InlineData(Ingredient.Chips, 250)]
        public void IngredientItemCalories(Ingredient i, uint expectedCalories)
        {
            IngredientItem ingredient = new IngredientItem(i);

            Assert.Equal(expectedCalories, ingredient.Calories);
        }

        /// <summary>
        /// Tests that the cost of an ingredient is correct
        /// </summary>
        /// <param name="i">The ingredient to make an item out of</param>
        /// <param name="expectedCost">The expected cost ofthe ingredient</param>
        /// 
        [Theory]
        [InlineData(Ingredient.BlackBeans, 1.00)]
        [InlineData(Ingredient.PintoBeans, 1.00)]
        [InlineData(Ingredient.Queso, 1.00)]
        [InlineData(Ingredient.Veggies, 0.00)]
        [InlineData(Ingredient.SourCream, 0.00)]
        [InlineData(Ingredient.Guacamole, 1.00)]
        [InlineData(Ingredient.Chicken, 2.00)]
        [InlineData(Ingredient.Steak, 2.00)]
        [InlineData(Ingredient.Carnitas, 2.00)]
        [InlineData(Ingredient.Rice, 0.00)]
        [InlineData(Ingredient.Chips, 0.00)]
        public void IngredientItemCost(Ingredient i, decimal expectedCost)
        {
            IngredientItem ingredient = new IngredientItem(i);

            Assert.Equal(expectedCost, ingredient.UnitCost);
        }

        /// <summary>
        /// Tests that IngredientItem can be cast as an INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            //Arbitrary ingredient
            IngredientItem i = new IngredientItem(Ingredient.SourCream);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(i);
        }
    }
}
