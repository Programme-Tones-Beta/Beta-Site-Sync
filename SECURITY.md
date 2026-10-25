# Security Policy

## Scope

SiteSync Tracker is a .NET MAUI Windows application with a local SQLite database. The repository also contains an ASP.NET-compatible `Web.config` at the project root, but the current `SiteSyncTracker.csproj` is a Windows MAUI project rather than an ASP.NET web application.

Security issues may affect:

- Local inventory data stored in `sitesync.db`.
- Manifest numbers, material descriptions, quantities, locations, GPS coordinates, and timestamps.
- Notification recipients such as email addresses or phone numbers.
- Build, packaging, and deployment configuration.
- Any future ASP.NET API, web front end, or synchronization service added to the project.

## Supported Versions

This project has not yet published stable releases. Until a release policy is established, security fixes should target the current development branch and the latest tagged release, if one exists.

| Version | Supported |
| --- | --- |
| Current development branch | Yes |
| Latest tagged release | Yes, when maintained |
| Older tagged releases | No guarantee |
| Unreleased or abandoned branches | No |

The application version currently declared in the MAUI project is `1.0.0`. This is a package version, not a guarantee of production security support.

## Reporting a Vulnerability

Do not open a public GitHub issue for an unpatched vulnerability. Report security issues privately to the repository maintainers through the repository's private vulnerability reporting feature or the private security contact configured by the project owner.

Include, when available:

1. A short description of the issue and its security impact.
2. The affected version, commit, project, page, endpoint, or platform.
3. Reproduction steps or a minimal proof of concept.
4. Required permissions, account state, device state, or network position.
5. Logs, screenshots, HTTP requests, stack traces, or database evidence with secrets and personal data removed.
6. A suggested mitigation or patch, if you have one.

Remove passwords, API keys, access tokens, private keys, personal data, production database files, and customer information before sending a report.

## Response Targets

Maintainers should acknowledge a private report within 5 business days, provide an initial assessment within 10 business days, and communicate remediation progress when the issue requires more time. These are project targets rather than contractual guarantees.

Reporters should allow reasonable time for investigation and remediation before public disclosure. Coordinated disclosure dates should be agreed with the maintainers.

## Handling and Remediation

Maintainers should:

- Confirm the affected component and supported versions.
- Reproduce the issue in an isolated development environment.
- Assess confidentiality, integrity, and availability impact.
- Identify whether local files, packaged builds, APIs, or dependencies are affected.
- Create a private fix and regression test where practical.
- Review dependency and build changes before release.
- Document the affected versions and mitigation steps.
- Publish a release note or advisory after a fix is available.

## Security Expectations for Development

- Never commit passwords, API keys, signing certificates, tokens, or connection strings.
- Do not store production secrets in XAML, source code, `Web.config`, project files, or container images.
- Protect local SQLite files using operating-system access controls and avoid copying them into issue attachments.
- Validate manifest numbers, descriptions, quantities, locations, notification addresses, and GPS data at every input boundary.
- Use parameterized database operations through Entity Framework Core.
- Apply authorization checks to every future ASP.NET endpoint, including read-only inventory endpoints.
- Use HTTPS in deployed ASP.NET environments and configure secure cookies, anti-forgery protection, and appropriate security headers.
- Avoid logging notification addresses, credentials, tokens, or complete manifest records unless required for diagnostics.
- Keep .NET, MAUI, ASP.NET, Entity Framework Core, and third-party packages patched.
- Review release and MSIX packaging settings before distribution.
- Run dependency, static-analysis, and secret-scanning checks in CI before publishing builds.

## Known Project Limitations

The current application is a local Windows MAUI prototype. Its form controls and dashboard values are partly static, and notification delivery, authentication, authorization, barcode scanning, and remote synchronization are not implemented. These capabilities require a security review before production use.

The repository's root `Web.config` targets ASP.NET Framework 4.8, while the MAUI project targets modern .NET for Windows. Adding ASP.NET support requires a separate ASP.NET project or a deliberate migration; a `Web.config` file alone does not make the MAUI application an ASP.NET application.

## Security Updates

Security changes should be included in the project changelog or release notes with:

- Affected versions.
- Fixed versions or commits.
- Severity and impact.
- Upgrade or mitigation instructions.
- Credit to the reporter when permission is granted.
