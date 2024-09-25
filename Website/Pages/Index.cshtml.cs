using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design;
using BuildYourBowl.Data;

namespace Website.Pages
{
    /// <summary>
    /// The code-behind for the Index page
    /// </summary>
    public class IndexModel : PageModel
    {
        
        /// <summary>
        /// A private Logger property
        /// </summary>
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Constructor for the code-behind of the Index page
        /// </summary>
        /// <param name="logger">The logger to be used</param>
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// The menu items that matched the filter
        /// </summary>
        public IEnumerable<IMenuItem> MenuItems { get; set; }

        /// <summary>
        /// A list of all possible entrees
        /// </summary>
        public IEnumerable<IMenuItem> Entrees => Menu.Entrees;

        /// <summary>
        /// A list of all possible sides
        /// </summary>
        public IEnumerable<IMenuItem> Sides => Menu.Sides;

        /// <summary>
        /// A list of all possible drinks
        /// </summary>
        public IEnumerable<IMenuItem> Drinks => Menu.Drinks;

        /// <summary>
        /// A list of all possible kids meals
        /// </summary>
        public IEnumerable<IMenuItem> KidsMeals => Menu.KidsMeals;

        /// <summary>
        /// A list of all possible ingredients
        /// </summary>
        public IEnumerable<IngredientItem> Ingredients => Menu.Ingredients;

        /// <summary>
        /// A list of all possible salsas
        /// </summary>
        public IEnumerable<Salsa> Salsas => Menu.Salsas;

        /// <summary>
        /// The search terms entered by the user
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string SearchTerms { get; set; }

        /// <summary>
        /// The minimum calories an item can be
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? CalorieMin { get; set; }

        /// <summary>
        /// The maximum calories an item can be
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? CalorieMax { get; set; }

        /// <summary>
        /// The minimum price an item can be
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public decimal? PriceMin { get; set; }

        /// <summary>
        /// The maximum price an item can be
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public decimal? PriceMax { get; set; }

        /// <summary>
        /// This is called whenever there is a get Request to the server
        /// </summary>
        public void OnGet()
        {
            MenuItems = Menu.Search(SearchTerms);
            MenuItems = Menu.FilterByCalories(MenuItems, CalorieMin, CalorieMax);
            MenuItems = Menu.FilterByPrice(MenuItems, PriceMin, PriceMax);
        }
    }
}