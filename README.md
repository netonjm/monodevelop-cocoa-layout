# Monodevelop Cocoa Layout Addin for Xamarin Studio
Provides experimental feature to show debug NSView layout more easily.

#Installation

Go to your monodevelop-cocoa-layout cloned repository and execute ./make.sh this will compile all stuff and generates the most recent .mpack addin file.

To finish: Open XS > Menu > Addins.. > Install from file > Search the generated .mpack file.

Done!

#Setup

Press Menu > View 

##ShowAllViews
Draw a border around the NSView objects to themselves in a different colour:

![Image of ShowAllViews](https://content.screencast.com/users/netonjm/folders/Jing/media/f4072a65-29e1-4bfd-8417-ebb9054eb6ac/00000373.png)

##VisualizeMutuallyExclusiveConstraints
This will render problematic constraints and allow you to click on them to dump the constraint settings to the console. 

![Image of VisualizeMutuallyExclusiveConstraints](https://content.screencast.com/users/netonjm/folders/Jing/media/22befbe6-c7ff-44c3-9cac-4418a5f2daf7/00000374.png)

