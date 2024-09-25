using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the Horchata class
    /// </summary>
    public class HorchataUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void HorchataDefault()
        {
            Horchata h = new Horchata();

            Assert.True(h.Ice);
            Assert.Equal(Size.Medium, h.DrinkSize);
            Assert.Equal(3.50m, h.Price);
            Assert.Equal((uint)280, h.Calories);

            string[] expectedOrderOutput = new string[] { "Medium"};

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, h.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, h.PreparationInformation.Count());
            Assert.Equal("Horchata", h.ToString());
        }

        /// <summary>
        /// This tests that the price after choosing different sizes is correct
        /// </summary>
        /// <param name="size">The size of the drink</param>
        /// <param name="hasIce">Whether the drink has ice</param>
        /// <param name="expectedPrice">The expected price of the drink</param>
        [Theory]
        [InlineData(Size.Kids, true, 2.50)]
        [InlineData(Size.Kids, false, 2.50)]
        [InlineData(Size.Small, true, 3.00)]
        [InlineData(Size.Small, false, 3.00)]
        [InlineData(Size.Medium, true, 3.50)]
        [InlineData(Size.Medium, false, 3.50)]
        [InlineData(Size.Large, true, 4.25)]
        [InlineData(Size.Large, false, 4.25)]
        public void HorchataPriceIncreaseWithDifferentSizesAndFlavors(Size size, bool hasIce, decimal expectedPrice)
        {
            Horchata h = new Horchata();

            h.DrinkSize = size;
            h.Ice = hasIce;

            Assert.Equal(expectedPrice, h.Price);
        }

        /// <summary>
        /// This tests that the number of calories after choosing different sizes, or whether the drink has ice is correct
        /// </summary>
        /// <param name="size">The size of the drink</param>
        /// <param name="hasIce">Whether the drink has ice</param>
        /// <param name="expectedCalories">The expected number of calories in the drink</param>
        [Theory]
        [InlineData(Size.Kids, true, 168)]
        [InlineData(Size.Kids, false, 186)]
        [InlineData(Size.Small, true, 210)]
        [InlineData(Size.Small, false, 232)]
        [InlineData(Size.Medium, true, 280)]
        [InlineData(Size.Medium, false, 310)]
        [InlineData(Size.Large, true, 420)]
        [InlineData(Size.Large, false, 465)]
        public void HorchataCalorieCountWithDifferentSizes(Size size, bool hasIce, uint expectedCalories)
        {
            Horchata h = new Horchata();

            h.DrinkSize = size;
            h.Ice = hasIce;

            Assert.Equal((uint)expectedCalories, h.Calories);
        }

        /// <summary>
        /// This tests that the order output after choosing different sizes, or whether the drink has ice is correct
        /// </summary>
        /// <param name="size">The size of the drink</param>
        /// <param name="hasIce">Whether the drink has ice</param>
        /// <param name="expectedOrderOutput">The expected order output for the drink</param>
        [Theory]
        [InlineData(Size.Kids, true, new string[] { "Kids"})]
        [InlineData(Size.Kids, false, new string[] { "Kids", "Hold Ice"})]
        [InlineData(Size.Small, true, new string[] { "Small"})]
        [InlineData(Size.Small, false, new string[] { "Small", "Hold Ice" })]
        [InlineData(Size.Medium, true, new string[] { "Medium"})]
        [InlineData(Size.Medium, false, new string[] { "Medium", "Hold Ice" })]
        [InlineData(Size.Large, true, new string[] { "Large"})]
        [InlineData(Size.Large, false, new string[] {  "Large", "Hold Ice" })]
        public void HorchataPreparationInfoWithDifferentSizes(Size size, bool hasIce, string[] expectedOrderOutput)
        {
            Horchata h = new Horchata();

            h.DrinkSize = size;
            h.Ice = hasIce;

            //Checks that each expected string is in the actual preparation info
            Assert.All(expectedOrderOutput, word => Assert.Contains(word, h.PreparationInformation));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(expectedOrderOutput.Length, h.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that Horchata can be cast as an IMenuItem, Drink, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            Horchata h = new Horchata();

            Assert.IsAssignableFrom<IMenuItem>(h);
            Assert.IsAssignableFrom<Drink>(h);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(h);
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing whether the drink has ice
        /// </summary>
        /// <param name="hasIce">Whether the drink has ice</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(true, "Ice")]
        [InlineData(false, "Ice")]
        [InlineData(true, "Calories")]
        [InlineData(false, "Calories")]
        [InlineData(true, "PreparationInformation")]
        [InlineData(false, "PreparationInformation")]
        public void ChangingIceShouldNotifyOfPropertyChanges(bool hasIce, string propertyName)
        {
            Horchata h = new Horchata();
            Assert.PropertyChanged(h, propertyName, () => {
                h.Ice = hasIce;
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing the size of the drink
        /// </summary>
        /// <param name="size">The size to change to</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(Size.Kids, "DrinkSize")]
        [InlineData(Size.Small, "DrinkSize")]
        [InlineData(Size.Medium, "DrinkSize")]
        [InlineData(Size.Large, "DrinkSize")]
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
            Horchata h = new Horchata();
            Assert.PropertyChanged(h, propertyName, () => {
                h.DrinkSize = size;
            });
        }

        /// <summary>
        /// Tests that two Horchatas with identical properties are equal to each other
        /// </summary>
        /// <param name="size1">The size of the first drink</param>
        /// <param name="size2">The size of the second drink</param>
        [Theory]
        [InlineData( Size.Small, Size.Small)]
        [InlineData( Size.Kids, Size.Kids)]
        [InlineData( Size.Large, Size.Large)]
        [InlineData( Size.Medium, Size.Medium)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(Size size1, Size size2)
        {
            Horchata h1 = new Horchata() { DrinkSize = size1 };
            Horchata h2 = new Horchata() { DrinkSize = size2 };

            Assert.True(h1.Equals(h2));
        }

        /// <summary>
        /// Tests that two Horchatas with different properties are not equal to each other
        /// </summary>
        /// <param name="size1">The size of the first drink</param>
        /// <param name="size2">The size of the second drink</param>
        [Theory]
        [InlineData(Size.Medium, Size.Small)]
        [InlineData(Size.Kids, Size.Large)]
        [InlineData(Size.Small, Size.Large)]
        [InlineData(Size.Medium, Size.Kids)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(Size size1, Size size2)
        {
            Horchata h1 = new Horchata() { DrinkSize = size1 };
            Horchata h2 = new Horchata() { DrinkSize = size2 };

            Assert.False(h1.Equals(h2));
        }
    }
}
