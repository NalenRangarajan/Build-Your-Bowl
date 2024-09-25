using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the ClassicNachos class
    /// </summary>
    public class ClassicNachosUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void ClassicNachosDefault()
        {
            ClassicNachos n = new ClassicNachos();
            n.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;

            Assert.True(n.AdditionalIngredients.TryGetValue(Ingredient.Steak, out i!) && i.Included);
            Assert.True(n.AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!) && i.Included);
            Assert.True(n.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!) && i.Included);
            Assert.Equal(Salsa.Medium, n.SalsaType);
            Assert.True(n.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && !i.Included);
            Assert.True(n.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!) && !i.Included);
            Assert.Equal(12.99m, n.Price);
            Assert.Equal((uint)710, n.Calories);
            string[] expectedOrderOutput = new string[] { };

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, n.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, n.PreparationInformation.Count());

            Assert.Equal("Classic Nachos", n.ToString());
        }

        /// <summary>
        /// This tests that the price after adding Guacamole is correct
        /// </summary>
        [Fact]
        public void PriceIncreaseWithGuacamole()
        {
            ClassicNachos n = new ClassicNachos();
            n.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;
            n.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = true;

            Assert.Equal(13.99m, n.Price);
        }

        /// <summary>
        /// This tests that the nachos have the correct number of calories with different ingredients included or excluded
        /// </summary>
        /// <param name="hasSteak">Whether the nachos should have steak</param>
        /// <param name="hasChicken">Whether the nachos should have chicken</param>
        /// <param name="hasQueso">Whether the nachos should have queso</param>
        /// <param name="typeOfSalsa">What type of salsa the nachos should have</param>
        /// <param name="hasGuacamole">Whether the nachos should have guacamole</param>
        /// <param name="hasSourCream">Whether the nachos should have sour cream</param>
        /// <param name="expectedCalories">The expected number of calories in the nachos</param>
        [Theory]
        [InlineData(false, false, false, Salsa.None, false, false, 250)]
        [InlineData(true, true, true, Salsa.Hot, true, true, 960)]
        [InlineData(false, true, false, Salsa.None, true, false, 550)]
        [InlineData(true, false, true, Salsa.Green, false, true, 660)]
        [InlineData(true, true, true, Salsa.None, false, false, 690)]
        [InlineData(false, false, false, Salsa.Mild, true, true, 520)]
        [InlineData(true, true, false, Salsa.Hot, true, false, 750)]
        [InlineData(false, false, true, Salsa.None, false, true, 460)]
        [InlineData(false, true, false, Salsa.None, true, true, 650)]
        public void ClassicNachosCaloriesWithDifferentIngredients(bool hasSteak, bool hasChicken, bool hasQueso, Salsa typeOfSalsa, bool hasGuacamole, bool hasSourCream, uint expectedCalories)
        {
            ClassicNachos n = new ClassicNachos();
            n.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;
            n.AdditionalIngredients.TryGetValue(Ingredient.Steak, out i!);
            i.Included = hasSteak;
            n.AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!);
            i.Included = hasChicken;
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
        /// <param name="hasSteak">Whether the nachos should have steak</param>
        /// <param name="hasChicken">Whether the nachos should have chicken</param>
        /// <param name="hasQueso">Whether the nachos should have queso</param>
        /// <param name="typeOfSalsa">What type of salsa the nachos should have</param>
        /// <param name="hasGuacamole">Whether the nachos should have guacamole</param>
        /// <param name="hasSourCream">Whether the nachos should have sour cream</param>
        /// <param name="expectedOrderOutput">The expected order output from the nachos</param>
        [Theory]
        [InlineData(false, false, false, Salsa.None, false, false, new string[] { "Hold Steak", "Hold Chicken", "Hold Queso", "Hold Salsa" })]
        [InlineData(true, true, true, Salsa.Hot, true, true, new string[] { "Swap Hot Salsa", "Add Guacamole", "Add Sour Cream" })]
        [InlineData(false, true, false, Salsa.None, true, false, new string[] { "Hold Steak", "Hold Queso", "Hold Salsa", "Add Guacamole" })]
        [InlineData(true, false, true, Salsa.Green, false, true, new string[] { "Hold Chicken", "Swap Green Salsa", "Add Sour Cream" })]
        [InlineData(true, true, true, Salsa.None, false, false, new string[] { "Hold Salsa" })]
        [InlineData(false, false, false, Salsa.Mild, true, true, new string[] { "Hold Steak", "Hold Chicken", "Hold Queso", "Swap Mild Salsa", "Add Guacamole", "Add Sour Cream" })]
        [InlineData(true, true, false, Salsa.Hot, true, false, new string[] { "Hold Queso", "Swap Hot Salsa", "Add Guacamole" })]
        [InlineData(false, false, true, Salsa.None, false, true, new string[] { "Hold Steak", "Hold Chicken", "Hold Salsa", "Add Sour Cream" })]
        [InlineData(false, true, false, Salsa.None, true, true, new string[] { "Hold Steak", "Hold Queso", "Hold Salsa", "Add Guacamole", "Add Sour Cream" })]
        public void ClassicNachosPreperationInformationWithDifferentIngredients(bool hasSteak, bool hasChicken, bool hasQueso, Salsa typeOfSalsa, bool hasGuacamole, bool hasSourCream, string[] expectedOrderOutput)
        {
            ClassicNachos n = new ClassicNachos();
            n.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;
            n.AdditionalIngredients.TryGetValue(Ingredient.Steak, out i!);
            i.Included = hasSteak;
            n.AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!);
            i.Included = hasChicken;
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
        /// Tests that ClassicNachos can be cast as an IMenuItem, Nachos, Entree, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            ClassicNachos c = new ClassicNachos();

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
        [InlineData(Ingredient.Steak)]
        public void IngredientsIncludedByDefault(Ingredient ingredient)
        {
            ClassicNachos c = new ClassicNachos();
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
        [InlineData(Ingredient.SourCream)]
        public void CanIncludeIngredients(Ingredient ingredient)
        {
            ClassicNachos c = new ClassicNachos();
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
        [InlineData(Ingredient.BlackBeans)]
        [InlineData(Ingredient.PintoBeans)]
        [InlineData(Ingredient.Rice)]
        [InlineData(Ingredient.Chips)]
        [InlineData(Ingredient.Veggies)]
        public void CantIncludeCertainIngredients(Ingredient ingredient)
        {
            ClassicNachos c = new ClassicNachos();
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
        [InlineData(Ingredient.Steak, "Calories")]
        [InlineData(Ingredient.Guacamole, "Calories")]
        [InlineData(Ingredient.SourCream, "Calories")]
        [InlineData(Ingredient.Chicken, "Price")]
        [InlineData(Ingredient.Queso, "Price")]
        [InlineData(Ingredient.Steak, "Price")]
        [InlineData(Ingredient.Guacamole, "Price")]
        [InlineData(Ingredient.SourCream, "Price")]
        [InlineData(Ingredient.Chicken, "PreparationInformation")]
        [InlineData(Ingredient.Queso, "PreparationInformation")]
        [InlineData(Ingredient.Steak, "PreparationInformation")]
        [InlineData(Ingredient.Guacamole, "PreparationInformation")]
        [InlineData(Ingredient.SourCream, "PreparationInformation")]
        public void UpdatingIngredientShouldNotifyOfPropertyChanges(Ingredient ingredient, string propertyName)
        {
            ClassicNachos c = new ClassicNachos();

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
            ClassicNachos c = new ClassicNachos();

            Assert.PropertyChanged(c, propertyName, () => {
                c.SalsaType = salsa;
            });
        }

        /// <summary>
        /// Tests that two ClassicNachos with identical properties are equal to each other
        /// </summary>
        /// <param name="salsa1">The salsa for the first nachos</param>
        /// <param name="salsa2">The salsa for the second nachos</param>
        /// <param name="add1">The additional ingredients for the first nachos</param>
        /// <param name="add2">The additional ingredients for the second nachos</param>
        [Theory]
        [InlineData(Salsa.Mild, Salsa.Mild, new Ingredient[] { Ingredient.Queso, Ingredient.Chicken }, new Ingredient[] { Ingredient.Queso, Ingredient.Chicken })]
        [InlineData(Salsa.Medium, Salsa.Medium, new Ingredient[] { Ingredient.SourCream, Ingredient.Steak }, new Ingredient[] { Ingredient.SourCream, Ingredient.Steak })]
        [InlineData(Salsa.Hot, Salsa.Hot, new Ingredient[] { Ingredient.Guacamole }, new Ingredient[] { Ingredient.Guacamole })]
        [InlineData(Salsa.Green, Salsa.Green, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken })]
        [InlineData(Salsa.None, Salsa.None, new Ingredient[] { Ingredient.SourCream, Ingredient.Chicken }, new Ingredient[] { Ingredient.SourCream, Ingredient.Chicken })]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(Salsa salsa1, Salsa salsa2, Ingredient[] add1, Ingredient[] add2)
        {
            ClassicNachos n1 = new ClassicNachos() { SalsaType = salsa1 };
            ClassicNachos n2 = new ClassicNachos() { SalsaType = salsa2 };

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
        /// Tests that two ClassicNachos with different properties are not equal to each other
        /// </summary>
        /// <param name="salsa1">The salsa for the first nachos</param>
        /// <param name="salsa2">The salsa for the second nachos</param>
        /// <param name="add1">The additional ingredients for the first nachos</param>
        /// <param name="add2">The additional ingredients for the second nachos</param>
        [Theory]
        [InlineData(Salsa.Mild, Salsa.Medium, new Ingredient[] { Ingredient.Queso, Ingredient.Chicken }, new Ingredient[] { Ingredient.Queso, Ingredient.Chicken })]
        [InlineData(Salsa.Medium, Salsa.Mild, new Ingredient[] { Ingredient.Chicken, Ingredient.Steak }, new Ingredient[] { Ingredient.SourCream, Ingredient.Steak })]
        [InlineData(Salsa.Hot, Salsa.Green, new Ingredient[] { Ingredient.Chicken }, new Ingredient[] { Ingredient.Guacamole })]
        [InlineData(Salsa.Medium, Salsa.Green, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken }, new Ingredient[] { Ingredient.SourCream, Ingredient.Chicken })]
        [InlineData(Salsa.None, Salsa.None, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken }, new Ingredient[] { Ingredient.SourCream, Ingredient.Chicken })]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(Salsa salsa1, Salsa salsa2, Ingredient[] add1, Ingredient[] add2)
        {
            ClassicNachos n1 = new ClassicNachos() { SalsaType = salsa1 };
            ClassicNachos n2 = new ClassicNachos() { SalsaType = salsa2 };

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
