using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the Bowl class
    /// </summary>
    public class BowlUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void BowlDefault()
        {
            Bowl b = new Bowl();
            IngredientItem rice = new IngredientItem(Ingredient.Rice);
            rice.Included = true;
            rice.Default = true;

            Assert.Equivalent(rice, b.BaseIngredient);
            Assert.True(b.BaseIngredient.Default);
            Assert.True(b.BaseIngredient.Included);
            Assert.Equal(Salsa.Medium, b.SalsaType);
            Assert.Equal(7.99m, b.Price);
            Assert.Equal((uint)230, b.Calories);

            string[] expectedOrderOutput = new string[] { };

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, b.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, b.PreparationInformation.Count());

            IngredientItem i;

            b.BaseAdditionalIngredientsIncludedAndDefault();

            //tests BaseAdditionalIngredientsIncludedAndDefault
            Assert.Equal(9, b.AdditionalIngredients.Count);

            Assert.True(b.AdditionalIngredients.TryGetValue(Ingredient.Chicken, out i!) && !i.Included);
            Assert.True(b.AdditionalIngredients.TryGetValue(Ingredient.Steak, out i!) && !i.Included);
            Assert.True(b.AdditionalIngredients.TryGetValue(Ingredient.Carnitas, out i!) && !i.Included);
            Assert.True(b.AdditionalIngredients.TryGetValue(Ingredient.Queso, out i!) && !i.Included);
            Assert.True(b.AdditionalIngredients.TryGetValue(Ingredient.PintoBeans, out i!) && !i.Included);
            Assert.True(b.AdditionalIngredients.TryGetValue(Ingredient.BlackBeans, out i!) && !i.Included);
            Assert.True(b.AdditionalIngredients.TryGetValue(Ingredient.Veggies, out i!) && !i.Included);
            Assert.True(b.AdditionalIngredients.TryGetValue(Ingredient.Guacamole, out i!) && !i.Included);
            Assert.True(b.AdditionalIngredients.TryGetValue(Ingredient.SourCream, out i!) && !i.Included);

            Assert.Equal("Build-Your-Own Bowl", b.ToString());
        }

        /// <summary>
        /// Tests that Bowl can be cast as an IMenuItem, Entree, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            Bowl b = new Bowl();

            Assert.IsAssignableFrom<IMenuItem>(b);
            Assert.IsAssignableFrom<Entree>(b);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(b);
        }

        /// <summary>
        /// Tests that AdditionalIngredients can be edited and an ingredient can be included
        /// </summary>
        /// <param name="ingredient">The ingredient to include in the bowl</param>
        [Theory]
        [InlineData(Ingredient.Chicken)]
        [InlineData(Ingredient.Steak)]
        [InlineData(Ingredient.Carnitas)]
        [InlineData(Ingredient.Queso)]
        [InlineData(Ingredient.PintoBeans)]
        [InlineData(Ingredient.BlackBeans)]
        [InlineData(Ingredient.Veggies)]
        [InlineData(Ingredient.Guacamole)]
        [InlineData(Ingredient.SourCream)]
        public void CanIncludeIngredients(Ingredient ingredient)
        {
            Bowl b = new Bowl();
            IngredientItem i;

            b.BaseAdditionalIngredientsIncludedAndDefault();

            //The ingredient isn't included
            Assert.False(b.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);

            b.EditOrderAdd(ingredient);

            //The ingredient is included
            Assert.True(b.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);
        }

        /// <summary>
        /// Tests that certain ingredients can't be added to AdditionalIngredients
        /// </summary>
        /// <param name="ingredient">The ingredient to include in the bowl</param>
        [Theory]
        [InlineData(Ingredient.Rice)]
        [InlineData(Ingredient.Chips)]
        public void CantIncludeCertainIngredients(Ingredient ingredient)
        {
            Bowl b = new Bowl();
            b.BaseAdditionalIngredientsIncludedAndDefault();
            IngredientItem i;

            Assert.False(b.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);

            Action action = () => b.EditOrderAdd(ingredient);

            Assert.Throws<NullReferenceException>(action);

            Assert.False(b.AdditionalIngredients.TryGetValue(ingredient, out i!) && i.Included);
        }

        /// <summary>
        /// Tests that Price is calculated correctly
        /// </summary>
        /// <param name="list">The list of ingredients to add</param>
        /// <param name="salsa">The salsa to include in the bowl</param>
        /// <param name="expectedPrice">The expected price of the bowl</param>
        [Theory]
        [InlineData(new Ingredient[] { Ingredient.Steak }, Salsa.Hot, 9.99)]
        [InlineData(new Ingredient[] { Ingredient.Steak, Ingredient.BlackBeans }, Salsa.None, 10.99)]
        [InlineData(new Ingredient[] { Ingredient.Chicken, Ingredient.Queso }, Salsa.Medium, 10.99)]
        [InlineData(new Ingredient[] { Ingredient.Chicken, Ingredient.BlackBeans, Ingredient.Queso }, Salsa.Mild, 11.99)]
        [InlineData(new Ingredient[] { Ingredient.Carnitas, Ingredient.Queso, Ingredient.PintoBeans, Ingredient.Guacamole }, Salsa.Green, 12.99)]
        [InlineData(new Ingredient[] {  }, Salsa.Hot, 7.99)]
        [InlineData(new Ingredient[] { Ingredient.BlackBeans }, Salsa.None, 8.99)]
        [InlineData(new Ingredient[] { Ingredient.Steak, Ingredient.Carnitas, Ingredient.Chicken, Ingredient.Queso, Ingredient.BlackBeans, Ingredient.PintoBeans, Ingredient.Guacamole }, Salsa.Medium, 17.99)]
        public void PriceChange(Ingredient[] list, Salsa salsa, decimal expectedPrice)
        {
            Bowl b = new Bowl();
            b.BaseAdditionalIngredientsIncludedAndDefault();
            b.SalsaType = salsa;

            foreach(Ingredient i in list)
            {
                b.EditOrderAdd(i);
            }

            Assert.Equal(expectedPrice, b.Price);
            
        }

        /// <summary>
        /// Tests that Calories is calculated correctly
        /// </summary>
        /// <param name="list">The list of ingredients to add</param>
        /// <param name="salsa">The sals to include in the bowl</param>
        /// <param name="expectedCalories">The expected calories in the bowl</param>
        [Theory]
        [InlineData(new Ingredient[] { Ingredient.Steak }, Salsa.Hot, 410)]
        [InlineData(new Ingredient[] { Ingredient.Steak, Ingredient.BlackBeans }, Salsa.None, 520)]
        [InlineData(new Ingredient[] { Ingredient.Chicken, Ingredient.Queso }, Salsa.Medium, 490)]
        [InlineData(new Ingredient[] { Ingredient.Chicken, Ingredient.BlackBeans, Ingredient.Queso }, Salsa.Mild, 620)]
        [InlineData(new Ingredient[] { Ingredient.Carnitas, Ingredient.Queso, Ingredient.PintoBeans, Ingredient.Guacamole }, Salsa.Green, 830)]
        [InlineData(new Ingredient[] { }, Salsa.Hot, 230)]
        [InlineData(new Ingredient[] { Ingredient.BlackBeans }, Salsa.None, 340)]
        [InlineData(new Ingredient[] { Ingredient.Steak, Ingredient.Carnitas, Ingredient.Chicken, Ingredient.Queso, Ingredient.BlackBeans, Ingredient.PintoBeans, Ingredient.Guacamole }, Salsa.Medium, 1290)]
        public void CalorieChange(Ingredient[] list, Salsa salsa, uint expectedCalories)
        {
            Bowl b = new Bowl();
            b.BaseAdditionalIngredientsIncludedAndDefault();

            b.SalsaType = salsa;

            foreach (Ingredient i in list)
            {
                b.EditOrderAdd(i);
            }

            Assert.Equal(expectedCalories, b.Calories);

        }

        /// <summary>
        /// Tests that PreparationInformation is calculated correctly
        /// </summary>
        /// <param name="list">The list of ingredients to add</param>
        /// <param name="salsa">The sals to include in the bowl</param>
        /// <param name="expectedOrderOutput">The expected order output from the bowl</param>
        [Theory]
        [InlineData(new Ingredient[] { Ingredient.Steak }, Salsa.Hot, new string[] {"Add Steak", "Swap Hot Salsa"})]
        [InlineData(new Ingredient[] { Ingredient.Steak, Ingredient.BlackBeans }, Salsa.None, new string[] {"Add Steak", "Add Black Beans", "Hold Salsa"})]
        [InlineData(new Ingredient[] { Ingredient.Chicken, Ingredient.Queso }, Salsa.Medium, new string[] {"Add Chicken", "Add Queso"})]
        [InlineData(new Ingredient[] { Ingredient.Chicken, Ingredient.BlackBeans, Ingredient.Queso }, Salsa.Mild, new string[] {"Add Chicken", "Add Black Beans", "Add Queso", "Swap Mild Salsa"})]
        [InlineData(new Ingredient[] { Ingredient.Carnitas, Ingredient.Queso, Ingredient.PintoBeans, Ingredient.Guacamole }, Salsa.Green, new string[] {"Add Carnitas", "Add Queso", "Add Pinto Beans", "Add Guacamole", "Swap Green Salsa"})]
        [InlineData(new Ingredient[] { }, Salsa.Hot, new string[] { "Swap Hot Salsa" })]
        [InlineData(new Ingredient[] { Ingredient.BlackBeans }, Salsa.None, new string[] {"Add Black Beans", "Hold Salsa"})]
        [InlineData(new Ingredient[] { Ingredient.Steak, Ingredient.Carnitas, Ingredient.Chicken, Ingredient.Queso, Ingredient.BlackBeans, Ingredient.PintoBeans, Ingredient.Guacamole }, Salsa.Medium, new string[] {"Add Steak", "Add Carnitas", "Add Chicken", "Add Queso", "Add Black Beans", "Add Pinto Beans", "Add Guacamole"})]
        public void PreparationInfoWithDifferentIngredients(Ingredient[] list, Salsa salsa, string[] expectedOrderOutput)
        {
            Bowl b = new Bowl();
            b.BaseAdditionalIngredientsIncludedAndDefault();

            b.SalsaType = salsa;

            foreach (Ingredient i in list)
            {
                b.EditOrderAdd(i);
            }

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, b.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, b.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when adding and removing ingredients
        /// </summary>
        /// <param name="ingredient">The ingredient to add/remove</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(Ingredient.Chicken, "Calories")]
        [InlineData(Ingredient.Steak, "Calories")]
        [InlineData(Ingredient.Carnitas, "Calories")]
        [InlineData(Ingredient.Queso, "Calories")]
        [InlineData(Ingredient.PintoBeans, "Calories")]
        [InlineData(Ingredient.BlackBeans, "Calories")]
        [InlineData(Ingredient.Veggies, "Calories")]
        [InlineData(Ingredient.Guacamole, "Calories")]
        [InlineData(Ingredient.SourCream, "Calories")]
        [InlineData(Ingredient.Chicken, "Price")]
        [InlineData(Ingredient.Steak, "Price")]
        [InlineData(Ingredient.Carnitas, "Price")]
        [InlineData(Ingredient.Queso, "Price")]
        [InlineData(Ingredient.PintoBeans, "Price")]
        [InlineData(Ingredient.BlackBeans, "Price")]
        [InlineData(Ingredient.Veggies, "Price")]
        [InlineData(Ingredient.Guacamole, "Price")]
        [InlineData(Ingredient.SourCream, "Price")]
        [InlineData(Ingredient.Chicken, "PreparationInformation")]
        [InlineData(Ingredient.Steak, "PreparationInformation")]
        [InlineData(Ingredient.Carnitas, "PreparationInformation")]
        [InlineData(Ingredient.Queso, "PreparationInformation")]
        [InlineData(Ingredient.PintoBeans, "PreparationInformation")]
        [InlineData(Ingredient.BlackBeans, "PreparationInformation")]
        [InlineData(Ingredient.Veggies, "PreparationInformation")]
        [InlineData(Ingredient.Guacamole, "PreparationInformation")]
        [InlineData(Ingredient.SourCream, "PreparationInformation")]
        public void UpdatingIngredientShouldNotifyOfPropertyChanges(Ingredient ingredient, string propertyName)
        {
            Bowl b = new Bowl();

            Assert.PropertyChanged(b, propertyName, () => {
                b.EditOrderAdd(ingredient);
            });

            Assert.PropertyChanged(b, propertyName, () => {
                b.EditOrderRemove(ingredient);
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
            Bowl b = new Bowl();

            Assert.PropertyChanged(b, propertyName, () => {
                b.SalsaType = salsa;
            });
        }

        /// <summary>
        /// Tests that two Bowls with identical properties are equal to each other
        /// </summary>
        /// <param name="salsa1">The salsa for the first bowl</param>
        /// <param name="salsa2">The salsa for the second bowl</param>
        /// <param name="add1">The additional ingredients for the first bowl</param>
        /// <param name="add2">The additional ingredients for the second bowl</param>
        [Theory]
        [InlineData(Salsa.Mild, Salsa.Mild, new Ingredient[] {Ingredient.BlackBeans, Ingredient.Carnitas}, new Ingredient[] { Ingredient.BlackBeans, Ingredient.Carnitas })]
        [InlineData(Salsa.Medium, Salsa.Medium, new Ingredient[] { Ingredient.PintoBeans, Ingredient.Steak, Ingredient.Carnitas }, new Ingredient[] { Ingredient.PintoBeans, Ingredient.Steak, Ingredient.Carnitas })]
        [InlineData(Salsa.Hot, Salsa.Hot, new Ingredient[] { Ingredient.SourCream }, new Ingredient[] { Ingredient.SourCream })]
        [InlineData(Salsa.Green, Salsa.Green, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken }, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken })]
        [InlineData(Salsa.None, Salsa.None, new Ingredient[] { Ingredient.Veggies, Ingredient.Carnitas }, new Ingredient[] { Ingredient.Veggies, Ingredient.Carnitas })]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(Salsa salsa1, Salsa salsa2, Ingredient[] add1, Ingredient[] add2)
        {
            Bowl b1 = new Bowl() { SalsaType = salsa1 };
            Bowl b2 = new Bowl() { SalsaType = salsa2 };

            foreach(Ingredient i in add1)
            {
                b1.EditOrderAdd(i);
            }
            foreach(Ingredient i in add2)
            {
                b2.EditOrderAdd(i);
            }

            Assert.True(b1.Equals(b2));
        }

        /// <summary>
        /// Tests that two Bowls with different properties are not equal to each other
        /// </summary>
        /// <param name="salsa1">The salsa for the first bowl</param>
        /// <param name="salsa2">The salsa for the second bowl</param>
        /// <param name="add1">The additional ingredients for the first bowl</param>
        /// <param name="add2">The additional ingredients for the second bowl</param>
        [Theory]
        [InlineData(Salsa.Mild, Salsa.Medium, new Ingredient[] { Ingredient.BlackBeans, Ingredient.Carnitas }, new Ingredient[] { Ingredient.BlackBeans, Ingredient.Carnitas })]
        [InlineData(Salsa.Green, Salsa.Medium, new Ingredient[] { Ingredient.Steak, Ingredient.Carnitas }, new Ingredient[] { Ingredient.PintoBeans, Ingredient.Steak, Ingredient.Carnitas })]
        [InlineData(Salsa.Hot, Salsa.Mild, new Ingredient[] { Ingredient.SourCream }, new Ingredient[] { Ingredient.Veggies })]
        [InlineData(Salsa.None, Salsa.Green, new Ingredient[] { Ingredient.Guacamole, Ingredient.Chicken }, new Ingredient[] { Ingredient.Carnitas, Ingredient.Chicken })]
        [InlineData(Salsa.None, Salsa.None, new Ingredient[] { Ingredient.SourCream, Ingredient.Carnitas }, new Ingredient[] { Ingredient.Veggies, Ingredient.Carnitas })]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(Salsa salsa1, Salsa salsa2, Ingredient[] add1, Ingredient[] add2)
        {
            Bowl b1 = new Bowl() { SalsaType = salsa1 };
            Bowl b2 = new Bowl() { SalsaType = salsa2 };

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
