using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the RefriedBeans class
    /// </summary>
    public class RefriedBeansUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void RefriedBeansDefault()
        {
            RefriedBeans b = new RefriedBeans();
            IngredientItem i;

            Assert.True(b.CheddarCheese);
            Assert.True(b.Onions);
            Assert.Equal(Size.Medium, b.SizeType);
            Assert.Equal(3.75m, b.Price);
            Assert.Equal((uint)300, b.Calories);

            string[] expectedOrderOutput = new string[] { "Medium" };

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, b.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, b.PreparationInformation.Count());

            Assert.Equal("Refried Beans", b.ToString());
        }

        /// <summary>
        /// This tests that the price after choosing a different size is correct
        /// </summary>
        /// <param name="size">The size of the side</param>
        /// <param name="hasOnions">Whether the refried beans have onions</param>
        /// <param name="hasCheddarCheese">Whether the refried beans have cheddar cheese</param>
        /// <param name="expectedPrice">The expected price of the side</param>
        [Theory]
        [InlineData(Size.Kids, true, true, 2.75)]
        [InlineData(Size.Small, false, false, 3.25)]
        [InlineData(Size.Medium, true, true, 3.75)]
        [InlineData(Size.Large, false, false, 4.50)]
        [InlineData(Size.Kids, true, false, 2.75)]
        [InlineData(Size.Small, false, true, 3.25)]
        [InlineData(Size.Medium, true, false, 3.75)]
        [InlineData(Size.Large, false, true, 4.50)]
        public void RefriedBeansPriceChangesWithDifferentSizes(Size size, bool hasOnions, bool hasCheddarCheese, decimal expectedPrice)
        {
            RefriedBeans b = new RefriedBeans();
            b.SizeType = size;

            b.CheddarCheese = hasCheddarCheese;
            b.Onions = hasOnions;

            Assert.Equal(b.Price, expectedPrice);
        }

        /// <summary>
        /// This tests that the number of calories after choosing a different size is correct
        /// </summary>
        /// <param name="size">The size of the side</param>
        /// <param name="hasCheddarCheese">Whether the refried beans have cheddar cheese</param>
        /// <param name="hasOnions">Whether the refried beans have onions</param>
        /// <param name="expectedCalories">The expected calories of the side</param>
        [Theory]
        [InlineData(Size.Kids, true, true, 180)]
        [InlineData(Size.Kids, false, false, 123)]
        [InlineData(Size.Small, true, false, 221)]
        [InlineData(Size.Small, false, true, 157)]
        [InlineData(Size.Medium, true, true, 300)]
        [InlineData(Size.Medium, false, false, 205)]
        [InlineData(Size.Large, true, false, 442)]
        [InlineData(Size.Large, false, true, 315)]
        public void RefriedBeansCaloriesChangesWithDifferentSizes(Size size, bool hasCheddarCheese, bool hasOnions, uint expectedCalories)
        {
            RefriedBeans b = new RefriedBeans();
            b.SizeType = size;

            b.CheddarCheese = hasCheddarCheese;
            b.Onions = hasOnions;

            Assert.Equal(b.Calories, expectedCalories);
        }

        /// <summary>
        /// This tests that the refried beans have the correct order output with different ingredients included or excluded and with the correct size
        /// </summary>
        /// <param name="size">The size of the side</param>
        /// <param name="hasCheddarCheese">Whether the refried beans have onions</param>
        /// <param name="hasOnions">Whether the refried beans have onions</param>
        /// <param name="expectedOrderOutput">The expected order output for the side</param>
        [Theory]
        [InlineData(Size.Kids, true, true, new string[] { "Kids"})]
        [InlineData(Size.Kids, false, false, new string[] { "Hold Cheddar Cheese", "Hold Onions", "Kids" })]
        [InlineData(Size.Small, true, false, new string[] {"Hold Onions", "Small"})]
        [InlineData(Size.Small, false, true, new string[] {"Hold Cheddar Cheese", "Small"})]
        [InlineData(Size.Medium, true, true, new string[] { "Medium" })]
        [InlineData(Size.Medium, false, false, new string[] { "Hold Cheddar Cheese", "Hold Onions", "Medium" })]
        [InlineData(Size.Large, true, false, new string[] { "Hold Onions", "Large" })]
        [InlineData(Size.Large, false, true, new string[] { "Hold Cheddar Cheese", "Large" })]
        public void RefriedBeansPreparationInfoWithDifferentSizes(Size size, bool hasCheddarCheese, bool hasOnions, string[] expectedOrderOutput)
        {
            RefriedBeans b = new RefriedBeans();
            b.SizeType = size;

            b.CheddarCheese = hasCheddarCheese;
            b.Onions = hasOnions;

            //Checks that each expected string is in the actual preparation info
            Assert.All(expectedOrderOutput, word => Assert.Contains(word, b.PreparationInformation));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(expectedOrderOutput.Length, b.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that RefriedBeans can be cast as an IMenuItem, Drink, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            RefriedBeans r = new RefriedBeans();

            Assert.IsAssignableFrom<IMenuItem>(r);
            Assert.IsAssignableFrom<Side>(r);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(r);
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing the size of the side
        /// </summary>
        /// <param name="size">The size to change to</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(Size.Kids, "SizeType")]
        [InlineData(Size.Small, "SizeType")]
        [InlineData(Size.Medium, "SizeType")]
        [InlineData(Size.Large, "SizeType")]
        [InlineData(Size.Kids, "Price")]
        [InlineData(Size.Small, "Price")]
        [InlineData(Size.Medium, "Price")]
        [InlineData(Size.Large, "Price")]
        [InlineData(Size.Kids, "Calories")]
        [InlineData(Size.Small, "Calories")]
        [InlineData(Size.Medium, "Calories")]
        [InlineData(Size.Large, "Calories")]
        [InlineData(Size.Kids, "PreparationInformation")]
        [InlineData(Size.Small, "PreparationInformation")]
        [InlineData(Size.Medium, "PreparationInformation")]
        [InlineData(Size.Large, "PreparationInformation")]
        public void ChangingSizeShouldNotifyOfPropertyChanges(Size size, string propertyName)
        {
            RefriedBeans r = new RefriedBeans();
            Assert.PropertyChanged(r, propertyName, () => {
                r.SizeType = size;
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing whether the refried beans have cheddar cheese
        /// </summary>
        /// <param name="hasCheese">Whether the beans have cheddar cheese</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(true, "CheddarCheese")]
        [InlineData(false, "CheddarCheese")]
        [InlineData(true, "Calories")]
        [InlineData(false, "Calories")]
        [InlineData(true, "PreparationInformation")]
        [InlineData(false, "PreparationInformation")]
        public void ChangingCheddarCheeseShouldNotifyOfPropertyChanges(bool hasCheese, string propertyName)
        {
            RefriedBeans r = new RefriedBeans();
            Assert.PropertyChanged(r, propertyName, () => {
                r.CheddarCheese = hasCheese;
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing whether the refried beans have onions
        /// </summary>
        /// <param name="hasOnions">Whether the beans have onions</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(true, "Onions")]
        [InlineData(false, "Onions")]
        [InlineData(true, "Calories")]
        [InlineData(false, "Calories")]
        [InlineData(true, "PreparationInformation")]
        [InlineData(false, "PreparationInformation")]
        public void ChangingOnionsShouldNotifyOfPropertyChanges(bool hasOnions, string propertyName)
        {
            RefriedBeans r = new RefriedBeans();
            Assert.PropertyChanged(r, propertyName, () => {
                r.Onions = hasOnions;
            });
        }

        /// <summary>
        /// Tests that two RefriedBeans with identical properties are equal to each other
        /// </summary>
        /// <param name="cheese1">Whether the first beans have cheddar cheese</param>
        /// <param name="onions1">Whether the first beans have onions</param>
        /// <param name="size1">The size of the first beans</param>
        /// <param name="cheese2">Whether the second beans have cheddar cheese</param>
        /// <param name="onions2">Whether the second beans have onions</param>
        /// <param name="size2">The size of the second beans</param>
        [Theory]
        [InlineData(true, true, Size.Small, true, true, Size.Small)]
        [InlineData(false, true, Size.Kids, false, true, Size.Kids)]
        [InlineData(true, false, Size.Large, true, false, Size.Large)]
        [InlineData(false, false, Size.Medium, false, false, Size.Medium)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(bool cheese1, bool onions1, Size size1, bool cheese2, bool onions2, Size size2)
        {
            RefriedBeans r1 = new RefriedBeans() { CheddarCheese = cheese1, Onions = onions1, SizeType = size1 };
            RefriedBeans r2 = new RefriedBeans() { CheddarCheese = cheese2, Onions = onions2, SizeType = size2 };
            Assert.True(r1.Equals(r2));
        }

        /// <summary>
        /// Tests that two RefriedBeans with different properties are not equal to each other
        /// </summary>
        /// <param name="cheese1">Whether the first beans have cheddar cheese</param>
        /// <param name="onions1">Whether the first beans have onions</param>
        /// <param name="size1">The size of the first beans</param>
        /// <param name="cheese2">Whether the second beans have cheddar cheese</param>
        /// <param name="onions2">Whether the second beans have onions</param>
        /// <param name="size2">The size of the second beans</param>
        [Theory]
        [InlineData(false, false, Size.Small, true, true, Size.Medium)]
        [InlineData(false, true, Size.Large, true, true, Size.Kids)]
        [InlineData(true, true, Size.Small, true, false, Size.Large)]
        [InlineData(true, false, Size.Kids, false, true, Size.Medium)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(bool cheese1, bool onions1, Size size1, bool cheese2, bool onions2, Size size2)
        {
            RefriedBeans r1 = new RefriedBeans() { CheddarCheese = cheese1, Onions = onions1, SizeType = size1 };
            RefriedBeans r2 = new RefriedBeans() { CheddarCheese = cheese2, Onions = onions2, SizeType = size2 };
            Assert.False(r1.Equals(r2));
        }
    }
}
