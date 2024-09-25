using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.UI.Xaml.Controls;

namespace BuildYourBowl.Data
{
	/// <summary>
	/// A class representing the menu that BuildYourBowl has to offer
	/// </summary>
	public static class Menu
	{
		/// <summary>
		/// A list of all available entrees
		/// </summary>
		public static IEnumerable<IMenuItem> Entrees
		{
			get
			{
				List<IMenuItem> entreesList = new List<IMenuItem>() 
				{ 
					new Bowl(), 
					new Nachos(), 
					new CarnitasBowl(), 
					new GreenChickenBowl(), 
					new SpicySteakBowl(), 
					new ChickenFajitaNachos(), 
					new ClassicNachos() 
				};

				return entreesList;
			}
		}

		/// <summary>
		/// A list of all available sides
		/// </summary>
		public static IEnumerable<IMenuItem> Sides
		{
			get
			{
				List<IMenuItem> sidesList = new List<IMenuItem>()
				{
                    new Fries() {SizeType = Size.Kids},
                    new Fries() {SizeType = Size.Kids, Curly = false},
                    new RefriedBeans(){SizeType = Size.Kids},
                    new RefriedBeans() {SizeType = Size.Kids, CheddarCheese = false},
                    new RefriedBeans() {SizeType = Size.Kids, Onions = false},
                    new RefriedBeans() {SizeType = Size.Kids, CheddarCheese = false, Onions = false},
                    new StreetCorn() {SizeType = Size.Kids},
                    new StreetCorn() {SizeType = Size.Kids, CotijaCheese = false},
                    new StreetCorn() {SizeType = Size.Kids, Cilantro = false},
                    new StreetCorn() {SizeType = Size.Kids, CotijaCheese = false, Cilantro = false},
                    new Fries() {SizeType = Size.Small},
					new Fries() {SizeType = Size.Small, Curly = false},
					new RefriedBeans(){SizeType = Size.Small},
					new RefriedBeans() {SizeType = Size.Small, CheddarCheese = false},
					new RefriedBeans() {SizeType = Size.Small, Onions = false},
					new RefriedBeans() {SizeType = Size.Small, CheddarCheese = false, Onions = false},
					new StreetCorn() {SizeType = Size.Small},
					new StreetCorn() {SizeType = Size.Small, CotijaCheese = false},
					new StreetCorn() {SizeType = Size.Small, Cilantro = false},
					new StreetCorn() {SizeType = Size.Small, CotijaCheese = false, Cilantro = false},
					new Fries() {SizeType = Size.Medium},
					new Fries() {SizeType = Size.Medium, Curly = false},
					new RefriedBeans(){SizeType = Size.Medium},
					new RefriedBeans() {SizeType = Size.Medium, CheddarCheese = false},
					new RefriedBeans() {SizeType = Size.Medium, Onions = false},
					new RefriedBeans() {SizeType = Size.Medium, CheddarCheese = false, Onions = false},
					new StreetCorn() {SizeType = Size.Medium},
					new StreetCorn() {SizeType = Size.Medium, CotijaCheese = false},
					new StreetCorn() {SizeType = Size.Medium, Cilantro = false},
					new StreetCorn() {SizeType = Size.Medium, CotijaCheese = false, Cilantro = false},
					new Fries() {SizeType = Size.Large},
					new Fries() {SizeType = Size.Large, Curly = false},
					new RefriedBeans(){SizeType = Size.Large},
					new RefriedBeans() {SizeType = Size.Large, CheddarCheese = false},
					new RefriedBeans() {SizeType = Size.Large, Onions = false},
					new RefriedBeans() {SizeType = Size.Large, CheddarCheese = false, Onions = false},
					new StreetCorn() {SizeType = Size.Large},
					new StreetCorn() {SizeType = Size.Large, CotijaCheese = false},
					new StreetCorn() {SizeType = Size.Large, Cilantro = false},
					new StreetCorn() {SizeType = Size.Large, CotijaCheese = false, Cilantro = false}
				};

				return sidesList;
			}
		}

