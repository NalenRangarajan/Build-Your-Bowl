using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the AguaFresca class
    /// </summary>
    public class AguaFrescaUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void AguaFrescaDefault()
        {
            AguaFresca a = new AguaFresca();

            Assert.Equal(Flavor.Limonada, a.DrinkFlavor);
            Assert.Equal(Size.Medium, a.DrinkSize);
            Assert.True(a.Ice);
            Assert.Equal(3.00m, a.Price);
            Assert.Equal((uint)125, a.Calories);
            string[] expectedOrderOutput = new string[] { "Limonada", "Medium"};

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, a.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, a.PreparationInformation.Count());
            Assert.Equal("Agua Fresca", a.ToString());
        }

        //Specific Required Test Case
        /// <summary>
        /// This tests the calories, price, and expected order output of a specific Agua Fresca drink
        /// </summary>
        [Fact]
        public void SpecificAguaFrescaDrink()
        {
            AguaFresca a = new AguaFresca();
            a.Ice = false;
            a.DrinkFlavor = Flavor.Tamarind;
            a.DrinkSize = Size.Large;

            string[] expectedOrderOutput = new string[] { "Large", "Tamarind", "Hold Ice" };

            Assert.Equal((uint)240, a.Calories);
            Assert.Equal(4.25m, a.Price);

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, a.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, a.PreparationInformation.Count());
        }

        /// <summary>
        /// This tests that the price after choosing different sizes or flavors is correct
        /// </summary>
        /// <param name="size">The size of the drink</param>
        /// <param name="flavor">The flavor of the drink</param>
        /// <param name="hasIce">Whether the drink has ice</param>
        /// <param name="expectedPrice">The expected price of the drink</param>
        [Theory]
        [InlineData(Size.Kids, Flavor.Lime, true, 2.00)]
        [InlineData(Size.Kids, Flavor.Tamarind, false, 2.50)]
        [InlineData(Size.Small, Flavor.Strawberry, false, 2.50)]
        [InlineData(Size.Small, Flavor.Tamarind, true, 3.00)]
        [InlineData(Size.Medium, Flavor.Limonada, true, 3.00)]
        [InlineData(Size.Medium, Flavor.Tamarind, false, 3.50)]
        [InlineData(Size.Large, Flavor.Cucumber, false, 3.75)]
        [InlineData(Size.Large, Flavor.Tamarind, true, 4.25)]
        public void AguaFrescaPriceIncreaseWithDifferentSizesAndFlavors(Size size, Flavor flavor, bool hasIce, decimal expectedPrice)
        {
            AguaFresca a = new AguaFresca();

            a.DrinkSize = size;
            a.DrinkFlavor = flavor;
            a.Ice = hasIce;

            Assert.Equal(expectedPrice, a.Price);
        }

        /// <summary>
        /// This tests that the number of calories after choosing different sizes, flavors, or whether the drink has ice is correct
        /// </summary>
        /// <param name="size">The size of the drink</param>
        /// <param name="flavor">The flavor of the drink</param>
        /// <param name="hasIce">Whether the drink has ice</param>
        /// <param name="expectedCalories">The expected number of calories in the drink</param>
        [Theory]
        [InlineData(Size.Kids, Flavor.Lime, true, 75)]
        [InlineData(Size.Kids, Flavor.Strawberry, false, 96)]
        [InlineData(Size.Kids, Flavor.Cucumber, true, 45)]
        [InlineData(Size.Small, Flavor.Limonada, false, 101)]
        [InlineData(Size.Small, Flavor.Tamarind, true, 112)]
        [InlineData(Size.Small, Flavor.Cucumber, false, 63)]
        [InlineData(Size.Medium, Flavor.Lime, true, 125)]
        [InlineData(Size.Medium, Flavor.Strawberry, false, 160)]
        [InlineData(Size.Medium, Flavor.Cucumber, true, 75)]
        [InlineData(Size.Large, Flavor.Limonada, false, 202)]
        [InlineData(Size.Large, Flavor.Strawberry, true, 225)]
        [InlineData(Size.Large, Flavor.Cucumber, false, 127)]
        public void AguaFrescaCalorieCountWithDifferentSizes(Size size, Flavor flavor, bool hasIce, uint expectedCalories)
        {
            AguaFresca a = new AguaFresca();

            a.DrinkSize = size;
            a.DrinkFlavor = flavor;
            a.Ice = hasIce;

            Assert.Equal((uint)expectedCalories, a.Calories);
        }

        /// <summary>
        /// This tests that the order output after choosing different sizes, flavors, or whether the drink has ice is correct
        /// </summary>
        /// <param name="size">The size of the drink</param>
        /// <param name="flavor">The flavor of the drink</param>
        /// <param name="hasIce">Whether the drink has ice</param>
        /// <param name="expectedOrderOutput">The expected order output for the drink</param>
        [Theory]
        [InlineData(Size.Kids, Flavor.Lime, true, new string[] { "Kids", "Lime"})]
        [InlineData(Size.Kids, Flavor.Strawberry, false, new string[] { "Kids", "Strawberry", "Hold Ice"})]
        [InlineData(Size.Kids, Flavor.Cucumber, true, new string[] {"Kids", "Cucumber"})]
        [InlineData(Size.Small, Flavor.Limonada, false, new string[] {"Small", "Limonada", "Hold Ice"})]
        [InlineData(Size.Small, Flavor.Tamarind, true, new string[] {"Small", "Tamarind"})]
        [InlineData(Size.Small, Flavor.Cucumber, false, new string[] {"Small", "Cucumber", "Hold Ice"})]
        [InlineData(Size.Medium, Flavor.Lime, true, new string[] {"Medium", "Lime"})]
        [InlineData(Size.Medium, Flavor.Strawberry, false, new string[] {"Medium", "Strawberry", "Hold Ice"})]
        [InlineData(Size.Medium, Flavor.Cucumber, true, new string[] {"Medium", "Cucumber"})]
        [InlineData(Size.Large, Flavor.Limonada, false, new string[] {"Large", "Limonada", "Hold Ice"})]
        [InlineData(Size.Large, Flavor.Strawberry, true, new string[] {"Large", "Strawberry"})]
        [InlineData(Size.Large, Flavor.Cucumber, false, new string[] {"Large", "Cucumber", "Hold Ice"})]
        public void AguaFrescaPreparationInfoWithDifferentSizes(Size size, Flavor flavor, bool hasIce, string[] expectedOrderOutput)
        {
            AguaFresca a = new AguaFresca();

            a.DrinkSize = size;
            a.DrinkFlavor = flavor;
            a.Ice = hasIce;

            //Checks that each expected string is in the actual preparation info
            Assert.All(expectedOrderOutput, word => Assert.Contains(word, a.PreparationInformation));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(expectedOrderOutput.Length, a.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that AguaFresca can be cast as an IMenuItem, Drink, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            AguaFresca a = new AguaFresca();

            Assert.IsAssignableFrom<IMenuItem>(a);
            Assert.IsAssignableFrom<Drink>(a);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(a);
        }


        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing the flavor of the drink
        /// </summary>
        /// <param name="flavor">The flavor to change to</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(Flavor.Lime, "DrinkFlavor")]
        [InlineData(Flavor.Limonada, "DrinkFlavor")]
        [InlineData(Flavor.Tamarind, "DrinkFlavor")]
        [InlineData(Flavor.Cucumber, "DrinkFlavor")]
        [InlineData(Flavor.Strawberry, "DrinkFlavor")]
        [InlineData(Flavor.Lime, "Price")]
        [InlineData(Flavor.Limonada, "Price")]
        [InlineData(Flavor.Tamarind, "Price")]
        [InlineData(Flavor.Cucumber, "Price")]
        [InlineData(Flavor.Strawberry, "Price")]
        [InlineData(Flavor.Lime, "Calories")]
        [InlineData(Flavor.Limonada, "Calories")]
        [InlineData(Flavor.Tamarind, "Calories")]
        [InlineData(Flavor.Cucumber, "Calories")]
        [InlineData(Flavor.Strawberry, "Calories")]
        [InlineData(Flavor.Lime, "PreparationInformation")]
        [InlineData(Flavor.Limonada, "PreparationInformation")]
        [InlineData(Flavor.Tamarind, "PreparationInformation")]
        [InlineData(Flavor.Cucumber, "PreparationInformation")]
        [InlineData(Flavor.Strawberry, "PreparationInformation")]
        public void ChangingFlavorShouldNotifyOfPropertyChanges(Flavor flavor, string propertyName)
        {
            AguaFresca a = new AguaFresca();
            Assert.PropertyChanged(a, propertyName, () => {
                a.DrinkFlavor = flavor;
            });
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
            AguaFresca a = new AguaFresca();
            Assert.PropertyChanged(a, propertyName, () => {
                a.Ice = hasIce;
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
            AguaFresca a = new AguaFresca();
            Assert.PropertyChanged(a, propertyName, () => {
                a.DrinkSize = size;
            });
        }

        /// <summary>
        /// Tests that two AguaFrescas with identical properties are equal to each other
        /// </summary>
        /// <param name="flavor1">The flavor of the first drink</param>
        /// <param name="size1">The size of the first drink</param>
        /// <param name="flavor2">The flavor of the second drink</param>
        /// <param name="size2">The size of the second drink</param>
        [Theory]
        [InlineData(Flavor.Tamarind, Size.Small, Flavor.Tamarind, Size.Small )]
        [InlineData(Flavor.Lime, Size.Kids, Flavor.Lime, Size.Kids)]
        [InlineData(Flavor.Limonada, Size.Large, Flavor.Limonada, Size.Large)]
        [InlineData(Flavor.Strawberry, Size.Medium, Flavor.Strawberry, Size.Medium)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(Flavor flavor1, Size size1, Flavor flavor2, Size size2)
        {
            AguaFresca a1 = new AguaFresca() { DrinkFlavor = flavor1, DrinkSize = size1 };
            AguaFresca a2 = new AguaFresca() { DrinkFlavor = flavor2, DrinkSize = size2 };
            Assert.True(a1.Equals(a2));
        }

        /// <summary>
        /// Tests that two AguaFrescas with different properties are not equal to each other
        /// </summary>
        /// <param name="flavor1">The flavor of the first drink</param>
        /// <param name="size1">The size of the first drink</param>
        /// <param name="flavor2">The flavor of the second drink</param>
        /// <param name="size2">The size of the second drink</param>
        [Theory]
        [InlineData(Flavor.Lime, Size.Small, Flavor.Tamarind, Size.Small)]
        [InlineData(Flavor.Tamarind, Size.Kids, Flavor.Lime, Size.Small)]
        [InlineData(Flavor.Cucumber, Size.Large, Flavor.Limonada, Size.Large)]
        [InlineData(Flavor.Lime, Size.Small, Flavor.Limonada, Size.Medium)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(Flavor flavor1, Size size1, Flavor flavor2, Size size2)
        {
            AguaFresca a1 = new AguaFresca() { DrinkFlavor = flavor1, DrinkSize = size1 };
            AguaFresca a2 = new AguaFresca() { DrinkFlavor = flavor2, DrinkSize = size2 };
            Assert.False(a1.Equals(a2));
        }
    }
}
