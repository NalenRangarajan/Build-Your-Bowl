using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the ChickenFajitaNachos class
    /// </summary>
    public class ChickenFajitaNachosUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void ChickenFajitaNachosDefault()
        {
            ChickenFajitaNachos n = new ChickenFajitaNachos();
            n.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;

            Assert.True(n.AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!) && i.Included);
            Assert.True(n.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!) && i.Included);
            Assert.True(n.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!) && i.Included);
            Assert.Equal(Salsa.Medium, n.SalsaType);
            Assert.True(n.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && !i.Included);
            Assert.True(n.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!) && i.Included);
            Assert.Equal(10.99m, n.Price);
            Assert.Equal((uint)650, n.Calories);

            string[] expectedOrderOutput = new string[] { };

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, n.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, n.PreparationInformation.Count());

            Assert.Equal("Chicken Fajita Nachos", n.ToString());
        }

        //Specific Required Test Case
        /// <summary>
        /// This tests the calories, price, and expected order output of a specific Chicken Fajita Nachos order
        /// </summary>
        [Fact]
        public void SpecificChickenFajitaNachos()
        {
            ChickenFajitaNachos n = new ChickenFajitaNachos();
            n.BaseAdditionalIngredientsIncludedAndDefault();
            
            IngredientItem i;
            n.AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!);
            i.Included = false;
            n.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = false;
            n.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = true;
            n.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!);
            i.Included = false;
            n.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = false;

            n.SalsaType = Salsa.Hot;

            string[] expectedOrderOutput = new string[] { "Hold Chicken", "Hold Veggies", "Swap Hot Salsa", "Hold Sour Cream" };

            Assert.Equal((uint)380, n.Calories);
            Assert.Equal(10.99m, n.Price);

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, n.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, n.PreparationInformation.Count());
        }


        /// <summary>
        /// This tests that the nachos have the correct number of calories with different ingredients included or excluded
        /// </summary>
        /// <param name="hasChicken">Whether the nachos should have chicken</param>
        /// <param name="hasVeggies">Whether the nachos should have veggies</param>
        /// <param name="hasQueso">Whether the nachos should have queso</param>
        /// <param name="typeOfSalsa">What type of salsa the nachos should have</param>
        /// <param name="hasGuacamole">Whether the nachos should have guacamole</param>
        /// <param name="hasSourCream">Whether the nachos should have sour cream</param>
        /// <param name="expectedCalories">The expected number of calories in the nachos</param>
        [Theory]
        [InlineData(false, false, false, Salsa.None, false, false, 250)]
        [InlineData(true, true, true, Salsa.Hot, true, true, 800)]
        [InlineData(false, true, false, Salsa.None, true, false, 420)]
        [InlineData(true, false, true, Salsa.Green, false, true, 630)]
        [InlineData(true, true, true, Salsa.None, false, false, 530)]
        [InlineData(false, false, false, Salsa.Mild, true, true, 520)]
        [InlineData(true, true, false, Salsa.Hot, true, false, 590)]
        [InlineData(false, false, true, Salsa.None, false, true, 460)]
        [InlineData(false, true, false, Salsa.None, true, true, 520)]
        public void ClassicNachosCaloriesWithDifferentIngredients(bool hasChicken, bool hasVeggies, bool hasQueso, Salsa typeOfSalsa, bool hasGuacamole, bool hasSourCream, uint expectedCalories)
        {
            ChickenFajitaNachos n = new ChickenFajitaNachos();
            n.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;
            n.AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!);
            i.Included = hasChicken;
            n.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = hasVeggies;
            n.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = hasQueso;
            n.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!);
            i.Included = hasSourCream;
            n.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = hasGuacamole;

            n.SalsaType = typeOfSalsa;

            Assert.Equal(expectedCalories, n.Calories);
        }

        /// <summary>
        /// This tests that the nachos have the order output with different ingredients included or excluded
        /// </summary>
        /// <param name="hasChicken">Whether the nachos should have chicken</param>
        /// <param name="hasVeggies">Whether the nachos should have veggies</param>
        /// <param name="hasQueso">Whether the nachos should have queso</param>
        /// <param name="typeOfSalsa">What type of salsa the nachos should have</param>
        /// <param name="hasGuacamole">Whether the nachos should have guacamole</param>
        /// <param name="hasSourCream">Whether the nachos should have sour cream</param>
        /// <param name="expectedOrderOutput">The expected order output from the nachos</param>
        [Theory]
        [InlineData(false, false, false, Salsa.None, false, false, new string[] { "Hold Chicken", "Hold Veggies", "Hold Queso", "Hold Salsa", "Hold Sour Cream"})]
        [InlineData(true, true, true, Salsa.Hot, true, true, new string[] { "Swap Hot Salsa", "Add Guacamole" })]
        [InlineData(false, true, false, Salsa.None, true, false, new string[] { "Hold Chicken", "Hold Queso", "Hold Salsa", "Add Guacamole", "Hold Sour Cream" })]
        [InlineData(true, false, true, Salsa.Green, false, true, new string[] { "Hold Veggies", "Swap Green Salsa"})]
        [InlineData(true, true, true, Salsa.None, false, false, new string[] { "Hold Salsa", "Hold Sour Cream" })]
        [InlineData(false, false, false, Salsa.Mild, true, true, new string[] { "Hold Chicken", "Hold Veggies", "Hold Queso", "Swap Mild Salsa", "Add Guacamole" })]
        [InlineData(true, true, false, Salsa.Hot, true, false, new string[] { "Hold Queso", "Swap Hot Salsa", "Add Guacamole", "Hold Sour Cream" })]
        [InlineData(false, false, true, Salsa.None, false, true, new string[] { "Hold Chicken", "Hold Veggies", "Hold Salsa" })]
        [InlineData(false, true, false, Salsa.None, true, true, new string[] { "Hold Chicken", "Hold Queso", "Hold Salsa", "Add Guacamole" })]
        public void ClassicNachosPreperationInformationWithDifferentIngredients(bool hasChicken, bool hasVeggies, bool hasQueso, Salsa typeOfSalsa, bool hasGuacamole, bool hasSourCream, string[] expectedOrderOutput)
        {
            ChickenFajitaNachos n = new ChickenFajitaNachos();
            n.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;
            n.AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!);
            i.Included = hasChicken;
            n.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = hasVeggies;
            n.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = hasQueso;
            n.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!);
            i.Included = hasSourCream;
            n.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = hasGuacamole;

            n.SalsaType = typeOfSalsa;

            //Checks that each expected string is in the actual preparation info
            Assert.All(expectedOrderOutput, word => Assert.Contains(word, n.PreparationInformation));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(expectedOrderOutput.Length, n.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that ChickenFajitaNachos can be cast as an IMenuItem, Nachos, Entree, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            ChickenFajitaNachos c = new ChickenFajitaNachos();

            Assert.IsAssignableFrom<IMenuItem>(c);
            Assert.IsAssignableFrom<Nachos>(c);
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
        [InlineData(Ingredient.SourCream)]
        [InlineData(Ingredient.Veggies)]
        public void IngredientsIncludedByDefault(Ingredient ingredient)
        {
            ChickenFajitaNachos c = new ChickenFajitaNachos();
            IngredientItem i;

            c.BaseAdditionalIngredientsIncludedAndDefault();

            //The ingredient is included by default
            Assert.True(c.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);
        }

        /// <summary>
        /// Tests that AdditionalIngredients can be edited and an ingredient can be included
        /// </summary>
        /// <param name="ingredient">The ingredient to include in the bowl</param>
        [Theory]
        [InlineData(Ingredient.Guacamole)]
        public void CanIncludeIngredients(Ingredient ingredient)
        {
            ChickenFajitaNachos c = new ChickenFajitaNachos();
            IngredientItem i;

            c.BaseAdditionalIngredientsIncludedAndDefault();

            //The ingredient isn't included
            Assert.False(c.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);

            c.EditOrderAdd(ingredient);

            //The ingredient is included
            Assert.True(c.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);
        }

        /// <summary>
        /// Tests that certain ingredients can't be added to AdditionalIngredients
        /// </summary>
        /// <param name="ingredient">The ingredient to include in the bowl</param>
        [Theory]
        [InlineData(Ingredient.Carnitas)]
        [InlineData(Ingredient.Steak)]
        [InlineData(Ingredient.BlackBeans)]
        [InlineData(Ingredient.PintoBeans)]
        [InlineData(Ingredient.Rice)]
        [InlineData(Ingredient.Chips)]
        public void CantIncludeCertainIngredients(Ingredient ingredient)
        {
            ChickenFajitaNachos c = new ChickenFajitaNachos();
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
        [InlineData(Ingredient.Veggies, "Calories")]
        [InlineData(Ingredient.Guacamole, "Calories")]
        [InlineData(Ingredient.SourCream, "Calories")]
        [InlineData(Ingredient.Chicken, "Price")]
        [InlineData(Ingredient.Queso, "Price")]
        [InlineData(Ingredient.Veggies, "Price")]
        [InlineData(Ingredient.Guacamole, "Price")]
        [InlineData(Ingredient.SourCream, "Price")]
        [InlineData(Ingredient.Chicken, "PreparationInformation")]
        [InlineData(Ingredient.Queso, "PreparationInformation")]
        [InlineData(Ingredient.Veggies, "PreparationInformation")]
        [InlineData(Ingredient.Guacamole, "PreparationInformation")]
        [InlineData(Ingredient.SourCream, "PreparationInformation")]
        public void UpdatingIngredientShouldNotifyOfPropertyChanges(Ingredient ingredient, string propertyName)
        {
            ChickenFajitaNachos c = new ChickenFajitaNachos();

            Assert.PropertyChanged(c, propertyName, () => {
                c.EditOrderAdd(ingredient);
            });

            Assert.PropertyChanged(c, propertyName, () => {
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
            ChickenFajitaNachos c = new ChickenFajitaNachos();

            Assert.PropertyChanged(c, propertyName, () => {
                c.SalsaType = salsa;
            });
        }

        /// <summary>
        /// Tests that two ChickenFajitaNachos with identical properties are equal to each other
        /// </summary>
        /// <param name="salsa1">The salsa for the first nachos</param>
        /// <param name="salsa2">The salsa for the second nachos</param>
        /// <param name="add1">The additional ingredients for the first nachos</param>
        /// <param name="add2">The additional ingredients for the second nachos</param>
        [Theory]
        [InlineData(Salsa.Mild, Salsa.Mild, new Ingredient[] { Ingredient.Queso, Ingredient.Chicken }, new Ingredient[] { Ingredient.Queso, Ingredient.Chicken })]
        [InlineData(Salsa.Medium, Salsa.Medium, new Ingredient[] { Ingredient.SourCream, Ingredient.Veggies }, new Ingredient[] { Ingredient.SourCream, Ingredient.Veggies })]
        [InlineData(Salsa.Hot, Salsa.Hot, new Ingredient[] { Ingredient.Guacamole }, new Ingredient[] { Ingredient.Guacamole })]
        [InlineData(Salsa.Green, Salsa.Green, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken })]
        [InlineData(Salsa.None, Salsa.None, new Ingredient[] { Ingredient.Veggies, Ingredient.Chicken }, new Ingredient[] { Ingredient.Veggies, Ingredient.Chicken })]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(Salsa salsa1, Salsa salsa2, Ingredient[] add1, Ingredient[] add2)
        {
            ChickenFajitaNachos n1 = new ChickenFajitaNachos() { SalsaType = salsa1 };
            ChickenFajitaNachos n2 = new ChickenFajitaNachos() { SalsaType = salsa2 };

            foreach (Ingredient i in add1)
            {
                n1.EditOrderAdd(i);
            }
            foreach (Ingredient i in add2)
            {
                n2.EditOrderAdd(i);
            }

            Assert.True(n1.Equals(n2));
        }

        /// <summary>
        /// Tests that two ChickenFajitaNachos with different properties are not equal to each other
        /// </summary>
        /// <param name="salsa1">The salsa for the first nachos</param>
        /// <param name="salsa2">The salsa for the second nachos</param>
        /// <param name="add1">The additional ingredients for the first nachos</param>
        /// <param name="add2">The additional ingredients for the second nachos</param>
        [Theory]
        [InlineData(Salsa.Mild, Salsa.Mild, new Ingredient[] { Ingredient.Queso, Ingredient.Chicken }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken })]
        [InlineData(Salsa.Medium, Salsa.Mild, new Ingredient[] { Ingredient.Queso, Ingredient.Veggies }, new Ingredient[] { Ingredient.SourCream, Ingredient.Veggies })]
        [InlineData(Salsa.Mild, Salsa.Hot, new Ingredient[] { Ingredient.Guacamole }, new Ingredient[] { Ingredient.SourCream })]
        [InlineData(Salsa.Green, Salsa.Mild, new Ingredient[] { Ingredient.Guacamole, Ingredient.Veggies }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Veggies })]
        [InlineData(Salsa.None, Salsa.None, new Ingredient[] { Ingredient.SourCream, Ingredient.Chicken }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken })]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(Salsa salsa1, Salsa salsa2, Ingredient[] add1, Ingredient[] add2)
        {
            ChickenFajitaNachos n1 = new ChickenFajitaNachos() { SalsaType = salsa1 };
            ChickenFajitaNachos n2 = new ChickenFajitaNachos() { SalsaType = salsa2 };

            foreach (Ingredient i in add1)
            {
                n1.EditOrderAdd(i);
            }
            foreach (Ingredient i in add2)
            {
                n2.EditOrderAdd(i);
            }

            Assert.False(n1.Equals(n2));
        }
    }
}