		/// <summary>
		/// A list of all available drinks
		/// </summary>
		public static IEnumerable<IMenuItem> Drinks
		{
			get
			{
				List<IMenuItem> drinksList = new List<IMenuItem>()
				{
					new Milk(),
					new Milk() {Chocolate = true},
                    new AguaFresca() {DrinkSize=Size.Kids, DrinkFlavor = Flavor.Limonada},
                    new AguaFresca() {DrinkSize=Size.Kids, DrinkFlavor = Flavor.Lime},
                    new AguaFresca() {DrinkSize=Size.Kids, DrinkFlavor = Flavor.Strawberry},
                    new AguaFresca() {DrinkSize=Size.Kids, DrinkFlavor = Flavor.Cucumber},
                    new AguaFresca() {DrinkSize=Size.Kids, DrinkFlavor = Flavor.Tamarind},
                    new Horchata(){DrinkSize=Size.Kids},
                    new AguaFresca() {DrinkSize=Size.Small, DrinkFlavor = Flavor.Limonada},
					new AguaFresca() {DrinkSize=Size.Small, DrinkFlavor = Flavor.Lime},
					new AguaFresca() {DrinkSize=Size.Small, DrinkFlavor = Flavor.Strawberry},
					new AguaFresca() {DrinkSize=Size.Small, DrinkFlavor = Flavor.Cucumber},
					new AguaFresca() {DrinkSize=Size.Small, DrinkFlavor = Flavor.Tamarind},
					new Horchata(){DrinkSize=Size.Small},
					new AguaFresca() {DrinkSize=Size.Medium, DrinkFlavor = Flavor.Limonada},
					new AguaFresca() {DrinkSize=Size.Medium, DrinkFlavor = Flavor.Lime},
					new AguaFresca() {DrinkSize=Size.Medium, DrinkFlavor = Flavor.Strawberry},
					new AguaFresca() {DrinkSize=Size.Medium, DrinkFlavor = Flavor.Cucumber},
					new AguaFresca() {DrinkSize=Size.Medium, DrinkFlavor = Flavor.Tamarind},
					new Horchata(){DrinkSize=Size.Medium},
					new AguaFresca() {DrinkSize=Size.Large, DrinkFlavor = Flavor.Limonada},
					new AguaFresca() {DrinkSize=Size.Large, DrinkFlavor = Flavor.Lime},
					new AguaFresca() {DrinkSize=Size.Large, DrinkFlavor = Flavor.Strawberry},
					new AguaFresca() {DrinkSize=Size.Large, DrinkFlavor = Flavor.Cucumber},
					new AguaFresca() {DrinkSize=Size.Large, DrinkFlavor = Flavor.Tamarind},
					new Horchata(){DrinkSize=Size.Large}
				};

				return drinksList;
			}
		}

		/// <summary>
		/// A list of all available entrees
		/// </summary>
		public static IEnumerable<IMenuItem> KidsMeals
		{
			get
			{
				//ask if need full customization of drinks and sides too
				List<IMenuItem> kidsMealsList = new List<IMenuItem>()
				{
					//Side: Fries, Drink: Milk
					new ChickenNuggetsMeal(),
					new CornDogBitesMeal(),
					new SlidersMeal(),
					new SlidersMeal() {AmericanCheese = false},

					//Side: Fries, Drink: Agua Fresca
					new ChickenNuggetsMeal() { DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids}},
					new CornDogBitesMeal() { DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids}},
					new SlidersMeal() { DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids}},
					new SlidersMeal() {AmericanCheese = false, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids}},

					//Side: Fries, Drink: Horchata
					new ChickenNuggetsMeal() { DrinkChoice = new Horchata() { DrinkSize = Size.Kids}},
					new CornDogBitesMeal() { DrinkChoice = new Horchata() { DrinkSize = Size.Kids}},
					new SlidersMeal() { DrinkChoice = new Horchata() { DrinkSize = Size.Kids}},
					new SlidersMeal() {AmericanCheese = false, DrinkChoice = new Horchata() { DrinkSize = Size.Kids}},

