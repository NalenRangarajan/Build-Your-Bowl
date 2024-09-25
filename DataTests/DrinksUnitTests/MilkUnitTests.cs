using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the Milk class
    /// </summary>
    public class MilkUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void MilkDefault()
        {
            Milk m = new Milk();

            Assert.False(m.Chocolate);
            Assert.Equal(Size.Kids, m.DrinkSize);
            Assert.Equal(2.50m, m.Price);
            Assert.Equal((uint)200, m.Calories);

            string[] expectedOrderOutput = new string[] { "Kids"};

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, m.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, m.PreparationInformation.Count());
            Assert.Equal("Milk", m.ToString());
        }

        /// <summary>
        /// This tests that the number of calories after choosing whether the milk is plain or chocolate is correct
        /// </summary>>
        /// <param name="isChocolate">Whether the milk is chocolate</param>
        /// <param name="expectedCalories">The expected number of calories in the drink</param>
        [Theory]
        [InlineData(false, 200)]
        [InlineData(true, 270)]
        public void MilkCalorieCountWithPlainOrChocolate(bool isChocolate, uint expectedCalories)
        {
            Milk m = new Milk();

            m.Chocolate = isChocolate;

            Assert.Equal((uint)expectedCalories, m.Calories);
        }

        /// <summary>
        /// This tests that the order output after choosing whether the milk is plain or chocolate is correct
        /// </summary>
        /// <param name="isChocolate">Whether the milk is chocolate</param>
        /// <param name="expectedOrderOutput">The expected order output for the drink</param>
        [Theory]
        [InlineData(false, new string[] { "Kids" })]
        [InlineData(true, new string[] { "Kids", "Chocolate" })]
        public void MilkPreparationInfoWithDifferentSizes(bool isChocolate, string[] expectedOrderOutput)
        {
            Milk m = new Milk();

            m.Chocolate = isChocolate;

            //Checks that each expected string is in the actual preparation info
            Assert.All(expectedOrderOutput, word => Assert.Contains(word, m.PreparationInformation));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(expectedOrderOutput.Length, m.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that Milk can be cast as an IMenuItem, Drink, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            Milk m = new Milk();

            Assert.IsAssignableFrom<IMenuItem>(m);
            Assert.IsAssignableFrom<Drink>(m);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(m);
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing whether the milk is chocolate
        /// </summary>
        /// <param name="isChocolate">Whether the drink is chocolate</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(true, "Chocolate")]
        [InlineData(false, "Chocolate")]
        [InlineData(true, "Calories")]
        [InlineData(false, "Calories")]
        [InlineData(true, "PreparationInformation")]
        [InlineData(false, "PreparationInformation")]
        public void ChangingIceShouldNotifyOfPropertyChanges(bool isChocolate, string propertyName)
        {
            Milk m = new Milk();
            Assert.PropertyChanged(m, propertyName, () => {
                m.Chocolate = isChocolate;
            });
        }

        /// <summary>
        /// Tests that two Milks with identical properties are equal to each other
        /// </summary>
        /// <param name="chocolate1">Whether the first drink has chocolate</param>
        /// <param name="chocolate2">Whether the second drink has chocolate</param>
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(bool chocolate1, bool chocolate2)
        {
            Milk m1 = new Milk() { Chocolate = chocolate1 };
            Milk m2 = new Milk() { Chocolate = chocolate2 };

            Assert.True(m1.Equals(m2));
        }

        /// <summary>
        /// Tests that two Milks with different properties are not equal to each other
        /// </summary>
        /// <param name="chocolate1">Whether the first drink has chocolate</param>
        /// <param name="chocolate2">Whether the second drink has chocolate</param>
        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(bool chocolate1, bool chocolate2)
        {
            Milk m1 = new Milk() { Chocolate = chocolate1 };
            Milk m2 = new Milk() { Chocolate = chocolate2 };

            Assert.False(m1.Equals(m2));
        }
    }
}
