using System;
using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin (
	"MonoDevelop.Debug.CocoaLayout",
	Namespace = "MonoDevelop.Debug.CocoaLayout",
	Version = "0.6"
)]

[assembly: AddinName ("Debug Cocoa Layout")]
[assembly: AddinCategory ("IDE extensions")]
[assembly: AddinDescription ("This addin allows you draw all constraints debugging/running an application")]
[assembly: AddinAuthor ("Jose Medrano")]
