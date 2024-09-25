using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the SpicySteakBowl class
    /// </summary>
    public class SpicySteakBowlUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void SpicySteakBowlDefault()
        {
            SpicySteakBowl s = new SpicySteakBowl();
            s.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;

            Assert.True(s.AdditionalIngredients.TryGetValue(Ingredient.Steak, out i!) && i.Included);
            Assert.True(s.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!) && !i.Included);
            Assert.True(s.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!) && i.Included);
            Assert.Equal(Salsa.Hot, s.SalsaType);
            Assert.True(s.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && !i.Included);
            Assert.True(s.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!) && i.Included);
            Assert.Equal(10.99m, s.Price);
            Assert.Equal((uint)620, s.Calories);

            string[] expectedOrderOutput = new string[] { };

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, s.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, s.PreparationInformation.Count());

            Assert.Equal("Spicy Steak Bowl", s.ToString());
        }

        /// <summary>
        /// This tests that the price after adding Guacamole is correct
        /// </summary>
        [Fact]
        public void PriceIncreaseWithGuacamole()
        {
            SpicySteakBowl s = new SpicySteakBowl();
            s.BaseAdditionalIngredientsIncludedAndDefault();

            IngredientItem i;
            s.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = true;

            Assert.Equal(11.99m, s.Price);
        }

        /// <summary>
        /// This tests that the bowl has the correct number of calories with different ingredients included or excluded
        /// </summary>
        /// <param name="hasSteak">Whether the bowl should have steak</param>
        /// <param name="hasVeggies">Whether the bowl should have veggies</param>
        /// <param name="hasQueso">Whether the bowl should have queso</param>
        /// <param name="typeOfSalsa">What type of salsa the bowl should have</param>
        /// <param name="hasGuacamole">Whether the bowl should have guacamole</param>
        /// <param name="hasSourCream">Whether the bowl should have sour cream</param>
        /// <param name="expectedCalories">The expected number of calories in the bowl</param>
        [Theory]
        [InlineData(false, false, false, Salsa.None, false, false, 210)]
        [InlineData(true, true, true, Salsa.Hot, true, true, 790)]
        [InlineData(false, true, false, Salsa.None, true, false, 380)]
        [InlineData(true, false, true, Salsa.Green, false, true, 620)]
        [InlineData(true, true, true, Salsa.None, false, false, 520)]
        [InlineData(false, false, false, Salsa.Mild, true, true, 480)]
        [InlineData(true, true, false, Salsa.Hot, true, false, 580)]
        [InlineData(false, false, true, Salsa.None, false, true, 420)]
        [InlineData(false, true, false, Salsa.None, true, true, 480)]
        public void SpicySteakBowlCaloriesWithDifferentIngredients(bool hasSteak, bool hasVeggies, bool hasQueso, Salsa typeOfSalsa, bool hasGuacamole, bool hasSourCream, uint expectedCalories)
        {
            SpicySteakBowl s = new SpicySteakBowl();
            s.BaseAdditionalIngredientsIncludedAndDefault();

            IngredientItem i;
            s.AdditionalIngredients.TryGetValue(Ingredient.Steak, out i!);
            i.Included = hasSteak;
            s.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = hasVeggies;
            s.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = hasQueso;
            s.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!);
            i.Included = hasSourCream;
            s.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = hasGuacamole;

            s.SalsaType = typeOfSalsa;

            Assert.Equal(expectedCalories, s.Calories);
        }


        /// <summary>
        /// This tests that the bowl has the order output with different ingredients included or excluded
        /// </summary>
        /// <param name="hasSteak">Whether the bowl should have steak</param>
        /// <param name="hasVeggies">Whether the bowl should have veggies</param>
        /// <param name="hasQueso">Whether the bowl should have queso</param>
        /// <param name="typeOfSalsa">What type of salsa the bowl should have</param>
        /// <param name="hasGuacamole">Whether the bowl should have guacamole</param>
        /// <param name="hasSourCream">Whether the bowl should have sour cream</param>
        /// <param name="expectedOrderOutput">The expected order output from the bowl</param>
        [Theory]
        [InlineData(false, false, false, Salsa.None, false, false, new string[] { "Hold Steak", "Hold Queso", "Hold Salsa", "Hold Sour Cream" })]
        [InlineData(true, true, true, Salsa.Hot, true, true, new string[] { "Add Veggies", "Add Guacamole" })]
        [InlineData(false, true, false, Salsa.None, true, false, new string[] { "Hold Steak", "Add Veggies", "Hold Queso", "Hold Salsa", "Add Guacamole", "Hold Sour Cream" })]
        [InlineData(true, false, true, Salsa.Green, false, true, new string[] { "Swap Green Salsa" })]
        [InlineData(true, true, true, Salsa.None, false, false, new string[] { "Add Veggies", "Hold Salsa", "Hold Sour Cream" })]
        [InlineData(false, false, false, Salsa.Mild, true, true, new string[] { "Hold Steak", "Hold Queso", "Swap Mild Salsa", "Add Guacamole" })]
        [InlineData(true, true, false, Salsa.Hot, true, false, new string[] { "Add Veggies", "Hold Queso", "Add Guacamole", "Hold Sour Cream" })]
        [InlineData(false, false, true, Salsa.None, false, true, new string[] { "Hold Steak", "Hold Salsa" })]
        [InlineData(false, true, false, Salsa.None, true, true, new string[] { "Hold Steak", "Add Veggies", "Hold Queso", "Hold Salsa", "Add Guacamole"})]
        public void SpicySteakBowlPreperationInformationWithDifferentIngredients(bool hasSteak, bool hasVeggies, bool hasQueso, Salsa typeOfSalsa, bool hasGuacamole, bool hasSourCream, string[] expectedOrderOutput)
        {
            SpicySteakBowl s = new SpicySteakBowl();
            s.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;
            s.AdditionalIngredients.TryGetValue(Ingredient.Steak, out i!);
            i.Included = hasSteak;
            s.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = hasVeggies;
            s.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = hasQueso;
            s.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!);
            i.Included = hasSourCream;
            s.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = hasGuacamole;

            s.SalsaType = typeOfSalsa;

            //Checks that each expected string is in the actual preparation info
            Assert.All(expectedOrderOutput, word => Assert.Contains(word, s.PreparationInformation));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(expectedOrderOutput.Length, s.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that SpicySteakBowl can be cast as an IMenuItem, Bowl, Entree, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            SpicySteakBowl s = new SpicySteakBowl();

            Assert.IsAssignableFrom<IMenuItem>(s);
            Assert.IsAssignableFrom<Bowl>(s);
            Assert.IsAssignableFrom<Entree>(s);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(s);
        }

        /// <summary>
        /// Tests that certain ingredients are included by default
        /// </summary>
        /// <param name="ingredient">The ingredient to check</param>
        [Theory]
        [InlineData(Ingredient.Steak)]
        [InlineData(Ingredient.Queso)]
        [InlineData(Ingredient.SourCream)]
        public void IngredientsIncludedByDefault(Ingredient ingredient)
        {
            SpicySteakBowl s = new SpicySteakBowl();
            IngredientItem i;

            s.BaseAdditionalIngredientsIncludedAndDefault();

            //The ingredient is included by default
            Assert.True(s.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);
        }

        /// <summary>
        /// Tests that AdditionalIngredients can be edited and an ingredient can be included
        /// </summary>
        /// <param name="ingredient">The ingredient to include in the bowl</param>
        [Theory]
        [InlineData(Ingredient.Guacamole)]
        [InlineData(Ingredient.Veggies)]
        public void CanIncludeIngredients(Ingredient ingredient)
        {
            SpicySteakBowl s = new SpicySteakBowl();
            IngredientItem i;

            s.BaseAdditionalIngredientsIncludedAndDefault();

            //The ingredient isn't included
            Assert.False(s.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);

            s.EditOrderAdd(ingredient);

            //The ingredient is included
            Assert.True(s.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);
        }

        /// <summary>
        /// Tests that certain ingredients can't be added to AdditionalIngredients
        /// </summary>
        /// <param name="ingredient">The ingredient to include in the bowl</param>
        [Theory]
        [InlineData(Ingredient.Chicken)]
        [InlineData(Ingredient.Carnitas)]
        [InlineData(Ingredient.PintoBeans)]
        [InlineData(Ingredient.BlackBeans)]
        [InlineData(Ingredient.Rice)]
        [InlineData(Ingredient.Chips)]
        public void CantIncludeCertainIngredients(Ingredient ingredient)
        {
            SpicySteakBowl s = new SpicySteakBowl();
            s.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;

            Assert.False(s.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);

            Action action = () => s.EditOrderAdd(ingredient);

            Assert.Throws<NullReferenceException>(action);

            Assert.False(s.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when adding and removing ingredients
        /// </summary>
        /// <param name="ingredient">The ingredient to add/remove</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(Ingredient.Steak, "Calories")]
        [InlineData(Ingredient.Queso, "Calories")]
        [InlineData(Ingredient.SourCream, "Calories")]
        [InlineData(Ingredient.Veggies, "Calories")]
        [InlineData(Ingredient.Guacamole, "Calories")]
        [InlineData(Ingredient.Steak, "Price")]
        [InlineData(Ingredient.Queso, "Price")]
        [InlineData(Ingredient.SourCream, "Price")]
        [InlineData(Ingredient.Veggies, "Price")]
        [InlineData(Ingredient.Guacamole, "Price")]
        [InlineData(Ingredient.Steak, "PreparationInformation")]
        [InlineData(Ingredient.Queso, "PreparationInformation")]
        [InlineData(Ingredient.SourCream, "PreparationInformation")]
        [InlineData(Ingredient.Veggies, "PreparationInformation")]
        [InlineData(Ingredient.Guacamole, "PreparationInformation")]
        public void UpdatingIngredientShouldNotifyOfPropertyChanges(Ingredient ingredient, string propertyName)
        {
            SpicySteakBowl s = new SpicySteakBowl();

            Assert.PropertyChanged(s, propertyName, () => {
                s.EditOrderAdd(ingredient);
            });

            Assert.PropertyChanged(s, propertyName, () => {
                s.EditOrderRemove(ingredient);
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
            SpicySteakBowl s = new SpicySteakBowl();

            Assert.PropertyChanged(s, propertyName, () => {
                s.SalsaType = salsa;
            });
        }

        /// <summary>
        /// Tests that two SpicySteakBowls with identical properties are equal to each other
        /// </summary>
        /// <param name="salsa1">The salsa for the first bowl</param>
        /// <param name="salsa2">The salsa for the second bowl</param>
        /// <param name="add1">The additional ingredients for the first bowl</param>
        /// <param name="add2">The additional ingredients for the second bowl</param>
        [Theory]
        [InlineData(Salsa.Mild, Salsa.Mild, new Ingredient[] { Ingredient.Steak, Ingredient.Veggies }, new Ingredient[] { Ingredient.Steak, Ingredient.Veggies })]
        [InlineData(Salsa.Medium, Salsa.Medium, new Ingredient[] { Ingredient.Guacamole, Ingredient.Steak }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Steak })]
        [InlineData(Salsa.Hot, Salsa.Hot, new Ingredient[] { Ingredient.SourCream }, new Ingredient[] { Ingredient.SourCream })]
        [InlineData(Salsa.Green, Salsa.Green, new Ingredient[] { Ingredient.Guacamole, Ingredient.Veggies }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Veggies })]
        [InlineData(Salsa.None, Salsa.None, new Ingredient[] { Ingredient.Veggies, Ingredient.SourCream }, new Ingredient[] { Ingredient.Veggies, Ingredient.SourCream })]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(Salsa salsa1, Salsa salsa2, Ingredient[] add1, Ingredient[] add2)
        {
            SpicySteakBowl b1 = new SpicySteakBowl() { SalsaType = salsa1 };
            SpicySteakBowl b2 = new SpicySteakBowl() { SalsaType = salsa2 };

            foreach (Ingredient i in add1)
            {
                b1.EditOrderAdd(i);
            }
            foreach (Ingredient i in add2)
            {
                b2.EditOrderAdd(i);
            }

            Assert.True(b1.Equals(b2));
        }

        /// <summary>
        /// Tests that two SpicySteakBowls with different properties are equal to each other
        /// </summary>
        /// <param name="salsa1">The salsa for the first bowl</param>
        /// <param name="salsa2">The salsa for the second bowl</param>
        /// <param name="add1">The additional ingredients for the first bowl</param>
        /// <param name="add2">The additional ingredients for the second bowl</param>
        [Theory]
        [InlineData(Salsa.Mild, Salsa.Mild, new Ingredient[] { Ingredient.Steak, Ingredient.Guacamole }, new Ingredient[] { Ingredient.Steak, Ingredient.Veggies })]
        [InlineData(Salsa.Medium, Salsa.None, new Ingredient[] { Ingredient.Guacamole, Ingredient.Steak }, new Ingredient[] { Ingredient.Veggies, Ingredient.Steak })]
        [InlineData(Salsa.Hot, Salsa.Medium, new Ingredient[] { Ingredient.SourCream }, new Ingredient[] { Ingredient.Veggies })]
        [InlineData(Salsa.Green, Salsa.Mild, new Ingredient[] { Ingredient.Steak, Ingredient.Veggies }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Veggies })]
        [InlineData(Salsa.None, Salsa.Medium, new Ingredient[] { Ingredient.Veggies, Ingredient.SourCream }, new Ingredient[] { Ingredient.Veggies, Ingredient.SourCream })]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(Salsa salsa1, Salsa salsa2, Ingredient[] add1, Ingredient[] add2)
        {
            SpicySteakBowl b1 = new SpicySteakBowl() { SalsaType = salsa1 };
            SpicySteakBowl b2 = new SpicySteakBowl() { SalsaType = salsa2 };

            foreach (Ingredient i in add1)
            {
                b1.EditOrderAdd(i);
            }
            foreach (Ingredient i in add2)
            {
                b2.EditOrderAdd(i);
            }

            Assert.False(b1.Equals(b2));
        }
    }
}
