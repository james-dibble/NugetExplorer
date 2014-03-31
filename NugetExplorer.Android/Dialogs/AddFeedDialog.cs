namespace NugetExplorer
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Runtime;
	using Android.Util;
	using Android.Views;
	using Android.Widget;

	using NugetExplorer.Model;

	public class AddFeedDialog : DialogFragment
	{
		private readonly FeedManager _feedManager;
		private View _view;

		public AddFeedDialog (FeedManager feedManager)
		{
			this._feedManager = feedManager;
		}

		public override Dialog OnCreateDialog (Bundle savedInstanceState)
		{
			this._view = this.Activity.LayoutInflater.Inflate (Resource.Layout.AddFeedDialog, null);

			var builder = new AlertDialog.Builder (this.Activity);

			builder.SetView (this._view)
				.SetPositiveButton ("Add Feed", AddClicked)
				.SetNegativeButton ("Cancel", CancelClicked);

			return builder.Create ();
		}

		private void CancelClicked (object sender, DialogClickEventArgs e)
		{
			return;
		}

		private void AddClicked (object sender, DialogClickEventArgs e)
		{
			var feedName = this._view.FindViewById<EditText> (Resource.Id.FeedName).Text;
			var feedUri = this._view.FindViewById<EditText> (Resource.Id.FeedUri).Text;

			var newFeed = new Feed { Name = feedName, FeedLocation = new Uri (feedUri) };

			this._feedManager.AddFeed (newFeed);
		}
	}
}