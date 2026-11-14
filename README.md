# SoftwareTestingExample – Playwright NUnit Automation Framework

[![Playwright NUnit CI](https://github.com/huntersy/SoftwareTestingExample/actions/workflows/playwright.yml/badge.svg)](https://github.com/huntersy/SoftwareTestingExample/actions/workflows/playwright.yml)

A C# UI automation project using Playwright for .NET, NUnit, and the Page Object Model (POM).

**Repository:** https://github.com/huntersy/SoftwareTestingExample

**Application under test:** A self-contained demo storefront in `TestApplication/`, maintained in this repository.

## Technology stack

- C# and .NET 8
- Microsoft.Playwright.NUnit 1.64.1
- NUnit 3.14.0
- Page Object Model
- Chromium
- Git and GitHub
- GitHub Actions

## Implemented test coverage

The suite contains 16 implemented NUnit test methods across four classes:

| Test class | Count | Coverage |
|---|---:|---|
| `HomePageTests` | 5 | Homepage title, search visibility, categories menu, URL, product cards |
| `ProductSearchTests` | 5 | Matching search, no-match search, partial search, Hammer search, result-count accuracy |
| `ProductDetailsTests` | 5 | Open product, product name, price, image visibility/load, description |
| `ProductFilterTests` | 1 | ECO filter selection and filtered product/badge validation |
| **Total** | **16** | |

The test identifiers are not sequential: the filter test is named `TC17`; `TC16` is not currently implemented.

## Application under test

The static storefront in `TestApplication/` is part of this repository and does not depend on a third-party website or service. It implements the UI elements and behaviors exercised by the current tests:

- Product listing and product cards
- Search by full or partial product name
- No-results message and result count
- Categories menu expansion
- ECO-friendly filtering and badges
- Product details, price, description, and image

The demo catalog intentionally includes four product names containing “Pliers” and six containing “Hammer” to support the existing search assertions. The Pliers product price is `12.01`.

## Run locally on Windows

Prerequisites: .NET 8 SDK, Git, and Python 3.

1. Clone the repository and open its directory.
2. Restore and build:

```powershell
dotnet restore
dotnet build
.\bin\Debug\net8.0\playwright.ps1 install chromium
```

3. In a separate PowerShell terminal, start the local application server from the repository root:

```powershell
py -m http.server 4173 --directory TestApplication
```

4. In the project terminal, configure the test URL and run the suite:

```powershell
$env:BASE_URL = "http://127.0.0.1:4173/"
dotnet test
```

Keep the local server running while tests execute. Stop it with Ctrl+C.

## GitHub Actions

The workflow at `.github/workflows/playwright.yml` runs automatically when changes are pushed to the `controlled-test-application` branch. It can also be started manually through GitHub Actions using `workflow_dispatch`.

The workflow restores and builds the .NET project, installs Chromium, starts the repository's demo application on the runner, runs the NUnit suite against that local URL, and uploads the TRX test report.

The workflow uses `BASE_URL=http://127.0.0.1:4173/` for the test step. No external application is contacted during the CI test run.

## CI validation status

The GitHub Actions run for commit `462422c1e27d503562ba2126e0960afc56b6f012` completed successfully:

- **Passed:** 16
- **Failed:** 0
- **Skipped:** 0
- **Total:** 16

[View the successful GitHub Actions run](https://github.com/huntersy/SoftwareTestingExample/actions/runs/43500482934).

The development computer previously blocked the locally built test assembly through its Windows Application Control policy. This project does not require disabling Windows security controls. GitHub-hosted CI provides a separate environment for validating the test suite.

## Project structure

```text
SoftwareTestingExample/
├── .github/workflows/playwright.yml
├── Pages/
├── Tests/
├── Utilities/
├── TestApplication/
│   ├── index.html
│   ├── product.html
│   ├── app.js
│   └── product-image.svg
├── SoftwareTestingExample.csproj
└── README.md
```

## Potential enhancements

- Add cart, checkout, login, and registration scenarios
- Add API and UI/API integration tests
- Add Firefox and WebKit execution
- Retain Playwright traces and screenshots for failed tests
- Add richer test reporting and test-data management
