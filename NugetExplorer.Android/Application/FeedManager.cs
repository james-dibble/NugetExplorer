namespace NugetExplorer
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	using NugetExplorer.Model;

	public class FeedManager
	{
		private readonly Lazy<ICollection<Feed>> _feeds;

		public event Action<Feed> FeedAdded;

		public FeedManager()
		{
			this._feeds = new Lazy<ICollection<Feed>> (this.LoadFeeds);
		}

		public IEnumerable<Feed> Feeds 
		{ 
			get
			{ 
				return this._feeds.Value;
			}
		}

		public void AddFeed(Feed newFeed)
		{
			this._feeds.Value.Add (newFeed);

			if (this.FeedAdded != null) 
			{
				this.FeedAdded (newFeed);
			}
		}

		private ICollection<Feed> LoadFeeds()
		{
			return new List<Feed> ();
		}
	}
}