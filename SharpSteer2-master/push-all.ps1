function Push-Nuget($path, $csproj) {
    $fullPathToCsprog = Join-Path -Path $path -ChildPath $csproj -resolve;
    
    nuget pack $fullPathToCsprog -Prop Configuration=Release -IncludeReferencedProjects
    
    get-childitem -Filter *.nupkg -name | foreach ($_) {
        Write-Host "Pushing " $_ -backgroundcolor darkgreen -foregroundcolor white;
    
        nuget push $_
        Remove-Item $_
        
        Write-Host "Done " $_ -backgroundcolor darkgreen -foregroundcolor white;
    }
}

Push-Nuget "SharpSteer2" "SharpSteer2.csproj"