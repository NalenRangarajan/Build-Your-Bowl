using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website.Pages
{
    /// <summary>
    /// The code-behind for the privacy page
    /// </summary>
    public class PrivacyModel : PageModel
    {
        /// <summary>
        /// A private Logger property
        /// </summary>
        private readonly ILogger<PrivacyModel> _logger;

        /// <summary>
        /// Constructor for the code-behind of the Privacy page
        /// </summary>
        /// <param name="logger">The logger to be used</param>
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method that completes specified actions when called
        /// </summary>
        public void OnGet()
        {

        }
    }
}