using BuildYourBowl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website.Pages.Shared
{
	/// <summary>
	/// The code-behind for the Reviews page
	/// </summary>
    public class ReviewsModel : PageModel
    {
		/// <summary>
		/// This is called whenever there is a get Request to the server
		/// </summary>
		public void OnGet()
        {
        }

		/// <summary>
		/// This is called whenever there is a post Request to the server
		/// </summary>
		public void OnPost()
		{
			ReviewDatabase.AddReview(ReviewText);
			ReviewText = null;
			AllReviews = ReviewDatabase.DisplayRecentReviews(AllReviews);
		}

		/// <summary>
		/// The review being submitted
		/// </summary>
		[BindProperty]
		public string ReviewText { get; set; }

		/// <summary>
		/// A list of all reviews
		/// </summary>
		public IEnumerable<Review> AllReviews { get; set; } = ReviewDatabase.DisplayRecentReviews(ReviewDatabase.Reviews);
	}
}
