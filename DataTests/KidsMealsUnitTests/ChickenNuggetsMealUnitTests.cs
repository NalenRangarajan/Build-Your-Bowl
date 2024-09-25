using System.ComponentModel;
using System.Diagnostics;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the ChickenNuggetsMeal class
    /// </summary>
    public class ChickenNuggetsMealUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void ChickenNuggetsMealDefault()
        {
            ChickenNuggetsMeal c = new ChickenNuggetsMeal();

            Assert.Equal((uint)5, c.ItemCount);
            Assert.True(c.DrinkChoice is Milk);
            //We know drink is milk so we can cast as milk
            Assert.False(((c.DrinkChoice) as Milk)!.Chocolate);
            Assert.True(c.SideChoice is Fries );
            Assert.Equal(Size.Kids, c.SideChoice.SizeType);
            //We know side is fries so we can cast as fries
            Assert.False((c.SideChoice as Fries)!.Curly);
            Assert.Equal(5.99m, c.Price);
            Assert.Equal((uint)710, c.Calories);

            string[] expectedOrderOutput = new string[] { "Side: Fries", "\tKids", "Drink: Milk", "\tKids"};

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, c.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, c.PreparationInformation.Count());

            Assert.Equal("Chicken Nuggets Kids Meal", c.ToString());
        }

        /// <summary>
        /// This tests that the amount of nuggets correctly change inside the constraints
        /// </summary>
        /// <param name="nuggetCountChange">The number of nuggets in the meal</param>
        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void ChickenNuggetsMealCountChangeInsideConstraints(uint nuggetCountChange)
        {
            ChickenNuggetsMeal c = new ChickenNuggetsMeal();

            c.ItemCount = nuggetCountChange;

            Assert.Equal(nuggetCountChange, c.ItemCount);
        }

        /// <summary>
        /// This tests that if the count is changed to a number outside the constraint then the count will be unchanged
        /// </summary>
        /// <param name="nuggetCountChange">The initial nugget count change which is inside the constraints</param>
        /// <param name="outsideConstraintNuggetCount">The subsequent nugget count change which is a number outside the constraints</param>
        [Theory]
        [InlineData(5, 1)]
        [InlineData(5, 10)]
        [InlineData(6, 4)]
        [InlineData(6, 15)]
        [InlineData(7, 9)]
        [InlineData(7, 3)]
        [InlineData(8, 125)]
        [InlineData(8, 2)]
        public void ChickenNuggetsMealCountChangeOutsideConstraints(uint nuggetCountChange, uint outsideConstraintNuggetCount)
        {
            ChickenNuggetsMeal c = new ChickenNuggetsMeal();

            c.ItemCount = nuggetCountChange;

            c.ItemCount = outsideConstraintNuggetCount;

            Assert.Equal(nuggetCountChange, c.ItemCount);
        }

        /// <summary>
        /// This tests that the price of the meal after changing the amount of nuggets, or size of the side or drink is correct
        /// </summary>
        /// <param name="nuggetCount">The amount of nuggets in the meal</param>
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
        [InlineData(8, Size.Kids, Size.Large,  9.74)]
        [InlineData(8, Size.Small, Size.Medium, 9.74)]
        [InlineData(8, Size.Medium, Size.Small, 9.74)]
        [InlineData(8, Size.Large, Size.Kids, 9.74)]
        public void ChickenNuggetsMealPriceIncrease(uint nuggetCount, Size sideSize, Size drinkSize, decimal expectedPrice)
        {
            ChickenNuggetsMeal c = new ChickenNuggetsMeal();

            c.ItemCount = nuggetCount;
            c.SideChoice = new MockSide() { SizeType = sideSize };
            c.DrinkChoice = new MockDrink() { DrinkSize = drinkSize };

            Assert.Equal(expectedPrice, c.Price);
        }

        /// <summary>
        /// This tests that the calorie count for the meal after changing the amount of nuggets, or size of the side is correct
        /// </summary>
        /// <param name="nuggetCount">The amount of nuggets in the meal</param>
        /// <param name="sideSize">The size of the side to be included</param>
        /// <param name="drinkSize">The size of the drink to be included</param>
        /// <param name="expectedCalories">The expected calorie count for the meal</param>
        [Theory]
        [InlineData(5, Size.Kids, Size.Large, 930)]
        [InlineData(5, Size.Small, Size.Medium, 825)]
        [InlineData(5, Size.Medium, Size.Small, 825)]
        [InlineData(5, Size.Large, Size.Kids, 930)]
        [InlineData(6, Size.Kids, Size.Large, 990)]
        [InlineData(6, Size.Small, Size.Medium, 885)]
        [InlineData(6, Size.Medium, Size.Small, 885)]
        [InlineData(6, Size.Large, Size.Kids, 990)]
        [InlineData(7, Size.Kids, Size.Large, 1050)]
        [InlineData(7, Size.Small, Size.Medium, 945)]
        [InlineData(7, Size.Medium, Size.Small, 945)]
        [InlineData(7, Size.Large, Size.Kids, 1050)]
        [InlineData(8, Size.Kids, Size.Large, 1110)]
        [InlineData(8, Size.Small, Size.Medium, 1005)]
        [InlineData(8, Size.Medium, Size.Small, 1005)]
        [InlineData(8, Size.Large, Size.Kids, 1110)]
        public void ChickenNuggetsMealCalorieCount(uint nuggetCount, Size sideSize, Size drinkSize, uint expectedCalories)
        {
            ChickenNuggetsMeal c = new ChickenNuggetsMeal();

            c.ItemCount = nuggetCount;
            c.SideChoice = new MockSide() { SizeType = sideSize };
            c.DrinkChoice = new MockDrink() { DrinkSize = drinkSize };

            Assert.Equal(expectedCalories, c.Calories);
        }

        /// <summary>
        /// This tests that the order output after choosing a certain number of nuggets is correct
        /// </summary>
        /// <param name="nuggetCount">The number of nuggets in the meal</param>
        /// <param name="sideSize">The size of the side to be included</param>
        /// <param name="drinkSize">The size of the drink to be included</param>
        /// <param name="expectedOrderOutput">The expected order output for the meal</param>
        [Theory]
        [InlineData(5, Size.Kids, Size.Small, new string[] { "Side: Mock Side", "\tKids", "Drink: Mock Drink", "\tSmall"})]
        [InlineData(5, Size.Small, Size.Small, new string[] { "Side: Mock Side", "\tSmall", "Drink: Mock Drink", "\tSmall" })]
        [InlineData(6, Size.Small, Size.Medium, new string[] { "6 Nuggets", "Side: Mock Side", "\tSmall", "Drink: Mock Drink", "\tMedium" })]
        [InlineData(6, Size.Medium, Size.Medium, new string[] { "6 Nuggets", "Side: Mock Side", "\tMedium", "Drink: Mock Drink", "\tMedium" })]
        [InlineData(7, Size.Medium, Size.Large, new string[] { "7 Nuggets", "Side: Mock Side", "\tMedium", "Drink: Mock Drink", "\tLarge" })]
        [InlineData(7, Size.Large, Size.Large, new string[] { "7 Nuggets", "Side: Mock Side", "\tLarge", "Drink: Mock Drink", "\tLarge" })]
        [InlineData(8, Size.Large, Size.Kids, new string[] { "8 Nuggets", "Side: Mock Side", "\tLarge", "Drink: Mock Drink", "\tLarge" })]
        [InlineData(8, Size.Kids, Size.Kids, new string[] { "8 Nuggets", "Side: Mock Side", "\tKids", "Drink: Mock Drink", "\tKids" })]
        public void ChickenNuggetsMealPreparationInfo(uint nuggetCount, Size sideSize, Size drinkSize, string[] expectedOrderOutput)
        {
            ChickenNuggetsMeal c = new ChickenNuggetsMeal();
            c.ItemCount = nuggetCount;
            c.SideChoice = new MockSide() { SizeType = sideSize };
            c.DrinkChoice = new MockDrink() { DrinkSize = drinkSize };

            //Checks that each expected string is in the actual preparation info
            Assert.All(expectedOrderOutput, word => Assert.Contains(word, c.PreparationInformation));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(expectedOrderOutput.Length, c.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that ChickenNuggetsMeal can be cast as an IMenuItem, KidsMeal, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            ChickenNuggetsMeal c = new ChickenNuggetsMeal();

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
            ChickenNuggetsMeal c = new ChickenNuggetsMeal();
            MockDrink d = new MockDrink();

            //create mockdrink outside and do similar assert
            Assert.PropertyChanged(c, propertyName, () => {
                if(propertyName == "DrinkChoice")
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
            ChickenNuggetsMeal c = new ChickenNuggetsMeal();
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
            ChickenNuggetsMeal c = new ChickenNuggetsMeal();

            //create mockdrink outside and do similar assert
            Assert.PropertyChanged(c, propertyName, () => {
                c.ItemCount = count;
            });
        }

        /// <summary>
        /// Tests that two ChickenNuggetMeals with identical properties are equal to each other
        /// </summary>
        /// <param name="count1">The number of nuggets in the first meal</param>
        /// <param name="count2">The number of nuggets in the second meal</param>
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

            ChickenNuggetsMeal c1 = new ChickenNuggetsMeal() { ItemCount = count1, SideChoice = side1, DrinkChoice = drink1 };
            ChickenNuggetsMeal c2 = new ChickenNuggetsMeal() { ItemCount = count2, SideChoice = side2, DrinkChoice = drink2 };

            Assert.True(c1.Equals(c2));
        }

        /// <summary>
        /// Tests that two ChickenNuggetMeals with different properties are not equal to each other
        /// </summary>
        /// <param name="count1">The number of nuggets in the first meal</param>
        /// <param name="count2">The number of nuggets in the second meal</param>
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

            ChickenNuggetsMeal c1 = new ChickenNuggetsMeal() { ItemCount = count1, SideChoice = side1, DrinkChoice = drink1 };
            ChickenNuggetsMeal c2 = new ChickenNuggetsMeal() { ItemCount = count2, SideChoice = side2, DrinkChoice = drink2 };

            Assert.False(c1.Equals(c2));
        }
    }
}
