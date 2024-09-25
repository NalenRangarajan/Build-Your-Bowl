using BuildYourBowl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Unit tests for the Menu class
    /// </summary>
    public class MenuUnitTests
    {
        /// <summary>
        /// Tests that the number of items in Entrees is what is expected, and that every item that should be in Entrees is included
        /// </summary>
        [Fact]
        public void EntreeCountAndCorrectItems()
        {
            Assert.Equal(7, Menu.Entrees.Count());

            IMenuItem[] entreesList = { new Bowl(), new CarnitasBowl(), new ChickenFajitaNachos(), new ClassicNachos(), new GreenChickenBowl(), new Nachos(), new SpicySteakBowl() };

            foreach (IMenuItem m in Menu.Entrees)
            {
                bool contained = false;
                for (int i = 0; i < entreesList.Length; i++)
                {
                    if (m is Entree entree1 && entreesList[i] is Entree entree2)
                    {
                        if (entree1.Equals(entree2))
                        {
                            contained = true;
                        }
                    }
                }
                Assert.True(contained);
            }
        }

        /// <summary>
        /// Tests that the number of items in Sides is what is expected, and that every item that should be in Sides is included
        /// </summary>
        [Fact]
        public void SidesCountAndCorrectItems()
        {
            Assert.Equal(4 * (2 + 2 * 2 + 2 * 2), Menu.Sides.Count());

            List<IMenuItem> sides = new List<IMenuItem>();

            IMenuItem[] potentialSides = { new Fries(), new RefriedBeans(), new StreetCorn() };

            foreach (Size s in Enum.GetValues(typeof(Size)))
            {
                for (int i = 0; i < potentialSides.Length; i++)
                {
                    if (potentialSides[i] is Fries)
                    {
                        sides.Add(new Fries() { SizeType = s });
                        sides.Add(new Fries() { SizeType = s, Curly = true });
                    }
                    else if (potentialSides[i] is RefriedBeans)
                    {
                        sides.Add(new RefriedBeans() { SizeType = s });
                        sides.Add(new RefriedBeans() { SizeType = s, CheddarCheese = false });
                        sides.Add(new RefriedBeans() { SizeType = s, Onions = false });
                        sides.Add(new RefriedBeans() { SizeType = s, CheddarCheese = false, Onions = false });
                    }
                    else
                    {
                        sides.Add(new StreetCorn() { SizeType = s });
                        sides.Add(new StreetCorn() { SizeType = s, CotijaCheese = false });
                        sides.Add(new StreetCorn() { SizeType = s, Cilantro = false });
                        sides.Add(new StreetCorn() { SizeType = s, CotijaCheese = false, Cilantro = false });
                    }
                }
            }

            foreach (IMenuItem m in Menu.Sides)
            {
                bool contained = false;
                for (int i = 0; i < sides.Count(); i++)
                {
                    if (m is Side side1 && sides[i] is Side side2)
                    {
                        if (side1.Equals(side2))
                        {
                            contained = true;
                        }
                    }
                }
                Assert.True(contained);
            }
        }

        /// <summary>
        /// Tests that the number of items in Drinks is what is expected, and that every item that should be in Drinks is included
        /// </summary>
        [Fact]
        public void DrinksCountAndCorrectItems()
        {
            Assert.Equal(2 + 4 * (5 + 1), Menu.Drinks.Count());

            List<IMenuItem> drinks = new List<IMenuItem>();

            IMenuItem[] potentialDrinks = { new Milk(), new AguaFresca(), new Horchata() };

            foreach (Size s in Enum.GetValues(typeof(Size)))
            {
                for (int i = 0; i < potentialDrinks.Length; i++)
                {
                    if (potentialDrinks[i] is AguaFresca)
                    {
                        drinks.Add(new AguaFresca() { DrinkSize = s, DrinkFlavor = Flavor.Limonada });
                        drinks.Add(new AguaFresca() { DrinkSize = s, DrinkFlavor = Flavor.Lime });
                        drinks.Add(new AguaFresca() { DrinkSize = s, DrinkFlavor = Flavor.Tamarind });
                        drinks.Add(new AguaFresca() { DrinkSize = s, DrinkFlavor = Flavor.Cucumber });
                        drinks.Add(new AguaFresca() { DrinkSize = s, DrinkFlavor = Flavor.Strawberry });
                    }
                    else if (potentialDrinks[i] is Horchata)
                    {
                        drinks.Add(new Horchata() { DrinkSize = s });
                    }
                }
            }

            drinks.Add(new Milk());
            drinks.Add(new Milk() { Chocolate = true });

            foreach (IMenuItem m in Menu.Drinks)
            {
                bool contained = false;
                for (int i = 0; i < drinks.Count(); i++)
                {
                    if (m is Drink drink1 && drinks[i] is Drink drink2)
                    {
                        if (drink1.Equals(drink2))
                        {
                            contained = true;
                        }
                    }
                }
                Assert.True(contained);
            }
        }

        /// <summary>
        /// Tests that the number of items in KidsMeals is what is expected, and that every item that should be in KidsMeals is included
        /// </summary>
        [Fact]
        public void KidsMealsCountAndCorrectItems()
        {
            Assert.Equal( 3 * 3 +  3 * 3 + 3 * 2 * 3 , Menu.KidsMeals.Count());

            List<IMenuItem> kidsMeals = new List<IMenuItem>();

            kidsMeals.Add(new ChickenNuggetsMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            kidsMeals.Add(new ChickenNuggetsMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new ChickenNuggetsMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new ChickenNuggetsMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            kidsMeals.Add(new ChickenNuggetsMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new ChickenNuggetsMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new ChickenNuggetsMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            kidsMeals.Add(new ChickenNuggetsMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new ChickenNuggetsMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new CornDogBitesMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            kidsMeals.Add(new CornDogBitesMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new CornDogBitesMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new CornDogBitesMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            kidsMeals.Add(new CornDogBitesMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new CornDogBitesMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new CornDogBitesMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            kidsMeals.Add(new CornDogBitesMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new CornDogBitesMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Milk(), AmericanCheese = false });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() {  DrinkSize = Size.Kids}, AmericanCheese = false });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids }, AmericanCheese = false });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Milk(), AmericanCheese = false });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids }, AmericanCheese = false });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids }, AmericanCheese = false });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Milk(), AmericanCheese = false });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids }, AmericanCheese = false });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            kidsMeals.Add(new SlidersMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids }, AmericanCheese = false });

            foreach (IMenuItem m in Menu.KidsMeals)
            {
                bool contained = false;
                bool found = false;
                for (int i = 0; i < kidsMeals.Count(); i++)
                {
                    if (m is KidsMeal kidsMeal1 && kidsMeals[i] is KidsMeal kidsMeal2 && !found)
                    {
                        if (kidsMeal1.Equals(kidsMeal2))
                        {
                            contained = true;
                            found = true;
                        }
                    }
                }
                Assert.True(contained);
            }

        }


        /// <summary>
        /// Tests that the number of items in FullMenu is what is expected, and that every item that should be in FullMenu is included
        /// </summary>
        [Fact]
        public void FullMenuCount()
        {
            Assert.Equal((7) + (4 * (2 + 2 * 2 + 2 * 2)) + (2 + 4 * (5 + 1)) + (3 * 3 + 3 * 3 + 3 * 2 * 3), Menu.FullMenu.Count());

            IMenuItem[] entreesList = { new Bowl(), new CarnitasBowl(), new ChickenFajitaNachos(), new ClassicNachos(), new GreenChickenBowl(), new Nachos(), new SpicySteakBowl() };

            IMenuItem[] potentialSides = { new Fries(), new RefriedBeans(), new StreetCorn() };

            IMenuItem[] potentialDrinks = { new Milk(), new AguaFresca(), new Horchata() };

            List<IMenuItem> menuItems = new List<IMenuItem>();

            //Add entrees
            foreach( IMenuItem m in entreesList)
            {
                menuItems.Add(m);
            }

            //Add drinks
            foreach (Size s in Enum.GetValues(typeof(Size)))
            {
                for (int i = 0; i < potentialDrinks.Length; i++)
                {
                    if (potentialDrinks[i] is AguaFresca)
                    {
                        menuItems.Add(new AguaFresca() { DrinkSize = s, DrinkFlavor = Flavor.Limonada });
                        menuItems.Add(new AguaFresca() { DrinkSize = s, DrinkFlavor = Flavor.Lime });
                        menuItems.Add(new AguaFresca() { DrinkSize = s, DrinkFlavor = Flavor.Tamarind });
                        menuItems.Add(new AguaFresca() { DrinkSize = s, DrinkFlavor = Flavor.Cucumber });
                        menuItems.Add(new AguaFresca() { DrinkSize = s, DrinkFlavor = Flavor.Strawberry });
                    }
                    else if (potentialDrinks[i] is Horchata)
                    {
                        menuItems.Add(new Horchata() { DrinkSize = s });
                    }
                }
            }

            menuItems.Add(new Milk());
            menuItems.Add(new Milk() { Chocolate = true });

            //Add sides
            foreach (Size s in Enum.GetValues(typeof(Size)))
            {
                for (int i = 0; i < potentialSides.Length; i++)
                {
                    if (potentialSides[i] is Fries)
                    {
                        menuItems.Add(new Fries() { SizeType = s });
                        menuItems.Add(new Fries() { SizeType = s, Curly = true });
                    }
                    else if (potentialSides[i] is RefriedBeans)
                    {
                        menuItems.Add(new RefriedBeans() { SizeType = s });
                        menuItems.Add(new RefriedBeans() { SizeType = s, CheddarCheese = false });
                        menuItems.Add(new RefriedBeans() { SizeType = s, Onions = false });
                        menuItems.Add(new RefriedBeans() { SizeType = s, CheddarCheese = false, Onions = false });
                    }
                    else
                    {
                        menuItems.Add(new StreetCorn() { SizeType = s });
                        menuItems.Add(new StreetCorn() { SizeType = s, CotijaCheese = false });
                        menuItems.Add(new StreetCorn() { SizeType = s, Cilantro = false });
                        menuItems.Add(new StreetCorn() { SizeType = s, CotijaCheese = false, Cilantro = false });
                    }
                }
            }

            //Add kidsMeals
            menuItems.Add(new ChickenNuggetsMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            menuItems.Add(new ChickenNuggetsMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            menuItems.Add(new ChickenNuggetsMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            menuItems.Add(new ChickenNuggetsMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            menuItems.Add(new ChickenNuggetsMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            menuItems.Add(new ChickenNuggetsMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            menuItems.Add(new ChickenNuggetsMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            menuItems.Add(new ChickenNuggetsMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            menuItems.Add(new ChickenNuggetsMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            menuItems.Add(new CornDogBitesMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            menuItems.Add(new CornDogBitesMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            menuItems.Add(new CornDogBitesMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            menuItems.Add(new CornDogBitesMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            menuItems.Add(new CornDogBitesMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            menuItems.Add(new CornDogBitesMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            menuItems.Add(new CornDogBitesMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            menuItems.Add(new CornDogBitesMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            menuItems.Add(new CornDogBitesMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            menuItems.Add(new SlidersMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            menuItems.Add(new SlidersMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Milk(), AmericanCheese = false });
            menuItems.Add(new SlidersMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            menuItems.Add(new SlidersMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids }, AmericanCheese = false });
            menuItems.Add(new SlidersMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            menuItems.Add(new SlidersMeal() { SideChoice = new Fries() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids }, AmericanCheese = false });
            menuItems.Add(new SlidersMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            menuItems.Add(new SlidersMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Milk(), AmericanCheese = false });
            menuItems.Add(new SlidersMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            menuItems.Add(new SlidersMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids }, AmericanCheese = false });
            menuItems.Add(new SlidersMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            menuItems.Add(new SlidersMeal() { SideChoice = new RefriedBeans() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids }, AmericanCheese = false });
            menuItems.Add(new SlidersMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Milk() });
            menuItems.Add(new SlidersMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Milk(), AmericanCheese = false });
            menuItems.Add(new SlidersMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids } });
            menuItems.Add(new SlidersMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids }, AmericanCheese = false });
            menuItems.Add(new SlidersMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids } });
            menuItems.Add(new SlidersMeal() { SideChoice = new StreetCorn() { SizeType = Size.Kids }, DrinkChoice = new Horchata() { DrinkSize = Size.Kids }, AmericanCheese = false });


            foreach(IMenuItem m in Menu.FullMenu)
            {
                bool contained = false;
                bool found = false;
                for(int i = 0; i < menuItems.Count(); i++)
                {
                    if (m is Entree entree1 && menuItems[i] is Entree entree2 && !found)
                    {
                        if (entree1.Equals(entree2))
                        {
                            contained = true;
                            found = true;
                        }
                    }
                    else if (m is Side side1 && menuItems[i] is Side side2 && !found)
                    {
                        if (side1.Equals(side2))
                        {
                            contained = true;
                            found = true;
                        }
                    }
                    else if (m is Drink drink1 && menuItems[i] is Drink drink2 && !found)
                    {
                        if (drink1.Equals(drink2))
                        {
                            contained = true;
                            found = true;
                        }
                    }
                    else if (m is KidsMeal kidsMeal1 && menuItems[i] is KidsMeal kidsMeal2 && !found)
                    {
                        if (kidsMeal1.Equals(kidsMeal2))
                        {
                            contained = true;
                            found = true;
                        }
                    }
                }
                Assert.True(contained);
            }
        }

        /// <summary>
        /// Tests that the number of items in Ingredients is what is expected, and that every item that should be in Ingredients is included
        /// </summary>
        [Fact]
        public void IngredientsCountAndCorrectItems()
        {
            Assert.Equal(9, Menu.Ingredients.Count());

            foreach(IngredientItem i in Menu.Ingredients)
            {
                bool contained = false;
                foreach(Ingredient ingred in Enum.GetValues(typeof(Ingredient)))
                {
                    IngredientItem item = new IngredientItem(ingred);

                    if (item.Equals(i))
                    {
                        contained = true;
                    }
                }

                Assert.True(contained);
            }
        }

        /// <summary>
        /// Tests that the number of items in Salsas is what is expected, and that every item that should be in Salsas is included
        /// </summary>
        [Fact]
        public void SalsasCountAndCorrectItems()
        {
            Assert.Equal(5, Menu.Salsas.Count());

            foreach (Salsa s in Menu.Salsas)
            {
                bool contained = false;
                foreach (Salsa salsa in Enum.GetValues(typeof(Salsa)))
                {
                    if (salsa == s)
                    {
                        contained = true;
                    }
                }

                Assert.True(contained);
            }
        }

        /// <summary>
        /// Tests that each type of search works correctly with null values
        /// </summary>
        [Fact]
        public void SearchWithNullValues()
        {
            Assert.Equal((7) + (4 * (2 + 2 * 2 + 2 * 2)) + (2 + 4 * (5 + 1)) + (3 * 3 + 3 * 3 + 3 * 2 * 3), Menu.Search("").Count());

            Assert.Equal((7) + (4 * (2 + 2 * 2 + 2 * 2)) + (2 + 4 * (5 + 1)) + (3 * 3 + 3 * 3 + 3 * 2 * 3), Menu.FilterByCalories(Menu.FullMenu, null, null).Count());

            Assert.Equal((7) + (4 * (2 + 2 * 2 + 2 * 2)) + (2 + 4 * (5 + 1)) + (3 * 3 + 3 * 3 + 3 * 2 * 3), Menu.FilterByPrice(Menu.FullMenu, null, null).Count());
        }

        /// <summary>
        /// Tests that the Calorie filter works with a range of values
        /// </summary>
        /// <param name="expectedCount">The expected of menu items that fall within the calorie range</param>
        /// <param name="minCal">The minimum amount of calories for the filter</param>
        /// <param name="maxCal">The maximum amount of calories for the filter</param>
        [Theory]
        [InlineData((2 + 26 + 7 + 2), 200, 500)]
        [InlineData(0, 500, 200)]
        [InlineData((2 + 38 + 26 + 2), null, 500)]
        [InlineData((7 + 28 + 7 + 36), 200, null)]
        [InlineData((2 + 0 + 0 + 2), 700, 900)]
        [InlineData((2 + 28 + 16 + 0), 100, 300)]
        [InlineData(0, 1000, 600)]
        [InlineData((5 + 2 + 0 + 34), 500, null)]
        public void FilterByCaloriesFiltersCorrectly(int expectedCount, double? minCal, double? maxCal)
        {
            Assert.Equal(expectedCount, Menu.FilterByCalories(Menu.FullMenu, minCal, maxCal).Count());
        }

        /// <summary>
        /// Tests that the Price filter works with a range of values
        /// </summary>
        /// <param name="expectedCount">The expected of menu items that fall within the price range</param>
        /// <param name="minPrice">The minimum price for the filter</param>
        /// <param name="maxPrice">The maximum price for the filter</param>
        [Theory]
        [InlineData((0 + 36 + 26 + 0), 2.00, 5.00)]
        [InlineData(0, 5.00, 2.00)]
        [InlineData((2 + 40 + 26 + 36), null, 9.00)]
        [InlineData((7 + 40 + 26 + 36), 2.00, null)]
        [InlineData((2 + 0 + 0 + 0), 7.00, 9.00)]
        [InlineData((0 + 8 + 18 + 0), 1.00, 3.00)]
        [InlineData(0, 10.00, 6.00)]
        [InlineData((7 + 4 + 0 + 36), 5.00, null)]
        public void FilterByPriceFiltersCorrectly(int expectedCount, double? minPrice, double? maxPrice)
        {
            if (minPrice != null && maxPrice != null) {
                Assert.Equal(expectedCount, Menu.FilterByPrice(Menu.FullMenu, Convert.ToDecimal(minPrice), Convert.ToDecimal(maxPrice)).Count());
            }
            else if(maxPrice != null)
            {
                Assert.Equal(expectedCount, Menu.FilterByPrice(Menu.FullMenu, null, Convert.ToDecimal(maxPrice)).Count());
            }
            else if(minPrice != null)
            {
                Assert.Equal(expectedCount, Menu.FilterByPrice(Menu.FullMenu, Convert.ToDecimal(minPrice), null).Count());
            }
            else
            {
                Assert.Equal(expectedCount, Menu.FilterByPrice(Menu.FullMenu, null, null).Count());
            }
            
        }

        /// <summary>
        /// Tests that the search correctly handles single terms, multiple terms, and different capitalizations
        /// </summary>
        /// <param name="expectedCount">The expected of menu items that contain the search terms</param>
        /// <param name="terms">The terms to search for</param>
        [Theory]
        [InlineData((2 + 16 + 0 + 12), "beans")]
        [InlineData((1 + 0 + 0 + 0), "GuacAmole")]
        [InlineData((0 + 2 + 0 + 0), "fries small")]
        [InlineData((0 + 0 + 0 + 9), "nuggets")]
        [InlineData((0 + 0 + 2 + 12), "milk")]
        [InlineData((0 + 0 + 20 + 12), "fRescA")]
        [InlineData((0 + 0 + 0 + 3), "bites horCHatA")]
        [InlineData((4 + 0 + 0 + 0), "bowl")]
        public void SearchCorrectlyHandlesDifferentSearchTerms(int expectedCount, string terms)
        {
            Assert.Equal(expectedCount, Menu.Search(terms).Count());
        }
    }
}
