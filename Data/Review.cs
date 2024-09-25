using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildYourBowl.Data
{
	/// <summary>
	/// A class that represents a single review
	/// </summary>
	public class Review
	{
		/// <summary>
		/// The text of a review
		/// </summary>
		public string ReviewMessage { get; set; }

		/// <summary>
		/// The time a review was made
		/// </summary>
		public DateTime ReviewTime { get; set; }
	}
}
