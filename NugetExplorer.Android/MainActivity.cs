namespace NugetExplorer
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Android.App;
	using Android.Content;
	using Android.Runtime;
	using Android.Views;
	using Android.Widget;
	using Android.OS;
	using Android.Support.V4.App;
	using Android.Support.V4.Widget;

	using NugetExplorer.Model;

	[Activity (Label = "Nuget Explorer", MainLauncher = true)]
	public class MainActivity : FragmentActivity
	{
		private FeedManager _feedManager;
		private ActionBarDrawerToggle _drawToggle;
		private ArrayAdapter<string> _drawListAdapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			this._feedManager = new FeedManager ();
			this._feedManager.FeedAdded += this.FeedAdded;

			SetContentView (Resource.Layout.Main);

			var drawList = this.FindViewById<ListView>(Resource.Id.DrawNavigation);

			this._drawListAdapter = new ArrayAdapter<string> (
				this, 
				Resource.Layout.DrawListItem,
				this._feedManager.Feeds.Select(f => f.Name == null ? f.FeedLocation.AbsoluteUri : f.Name).ToList());

			drawList.Adapter = this._drawListAdapter;

			var layout = this.FindViewById<DrawerLayout> (Resource.Id.MainLayout);

			this.ActionBar.SetDisplayHomeAsUpEnabled (true);
			this.ActionBar.SetHomeButtonEnabled(true);

			this._drawToggle = new ActionBarDrawerToggle(
				this, 
				layout,
				Resource.Drawable.ic_drawer,
				Resource.String.DrawerOpen,
				Resource.String.DrawerClose);

			layout.SetDrawerListener (this._drawToggle);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (item.ItemId == Resource.Id.AddFeedItem) 
			{
				this.AddFeed ();
				return true;
			}

			return this._drawToggle.OnOptionsItemSelected(item) ? true : base.OnOptionsItemSelected (item);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			this.MenuInflater.Inflate (Resource.Menu.ActionBarMenu, menu);
			var addFeedButton = menu.FindItem (Resource.Id.AddFeedItem);

			return base.OnCreateOptionsMenu (menu);
		}

		private void AddFeed()
		{
			var dialog = new AddFeedDialog (this._feedManager);
			dialog.Show (this.FragmentManager, "Add Feed");
		}

		private void FeedAdded(Feed newFeed)
		{
			this._drawListAdapter.Add (newFeed.Name == null ? newFeed.FeedLocation.AbsoluteUri : newFeed.Name);
			this._drawListAdapter.NotifyDataSetChanged ();
		}
	}
}