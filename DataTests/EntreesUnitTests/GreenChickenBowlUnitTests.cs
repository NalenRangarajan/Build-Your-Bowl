using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the GreenChickenBowl class
    /// </summary>
    public class GreenChickenBowlUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void GreenChickenBowlDefault()
        {
            GreenChickenBowl c = new GreenChickenBowl();
            c.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;

            Assert.True(c.AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!) && i.Included);
            Assert.True(c.AdditionalIngredients.TryGetValue(Ingredient.BlackBeans, out i!) && i.Included);
            Assert.True(c.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!) && i.Included);
            Assert.True(c.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!) && i.Included);
            Assert.Equal(Salsa.Green, c.SalsaType);
            Assert.True(c.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && i.Included);
            Assert.True(c.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!) && i.Included);
            Assert.Equal(9.99m, c.Price);
            Assert.Equal((uint)890, c.Calories);

            string[] expectedOrderOutput = new string[] { };

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, c.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, c.PreparationInformation.Count());

            Assert.Equal("Green Chicken Bowl", c.ToString());
        }



        /// <summary>
        /// This tests that the bowl has the correct number of calories with different ingredients included or excluded
        /// </summary>
        /// <param name="hasChicken">Whether the bowl should have chicken</param>
        /// <param name="hasBlackBeans">Whether the bowl should have black beans</param>
        /// <param name="hasQueso">Whether the bowl should have queso</param>
        /// <param name="hasVeggies">Whether the bowl should have veggies</param>
        /// <param name="typeOfSalsa">What type of salsa the bowl should have</param>
        /// <param name="hasGuacamole">Whether the bowl should have guacamole</param>
        /// <param name="hasSourCream">Whether the bowl should have sour cream</param>
        /// <param name="expectedCalories">The expected number of calories in the bowl</param>
        [Theory]
        [InlineData(false, false, false, false, Salsa.None, false, false, 210)]
        [InlineData(true, true, true, true, Salsa.Hot, true, true, 890)]
        [InlineData(false, true, false, true, Salsa.None, true, false, 510)]
        [InlineData(true, false, true, false, Salsa.Green, false, true, 590)]
        [InlineData(true, true, true, true, Salsa.None, false, false, 620)]
        [InlineData(false, false, false, false, Salsa.Mild, true, true, 480)]
        [InlineData(true, true, false, false, Salsa.Hot, true, false, 660)]
        [InlineData(false, false, true, true, Salsa.None, false, true, 440)]
        [InlineData(false, true, false, false, Salsa.None, true, true, 590)]
        public void GreenChickenBowlCaloriesWithDifferentIngredients(bool hasChicken, bool hasBlackBeans, bool hasQueso, bool hasVeggies, Salsa typeOfSalsa, bool hasGuacamole, bool hasSourCream, uint expectedCalories)
        {
            GreenChickenBowl c = new GreenChickenBowl();
            c.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;
            c.AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!);
            i.Included = hasChicken;
            c.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = hasVeggies;
            c.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = hasQueso;
            c.AdditionalIngredients.TryGetValue(Ingredient.BlackBeans, out i!);
            i.Included = hasBlackBeans;
            c.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!);
            i.Included = hasSourCream;
            c.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = hasGuacamole;

            c.SalsaType = typeOfSalsa;

            Assert.Equal(expectedCalories, c.Calories);
        }

        /// <summary>
        /// This tests that the bowl has the order output with different ingredients included or excluded
        /// </summary>
        /// <param name="hasChicken">Whether the bowl should have chicken</param>
        /// <param name="hasBlackBeans">Whether the bowl should have black beans</param>
        /// <param name="hasQueso">Whether the bowl should have queso</param>
        /// <param name="hasVeggies">Whether the bowl should have veggies</param>
        /// <param name="typeOfSalsa">What type of salsa the bowl should have</param>
        /// <param name="hasGuacamole">Whether the bowl should have guacamole</param>
        /// <param name="hasSourCream">Whether the bowl should have sour cream</param>
        /// <param name="expectedOrderOutput">The expected order output from the bowl</param>
        [Theory]
        [InlineData(false, false, false, false, Salsa.None, false, false, new string[] { "Hold Chicken", "Hold Black Beans", "Hold Queso", "Hold Veggies", "Hold Salsa", "Hold Guacamole", "Hold Sour Cream" })]
        [InlineData(true, true, true, true, Salsa.Hot, true, true, new string[] { "Swap Hot Salsa" })]
        [InlineData(false, true, false, true, Salsa.None, true, false, new string[] { "Hold Chicken", "Hold Queso", "Hold Salsa", "Hold Sour Cream" })]
        [InlineData(true, false, true, false, Salsa.Green, false, true, new string[] { "Hold Black Beans", "Hold Veggies", "Hold Guacamole" })]
        [InlineData(true, true, true, true, Salsa.None, false, false, new string[] { "Hold Salsa", "Hold Guacamole", "Hold Sour Cream" })]
        [InlineData(false, false, false, false, Salsa.Mild, true, true, new string[] { "Hold Chicken", "Hold Black Beans", "Hold Queso", "Hold Veggies", "Swap Mild Salsa", })]
        [InlineData(true, true, false, false, Salsa.Hot, true, false, new string[] { "Hold Queso", "Hold Veggies", "Swap Hot Salsa", "Hold Sour Cream", })]
        [InlineData(false, false, true, true, Salsa.None, false, true, new string[] { "Hold Chicken", "Hold Black Beans", "Hold Salsa", "Hold Guacamole" })]
        [InlineData(false, true, false, false, Salsa.None, true, true, new string[] { "Hold Chicken", "Hold Queso", "Hold Veggies", "Hold Salsa" })]
        public void GreenChickenBowlPreperationInformationWithDifferentIngredients(bool hasChicken, bool hasBlackBeans, bool hasQueso, bool hasVeggies, Salsa typeOfSalsa, bool hasGuacamole, bool hasSourCream, string[] expectedOrderOutput)
        {
            GreenChickenBowl c = new GreenChickenBowl();
            c.BaseAdditionalIngredientsIncludedAndDefault();

            IngredientItem i;
            c.AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!);
            i.Included = hasChicken;
            c.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = hasVeggies;
            c.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = hasQueso;
            c.AdditionalIngredients.TryGetValue(Ingredient.BlackBeans, out i!);
            i.Included = hasBlackBeans;
            c.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!);
            i.Included = hasSourCream;
            c.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = hasGuacamole;

            c.SalsaType = typeOfSalsa;

            //Checks that each expected string is in the actual preparation info
            Assert.All(expectedOrderOutput, word => Assert.Contains(word, c.PreparationInformation));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(expectedOrderOutput.Length, c.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that GreenChickenBowl can be cast as an IMenuItem, Bowl, Entree, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            GreenChickenBowl c = new GreenChickenBowl();

            Assert.IsAssignableFrom<IMenuItem>(c);
            Assert.IsAssignableFrom<Bowl>(c);
            Assert.IsAssignableFrom<Entree>(c);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(c);
        }

        /// <summary>
        /// Tests that certain ingredients are included by default
        /// </summary>
        /// <param name="ingredient">The ingredient to check</param>
        [Theory]
        [InlineData(Ingredient.Chicken)]
        [InlineData(Ingredient.Queso)]
        [InlineData(Ingredient.BlackBeans)]
        [InlineData(Ingredient.SourCream)]
        [InlineData(Ingredient.Veggies)]
        [InlineData(Ingredient.Guacamole)]
        public void IngredientsIncludedByDefault(Ingredient ingredient)
        {
            GreenChickenBowl c = new GreenChickenBowl();
            IngredientItem i;

            c.BaseAdditionalIngredientsIncludedAndDefault();

            //The ingredient is included by default
            Assert.True(c.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);
        }

        /// <summary>
        /// Tests that certain ingredients can't be added to AdditionalIngredients
        /// </summary>
        /// <param name="ingredient">The ingredient to include in the bowl</param>
        [Theory]
        [InlineData(Ingredient.Carnitas)]
        [InlineData(Ingredient.Steak)]
        [InlineData(Ingredient.PintoBeans)]
        [InlineData(Ingredient.Rice)]
        [InlineData(Ingredient.Chips)]
        public void CantIncludeCertainIngredients(Ingredient ingredient)
        {
            GreenChickenBowl c = new GreenChickenBowl();
            c.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;

            Assert.False(c.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);

            Action action = () => c.EditOrderAdd(ingredient);

            Assert.Throws<NullReferenceException>(action);

            Assert.False(c.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when adding and removing ingredients
        /// </summary>
        /// <param name="ingredient">The ingredient to add/remove</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(Ingredient.Chicken, "Calories")]
        [InlineData(Ingredient.Queso, "Calories")]
        [InlineData(Ingredient.BlackBeans, "Calories")]
        [InlineData(Ingredient.Veggies, "Calories")]
        [InlineData(Ingredient.Guacamole, "Calories")]
        [InlineData(Ingredient.SourCream, "Calories")]
        [InlineData(Ingredient.Chicken, "Price")]
        [InlineData(Ingredient.Queso, "Price")]
        [InlineData(Ingredient.BlackBeans, "Price")]
        [InlineData(Ingredient.Veggies, "Price")]
        [InlineData(Ingredient.Guacamole, "Price")]
        [InlineData(Ingredient.SourCream, "Price")]
        [InlineData(Ingredient.Chicken, "PreparationInformation")]
        [InlineData(Ingredient.Queso, "PreparationInformation")]
        [InlineData(Ingredient.BlackBeans, "PreparationInformation")]
        [InlineData(Ingredient.Veggies, "PreparationInformation")]
        [InlineData(Ingredient.Guacamole, "PreparationInformation")]
        [InlineData(Ingredient.SourCream, "PreparationInformation")]
        public void UpdatingIngredientShouldNotifyOfPropertyChanges(Ingredient ingredient, string propertyName)
        {
            GreenChickenBowl c = new GreenChickenBowl();

            Assert.PropertyChanged(c, propertyName, () =>
            {
                c.EditOrderAdd(ingredient);
            });

            Assert.PropertyChanged(c, propertyName, () =>
            {
                c.EditOrderRemove(ingredient);
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing the salsa
        /// </summary>
        /// <param name="salsa">The salsa to change to</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(Salsa.Mild, "Calories")]
        [InlineData(Salsa.Medium, "Calories")]
        [InlineData(Salsa.Hot, "Calories")]
        [InlineData(Salsa.Green, "Calories")]
        [InlineData(Salsa.None, "Calories")]
        [InlineData(Salsa.Mild, "PreparationInformation")]
        [InlineData(Salsa.Medium, "PreparationInformation")]
        [InlineData(Salsa.Hot, "PreparationInformation")]
        [InlineData(Salsa.Green, "PreparationInformation")]
        [InlineData(Salsa.None, "PreparationInformation")]
        [InlineData(Salsa.Mild, "SalsaType")]
        [InlineData(Salsa.Medium, "SalsaType")]
        [InlineData(Salsa.Hot, "SalsaType")]
        [InlineData(Salsa.Green, "SalsaType")]
        [InlineData(Salsa.None, "SalsaType")]
        public void ChangingSalsaTypeShouldNotifyOfPropertyChanges(Salsa salsa, string propertyName)
        {
            GreenChickenBowl c = new GreenChickenBowl();

            Assert.PropertyChanged(c, propertyName, () =>
            {
                c.SalsaType = salsa;
            });
        }

        /// <summary>
        /// Tests that two GreenChickenBowls with identical properties are equal to each other
        /// </summary>
        /// <param name="salsa1">The salsa for the first bowl</param>
        /// <param name="salsa2">The salsa for the second bowl</param>
        /// <param name="add1">The additional ingredients for the first bowl</param>
        /// <param name="add2">The additional ingredients for the second bowl</param>
        [Theory]
        [InlineData(Salsa.Mild, Salsa.Mild, new Ingredient[] { Ingredient.BlackBeans, Ingredient.Chicken }, new Ingredient[] { Ingredient.BlackBeans, Ingredient.Chicken })]
        [InlineData(Salsa.Medium, Salsa.Medium, new Ingredient[] { Ingredient.Queso, Ingredient.SourCream }, new Ingredient[] { Ingredient.Queso, Ingredient.SourCream })]
        [InlineData(Salsa.Hot, Salsa.Hot, new Ingredient[] { Ingredient.SourCream }, new Ingredient[] { Ingredient.SourCream })]
        [InlineData(Salsa.Green, Salsa.Green, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken })]
        [InlineData(Salsa.None, Salsa.None, new Ingredient[] { Ingredient.Veggies, Ingredient.Chicken }, new Ingredient[] { Ingredient.Veggies, Ingredient.Chicken })]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(Salsa salsa1, Salsa salsa2, Ingredient[] add1, Ingredient[] add2)
        {
            GreenChickenBowl b1 = new GreenChickenBowl() { SalsaType = salsa1 };
            GreenChickenBowl b2 = new GreenChickenBowl() { SalsaType = salsa2 };

            //removing since everything is on by default
            foreach (Ingredient i in add1)
            {
                b1.EditOrderRemove(i);
            }
            foreach (Ingredient i in add2)
            {
                b2.EditOrderRemove(i);
            }

            Assert.True(b1.Equals(b2));
        }

        /// <summary>
        /// Tests that two GreenChickenBowls with different properties are not equal to each other
        /// </summary>
        /// <param name="salsa1">The salsa for the first bowl</param>
        /// <param name="salsa2">The salsa for the second bowl</param>
        /// <param name="add1">The additional ingredients for the first bowl</param>
        /// <param name="add2">The additional ingredients for the second bowl</param>
        [Theory]
        [InlineData(Salsa.Mild, Salsa.Mild, new Ingredient[] { Ingredient.Veggies, Ingredient.Chicken }, new Ingredient[] { Ingredient.BlackBeans, Ingredient.Chicken })]
        [InlineData(Salsa.Medium, Salsa.Hot, new Ingredient[] { Ingredient.Queso, Ingredient.SourCream }, new Ingredient[] { Ingredient.Chicken, Ingredient.SourCream })]
        [InlineData(Salsa.Hot, Salsa.Green, new Ingredient[] { Ingredient.Veggies }, new Ingredient[] { Ingredient.SourCream })]
        [InlineData(Salsa.None, Salsa.Green, new Ingredient[] { Ingredient.Guacamole, Ingredient.Queso }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken })]
        [InlineData(Salsa.None, Salsa.None, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken }, new Ingredient[] { Ingredient.Veggies, Ingredient.Chicken })]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(Salsa salsa1, Salsa salsa2, Ingredient[] add1, Ingredient[] add2)
        {
            GreenChickenBowl b1 = new GreenChickenBowl() { SalsaType = salsa1 };
            GreenChickenBowl b2 = new GreenChickenBowl() { SalsaType = salsa2 };

            //removing since everything is on by default
            foreach (Ingredient i in add1)
            {
                b1.EditOrderRemove(i);
            }
            foreach (Ingredient i in add2)
            {
                b2.EditOrderRemove(i);
            }

            Assert.False(b1.Equals(b2));
        }
    }
}
