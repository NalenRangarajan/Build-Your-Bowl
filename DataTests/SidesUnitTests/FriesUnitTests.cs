using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the Fries class
    /// </summary>
    public class FriesUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void FriesDefault()
        {
            Fries f = new Fries();

            Assert.False(f.Curly);
            Assert.Equal(Size.Medium, f.SizeType);
            Assert.Equal(3.50m, f.Price);
            Assert.Equal((uint)350, f.Calories);

            string[] expectedOrderOutput = new string[] { "Medium"};

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, f.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, f.PreparationInformation.Count());

            Assert.Equal("Fries", f.ToString());
        }

        /// <summary>
        /// This tests that the price after choosing a different size is correct
        /// </summary>
        /// <param name="size">The size of the side</param>
        /// <param name="isCurly">Whether the fries are curly</param>
        /// <param name="expectedPrice">The expected price of the side</param>
        [Theory]
        [InlineData(Size.Kids, true, 2.50)]
        [InlineData(Size.Small, true, 3.00)]
        [InlineData(Size.Medium, true, 3.50)]
        [InlineData(Size.Large, true, 4.25)]
        [InlineData(Size.Kids, false, 2.50)]
        [InlineData(Size.Small, false, 3.00)]
        [InlineData(Size.Medium, false, 3.50)]
        [InlineData(Size.Large, false, 4.25)]
        public void FriesPriceChangesWithDifferentSizes(Size size, bool isCurly, decimal expectedPrice)
        {
            Fries f = new Fries();
            f.SizeType = size;
            f.Curly = isCurly;

            Assert.Equal(f.Price, expectedPrice);
        }

        /// <summary>
        /// This tests that the number of calories after choosing a different size is correct
        /// </summary>
        /// <param name="size">The size of the side</param>
        /// <param name="isCurly">Whether the fries are curly</param>
        /// <param name="expectedCalories">The expected calories of the side</param>
        [Theory]
        [InlineData(Size.Kids, true, 210)]
        [InlineData(Size.Small, true, 262)]
        [InlineData(Size.Medium, true, 350)]
        [InlineData(Size.Large, true, 525)]
        [InlineData(Size.Kids, false, 210)]
        [InlineData(Size.Small, false, 262)]
        [InlineData(Size.Medium, false, 350)]
        [InlineData(Size.Large, false, 525)]
        public void FriesCaloriesChangesWithDifferentSizes(Size size, bool isCurly, uint expectedCalories)
        {
            Fries f = new Fries();
            f.SizeType = size;
            f.Curly = isCurly;

            Assert.Equal(f.Calories, expectedCalories);
        }

        /// <summary>
        /// This tests that the order output after choosing different sizes, or whether the fries are curly is correct
        /// </summary>
        /// <param name="size">The size of the side</param>
        /// <param name="isCurly">Whether the fries are curly</param>
        /// <param name="expectedOrderOutput">The expected order output for the side</param>
        [Theory]
        [InlineData(Size.Kids, true, new string[] {"Curly", "Kids"})]
        [InlineData(Size.Small, true, new string[] { "Curly", "Small" })]
        [InlineData(Size.Medium, true, new string[] { "Curly", "Medium" })]
        [InlineData(Size.Large, true, new string[] { "Curly", "Large" })]
        [InlineData(Size.Kids, false, new string[] { "Kids" })]
        [InlineData(Size.Small, false, new string[] { "Small" })]
        [InlineData(Size.Medium, false, new string[] { "Medium" })]
        [InlineData(Size.Large, false, new string[] { "Large" })]
        public void FriesPreparationInfoWithDifferentSizes(Size size, bool isCurly, string[] expectedOrderOutput)
        {
            Fries f = new Fries();
            f.SizeType = size;
            f.Curly = isCurly;

            //Checks that each expected string is in the actual preparation info
            Assert.All(expectedOrderOutput, word => Assert.Contains(word, f.PreparationInformation));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(expectedOrderOutput.Length, f.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that Fries can be cast as an IMenuItem, Drink, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            Fries f = new Fries();

            Assert.IsAssignableFrom<IMenuItem>(f);
            Assert.IsAssignableFrom<Side>(f);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(f);
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
            Fries f = new Fries();
            Assert.PropertyChanged(f, propertyName, () => {
                f.SizeType = size;
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing whether the fries are curly
        /// </summary>
        /// <param name="isCurly">Whether the fries are curly</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(true, "Curly")]
        [InlineData(false, "Curly")]
        [InlineData(true, "PreparationInformation")]
        [InlineData(false, "PreparationInformation")]
        public void ChangingCurlyShouldNotifyOfPropertyChanges(bool isCurly, string propertyName)
        {
            Fries f = new Fries();
            Assert.PropertyChanged(f, propertyName, () => {
                f.Curly = isCurly;
            });
        }

        /// <summary>
        /// Tests that two Fries with identical properties are equal to each other
        /// </summary>
        /// <param name="curly1">Whether the first fries are curly</param>
        /// <param name="size1">The size of the first fries</param>
        /// <param name="curly2">Whether the second fries are curly</param>
        /// <param name="size2">The size of the second fries</param>
        [Theory]
        [InlineData(true, Size.Small, true, Size.Small)]
        [InlineData(false, Size.Kids, false, Size.Kids)]
        [InlineData(true, Size.Large, true, Size.Large)]
        [InlineData(false, Size.Medium, false, Size.Medium)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(bool curly1, Size size1, bool curly2, Size size2)
        {
            Fries f1 = new Fries() { Curly = curly1, SizeType = size1 };
            Fries f2 = new Fries() { Curly = curly2, SizeType = size2 };
            Assert.True(f1.Equals(f2));
        }

        /// <summary>
        /// Tests that two Fries with different properties are not equal to each other
        /// </summary>
        /// <param name="curly1">Whether the first fries are curly</param>
        /// <param name="size1">The size of the first fries</param>
        /// <param name="curly2">Whether the second fries are curly</param>
        /// <param name="size2">The size of the second fries</param>
        [Theory]
        [InlineData(false, Size.Medium, true, Size.Small)]
        [InlineData(false, Size.Kids, true, Size.Kids)]
        [InlineData(true, Size.Small, true, Size.Large)]
        [InlineData(true, Size.Medium, false, Size.Medium)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(bool curly1, Size size1, bool curly2, Size size2)
        {
            Fries f1 = new Fries() { Curly = curly1, SizeType = size1 };
            Fries f2 = new Fries() { Curly = curly2, SizeType = size2 };
            Assert.False(f1.Equals(f2));
        }

    }
}
