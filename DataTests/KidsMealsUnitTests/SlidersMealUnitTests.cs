using System.ComponentModel;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the SlidersMeal class
    /// </summary>
    public class SlidersMealUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void SlidersMealDefault()
        {
            SlidersMeal s = new SlidersMeal();

            Assert.Equal((uint)2, s.ItemCount);
            Assert.True(s.AmericanCheese);
            Assert.True(s.DrinkChoice is Milk);
            //We know drink is milk so we can cast as milk
            Assert.False(((s.DrinkChoice) as Milk)!.Chocolate);
            Assert.True(s.SideChoice is Fries);
            Assert.Equal(Size.Kids, s.SideChoice.SizeType);
            //We know side is fries so we can cast as fries
            Assert.False((s.SideChoice as Fries)!.Curly);
            Assert.Equal(5.99m, s.Price);
            Assert.Equal((uint)710, s.Calories);

            string[] expectedOrderOutput = new string[] { "Side: Fries", "\tKids", "Drink: Milk", "\tKids" };

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, s.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, s.PreparationInformation.Count());

            Assert.Equal("Sliders Kids Meal", s.ToString());
        }

        //Specific Required Test Case
        /// <summary>
        /// This tests the calories, price, and expected order output of a specific Sliders meal
        /// </summary>
        [Fact]
        public void SpecificSlidersMeal()
        {
            SlidersMeal s = new SlidersMeal();
            s.SideChoice = new Fries() { SizeType = Size.Large };
            s.DrinkChoice = new Milk() { Chocolate = true};
            s.ItemCount = 3;
            s.AmericanCheese = false;

            string[] expectedOrderOutput = new string[] { "Hold American Cheese", "3 Sliders", "Side: Fries", "\tLarge", "Drink: Milk", "\tKids", "\tChocolate" };

            Assert.Equal((uint)1125, s.Calories);
            Assert.Equal(9.49m, s.Price);

            Assert.All(expectedOrderOutput, word => Assert.Contains(word, s.PreparationInformation));

            Assert.Equal(expectedOrderOutput.Length, s.PreparationInformation.Count());
        }

        /// <summary>
        /// This tests that the amount of sliders correctly change inside the constraints
        /// </summary>
        /// <param name="slidersCountChange">The number of sliders in the meal</param>
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void SlidersMealCountChangeInsideConstraints(uint slidersCountChange)
        {
            SlidersMeal s = new SlidersMeal();

            s.ItemCount = slidersCountChange;

            Assert.Equal(slidersCountChange, s.ItemCount);
        }

        /// <summary>
        /// This tests that if the count is changed to a number outside the constraints then the count will be unchanged
        /// </summary>
        /// <param name="slidersCountChange">The initial sliders count change which is inside the constraints</param>
        /// <param name="outsideConstraintsSidersCount">The subsequent sliders count change which is a number outside the constraints</param>
        [Theory]
        [InlineData(2, 1)]
        [InlineData(2, 7)]
        [InlineData(3, 0)]
        [InlineData(3, 5)]
        [InlineData(4, 1)]
        [InlineData(4, 9)]
        public void SlidersMealCountChangeOutsideConstraints(uint slidersCountChange, uint outsideConstraintsSidersCount)
        {
            SlidersMeal s = new SlidersMeal();

            s.ItemCount = slidersCountChange;

            s.ItemCount = outsideConstraintsSidersCount;

            Assert.Equal(slidersCountChange, s.ItemCount);
        }

        /// <summary>
        /// This tests that the price of the meal after changing the amount of sliders, or size of the side or drink is correct
        /// </summary>
        /// <param name="slidersCount">The amount of sliders in the meal</param>
        /// <param name="hasAmericanCheese">WHether the sliders have American Cheese</param>
        /// <param name="sideSize">The size of the side to be included</param>
        /// <param name="drinkSize">The size of the drink to be included</param>
        /// <param name="expectedPrice">The expected price of the meal</param>
        [Theory]
        [InlineData(2, true, Size.Kids, Size.Large, 7.49)]
        [InlineData(2, false, Size.Small, Size.Medium, 7.49)]
        [InlineData(2, true, Size.Medium, Size.Small, 7.49)]
        [InlineData(2, false, Size.Large, Size.Kids, 7.49)]
        [InlineData(3, true, Size.Kids, Size.Large, 9.49)]
        [InlineData(3, false, Size.Small, Size.Medium, 9.49)]
        [InlineData(3, true, Size.Medium, Size.Small, 9.49)]
        [InlineData(3, false, Size.Large, Size.Kids, 9.49)]
        [InlineData(4, true, Size.Kids, Size.Large, 11.49)]
        [InlineData(4, false, Size.Small, Size.Medium, 11.49)]
        [InlineData(4, true, Size.Medium, Size.Small, 11.49)]
        [InlineData(4, false, Size.Large, Size.Kids, 11.49)]
        public void SlidersMealPriceIncrease(uint slidersCount, bool hasAmericanCheese, Size sideSize, Size drinkSize, decimal expectedPrice)
        {
            SlidersMeal s = new SlidersMeal();

            s.ItemCount = slidersCount;
            s.SideChoice = new MockSide() { SizeType = sideSize };
            s.DrinkChoice = new MockDrink() { DrinkSize = drinkSize };
            s.AmericanCheese = hasAmericanCheese;

            Assert.Equal(expectedPrice, s.Price);
        }

        /// <summary>
        /// This tests that the calorie count for the meal after changing the amount of sliders, whether the sliders have American Cheese, 
        /// or size of the side or drink is correct
        /// </summary>
        /// <param name="slidersCount">The amount of sliders in the meal</param>
        /// <param name="hasAmericanCheese">Whether the sliders have American Cheese</param>
        /// <param name="sideSize">The size of the side to be included</param>
        /// <param name="drinkSize">The size of the drink to be included</param>
        /// <param name="expectedCalories">The expected calories in the meal</param>
        [Theory]
        [InlineData(2, true, Size.Kids, Size.Large, 930)]
        [InlineData(2, false, Size.Small, Size.Medium, 745)]
        [InlineData(2, true, Size.Medium, Size.Small, 825)]
        [InlineData(2, false, Size.Large, Size.Kids, 850)]
        [InlineData(3, true, Size.Kids, Size.Large, 1080)]
        [InlineData(3, false, Size.Small, Size.Medium, 855)]
        [InlineData(3, true, Size.Medium, Size.Small, 975)]
        [InlineData(3, false, Size.Large, Size.Kids, 960)]
        [InlineData(4, true, Size.Kids, Size.Large, 1230)]
        [InlineData(4, false, Size.Small, Size.Medium, 965)]
        [InlineData(4, true, Size.Medium, Size.Small, 1125)]
        [InlineData(4, false, Size.Large, Size.Kids, 1070)]
        public void SlidersMealCalorieCount(uint slidersCount, bool hasAmericanCheese, Size sideSize, Size drinkSize, uint expectedCalories)
        {
            SlidersMeal s = new SlidersMeal();
            s.AmericanCheese = hasAmericanCheese;
            s.ItemCount = slidersCount;
            s.SideChoice = new MockSide() { SizeType = sideSize };
            s.DrinkChoice = new MockDrink() { DrinkSize = drinkSize };

            Assert.Equal(expectedCalories, s.Calories);
        }

        /// <summary>
        /// This tests that the order output after choosing a certain number of sliders, and whether they have American Cheese is correct
        /// </summary>
        /// <param name="slidersCount">The number of slidersin the meal</param>
        /// <param name="hasAmericanCheese">Whether the sliders have American Cheese</param>
        /// <param name="sideSize">The size of the side to be included</param>
        /// <param name="drinkSize">The size of the drink to be included</param>
        /// <param name="expectedOrderOutput">The expected order output for the meal</param>
        [Theory]
        [InlineData(2, true, Size.Kids, Size.Large, new string[] { "Side: Mock Side", "\tKids", "Drink: Mock Drink", "\tLarge" })]
        [InlineData(3, true, Size.Small, Size.Medium, new string[] { "3 Sliders", "Side: Mock Side", "\tSmall", "Drink: Mock Drink", "\tMedium" })]
        [InlineData(4, true, Size.Medium, Size.Small, new string[] { "4 Sliders", "Side: Mock Side", "\tMedium", "Drink: Mock Drink", "\tSmall" })]
        [InlineData(2, false, Size.Large, Size.Kids, new string[] { "Hold American Cheese", "Side: Mock Side", "\tLarge", "Drink: Mock Drink", "\tKids" })]
        [InlineData(3, false, Size.Kids, Size.Large, new string[] { "3 Sliders", "Hold American Cheese", "Side: Mock Side", "\tKids", "Drink: Mock Drink", "\tLarge" })]
        [InlineData(4, false, Size.Small, Size.Medium, new string[] { "4 Sliders", "Hold American Cheese", "Side: Mock Side", "\tSmall", "Drink: Mock Drink", "\tMedium" })]
        [InlineData(2, false, Size.Medium, Size.Small, new string[] { "Hold American Cheese", "Side: Mock Side", "\tMedium", "Drink: Mock Drink", "\tSmall" })]
        [InlineData(3, false, Size.Large, Size.Kids, new string[] { "3 Sliders", "Hold American Cheese", "Side: Mock Side", "\tLarge", "Drink: Mock Drink", "\tKids" })]
        [InlineData(4, false, Size.Kids, Size.Large, new string[] { "4 Sliders", "Hold American Cheese", "Side: Mock Side", "\tKids", "Drink: Mock Drink", "\tLarge" })]
        public void SlidersMealPreparationInfo(uint slidersCount, bool hasAmericanCheese, Size sideSize, Size drinkSize, string[] expectedOrderOutput)
        {
            SlidersMeal s = new SlidersMeal();
            s.AmericanCheese = hasAmericanCheese;
            s.ItemCount = slidersCount;
            s.SideChoice = new MockSide() { SizeType = sideSize };
            s.DrinkChoice = new MockDrink() { DrinkSize = drinkSize };

            //Checks that each expected string is in the actual preparation info
            Assert.All(expectedOrderOutput, word => Assert.Contains(word, s.PreparationInformation));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(expectedOrderOutput.Length, s.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests that SlidersMeal can be cast as an IMenuItem, KidsMeal, and INotifyPropertyChanged
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            SlidersMeal s = new SlidersMeal();

            Assert.IsAssignableFrom<IMenuItem>(s);
            Assert.IsAssignableFrom<KidsMeal>(s);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(s);
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
            SlidersMeal s = new SlidersMeal();
            MockDrink d = new MockDrink();

            //create mockdrink outside and do similar assert
            Assert.PropertyChanged(s, propertyName, () => {
                if (propertyName == "DrinkChoice")
                {
                    d.DrinkSize = size;
                    s.DrinkChoice = d;
                }
                else
                {
                    Assert.PropertyChanged(d, propertyName, () => {
                        d.DrinkSize = size;
                    });

                    s.DrinkChoice = d;
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
            SlidersMeal sm = new SlidersMeal();
            MockSide s = new MockSide();

            //create mockdrink outside and do similar assert
            Assert.PropertyChanged(sm, propertyName, () => {
                if (propertyName == "SideChoice")
                {
                    s.SizeType = size;
                    sm.SideChoice = s;
                }
                else
                {
                    Assert.PropertyChanged(s, propertyName, () => {
                        s.SizeType = size;
                    });

                    sm.SideChoice = s;
                }
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing the count
        /// </summary>
        /// <param name="count">The count to change to</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(2, "ItemCount")]
        [InlineData(3, "ItemCount")]
        [InlineData(4, "ItemCount")]
        [InlineData(2, "Price")]
        [InlineData(3, "Price")]
        [InlineData(4, "Price")]
        [InlineData(2, "Calories")]
        [InlineData(3, "Calories")]
        [InlineData(4, "Calories")]
        [InlineData(2, "PreparationInformation")]
        [InlineData(3, "PreparationInformation")]
        [InlineData(4, "PreparationInformation")]
        public void ChangingCountShouldNotifyOfPropertyChanges(uint count, string propertyName)
        {
            SlidersMeal s = new SlidersMeal();

            //create mockdrink outside and do similar assert
            Assert.PropertyChanged(s, propertyName, () => {
                s.ItemCount = count;
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is correctly invoked when changing whether the sliders have cheese
        /// </summary>
        /// <param name="hasAmericanCheese">Whether the sliders have cheese</param>
        /// <param name="propertyName">The property that should be changing</param>
        [Theory]
        [InlineData(true, "AmericanCheese")]
        [InlineData(false, "AmericanCheese")]
        [InlineData(true, "Calories")]
        [InlineData(false, "Calories")]
        [InlineData(true, "PreparationInformation")]
        [InlineData(false, "PreparationInformation")]
        public void ChangingAmericanCheeseShouldNotifyOfPropertyChanges(bool hasAmericanCheese, string propertyName)
        {
            SlidersMeal s = new SlidersMeal();
            Assert.PropertyChanged(s, propertyName, () => {
                s.AmericanCheese = hasAmericanCheese;
            });
        }

        /// <summary>
        /// Tests that two SlidersMeals with identical properties are equal to each other
        /// </summary>
        /// <param name="count1">The number of bites in the first meal</param>
        /// <param name="count2">The number of bites in the second meal</param>
        /// <param name="drinkSize1">The size of the first drink</param>
        /// <param name="drinkSize2">The size of the second drink</param>
        /// <param name="sideSize1">The size of the first side</param>
        /// <param name="sideSize2">The size of the second side</param>
        /// <param name="cheese1">Whether the first meal's sliders have cheese</param>
        /// <param name="cheese2">Whether the second meal's sliders have cheese</param>
        [Theory]
        [InlineData(2, 2, Size.Kids, Size.Kids, Size.Small, Size.Small, false, false)]
        [InlineData(3, 3, Size.Small, Size.Small, Size.Medium, Size.Medium, true, true)]
        [InlineData(4, 4, Size.Medium, Size.Medium, Size.Large, Size.Large, false, false)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreEqual(uint count1, uint count2, Size drinkSize1, Size drinkSize2, Size sideSize1, Size sideSize2, bool cheese1, bool cheese2)
        {
            MockSide side1 = new MockSide() { SizeType = sideSize1 };
            MockSide side2 = new MockSide() { SizeType = sideSize2 };
            MockDrink drink1 = new MockDrink() { DrinkSize = drinkSize1 };
            MockDrink drink2 = new MockDrink() { DrinkSize = drinkSize2 };

            SlidersMeal s1 = new SlidersMeal() { ItemCount = count1, AmericanCheese = cheese1, SideChoice = side1, DrinkChoice = drink1 };
            SlidersMeal s2 = new SlidersMeal() { ItemCount = count2, AmericanCheese = cheese2, SideChoice = side2, DrinkChoice = drink2 };

            Assert.True(s1.Equals(s2));
        }

        /// <summary>
        /// Tests that two SlidersMeals with different properties are not equal to each other
        /// </summary>
        /// <param name="count1">The number of bites in the first meal</param>
        /// <param name="count2">The number of bites in the second meal</param>
        /// <param name="drinkSize1">The size of the first drink</param>
        /// <param name="drinkSize2">The size of the second drink</param>
        /// <param name="sideSize1">The size of the first side</param>
        /// <param name="sideSize2">The size of the second side</param>
        /// <param name="cheese1">Whether the first meal's sliders have cheese</param>
        /// <param name="cheese2">Whether the second meal's sliders have cheese</param>
        [Theory]
        [InlineData(5, 6, Size.Kids, Size.Kids, Size.Small, Size.Small, true, false)]
        [InlineData(7, 6, Size.Small, Size.Kids, Size.Medium, Size.Large, false, true)]
        [InlineData(7, 5, Size.Large, Size.Medium, Size.Kids, Size.Large, true, false)]
        [InlineData(8, 8, Size.Large, Size.Kids, Size.Small, Size.Kids, false, true)]
        public void EqualsCorrectlyDeterminesWhenObjectsAreNotEqual(uint count1, uint count2, Size drinkSize1, Size drinkSize2, Size sideSize1, Size sideSize2, bool cheese1, bool cheese2)
        {
            MockSide side1 = new MockSide() { SizeType = sideSize1 };
            MockSide side2 = new MockSide() { SizeType = sideSize2 };
            MockDrink drink1 = new MockDrink() { DrinkSize = drinkSize1 };
            MockDrink drink2 = new MockDrink() { DrinkSize = drinkSize2 };

            SlidersMeal s1 = new SlidersMeal() { ItemCount = count1, AmericanCheese = cheese1, SideChoice = side1, DrinkChoice = drink1 };
            SlidersMeal s2 = new SlidersMeal() { ItemCount = count2, AmericanCheese = cheese2, SideChoice = side2, DrinkChoice = drink2 };

            Assert.False(s1.Equals(s2));
        }
    }
}
