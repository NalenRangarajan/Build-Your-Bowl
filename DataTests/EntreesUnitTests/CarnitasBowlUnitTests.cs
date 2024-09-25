using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the CarnitasBowl class
    /// </summary>
    public class CarnitasBowlUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void CarnitasBowlDefault()
        {
            CarnitasBowl c = new CarnitasBowl();
            c.BaseAdditionalIngredientsIncludedAndDefault();

            IngredientItem i;

            Assert.True(c.AdditionalIngredients.TryGetValue(Ingredient.Carnitas, out i!) && i.Included);
            Assert.True(c.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!) && !i.Included);
            Assert.True(c.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!) && i.Included);
            Assert.True(c.AdditionalIngredients.TryGetValue(Ingredient.PintoBeans, out i!) && i.Included);
            Assert.Equal(Salsa.Medium, c.SalsaType);
            Assert.True(c.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && !i.Included);
            Assert.True(c.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!) && !i.Included);
            Assert.Equal(9.99m, c.Price);
            Assert.Equal((uint)680, c.Calories);

            string[] expectedOrderOutput = new string[] { };

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, c.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, c.PreparationInformation.Count());
            Assert.Equal("Carnitas Bowl", c.ToString());

        }

        /// <summary>
        /// This tests that the price after adding Guacamole is correct
        /// </summary>
        [Fact]
        public void PriceIncreaseWithGuacamole()
        {
            CarnitasBowl c = new CarnitasBowl();
            c.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;
            c.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = true;

            Assert.Equal(10.99m, c.Price);
        }

        //Specific Required Test Case
        /// <summary>
        /// This tests the calories, price, and expected order output of a specific Carnitas bowl
        /// </summary>
        [Fact]
        public void SpecificCarnitasBowl()
        {
            CarnitasBowl c = new CarnitasBowl();
            c.BaseAdditionalIngredientsIncludedAndDefault();

            IngredientItem i;
            c.AdditionalIngredients.TryGetValue(Ingredient.Carnitas, out i!);
            i.Included = false;
            c.AdditionalIngredients.TryGetValue(Ingredient.PintoBeans, out i!);
            i.Included = false;
            c.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = true;
            c.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = false;
            c.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!);
            i.Included = true;
            c.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!);
            i.Included = true;

            string[] expectedOrderOutput = new string[] { "Hold Carnitas", "Add Veggies", "Hold Queso", "Hold Pinto Beans", "Add Guacamole", "Add Sour Cream" };

            Assert.Equal((uint)500, c.Calories);
            Assert.Equal(10.99m, c.Price);

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, c.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, c.PreparationInformation.Count());
        }


        /// <summary>
        /// This tests that the bowl has the correct number of calories with different ingredients included or excluded
        /// </summary>
        /// <param name="hasCarnitas">Whether the bowl should have carnitas</param>
        /// <param name="hasVeggies">Whether the bowl should have veggies</param>
        /// <param name="hasQueso">Whether the bowl should have queso</param>
        /// <param name="hasPintoBeans">Whether the bowl should have pinto beans</param>
        /// <param name="typeOfSalsa">What type of salsa the bowl should have</param>
        /// <param name="hasGuacamole">Whether the bowl should have guacamole</param>
        /// <param name="hasSourCream">Whether the bowl should have sour cream</param>
        /// <param name="expectedCalories">The expected number of calories in the bowl</param>
        [Theory]
        [InlineData(false, false, false, false, Salsa.None, false, false, 210)]
        [InlineData(true, true, true, true, Salsa.Hot, true, true, 950)]
        [InlineData(false, true, false, true, Salsa.None, true, false, 510)]
        [InlineData(true, false, true, false, Salsa.Green, false, true, 650)]
        [InlineData(true, true, true, true, Salsa.None, false, false, 680)]
        [InlineData(false, false, false, false, Salsa.Mild, true, true, 480)]
        [InlineData(true, true, false, false, Salsa.Hot, true, false, 610)]
        [InlineData(false, false, true, true, Salsa.None, false, true, 550)]
        [InlineData(false, true, false, false, Salsa.None, true, true, 480)]
        public void CarnitasBowlCaloriesWithDifferentIngredients(bool hasCarnitas, bool hasVeggies, bool hasQueso, bool hasPintoBeans, Salsa typeOfSalsa, bool hasGuacamole, bool hasSourCream, uint expectedCalories)
        {
            CarnitasBowl c = new CarnitasBowl();
            c.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;
            c.AdditionalIngredients.TryGetValue(Ingredient.Carnitas, out i!);
            i.Included = hasCarnitas;
            c.AdditionalIngredients.TryGetValue(Ingredient.PintoBeans, out i!);
            i.Included = hasPintoBeans;
            c.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = hasVeggies;
            c.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = hasQueso;
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
        /// <param name="hasCarnitas">Whether the bowl should have carnitas</param>
        /// <param name="hasVeggies">Whether the bowl should have veggies</param>
        /// <param name="hasQueso">Whether the bowl should have queso</param>
        /// <param name="hasPintoBeans">Whether the bowl should have pinto beans</param>
        /// <param name="typeOfSalsa">What type of salsa the bowl should have</param>
        /// <param name="hasGuacamole">Whether the bowl should have guacamole</param>
        /// <param name="hasSourCream">Whether the bowl should have sour cream</param>
        /// <param name="expectedOrderOutput">The expected order output from the bowl</param>
        [Theory]
        [InlineData(false, false, false, false, Salsa.None, false, false, new string[] { "Hold Carnitas", "Hold Queso", "Hold Pinto Beans", "Hold Salsa"})]
        [InlineData(true, true, true, true, Salsa.Hot, true, true, new string[] { "Add Veggies", "Swap Hot Salsa", "Add Guacamole", "Add Sour Cream" })]
        [InlineData(false, true, false, true, Salsa.None, true, false, new string[] { "Hold Carnitas", "Add Veggies", "Hold Queso", "Hold Salsa", "Add Guacamole"})]
        [InlineData(true, false, true, false, Salsa.Green, false, true, new string[] { "Hold Pinto Beans", "Swap Green Salsa", "Add Sour Cream"})]
        [InlineData(true, true, true, true, Salsa.None, false, false, new string[] { "Add Veggies", "Hold Salsa" })]
        [InlineData(false, false, false, false, Salsa.Mild, true, true, new string[] { "Hold Carnitas", "Hold Queso", "Hold Pinto Beans", "Swap Mild Salsa", "Add Guacamole", "Add Sour Cream" })]
        [InlineData(true, true, false, false, Salsa.Hot, true, false, new string[] { "Add Veggies", "Hold Queso", "Hold Pinto Beans", "Swap Hot Salsa", "Add Guacamole", })]
        [InlineData(false, false, true, true, Salsa.None, false, true, new string[] { "Hold Carnitas", "Hold Salsa", "Add Sour Cream" })]
        [InlineData(false, true, false, false, Salsa.None, true, true, new string[] { "Hold Carnitas", "Add Veggies", "Hold Queso", "Hold Pinto Beans", "Hold Salsa", "Add Guacamole", "Add Sour Cream" })]
        public void CarnitasBowlPreperationInformationWithDifferentIngredients(bool hasCarnitas, bool hasVeggies, bool hasQueso, bool hasPintoBeans, Salsa typeOfSalsa, bool hasGuacamole, bool hasSourCream, string[] expectedOrderOutput)
        {
            CarnitasBowl c = new CarnitasBowl();
            c.BaseAdditionalIngredientsIncludedAndDefault();

            IngredientItem i;
            c.AdditionalIngredients.TryGetValue(Ingredient.Carnitas, out i!);
            i.Included = hasCarnitas;
            c.AdditionalIngredients.TryGetValue(Ingredient.PintoBeans, out i!);
            i.Included = hasPintoBeans;
            c.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!);
            i.Included = hasVeggies;
            c.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!);
            i.Included = hasQueso;
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
        /// Tests that CarnitasBowl can be cast as an IMenuItem, Bowl, Entree, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            CarnitasBowl c = new CarnitasBowl();

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
        [InlineData(Ingredient.Carnitas)]
        [InlineData(Ingredient.Queso)]
        [InlineData(Ingredient.PintoBeans)]
        public void IngredientsIncludedByDefault(Ingredient ingredient)
        {
            CarnitasBowl c = new CarnitasBowl();
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
        [InlineData(Ingredient.Veggies)]
        [InlineData(Ingredient.Guacamole)]
        [InlineData(Ingredient.SourCream)]
        public void CanIncludeIngredients(Ingredient ingredient)
        {
            CarnitasBowl c = new CarnitasBowl();
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
        [InlineData(Ingredient.Chicken)]
        [InlineData(Ingredient.Steak)]
        [InlineData(Ingredient.BlackBeans)]
        [InlineData(Ingredient.Rice)]
        [InlineData(Ingredient.Chips)]
        public void CantIncludeCertainIngredients(Ingredient ingredient)
        {
            CarnitasBowl c = new CarnitasBowl();
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
        [InlineData(Ingredient.Carnitas, "Calories")]
        [InlineData(Ingredient.Queso, "Calories")]
        [InlineData(Ingredient.PintoBeans, "Calories")]
        [InlineData(Ingredient.Veggies, "Calories")]
        [InlineData(Ingredient.Guacamole, "Calories")]
        [InlineData(Ingredient.SourCream, "Calories")]
        [InlineData(Ingredient.Carnitas, "Price")]
        [InlineData(Ingredient.Queso, "Price")]
        [InlineData(Ingredient.PintoBeans, "Price")]
        [InlineData(Ingredient.Veggies, "Price")]
        [InlineData(Ingredient.Guacamole, "Price")]
        [InlineData(Ingredient.SourCream, "Price")]
        [InlineData(Ingredient.Carnitas, "PreparationInformation")]
        [InlineData(Ingredient.Queso, "PreparationInformation")]
        [InlineData(Ingredient.PintoBeans, "PreparationInformation")]
        [InlineData(Ingredient.Veggies, "PreparationInformation")]
        [InlineData(Ingredient.Guacamole, "PreparationInformation")]
        [InlineData(Ingredient.SourCream, "PreparationInformation")]
        public void UpdatingIngredientShouldNotifyOfPropertyChanges(Ingredient ingredient, string propertyName)
        {
            CarnitasBowl c = new CarnitasBowl();

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
            CarnitasBowl c = new CarnitasBowl();

            Assert.PropertyChanged(c, propertyName, () => {
                c.SalsaType = salsa;
            });
        }

        /// <summary>
        /// Tests that two CarnitasBowls with identical properties are equal to each other
        /// </summary>
        /// <param name="salsa1">The salsa for the first bowl</param>
        /// <param name="salsa2">The salsa for the second bowl</param>
        /// <param name="add1">The additional ingredients for the first bowl</param>
        /// <param name="add2">The additional ingredients for the second bowl</param>
        [Theory]
        [InlineData(Salsa.Mild, Salsa.Mild, new Ingredient[] { Ingredient.PintoBeans, Ingredient.Carnitas }, new Ingredient[] { Ingredient.PintoBeans, Ingredient.Carnitas })]
        [InlineData(Salsa.Medium, Salsa.Medium, new Ingredient[] { Ingredient.PintoBeans, Ingredient.Queso, Ingredient.Carnitas }, new Ingredient[] { Ingredient.PintoBeans, Ingredient.Queso, Ingredient.Carnitas })]
        [InlineData(Salsa.Hot, Salsa.Hot, new Ingredient[] { Ingredient.SourCream }, new Ingredient[] { Ingredient.SourCream })]
        [InlineData(Salsa.Green, Salsa.Green, new Ingredient[] { Ingredient.Guacamole, Ingredient.Carnitas }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Carnitas })]
        [InlineData(Salsa.None, Salsa.None, new Ingredient[] { Ingredient.Veggies, Ingredient.Carnitas }, new Ingredient[] { Ingredient.Veggies, Ingredient.Carnitas })]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(Salsa salsa1, Salsa salsa2, Ingredient[] add1, Ingredient[] add2)
        {
            CarnitasBowl b1 = new CarnitasBowl() { SalsaType = salsa1 };
            CarnitasBowl b2 = new CarnitasBowl() { SalsaType = salsa2 };

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
        /// Tests that two CarnitasBowls with different properties are not equal to each other
        /// </summary>
        /// <param name="salsa1">The salsa for the first bowl</param>
        /// <param name="salsa2">The salsa for the second bowl</param>
        /// <param name="add1">The additional ingredients for the first bowl</param>
        /// <param name="add2">The additional ingredients for the second bowl</param>
        [Theory]
        [InlineData(Salsa.Mild, Salsa.Medium, new Ingredient[] { Ingredient.PintoBeans, Ingredient.Carnitas }, new Ingredient[] { Ingredient.PintoBeans, Ingredient.Carnitas })]
        [InlineData(Salsa.Hot, Salsa.Medium, new Ingredient[] { Ingredient.PintoBeans, Ingredient.Guacamole, Ingredient.Carnitas }, new Ingredient[] { Ingredient.Veggies, Ingredient.Queso, Ingredient.Carnitas })]
        [InlineData(Salsa.Hot, Salsa.Mild, new Ingredient[] { Ingredient.Veggies }, new Ingredient[] { Ingredient.SourCream })]
        [InlineData(Salsa.Green, Salsa.Medium, new Ingredient[] { Ingredient.Guacamole, Ingredient.Carnitas }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Veggies })]
        [InlineData(Salsa.None, Salsa.None, new Ingredient[] { Ingredient.Veggies, Ingredient.Carnitas }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Carnitas })]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(Salsa salsa1, Salsa salsa2, Ingredient[] add1, Ingredient[] add2)
        {
            CarnitasBowl b1 = new CarnitasBowl() { SalsaType = salsa1 };
            CarnitasBowl b2 = new CarnitasBowl() { SalsaType = salsa2 };

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
