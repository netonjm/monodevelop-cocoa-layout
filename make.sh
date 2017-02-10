#!/bin/bash
# nuget restoring
if [ ! -f ./tools/nuget.exe ]; then
	mkdir -p tools
    echo "nuget.exe not found! downloading latest version"
    curl -O https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
    mv nuget.exe tools/
fi

echo "Restoring xamarinstudio.raspberry nugets..."
mono tools/nuget.exe restore src/MonoDevelop.Debug.CocoaLayout.sln

echo "build project in release mode"
xbuild /p:Configuration=Release src/MonoDevelop.Debug.CocoaLayout.sln 

echo "Generating addin..."
/Applications/Xamarin\ Studio.app/Contents/MacOS/mdtool setup pack src/bin/Release/MonoDevelop.Debug.CocoaLayout.dll

echo "Finished."
