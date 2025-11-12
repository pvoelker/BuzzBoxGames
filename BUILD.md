# Transferring Development Certificates

## MacOS

Use **xcode** to export the certificates as 'p12' files.  Then import on the target machine using the passwords applied when the 'p12' files were created.  This is the only way to transfer the certificates with the private keys intact.

# Updating Workloads

Always make sure to update the dotnet workloads:

> dotnet workload update

# Updating Version

File version format is: [major].[minor].[build].[revision].  Build number never resets and keeps incrementing.  Revision will typically be zero (0)
Using:
- https://semver.org/spec/v2.0.0.html
- https://learn.microsoft.com/en-us/dotnet/standard/library-guidance/versioning

- Increment 'ApplicationVersion'
- Update 'AssemblyVersion'
- Update 'FileVersion'
- Update 'ApplicationDisplayVersion'
- In 'Package.appxmanifest' for Windows platform, update 'Version' in 'Identity'
- Document new version in 'CHANGELOG.md'

# Updating Copyright

- Update copyright in BuzzBoxGamesApp project file
- Update copyright information in 'Info.plist' for MacCatalyst platform.

# Building

## Windows

- The signing certificate needs to be added to the Certificate Manager ('certmgr') or need to use a certificate dongle
- The certificate thumbnail/hash needs to be known
- In the BuzzBoxGamesApp project file, 'PackageCertificateThumbprint' may need to be updated to the current thumbnail/hash

### 64-bit Windows
> dotnet publish -f net9.0-windows10.0.19041.0 -c Release

## MacOS (Catalyst)

Reference: https://learn.microsoft.com/en-us/dotnet/maui/mac-catalyst/deployment/publish-outside-app-store

> dotnet publish -f net9.0-maccatalyst -c Release

You may need to enter the admin password for the build machine serveral times.

Submit package for notarization. Use an app-specific password generated at appleid.apple.com:

> xcrun notarytool submit {my_package_filename} --wait --apple-id {my_apple_id} --password {my_app_specific_password} --team-id {my_team_id}

'Staple' the return to the package:

> xcrun stapler staple {filename}.pkg

**Note:** Because on MacOS the HID interfaces are being accessed directly, an *Input Monitoring* warning will trigger.  At this time it looks like this does **not** need to be set