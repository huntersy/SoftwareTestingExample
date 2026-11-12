# SoftwareTestingExample – Playwright NUnit Automation Framework

[![Playwright NUnit CI](https://github.com/huntersy/SoftwareTestingExample/actions/workflows/playwright.yml/badge.svg)](https://github.com/huntersy/SoftwareTestingExample/actions/workflows/playwright.yml)

A C# UI test automation project demonstrating Playwright for .NET, NUnit, and the Page Object Model (POM). The tests target the Practice Software Testing application and cover homepage behavior, product search, product details, and product filtering.

**Repository:** https://github.com/huntersy/SoftwareTestingExample

**Target application:** https://practicesoftwaretesting.com/

## Project overview

This project demonstrates how to structure and maintain browser-based automated tests using:

- C# and .NET 8
- Microsoft Playwright for .NET
- NUnit test framework
- Page Object Model (POM)
- Git and GitHub
- GitHub Actions workflow configuration

The project emphasizes readable tests, reusable page objects, explicit assertions, configurable test environments, and CI workflow documentation.

## Technology stack

| Technology | Version / purpose |
|---|---|
| .NET | 8.0 |
| C# | Test implementation language |
| Microsoft.Playwright.NUnit | 1.64.1 |
| NUnit | 3.14.0 |
| NUnit3TestAdapter | 4.5.0 |
| Microsoft.NET.Test.Sdk | 17.8.0 |
| NUnit.Analyzers | 3.9.0 |
| coverlet.collector | 6.0.0 |
| Browser | Chromium |
| IDE used during development | Visual Studio Code |
| CI | GitHub Actions |

## Project structure

```text
SoftwareTestingExample/
│
├── .github/
│   └── workflows/
│       └── playwright.yml
│
├── Pages/
│   ├── BasePage.cs
│   ├── CartPage.cs
│   ├── CheckoutPage.cs
│   ├── HomePage.cs
│   ├── LoginPage.cs
│   ├── ProductListingPage.cs
│   ├── ProductPage.cs
│   └── RegisterPage.cs
│
├── TestData/
│   └── TestUsers.cs              # Placeholder
│
├── Tests/
│   ├── CheckoutTests.cs          # Placeholder
│   ├── HomePageTests.cs
│   ├── LoginTests.cs             # Placeholder
│   ├── ProductDetailsTests.cs
│   ├── ProductFilterTests.cs
│   ├── ProductSearchTests.cs
│   └── RegistrationTests.cs      # Placeholder
│
├── Utilities/
│   └── TestConfig.cs
│
├── SoftwareTestingExample.csproj
├── README.md
└── .gitignore
```

The `CheckoutTests.cs`, `LoginTests.cs`, and `RegistrationTests.cs` files are currently placeholders. They do not contain implemented NUnit test cases and are not included in the implemented test count.

## Page Object Model design

The project separates test scenarios from page-specific browser interactions.

- **Tests** contain the test steps and NUnit assertions.
- **Pages** contain page locators and reusable methods for interacting with the application.
- **Utilities** contain shared configuration, including the target base URL.

For example, `HomePage` owns the homepage locators and actions such as navigating, searching, opening the categories menu, and selecting a product. Test classes call those methods rather than repeating the locator definitions.

This separation makes tests easier to read and helps centralize maintenance when the application's UI changes.

## Implemented test coverage

There are currently **16 implemented NUnit test methods across four test classes**.

| Test class | Count | Implemented scenarios |
|---|---:|---|
| `HomePageTests` | 5 | Homepage title is not empty; search box visibility; categories menu expansion; homepage URL; homepage product display |
| `ProductSearchTests` | 5 | Matching Pliers search; no-match search; partial-name search; Hammer search; reported result count compared with displayed product count |
| `ProductDetailsTests` | 5 | Open Pliers from homepage; product name; product price; product image visibility and load state; product description |
| `ProductFilterTests` | 1 | ECO filter selection, non-empty filtered results, ECO badge count, and change from the initial product list |
| **Total** | **16** | **Four test classes with implemented test methods** |

### Test case identifiers

The implemented methods use the following identifiers:

- Homepage: `Homepage_ShouldLoadSuccessfully`, `TC02`, `TC03`, `TC04`, `TC05`
- Search: `TC06` through `TC10`
- Product details: `TC11` through `TC15`
- Product filtering: `TC17`

The current suite contains 16 implemented test methods. Test identifiers are not sequential; TC16 is not currently implemented, while `TC17` covers the eco-friendly product filter. Test identifiers are labels in method names; they do not indicate that every number in the sequence has a corresponding test.

### Scope and rationale

The 16 tests provide focused coverage of the main implemented user journeys: opening the homepage, searching for products, viewing a product, and applying the ECO filter. The goal is to demonstrate meaningful scenarios and maintainable automation rather than inflate the test count with repetitive cases.

Some scenarios intentionally validate specific catalog data. For example, the current tests expect the Pliers price to be `12.01` and the Hammer search to return six products. Those assertions may need maintenance if the target application's catalog changes.

The product-opening test (TC11) verifies that selecting a product navigates away from the homepage and that the resulting page displays a product name. The product-name test (TC12) separately verifies that the displayed product name is exactly "Pliers". These tests cover related functionality but have distinct verification objectives.

## Install and use Playwright

The following instructions use **Windows PowerShell** and the .NET 8 SDK.

### Prerequisites

Install:

1. [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. [Git for Windows](https://git-scm.com/downloads)
3. [Visual Studio Code](https://code.visualstudio.com/) or another C# development environment

Confirm that the .NET SDK is available:

```powershell
dotnet --version
```

The command should report an installed .NET 8 SDK version.

### 1. Clone the repository

```powershell
git clone https://github.com/huntersy/SoftwareTestingExample.git
cd SoftwareTestingExample
```

### 2. Restore the NuGet packages

```powershell
dotnet restore
```

This downloads the project dependencies declared in `SoftwareTestingExample.csproj`, including `Microsoft.Playwright.NUnit`, NUnit, and the .NET test SDK.

**Package installation and browser installation are separate steps.** Restoring the NuGet package installs the Playwright .NET library; it does not, by itself, install the Chromium browser binary.

### 3. Build the project

```powershell
dotnet build
```

The default build configuration is Debug. Building the project generates the Playwright browser installation script under the build output directory.

### 4. Install the Chromium browser

For the default Debug build, run:

```powershell
.\bin\Debug\net8.0\playwright.ps1 install chromium
```

This downloads the Chromium browser build required by the installed Playwright version.

If you build using Release configuration instead:

```powershell
dotnet build --configuration Release
.\bin\Release\net8.0\playwright.ps1 install chromium
```

This project currently configures Chromium for browser execution. It does not currently configure Firefox or WebKit test runs.

### 5. Run the NUnit test suite

```powershell
dotnet test
```

This command builds the test project as needed and asks the .NET test platform to discover and execute the NUnit tests.

To run a specific test method, use the NUnit test name in a filter. For example:

```powershell
dotnet test --filter "FullyQualifiedName~TC06_SearchReturnsMatchingProducts"
```

To list discovered tests without executing them:

```powershell
dotnet test --list-tests
```

### 6. Open the project in Visual Studio Code

From the repository directory:

```powershell
code .
```

You can inspect the page objects, test classes, project dependencies, and GitHub Actions workflow directly in the editor.

## Configuring the target environment

The target URL is configured in `Utilities/TestConfig.cs`:

```csharp
namespace SoftwareTestingExample.Utilities;

public static class TestConfig
{
    public static string BaseUrl =>
        Environment.GetEnvironmentVariable("BASE_URL")
        ?? "https://practicesoftwaretesting.com/";
}
```

By default, the tests navigate to:

```text
https://practicesoftwaretesting.com/
```

To provide a different target URL in PowerShell for the current terminal session:

```powershell
$env:BASE_URL = "https://your-authorized-test-environment.example"
dotnet test
```

The environment variable is read when `TestConfig.BaseUrl` is accessed. Use only an application and environment that you own or are authorized to test.

**Current implementation:** `TC04_HomepageUrlIsCorrect` compares the browser's current URL against `TestConfig.BaseUrl`. This allows the URL assertion to follow the configured target environment when the `BASE_URL` environment variable is supplied.

## GitHub Actions workflow

The workflow is defined in:

```text
.github/workflows/playwright.yml
```

It is configured for manual execution through GitHub Actions using `workflow_dispatch`.

The workflow does not automatically run when commits are pushed to `main` or when pull requests target `main`.

1. Checks out the repository.
2. Sets up .NET 8.
3. Restores NuGet dependencies.
4. Builds the project in Release configuration.
5. Installs Playwright Chromium.
6. Runs the NUnit test suite.
7. Uploads the TRX test-results file as a workflow artifact, even when the test step fails.

The CI workflow uses the Release output path when installing Chromium:

```powershell
.\bin\Release\net8.0\playwright.ps1 install chromium
```

### Current CI validation status

A previous GitHub Actions run successfully completed dependency restoration, compilation, and Chromium installation. The test execution step then failed before the application interface became available.

The runner diagnostics recorded an HTTP `403` response and a page title of `Just a moment...` when requesting the public target website. The tests consequently timed out waiting for the homepage search field.

This indicates that the hosted runner's request was denied or intercepted by a website security challenge. It does **not** establish that the 16 test assertions passed, and it does not establish that the individual test locators are incorrect. The tests could not proceed through their normal application interactions.

**Status distinction:**
- Project build: successful in the observed workflow.
- Playwright Chromium installation: successful in the observed workflow.
- NUnit execution against the public target: blocked before normal test interactions by the HTTP 403/security-challenge response.
- All 16 tests passing in GitHub Actions: **not yet demonstrated**.

Check the repository's [GitHub Actions runs](https://github.com/huntersy/SoftwareTestingExample/actions) for the latest status. CI results can change between runs.

### Authorization and hosted test execution

The target application's repository supplies a restrictive license. The license text reviewed for this project permits private, internal, and non-commercial use, while restricting certain commercial, public-hosting, distribution, and third-party-service uses without prior written permission.

The scope of permission for automated execution from a third-party hosted CI runner should not be assumed. Obtain written authorization from the application owner before relying on GitHub-hosted automation against that application. A test environment that you own or are expressly authorized to use is the preferred long-term CI target.

## Local execution note

On the development computer used for this project, Windows Smart App Control blocked execution/loading of the locally built test assembly. Smart App Control was intentionally left enabled. This is a machine-specific execution restriction, not evidence that the project cannot run on a compatible development machine.

The standard `dotnet test` instructions above describe the normal .NET test workflow. Do not disable Windows security protections merely to reproduce a test run.

## Current limitations

- The implemented suite contains 16 test methods; the checkout, login, and registration test files are placeholders.
- The current test coverage focuses on homepage, search, product details, and ECO filtering.
- The current workflow installs and runs Chromium only.
- Some assertions depend on current public catalog data, such as product price and search-result count.
- The observed GitHub-hosted run was blocked before the application loaded; successful CI test execution has not yet been demonstrated.
- The target application's license and authorization requirements must be resolved before using a hosted CI runner against it.

## Potential future enhancements

Subject to using an authorized test environment, potential next steps include:

- Expand environment configuration to support additional test environments and execution settings.
- Add cart and quantity validation scenarios.
- Add login and registration scenarios.
- Add checkout scenarios.
- Add API-level tests and UI/API integration coverage.
- Add Firefox and WebKit browser projects or CI matrix execution.
- Publish richer test reports and retain Playwright traces or screenshots for failures.
- Add test data management and reusable setup/teardown patterns.

## Project objective

This repository demonstrates practical QA automation skills through a C# Playwright NUnit framework using the Page Object Model. It documents the implemented test scope, environment configuration, browser setup, and the distinction between successful compilation and successful end-to-end execution.

The project is intentionally transparent about current validation limitations. Test results should be reported only when the tests have actually executed and their outcomes have been verified.



