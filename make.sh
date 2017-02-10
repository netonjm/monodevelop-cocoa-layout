#!/bin/bash
echo "Restoring xamarinstudio.raspberry nugets..."
mono tools/nuget.exe restore src/XamarinStudio.Raspberry.sln

echo "build project in release mode"
xbuild /p:Configuration=Release src/MonoDevelop.Debug.CocoaLayout.sln 

echo "Generating addin..."
/Applications/Xamarin\ Studio.app/Contents/MacOS/mdtool setup pack src/bin/Release/MonoDevelop.Debug.CocoaLayout.dll

echo "Finished."