					//Side: Refried Beans, Drink: Horchata
					new ChickenNuggetsMeal() { DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids}, SideChoice = new RefriedBeans() { SizeType = Size.Kids}},
					new CornDogBitesMeal() { DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids}, SideChoice = new RefriedBeans() { SizeType = Size.Kids}},
					new SlidersMeal() { DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids}, SideChoice = new RefriedBeans() { SizeType = Size.Kids}},
					new SlidersMeal() {AmericanCheese = false, DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids}, SideChoice = new RefriedBeans() { SizeType = Size.Kids}},

					//Side: Refried Beans, Drink: Horchata
					new ChickenNuggetsMeal() { DrinkChoice = new Horchata() { DrinkSize = Size.Kids}, SideChoice = new RefriedBeans() { SizeType = Size.Kids}},
					new CornDogBitesMeal() { DrinkChoice = new Horchata() { DrinkSize = Size.Kids}, SideChoice = new RefriedBeans() { SizeType = Size.Kids}},
					new SlidersMeal() { DrinkChoice = new Horchata() { DrinkSize = Size.Kids}, SideChoice = new RefriedBeans() { SizeType = Size.Kids}},
					new SlidersMeal() {AmericanCheese = false, DrinkChoice = new Horchata() { DrinkSize = Size.Kids}, SideChoice = new RefriedBeans() { SizeType = Size.Kids}},
					
					//Side: Refried Beans, Drink: Milk
					new ChickenNuggetsMeal() {SideChoice = new RefriedBeans() {SizeType = Size.Kids}},
					new CornDogBitesMeal() {SideChoice = new RefriedBeans() {SizeType = Size.Kids}},
					new SlidersMeal() {SideChoice = new RefriedBeans() {SizeType = Size.Kids}},
					new SlidersMeal() {SideChoice = new RefriedBeans() {SizeType = Size.Kids}, AmericanCheese = false},

					//Side: Street Corn, Drink: Milk
					new ChickenNuggetsMeal() {SideChoice = new StreetCorn() {SizeType = Size.Kids}},
					new CornDogBitesMeal() {SideChoice = new StreetCorn() {SizeType = Size.Kids}},
					new SlidersMeal() {SideChoice = new StreetCorn() {SizeType = Size.Kids}},
					new SlidersMeal() {SideChoice = new StreetCorn() {SizeType = Size.Kids}, AmericanCheese = false},

					//Side: Street Corn, Drink: Agua Fresca
					new ChickenNuggetsMeal() {DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids}, SideChoice = new StreetCorn() {SizeType = Size.Kids}},
					new CornDogBitesMeal() {DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids}, SideChoice = new StreetCorn() {SizeType = Size.Kids}},
					new SlidersMeal() {DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids}, SideChoice = new StreetCorn() {SizeType = Size.Kids}},
					new SlidersMeal() {DrinkChoice = new AguaFresca() { DrinkSize = Size.Kids}, SideChoice = new StreetCorn() {SizeType = Size.Kids}, AmericanCheese = false},

					//Side: Street Corn, Drink: Horchata
					new ChickenNuggetsMeal() {DrinkChoice = new Horchata() { DrinkSize = Size.Kids}, SideChoice = new StreetCorn() {SizeType = Size.Kids}},
					new CornDogBitesMeal() {DrinkChoice = new Horchata() { DrinkSize = Size.Kids}, SideChoice = new StreetCorn() {SizeType = Size.Kids}},
					new SlidersMeal() {DrinkChoice = new Horchata() { DrinkSize = Size.Kids}, SideChoice = new StreetCorn() {SizeType = Size.Kids}},
					new SlidersMeal() {DrinkChoice = new Horchata() { DrinkSize = Size.Kids}, SideChoice = new StreetCorn() {SizeType = Size.Kids}, AmericanCheese = false}
				};

				return kidsMealsList;
			}
		}

		/// <summary>
		/// A list of all items on the menu
		/// </summary>
		public static IEnumerable<IMenuItem> FullMenu
		{
			get
			{
				List<IMenuItem> itemsList = new List<IMenuItem>();

				foreach(Entree e in Entrees)
				{
					itemsList.Add(e);
				}
				foreach (Side s in Sides)
				{
					itemsList.Add(s);
				}
				foreach (Drink d in Drinks)
				{
					itemsList.Add(d);
				}
				foreach (KidsMeal k in KidsMeals)
				{
					itemsList.Add(k);
				}

				return itemsList;
			}
		}

		/// <summary>
		/// A list of ingredients that can be added to a build-your-own bowl or nachos order
		/// </summary>
		public static IEnumerable<IngredientItem> Ingredients
		{
			get
			{
				List<IngredientItem> ingredientsList = new List<IngredientItem>()
				{
					new IngredientItem(Ingredient.BlackBeans),
					new IngredientItem(Ingredient.PintoBeans),
					new IngredientItem(Ingredient.Queso),
					new IngredientItem(Ingredient.Veggies),
					new IngredientItem(Ingredient.SourCream),
					new IngredientItem(Ingredient.Guacamole),
					new IngredientItem(Ingredient.Chicken),
					new IngredientItem(Ingredient.Steak),
					new IngredientItem(Ingredient.Carnitas)
				};

				return ingredientsList;
			}
		}

		/// <summary>
		/// A list of all available salsa options
		/// </summary>
		public static IEnumerable<Salsa> Salsas
		{
			get
			{
				List<Salsa> salsasList = new List<Salsa>()
				{
					Salsa.Mild,
					Salsa.Medium,
					Salsa.Hot,
					Salsa.Green,
					Salsa.None
				};

				return salsasList;
			}
		}

		/// <summary>
		/// Searches the full menu for any menu items that contain the search terms in their name or preparation information
		/// </summary>
		/// <param name="terms">The terms to search for</param>
		/// <returns>An IEnumerable with the fitting terms</returns>
        public static IEnumerable<IMenuItem> Search(string terms)
        {
			List<IMenuItem> results = new List<IMenuItem>();

            if (terms == null)
            {
                return FullMenu;
            }

            string[] words = terms.Split(" ");

            foreach (IMenuItem m in FullMenu)
			{
				if(SearchTitleAndPreparationInformation(m, words))
				{
					results.Add(m);
				}
			}
			//returns all the items that contains the search terms in either their name or preparation information
			return results;
        }

		/// <summary>
		/// Searches the title and prep info of a specific menu item to check if every word in the search terms is in the title or prep info
		/// </summary>
		/// <param name="m">The item to search</param>
		/// <param name="terms">The terms to search for</param>
		/// <returns>A bool representing if all the terms were found</returns>
		private static bool SearchTitleAndPreparationInformation(IMenuItem m, string[] terms)
		{
			foreach(string s in terms)
            {
                bool found;
                if (!m.Name.ToLower().Contains(s, StringComparison.CurrentCultureIgnoreCase))
				{
                    found = false;
                    foreach (string p in m.PreparationInformation)
                    {
                        if (p.ToLower().Contains(s, StringComparison.CurrentCultureIgnoreCase))
                        {
                            found = true;
                        }
                    }
					if(m is Entree e)
					{
						if (e.BaseIngredient.Name.ToLower().Contains(s, StringComparison.CurrentCultureIgnoreCase))
						{
							found = true;
						}
						else
						{
                            foreach (KeyValuePair<Ingredient, IngredientItem> ingred in e.AdditionalIngredients)
							{
								if (ingred.Value.Included)
								{
                                    if (ingred.Value.Name.ToLower().Contains(s, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        found = true;
                                    }
                                }
							}
                        }
					}
                    if (!found)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

		/// <summary>
		/// Searches current filtered menu for the items that fall into the calorie range
		/// </summary>
		/// <param name="items">The current filtered menu</param>
		/// <param name="min">The minimum calorie count</param>
		/// <param name="max">The maximum calorie count</param>
		/// <returns>An IEnumerable containing items that fall inside the calorie range</returns>
        public static IEnumerable<IMenuItem> FilterByCalories(IEnumerable<IMenuItem> items, double? min, double? max)
        {
            if (min == null && max == null)
            {
                return items;
            }

            if (min == null)
            {
                return items.Where(item => item.Calories <= max);
            }

            if (max == null)
            {
                return items.Where(item => item.Calories >= min);
            }

            return items.Where(item => item.Calories >= min && item.Calories <= max);
        }

        /// <summary>
        /// Searches current filtered menu for the items that fall into the price range
        /// </summary>
        /// <param name="items">The current filtered menu</param>
        /// <param name="min">The minimum calorie count</param>
        /// <param name="max">The maximum calorie count</param>
        /// <returns>An IEnumerable containing items that fall inside the price range</returns>
        public static IEnumerable<IMenuItem> FilterByPrice(IEnumerable<IMenuItem> items, decimal? min, decimal? max)
        {
            if (min == null && max == null)
            {
                return items;
            }

            if (min == null)
            {
                return items.Where(item => item.Price <= max);
            }

            if (max == null)
            {
                return items.Where(item => item.Price >= min);
            }

            return items.Where(item => item.Price >= min && item.Price <= max);
        }
    }
}
