# Updating Workloads

Always make sure to update the dotnet workloads:

> dotnet workload update

# Updating Version

- Increment 'ApplicationVersion'
- Update 'AssemblyVersion'
- Update 'FileVersion'
- Update 'ApplicationDisplayVersion'
- In 'Package.appxmanifest' for Windows platform, update 'Version' in 'Identity'
- Document new version in 'CHANGELOG.md'

# Updating Copyright

- Update copyright in BuzzBoxGamesApp project file
- Update copyright information in 'Info.plist' for MacCatalyst platform. VALIDATE IF NEEDED???

# Building

## Windows

- The signing certificate needs to be added to the Certificate Manager ('certmgr')
- The certificate thumbnail needs to be known
- In the BuzzBoxGamesApp project file, 'PackageCertificateThumbprint' may need to be updated to the current thumbnail
- Make sure that 'Package.appxmanifest' is updated with the correct version

### 64-bit Windows
> dotnet publish -f net8.0-windows10.0.19041.0 -c Release /p:RuntimeIdentifierOverride=win10-x64