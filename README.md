# SiteSync Tracker

A high-contrast, mobile-first inventory and material management system designed explicitly for boots-on-the-ground construction crews.

## 🏗️ The Problem
Heavy enterprise construction software is often too complex and slow for field operations. Workers still rely on paper manifests, leading to lost materials, staging confusion, and communication delays between the layout yard and off-site supervisors.

## ⚙️ Our Solution
SiteSync Tracker bridges the gap with a lightweight, tap-friendly UI optimized for outdoor mobile use. It allows field workers to instantly update tracking milestones and scan manifests using simple hardware automation.

## 🛠️ Architecture & Tech Stack
- **Frontend Framework:** .NET MAUI (Windows)
- **Database Layer:** SQLite Local Storage managed via Entity Framework Core (EF Core)
- **Design Pattern:** MVVM (Model-View-ViewModel) for clean state separation

## 📦 Microsoft Store packaging

Debug runs unpackaged (`WindowsPackageType=None`). Release builds a self-contained MSIX for Partner Center.

```powershell
dotnet publish SiteSyncTracker.csproj -f net10.0-windows10.0.19041.0 -c Release -p:RuntimeIdentifierOverride=win-x64
```

The `.msix` is written under `AppPackages\SiteSyncTracker_<version>_Test\`.

Before a live Store submission:

1. Reserve the app name in [Partner Center](https://partner.microsoft.com/dashboard).
2. In Visual Studio, use **Publish → Associate App with the Store** so `Package.appxmanifest` gets the real publisher identity.
3. Upload the MSIX (or `.msixupload`) in Partner Center. Partner Center signs the package.
