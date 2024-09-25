using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Services.Store;
using Newtonsoft.Json;

namespace BuildYourBowl.Data
{
	/// <summary>
	/// A class representing the list of all Reviews
	/// </summary>
	public static class ReviewDatabase
	{
		/// <summary>
		/// Private field representing the list of reviews
		/// </summary>
		private static List<Review> _reviews;

		/// <summary>
		/// The list of reviews
		/// </summary>
		public static IEnumerable<Review> Reviews => _reviews;

		/// <summary>
		/// Constructs a list of reviews
		/// </summary>
		static ReviewDatabase()
		{
			if (File.Exists("reviews.json"))
			{
				using(StreamReader s = File.OpenText("reviews.json"))
				{
					string json = s.ReadToEnd();

					//deserialize turns json into an object or list of objects

					_reviews = JsonConvert.DeserializeObject<List<Review>>(json);

					
				}
				
			}
			if (_reviews == null)
			{
				_reviews = new List<Review>();
			}
			else
			{
				DisplayRecentReviews(_reviews);
			}

		}

		/// <summary>
		/// Adds a single review to the Database
		/// </summary>
		/// <param name="n">The text for the review</param>
		public static void AddReview(string n)
		{
			if(n != null && n != "")
			{
				_reviews.Add(new Review() { ReviewTime = DateTime.Now, ReviewMessage = n});

				File.WriteAllText("reviews.json", JsonConvert.SerializeObject(_reviews));
			}
		}

		/// <summary>
		/// Orders the reviews based on their time placed
		/// </summary>
		/// <param name="reviews">The list of all reviews</param>
		/// <returns>The ordered list</returns>
		public static IEnumerable<Review> DisplayRecentReviews(IEnumerable<Review> reviews)
		{
			return reviews.OrderByDescending(r => r.ReviewTime);
		}
	}
}
