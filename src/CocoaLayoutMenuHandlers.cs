using System;
using System.Diagnostics;
using System.IO;
using Foundation;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace MonoDevelop.Debug.CocoaLayout
{
	class CocoaLayoutCommandHandler : CommandHandler
	{
		readonly protected CocoaLayoutContext context;
		public CocoaLayoutCommandHandler ()
		{
			context = CocoaLayoutContext.Instance;
		}
	}

	class ShowAllViewsCommandHandler : CocoaLayoutCommandHandler
	{
		protected override void Run ()
		{
			var expectedValue = !context.ShowAllViews;
			context.SetShowAllViews (expectedValue);
		}

		protected override void Update (CommandInfo info)
		{
			var actual = context.ShowAllViews ? "Disable" : "Enable";
			info.Text = $"{actual} Show All Views";
			info.Enabled = !string.IsNullOrEmpty (context.CurrentDomain);
			info.Visible = true;
		}
	}

	class VisualizeConstraintsCommandHandler : CocoaLayoutCommandHandler
	{
		protected override void Run ()
		{
			var value = context.VisualizeMutuallyExclusiveConstraints;
			context.SetVisualizeMutuallyExclusiveConstraints (!value);
		}

		protected override void Update (CommandInfo info)
		{
			var actual = context.VisualizeMutuallyExclusiveConstraints ? "Disable" : "Enable";
			info.Text = $"{actual} Visualize Mutually Exclusive Constraints";
			info.Enabled = !string.IsNullOrEmpty (context.CurrentDomain);
			info.Visible = true;
		}
	}
}