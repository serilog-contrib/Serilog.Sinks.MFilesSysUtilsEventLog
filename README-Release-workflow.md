# Serilog.Sinks.MFilesSysUtilsEventLog RELEASE WORFLOW

- Branch `master` contains the latest stable code and release.
- Develop in branch `develop` or a `feature/something` branch.
- Merge to `master` when about to release.
- Update the package **version number** in the project properties.
- Do a Release configuration build; directory `\current` will contain the latest nuget package `Serilog.Sinks.MFilesSysUtilsEventLog.x.x.x.nupkg` file
- Merge to `Release` branch, where the GitHub Action `Publish-current-to-nuget` will publish the NuGET package to nuget.org.
- Check the release on the [NuGet.org Serilog.Sinks.MFilesSysUtilsEventLog page](https://www.nuget.org/packages/Serilog.Sinks.MFilesSysUtilsEventLog/)
