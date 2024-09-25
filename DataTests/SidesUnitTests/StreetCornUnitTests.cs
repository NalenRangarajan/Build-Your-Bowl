using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the StreetCorn class
    /// </summary>
    public class StreetCornUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void StreetCornDefault()
        {
            StreetCorn s = new StreetCorn();
            IngredientItem i;

            Assert.True(s.CotijaCheese);
            Assert.True(s.Cilantro);
            Assert.Equal(Size.Medium, s.SizeType);
            Assert.Equal(4.50m, s.Price);
            Assert.Equal((uint)300, s.Calories);

            string[] expectedOrderOutput = new string[] { "Medium" };

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, s.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, s.PreparationInformation.Count());

            Assert.Equal("Street Corn", s.ToString());
        }

        //Specific Required Test Case
        /// <summary>
        /// This tests the calories, price, and expected order output of a specific Street Corn side
        /// </summary>
        [Fact]
        public void SpecificStreetCornSide()
        {
            StreetCorn s = new StreetCorn();
            s.CotijaCheese = true;
            s.Cilantro = false;
            s.SizeType = Size.Small;

            string[] expectedOrderOutput = new string[] { "Small", "Hold Cilantro" };

            Assert.Equal((uint)221, s.Calories);
            Assert.Equal(3.75m, s.Price);

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, s.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, s.PreparationInformation.Count());
        }

        /// <summary>
        /// This tests that the price after choosing a different size is correct
        /// </summary>
        /// <param name="size">The size of the side</param>
        /// <param name="hasCotijaCheese">Whether the street corn has cotija cheese</param>
        /// <param name="hasCilantro">Whether the street corn has cilantro</param>
        /// <param name="expectedPrice">The expected price of the side</param>
        [Theory]
        [InlineData(Size.Kids, true, true, 3.25)]
        [InlineData(Size.Small, false, false, 3.75)]
        [InlineData(Size.Medium, true, true, 4.50)]
        [InlineData(Size.Large, false, false, 5.50)]
        [InlineData(Size.Kids, true, false, 3.25)]
        [InlineData(Size.Small, false, true, 3.75)]
        [InlineData(Size.Medium, true, false, 4.50)]
        [InlineData(Size.Large, false, true, 5.50)]
        public void StreetCornPriceChangesWithDifferentSizes(Size size, bool hasCotijaCheese, bool hasCilantro, decimal expectedPrice)
        {
            StreetCorn s = new StreetCorn();
            s.SizeType = size;

            s.CotijaCheese = hasCotijaCheese;
            s.Cilantro = hasCilantro;

            Assert.Equal(s.Price, expectedPrice);
        }

        /// <summary>
        /// This tests that the number of calories after choosing a different size is correct
        /// </summary>
        /// <param name="size">The size of the side</param>
        /// <param name="hasCotijaCheese">Whether the street corn has cotija cheese</param>
        /// <param name="hasCilantro">Whether the street corn has cilantro</param>
        /// <param name="expectedCalories">The expected calories of the side</param>
        [Theory]
        [InlineData(Size.Kids, true, true, 180)]
        [InlineData(Size.Kids, false, false, 129)]
        [InlineData(Size.Small, true, false, 221)]
        [InlineData(Size.Small, false, true, 165)]
        [InlineData(Size.Medium, true, true, 300)]
        [InlineData(Size.Medium, false, false, 215)]
        [InlineData(Size.Large, true, false, 442)]
        [InlineData(Size.Large, false, true, 330)]
        public void StreetCornCaloriesChangesWithDifferentSizes(Size size, bool hasCotijaCheese, bool hasCilantro, uint expectedCalories)
        {
            StreetCorn s = new StreetCorn();
            s.CotijaCheese = hasCotijaCheese;
            s.Cilantro = hasCilantro;
            s.SizeType = size;

            Assert.Equal(s.Calories, expectedCalories);
        }

        /// <summary>
        /// This tests that the street corn has the correct order output with different ingredients included or excluded and with the correct size
        /// </summary>
        /// <param name="size">The size of the side</param>
        /// <param name="hasCotijaCheese">Whether the street corn has cotija cheese</param>
        /// <param name="hasCilantro">Whether the street corn has cilantro</param>
        /// <param name="expectedOrderOutput">The expected order output for the side</param>
        [Theory]
        [InlineData(Size.Kids, true, true, new string[] { "Kids" })]
        [InlineData(Size.Kids, false, false, new string[] { "Hold Cotija Cheese", "Hold Cilantro", "Kids" })]
        [InlineData(Size.Small, true, false, new string[] { "Hold Cilantro", "Small" })]
        [InlineData(Size.Small, false, true, new string[] { "Hold Cotija Cheese", "Small" })]
        [InlineData(Size.Medium, true, true, new string[] { "Medium" })]
        [InlineData(Size.Medium, false, false, new string[] { "Hold Cotija Cheese", "Hold Cilantro", "Medium" })]
        [InlineData(Size.Large, true, false, new string[] { "Hold Cilantro", "Large" })]
        [InlineData(Size.Large, false, true, new string[] { "Hold Cotija Cheese", "Large" })]
        public void StreetCornPreparationInfoWithDifferentSizes(Size size, bool hasCotijaCheese, bool hasCilantro, string[] expectedOrderOutput)
        {
            StreetCorn s = new StreetCorn();
            s.SizeType = size;

            s.CotijaCheese = hasCotijaCheese;
            s.Cilantro = hasCilantro;

            //Checks that each expected string is in the actual preparation info
            Assert.All(expectedOrderOutput, word => Assert.Contains(word, s.PreparationInformation));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(expectedOrderOutput.Length, s.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that RefriedBeans can be cast as an IMenuItem, Drink, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            StreetCorn c = new StreetCorn();

            Assert.IsAssignableFrom<IMenuItem>(c);
            Assert.IsAssignableFrom<Side>(c);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(c);
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
        /// Tests that PropertyChanged is correctly invoked when changing whether the street corn has cotija cheese
        /// </summary>
        /// <param name="hasCheese">Whether the corn has cotija cheese</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(true, "CotijaCheese")]
        [InlineData(false, "CotijaCheese")]
        [InlineData(true, "Calories")]
        [InlineData(false, "Calories")]
        [InlineData(true, "PreparationInformation")]
        [InlineData(false, "PreparationInformation")]
        public void ChangingCotijaCheeseShouldNotifyOfPropertyChanges(bool hasCheese, string propertyName)
        {
            StreetCorn c = new StreetCorn();
            Assert.PropertyChanged(c, propertyName, () => {
                c.CotijaCheese = hasCheese;
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing whether the street corn has cilantro
        /// </summary>
        /// <param name="hasCilantro">Whether the corn has cilantro</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(true, "Cilantro")]
        [InlineData(false, "Cilantro")]
        [InlineData(true, "Calories")]
        [InlineData(false, "Calories")]
        [InlineData(true, "PreparationInformation")]
        [InlineData(false, "PreparationInformation")]
        public void ChangingCilantroShouldNotifyOfPropertyChanges(bool hasCilantro, string propertyName)
        {
            StreetCorn c = new StreetCorn();
            Assert.PropertyChanged(c, propertyName, () => {
                c.Cilantro = hasCilantro;
            });
        }

        /// <summary>
        /// Tests that two StreetCorns with identical properties are equal to each other
        /// </summary>
        /// <param name="cheese1">Whether the first corn has cotija cheese</param>
        /// <param name="cilantro1">Whether the first corn has cilantro</param>
        /// <param name="size1">The size of the first corn</param>
        /// <param name="cheese2">Whether the second corn has cotija cheese</param>
        /// <param name="cilantro2">Whether the second corn has cilantro</param>
        /// <param name="size2">The size of the second corn</param>
        [Theory]
        [InlineData(true, true, Size.Small, true, true, Size.Small)]
        [InlineData(false, true, Size.Kids, false, true, Size.Kids)]
        [InlineData(true, false, Size.Large, true, false, Size.Large)]
        [InlineData(false, false, Size.Medium, false, false, Size.Medium)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(bool cheese1, bool cilantro1, Size size1, bool cheese2, bool cilantro2, Size size2)
        {
            StreetCorn s1 = new StreetCorn() { CotijaCheese = cheese1, Cilantro = cilantro1, SizeType = size1 };
            StreetCorn s2 = new StreetCorn() { CotijaCheese = cheese2, Cilantro = cilantro2, SizeType = size2 };
            Assert.True(s1.Equals(s2));
        }

        /// <summary>
        /// Tests that two StreetCorns with different properties are not equal to each other
        /// </summary>
        /// <param name="cheese1">Whether the first corn has cotija cheese</param>
        /// <param name="cilantro1">Whether the first corn has cilantro</param>
        /// <param name="size1">The size of the first corn</param>
        /// <param name="cheese2">Whether the second corn has cotija cheese</param>
        /// <param name="cilantro2">Whether the second corn has cilantro</param>
        /// <param name="size2">The size of the second corn</param>
        [Theory]
        [InlineData(false, false, Size.Small, true, true, Size.Medium)]
        [InlineData(false, true, Size.Large, true, true, Size.Kids)]
        [InlineData(true, true, Size.Small, true, false, Size.Large)]
        [InlineData(true, false, Size.Kids, false, true, Size.Medium)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(bool cheese1, bool cilantro1, Size size1, bool cheese2, bool cilantro2, Size size2)
        {
            StreetCorn s1 = new StreetCorn() { CotijaCheese = cheese1, Cilantro = cilantro1, SizeType = size1 };
            StreetCorn s2 = new StreetCorn() { CotijaCheese = cheese2, Cilantro = cilantro2, SizeType = size2 };
            Assert.False(s1.Equals(s2));
        }
    }
}
