using System;
using System.IO;
using Foundation;
using MonoDevelop.Ide;

namespace MonoDevelop.Debug.CocoaLayout
{
	class CocoaLayoutContext
	{
		const string ShowAllViewsKey = "NSShowAllViews";
		const string VisualizeMutuallyExclusiveConstraintsKey = "NSConstraintBasedLayoutVisualizeMutuallyExclusiveConstraints";

		public string CurrentDomain = null;

		bool showAllViews;
		public bool ShowAllViews {
			get { return showAllViews; }
		}

		bool visualizeMutuallyExclusiveConstraints;
		public bool VisualizeMutuallyExclusiveConstraints {
			get { return visualizeMutuallyExclusiveConstraints; }
		}

		public CocoaLayoutContext ()
		{
			IdeApp.ProjectOperations.CurrentProjectChanged += (sender, e) => {
				RefreshCurrentDomain ();
			};

			RefreshStates ();
			RefreshCurrentDomain ();
		}

		void RefreshCurrentDomain ()
		{
			if (IdeApp.ProjectOperations.CurrentSelectedProject == null)
				return;
			var baseDirectory = IdeApp.ProjectOperations.CurrentSelectedProject.BaseDirectory;
			var infoPlist = Path.Combine (baseDirectory, "Info.plist");
			CurrentDomain = GetCFBundleIdentifier (infoPlist);
		}

		string GetCFBundleIdentifier (string plistFile)
		{
			if (!File.Exists (plistFile))
				return null;

			try {
				using (var settingsDict = new NSDictionary (plistFile)) {
					foreach (var prefItem in settingsDict) {
						var key = prefItem.Key as NSString;
						if (key == null)
							continue;
						if (key.ToString () == "CFBundleIdentifier") {
							return prefItem.Value?.ToString ();
						}
					};
					return null;
				};
			} catch (Exception ex) {
				Console.WriteLine (ex);
				return null;
			}
		}

		void RefreshStates ()
		{
			showAllViews = ShellScript.GetDefaultBooleanKey (CurrentDomain, ShowAllViewsKey);
			visualizeMutuallyExclusiveConstraints = ShellScript.GetDefaultBooleanKey (CurrentDomain, VisualizeMutuallyExclusiveConstraintsKey);
		}

		public void SetShowAllViews (bool value)
		{
			ShellScript.SetDefaultBooleanKey (CurrentDomain, ShowAllViewsKey, value);
			showAllViews = value;
		}

		public void SetVisualizeMutuallyExclusiveConstraints (bool value)
		{
			ShellScript.SetDefaultBooleanKey (CurrentDomain, VisualizeMutuallyExclusiveConstraintsKey, value);
			visualizeMutuallyExclusiveConstraints = value;
		}

		static CocoaLayoutContext instance;
		public static CocoaLayoutContext Instance {
			get {
				if (instance == null) {
					instance = new CocoaLayoutContext ();
				}
				return instance;
			}
		}
	}
}
