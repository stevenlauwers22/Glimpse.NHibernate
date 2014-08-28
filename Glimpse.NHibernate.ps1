#properties ---------------------------------------------------------------------------------------------------------

properties {
    $base_dir = resolve-path .
    $build_dir = "$base_dir\Builds"
    $source_dir = "$base_dir\Source"
    $tools_dir = "$base_dir\Tools"
}

#tasks -------------------------------------------------------------------------------------------------------------

task default -depends build

task clean {
    "Cleaning Glimpse.NHibernate bin and obj directories"
    delete_directory "$source_dir\Glimpse.NHibernate\bin"
    delete_directory "$source_dir\Glimpse.NHibernate\obj"
}

task build -depends clean {
    "Building Glimpse.NHibernate.sln"
    exec { msbuild $source_dir\Glimpse.NHibernate.sln /p:Configuration=Release }
}

task package -depends build {
    "Creating Glimpse.NHibernate.nupkg"
    exec { & $tools_dir\NuGet.CommandLine.2.1.0\tools\nuget.exe pack $source_dir\Glimpse.NHibernate\package.nuspec -OutputDirectory $build_dir }
}

task publish {
    "Publishing Glimpse.NHibernate.nupkg"
    $apiKey = Read-Host 'Enter your API key: '
    $version = Read-Host 'Enter the number of the version you want to publish: '
    exec { & $tools_dir\NuGet.CommandLine.2.1.0\tools\nuget.exe push $build_dir\Glimpse.NHibernate.$version.nupkg $apiKey }
}

#functions ---------------------------------------------------------------------------------------------------------

function global:delete_directory($directory_name) {
    rd $directory_name -recurse -force -ErrorAction SilentlyContinue | out-null
}