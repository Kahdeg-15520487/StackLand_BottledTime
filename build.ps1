New-Item -Path "TSMMPackage/plugins" -ItemType Directory -Force
Copy-Item "icon.png" -Destination "./TSMMPackage" -Force
Copy-Item "README.md" -Destination "./TSMMPackage" -Force
Copy-Item "manifest.json" -Destination "./TSMMPackage" -Force
Copy-Item "./Blueprints" -Destination "./TSMMPackage/plugins" -Recurse -Force
Copy-Item "./Cards" -Destination "./TSMMPackage/plugins" -Recurse -Force
Copy-Item "./Images" -Destination "./TSMMPackage/plugins" -Recurse -Force
Copy-Item "./Sounds" -Destination "./TSMMPackage/plugins" -Recurse -Force

$buildResult = [string[]](dotnet build .)
$dll = $buildResult[5].Split('>')[1].Trim()
Copy-Item $dll -Destination "./TSMMPackage/plugins"

Compress-Archive -Path "./TSMMPackage/*" -DestinationPath "TSMMPackage.zip" -Force