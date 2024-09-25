using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the CornDogBitesMeal class
    /// </summary>
    public class CornDogBitesUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void CornDogBitesMealDefault()
        {
            CornDogBitesMeal c = new CornDogBitesMeal();

            Assert.Equal((uint)5, c.ItemCount);
            Assert.True(c.DrinkChoice is Milk);
            //We know drink is milk so we can cast as milk
            Assert.False(((c.DrinkChoice) as Milk)!.Chocolate);
            Assert.True(c.SideChoice is Fries);
            Assert.Equal(Size.Kids, c.SideChoice.SizeType);
            //We know side is fries so we can cast as fries
            Assert.False((c.SideChoice as Fries)!.Curly);
            Assert.Equal(5.99m, c.Price);
            Assert.Equal((uint)660, c.Calories);

            string[] expectedOrderOutput = new string[] { "Side: Fries", "\tKids", "Drink: Milk", "\tKids" };

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, c.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, c.PreparationInformation.Count());

            Assert.Equal("Corn Dog Bites Kids Meal", c.ToString());
        }

        /// <summary>
        /// This tests that the amount of corn dog bites correctly change inside the constraints
        /// </summary>
        /// <param name="bitesCountChange">The number of corn dog bites in the meal</param>
        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void CornDogBitesMealCountChangeInsideConstraints(uint bitesCountChange)
        {
            CornDogBitesMeal c = new CornDogBitesMeal();

            c.ItemCount = bitesCountChange;

            Assert.Equal(bitesCountChange, c.ItemCount);
        }

        /// <summary>
        /// This tests that if the count is changed to a number outside the constraints then the count will be unchanged
        /// </summary>
        /// <param name="bitesCountChange">The initial corn dog bites count change which is inside the constraints</param>
        /// <param name="outsideConstraintBitesCount">The subsequent corn dog bites count change which is a number outside the constraints</param>
        [Theory]
        [InlineData(5, 1)]
        [InlineData(5, 10)]
        [InlineData(6, 4)]
        [InlineData(6, 15)]
        [InlineData(7, 9)]
        [InlineData(7, 3)]
        [InlineData(8, 125)]
        [InlineData(8, 2)]
        public void CornDogBitesMealCountChangeOutsideConstraints(uint bitesCountChange, uint outsideConstraintBitesCount)
        {
            CornDogBitesMeal c = new CornDogBitesMeal();

            c.ItemCount = bitesCountChange;

            c.ItemCount = outsideConstraintBitesCount;

            Assert.Equal(bitesCountChange, c.ItemCount);
        }

        /// <summary>
        /// This tests that the price of the meal after changing the amount of corn dog bites, or size of the side or drink is correct
        /// </summary>
        /// <param name="bitesCount">The amount of corn dog bites in the meal</param>
        /// <param name="sideSize">The size of the side to be included</param>
        /// <param name="drinkSize">The size of the drink to be included</param>
        /// <param name="expectedPrice">The expected price of the meal</param>
        [Theory]
        [InlineData(5, Size.Kids, Size.Large, 7.49)]
        [InlineData(5, Size.Small, Size.Medium, 7.49)]
        [InlineData(5, Size.Medium, Size.Small, 7.49)]
        [InlineData(5, Size.Large, Size.Kids, 7.49)]
        [InlineData(6, Size.Kids, Size.Large, 8.24)]
        [InlineData(6, Size.Small, Size.Medium, 8.24)]
        [InlineData(6, Size.Medium, Size.Small, 8.24)]
        [InlineData(6, Size.Large, Size.Kids, 8.24)]
        [InlineData(7, Size.Kids, Size.Large, 8.99)]
        [InlineData(7, Size.Small, Size.Medium, 8.99)]
        [InlineData(7, Size.Medium, Size.Small, 8.99)]
        [InlineData(7, Size.Large, Size.Kids, 8.99)]
        [InlineData(8, Size.Kids, Size.Large, 9.74)]
        [InlineData(8, Size.Small, Size.Medium, 9.74)]
        [InlineData(8, Size.Medium, Size.Small, 9.74)]
        [InlineData(8, Size.Large, Size.Kids, 9.74)]
        public void CornDogBitesMealPriceIncrease(uint bitesCount, Size sideSize, Size drinkSize, decimal expectedPrice)
        {
            CornDogBitesMeal c = new CornDogBitesMeal();

            c.ItemCount = bitesCount;
            c.SideChoice = new MockSide() { SizeType = sideSize };
            c.DrinkChoice = new MockDrink() { DrinkSize = drinkSize };

            Assert.Equal(expectedPrice, c.Price);
        }

        /// <summary>
        /// This tests that the calorie count for the meal after changing the amount of corn dog bites, or size of the side is correct
        /// </summary>
        /// <param name="bitesCount">The amount of corn dog bites in the meal</param>
        /// <param name="sideSize">The size of the side to be included</param>
        /// <param name="drinkSize">The size of the drink to be included</param>
        /// <param name="expectedCalories">The expected calorie count for the meal</param>
        [Theory]
        [InlineData(5, Size.Kids, Size.Large, 880)]
        [InlineData(5, Size.Small, Size.Medium, 775)]
        [InlineData(5, Size.Medium, Size.Small, 775)]
        [InlineData(5, Size.Large, Size.Kids, 880)]
        [InlineData(6, Size.Kids, Size.Large, 930)]
        [InlineData(6, Size.Small, Size.Medium, 825)]
        [InlineData(6, Size.Medium, Size.Small, 825)]
        [InlineData(6, Size.Large, Size.Kids, 930)]
        [InlineData(7, Size.Kids, Size.Large, 980)]
        [InlineData(7, Size.Small, Size.Medium, 875)]
        [InlineData(7, Size.Medium, Size.Small, 875)]
        [InlineData(7, Size.Large, Size.Kids, 980)]
        [InlineData(8, Size.Kids, Size.Large, 1030)]
        [InlineData(8, Size.Small, Size.Medium, 925)]
        [InlineData(8, Size.Medium, Size.Small, 925)]
        [InlineData(8, Size.Large, Size.Kids, 1030)]
        public void CornDogBitesMealCalorieCount(uint bitesCount, Size sideSize, Size drinkSize, uint expectedCalories)
        {
            CornDogBitesMeal c = new CornDogBitesMeal();

            c.ItemCount = bitesCount;
            c.SideChoice = new MockSide() { SizeType = sideSize };
            c.DrinkChoice = new MockDrink() { DrinkSize = drinkSize };

            Assert.Equal(expectedCalories, c.Calories);
        }

        /// <summary>
        /// This tests that the order output after choosing a certain number of corn dog bites is correct
        /// </summary>
        /// <param name="bitesCount">The number of corn dog bites in the meal</param>
        /// <param name="sideSize">The size of the side to be included</param>
        /// <param name="drinkSize">The size of the drink to be included</param>
        /// <param name="expectedOrderOutput">The expected order output for the meal</param>
        [Theory]
        [InlineData(5, Size.Kids, Size.Small, new string[] { "Side: Mock Side", "\tKids", "Drink: Mock Drink", "\tSmall" })]
        [InlineData(5, Size.Small, Size.Small, new string[] { "Side: Mock Side", "\tSmall", "Drink: Mock Drink", "\tSmall" })]
        [InlineData(6, Size.Small, Size.Medium, new string[] { "6 Bites", "Side: Mock Side", "\tSmall", "Drink: Mock Drink", "\tMedium" })]
        [InlineData(6, Size.Medium, Size.Medium, new string[] { "6 Bites", "Side: Mock Side", "\tMedium", "Drink: Mock Drink", "\tMedium" })]
        [InlineData(7, Size.Medium, Size.Large, new string[] { "7 Bites", "Side: Mock Side", "\tMedium", "Drink: Mock Drink", "\tLarge" })]
        [InlineData(7, Size.Large, Size.Large, new string[] { "7 Bites", "Side: Mock Side", "\tLarge", "Drink: Mock Drink", "\tLarge" })]
        [InlineData(8, Size.Large, Size.Kids, new string[] { "8 Bites", "Side: Mock Side", "\tLarge", "Drink: Mock Drink", "\tKids" })]
        [InlineData(8, Size.Kids, Size.Kids, new string[] { "8 Bites", "Side: Mock Side", "\tKids", "Drink: Mock Drink", "\tKids" })]
        public void CornDogBitesMealPreparationInfo(uint bitesCount, Size sideSize, Size drinkSize, string[] expectedOrderOutput)
        {
            CornDogBitesMeal c = new CornDogBitesMeal();
            c.ItemCount = bitesCount;
            c.SideChoice = new MockSide() { SizeType = sideSize };
            c.DrinkChoice = new MockDrink() { DrinkSize = drinkSize };

            //Checks that each expected string is in the actual preparation info
            Assert.All(expectedOrderOutput, word => Assert.Contains(word, c.PreparationInformation));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(expectedOrderOutput.Length, c.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that CornDogBitesMeal can be cast as an IMenuItem, KidsMeal, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            CornDogBitesMeal c = new CornDogBitesMeal();

            Assert.IsAssignableFrom<IMenuItem>(c);
            Assert.IsAssignableFrom<KidsMeal>(c);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(c);
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing the size of the drink
        /// </summary>
        /// <param name="size">The size to change to</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(Size.Kids, "DrinkChoice")]
        [InlineData(Size.Small, "DrinkChoice")]
        [InlineData(Size.Medium, "DrinkChoice")]
        [InlineData(Size.Large, "DrinkChoice")]
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
        public void ChangingDrinkSizeShouldNotifyOfPropertyChanges(Size size, string propertyName)
        {
            CornDogBitesMeal c = new CornDogBitesMeal();
            MockDrink d = new MockDrink();

            //create mockdrink outside and do similar assert
            Assert.PropertyChanged(c, propertyName, () => {
                if (propertyName == "DrinkChoice")
                {
                    d.DrinkSize = size;
                    c.DrinkChoice = d;
                }
                else
                {
                    Assert.PropertyChanged(d, propertyName, () => {
                        d.DrinkSize = size;
                    });

                    c.DrinkChoice = d;
                }
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing the size of the side
        /// </summary>
        /// <param name="size">The size to change to</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(Size.Kids, "SideChoice")]
        [InlineData(Size.Small, "SideChoice")]
        [InlineData(Size.Medium, "SideChoice")]
        [InlineData(Size.Large, "SideChoice")]
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
        public void ChangingSideSizeShouldNotifyOfPropertyChanges(Size size, string propertyName)
        {
            CornDogBitesMeal c = new CornDogBitesMeal();
            MockSide s = new MockSide();

            //create mockdrink outside and do similar assert
            Assert.PropertyChanged(c, propertyName, () => {
                if (propertyName == "SideChoice")
                {
                    s.SizeType = size;
                    c.SideChoice = s;
                }
                else
                {
                    Assert.PropertyChanged(s, propertyName, () => {
                        s.SizeType = size;
                    });

                    c.SideChoice = s;
                }
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing the count
        /// </summary>
        /// <param name="count">The count to change to</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(5, "ItemCount")]
        [InlineData(6, "ItemCount")]
        [InlineData(7, "ItemCount")]
        [InlineData(8, "ItemCount")]
        [InlineData(5, "Price")]
        [InlineData(6, "Price")]
        [InlineData(7, "Price")]
        [InlineData(8, "Price")]
        [InlineData(5, "Calories")]
        [InlineData(6, "Calories")]
        [InlineData(7, "Calories")]
        [InlineData(8, "Calories")]
        [InlineData(5, "PreparationInformation")]
        [InlineData(6, "PreparationInformation")]
        [InlineData(7, "PreparationInformation")]
        [InlineData(8, "PreparationInformation")]
        public void ChangingCountShouldNotifyOfPropertyChanges(uint count, string propertyName)
        {
            CornDogBitesMeal c = new CornDogBitesMeal(); 

            //create mockdrink outside and do similar assert
            Assert.PropertyChanged(c, propertyName, () => {
                c.ItemCount = count;
            });
        }

        /// <summary>
        /// Tests that two CornDogBitesMeals with identical properties are equal to each other
        /// </summary>
        /// <param name="count1">The number of bites in the first meal</param>
        /// <param name="count2">The number of bites in the second meal</param>
        /// <param name="drinkSize1">The size of the first drink</param>
        /// <param name="drinkSize2">The size of the second drink</param>
        /// <param name="sideSize1">The size of the first side</param>
        /// <param name="sideSize2">The size of the second side</param>
        [Theory]
        [InlineData(5, 5, Size.Kids, Size.Kids, Size.Small, Size.Small)]
        [InlineData(6, 6, Size.Small, Size.Small, Size.Medium, Size.Medium)]
        [InlineData(7, 7, Size.Medium, Size.Medium, Size.Large, Size.Large)]
        [InlineData(8, 8, Size.Large, Size.Large, Size.Kids, Size.Kids)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(uint count1, uint count2, Size drinkSize1, Size drinkSize2, Size sideSize1, Size sideSize2)
        {
            MockSide side1 = new MockSide() { SizeType = sideSize1 };
            MockSide side2 = new MockSide() { SizeType = sideSize2 };
            MockDrink drink1 = new MockDrink() { DrinkSize = drinkSize1 };
            MockDrink drink2 = new MockDrink() { DrinkSize = drinkSize2 };

            CornDogBitesMeal c1 = new CornDogBitesMeal() { ItemCount = count1, SideChoice = side1, DrinkChoice = drink1 };
            CornDogBitesMeal c2 = new CornDogBitesMeal() { ItemCount = count2, SideChoice = side2, DrinkChoice = drink2 };

            Assert.True(c1.Equals(c2));
        }

        /// <summary>
        /// Tests that two CornDogBitesMeals with different properties are not equal to each other
        /// </summary>
        /// <param name="count1">The number of bites in the first meal</param>
        /// <param name="count2">The number of bites in the second meal</param>
        /// <param name="drinkSize1">The size of the first drink</param>
        /// <param name="drinkSize2">The size of the second drink</param>
        /// <param name="sideSize1">The size of the first side</param>
        /// <param name="sideSize2">The size of the second side</param>
        [Theory]
        [InlineData(5, 6, Size.Kids, Size.Kids, Size.Small, Size.Small)]
        [InlineData(7, 6, Size.Small, Size.Kids, Size.Medium, Size.Large)]
        [InlineData(7, 5, Size.Large, Size.Medium, Size.Kids, Size.Large)]
        [InlineData(8, 8, Size.Large, Size.Kids, Size.Small, Size.Kids)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNiotEqual(uint count1, uint count2, Size drinkSize1, Size drinkSize2, Size sideSize1, Size sideSize2)
        {
            MockSide side1 = new MockSide() { SizeType = sideSize1 };
            MockSide side2 = new MockSide() { SizeType = sideSize2 };
            MockDrink drink1 = new MockDrink() { DrinkSize = drinkSize1 };
            MockDrink drink2 = new MockDrink() { DrinkSize = drinkSize2 };

            CornDogBitesMeal c1 = new CornDogBitesMeal() { ItemCount = count1, SideChoice = side1, DrinkChoice = drink1 };
            CornDogBitesMeal c2 = new CornDogBitesMeal() { ItemCount = count2, SideChoice = side2, DrinkChoice = drink2 };

            Assert.False(c1.Equals(c2));
        }
    }
}
