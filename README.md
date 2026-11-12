# Practice Software Testing — Playwright Automation Framework

[![Playwright NUnit CI](https://github.com/huntersy/SoftwareTestingExample/actions/workflows/playwright.yml/badge.svg)](https://github.com/huntersy/SoftwareTestingExample/actions/workflows/playwright.yml)

A UI test automation portfolio project built with **C#**, **.NET 8**, **Microsoft Playwright for .NET**, **NUnit**, and the **Page Object Model (POM)** design pattern.

**Repository:** https://github.com/huntersy/SoftwareTestingExample
**Target application:** https://practicesoftwaretesting.com/

> **Execution status:** The project builds successfully in GitHub Actions. At the time of this documentation, browser-test execution against the public target application is blocked because the GitHub-hosted runner receives an HTTP 403 response and a security-challenge page instead of the application. See [CI/CD execution status](#cicd-execution-status).

---

## Table of Contents

- [Project Overview](#project-overview)
- [Project Objectives](#project-objectives)
- [Technology Stack](#technology-stack)
- [Framework Architecture](#framework-architecture)
- [Test Strategy and Coverage](#test-strategy-and-coverage)
- [Why 16 Test Cases](#why-16-test-cases)
- [Configuration](#configuration)
- [Getting Started](#getting-started)
- [Running the Tests](#running-the-tests)
- [Continuous Integration](#continuous-integration)
- [CI/CD Execution Status](#cicd-execution-status)
- [Troubleshooting and Investigation](#troubleshooting-and-investigation)
- [Known Limitations](#known-limitations)
- [Future Enhancements](#future-enhancements)
- [Author's Notes](#authors-notes)

---

## Project Overview

This project demonstrates the design and implementation of a maintainable browser-based test automation framework for an e-commerce application.

The framework uses Playwright to interact with the application through a browser and NUnit to organize and execute automated test cases. The Page Object Model separates page-specific locators and interactions from test logic, helping make the suite easier to read, maintain, and extend.

The current suite contains **16 automated test cases** focused on representative user-facing functionality. The goal is to demonstrate sound automation design and meaningful coverage rather than maximize the number of test cases.

## Project Objectives

- Build a reusable UI automation framework using C# and Playwright.
- Apply the Page Object Model to separate page interactions from test assertions.
- Create readable NUnit tests with clear expected outcomes.
- Use maintainable locators and reusable page methods.
- Support environment-specific application URLs through configuration.
- Integrate build and test execution into a GitHub Actions workflow.
- Investigate and document failures using CI logs and test-result artifacts.

## Technology Stack

| Technology | Purpose |
|---|---|
| C# | Automation programming language |
| .NET 8 | Application and test runtime |
| Microsoft Playwright for .NET 1.64.1 | Browser automation |
| NUnit 3.14 | Test framework and assertions |
| NUnit3TestAdapter 4.5.0 | NUnit test discovery and execution through .NET test tools |
| Microsoft.NET.Test.Sdk 17.8.0 | .NET test infrastructure |
| Visual Studio Code | Development environment |
| Git and GitHub | Source control and repository hosting |
| GitHub Actions | Continuous integration workflow |

## Framework Architecture

The project follows the Page Object Model (POM) design pattern.

In this pattern, page classes encapsulate page-specific locators and actions. Test classes use those page objects to perform workflows and verify expected results. This reduces duplicated browser interaction code and keeps test intent separate from page implementation details.

### Project structure

```text
SoftwareTestingExample/
│
├── Pages/
│   └── Page Object classes and reusable page interactions
│
├── Tests/
│   └── NUnit automated test cases
│
├── TestData/
│   └── Test data used by automated scenarios
│
├── Utilities/
│   └── Shared configuration and utilities
│       └── TestConfig.cs
│
├── .github/
│   └── workflows/
│       └── playwright.yml
│
├── SoftwareTestingExample.csproj
├── README.md
└── .gitignore
```

The exact set of page and test classes may evolve as the framework is extended.

### Page Object Model responsibilities

- **Page classes:** Define page-specific locators and reusable actions.
- **Test classes:** Arrange and execute scenarios, then assert expected outcomes.
- **Utilities:** Hold shared configuration and supporting functionality.
- **Test data:** Keep reusable test inputs separate from test implementation where appropriate.

## Test Strategy and Coverage

The 16 test cases were selected to exercise representative e-commerce workflows and demonstrate a range of UI automation techniques.

The current coverage includes:

| Functional area | Representative validation |
|---|---|
| Homepage | Page navigation, successful loading, and key interface visibility |
| Category navigation | Category menu interaction and navigation behavior |
| Product listings | Product cards and listing visibility |
| Product search | Matching results, partial search terms, and no-result behavior |
| Search results | Result content and result-count validation |
| Product details | Opening a product and validating displayed details |
| Product information | Price and product-image checks |

The scenarios include positive and negative search conditions. For example, search behavior is checked with matching terms, partial terms, and a term expected to return no products.

> The table summarizes the implemented coverage at a functional level. It is not a claim that every possible workflow in the application has been automated.

## Why 16 Test Cases

The project currently contains 16 automated test cases. The suite was intentionally kept focused on representative scenarios rather than expanded simply to reach a larger test count.

The test-design approach was to:

1. Review the application's primary user-facing functionality.
2. Identify observable behaviors and expected outcomes.
3. Select representative positive and negative scenarios.
4. Organize page interactions into reusable Page Object classes.
5. Keep test methods focused on a specific behavior or validation.
6. Build a foundation that can be extended as additional workflows become available.

A smaller, maintainable suite with clear assertions and reusable architecture is more valuable than a larger suite containing repetitive tests or fragile implementation.

## Configuration

The application URL is configured in `Utilities/TestConfig.cs`:

```csharp
namespace SoftwareTestingExample.Utilities;

public static class TestConfig
{
    public static string BaseUrl =>
        Environment.GetEnvironmentVariable("BASE_URL")
        ?? "https://practicesoftwaretesting.com/";
}
```

The framework reads the `BASE_URL` environment variable when it is defined. If it is not defined, the public Practice Software Testing URL is used as the default.

This allows the same test code to target different authorized environments without changing individual test cases.

### Set the base URL in PowerShell

For the current PowerShell session:

```powershell
$env:BASE_URL = "https://your-authorized-test-environment.example"
```

Replace the example address with an application environment you are authorized to test. The environment variable is session-scoped and is not permanently saved by this command.

## Getting Started

### Prerequisites

- Windows, macOS, or Linux development environment.
- .NET 8 SDK.
- Git.
- Visual Studio Code or another C#-compatible IDE.
- An application environment that you are authorized to access and automate.

### Clone the repository

```powershell
git clone https://github.com/huntersy/SoftwareTestingExample.git
cd SoftwareTestingExample
```

### Restore dependencies

```powershell
dotnet restore
```

### Build the project

```powershell
dotnet build
```

### Install the Playwright Chromium browser

After building the project, install the browser:

```powershell
.\bin\Debug\net8.0\playwright.ps1 install chromium
```

The browser installation command downloads the browser binaries required by Playwright.

## Running the Tests

When the test environment is accessible and the local security configuration permits test execution, the standard NUnit command is:

```powershell
dotnet test
```

To target a different authorized environment for the current PowerShell session:

```powershell
$env:BASE_URL = "https://your-authorized-test-environment.example"
dotnet test
```

**Environment note:** On the original development machine, Windows Smart App Control blocked local test-assembly execution. Smart App Control was intentionally left enabled. The project was therefore built locally, while browser-test execution was investigated through GitHub Actions. Do not interpret a successful build as proof that the tests executed successfully.

## Continuous Integration

The repository includes a GitHub Actions workflow at:

```text
.github/workflows/playwright.yml
```

The workflow is configured to run on:

- Pushes to the `main` branch.
- Pull requests targeting `main`.
- Manual execution through `workflow_dispatch`.

### CI workflow stages

1. Check out the repository.
2. Set up the .NET 8 SDK.
3. Restore NuGet dependencies.
4. Build the project in Release configuration.
5. Install Playwright Chromium.
6. Execute the NUnit test suite.
7. Upload the TRX test-result file as a workflow artifact, including when the test step fails.

### View workflow runs

[Open GitHub Actions](https://github.com/huntersy/SoftwareTestingExample/actions)

Open a workflow run and select the **Build and Run Playwright Tests** job to inspect individual steps and logs.

## CI/CD Execution Status

The GitHub Actions workflow has successfully completed dependency restoration, Release compilation, and Chromium installation.

However, browser-test execution against the public target application is currently blocked. The GitHub-hosted runner receives:

- HTTP response status: `403`
- Final URL: `https://practicesoftwaretesting.com/`
- Page title: `Just a moment...`

The expected application interface does not load. Playwright consequently times out while waiting for the homepage search control.

The investigation indicates that the public target's security protection is challenging or denying automated requests from the hosted runner. The available evidence does not establish that the individual test assertions or locators are defective; the tests cannot reach the expected application state.

The test-result artifact and GitHub Actions logs were used to identify the response. The workflow currently demonstrates CI setup and build execution, but **the browser-test suite is not yet passing in GitHub-hosted CI**.

The next step is to establish an authorized and reliable test environment that the CI runner can access. The test suite should not be changed merely to conceal or work around the access restriction.

## Troubleshooting and Investigation

The CI investigation followed a diagnostic process:

1. Opened the failed GitHub Actions run and inspected the job steps.
2. Confirmed that dependency restoration and Release build completed successfully.
3. Confirmed that Playwright Chromium installed successfully.
4. Downloaded the NUnit TRX test-result artifact.
5. Retrieved the job logs and searched for navigation diagnostics.
6. Identified the HTTP 403 response and security-challenge page.
7. Distinguished an environment-access failure from a test assertion failure.

This investigation is documented to show the troubleshooting process and the importance of examining execution evidence before changing test code.

## Known Limitations

- The public target application may restrict automated traffic from hosted CI infrastructure.
- GitHub-hosted browser-test execution is currently blocked by the observed HTTP 403/security-challenge response.
- Local test execution on the original Windows development machine is restricted by Smart App Control; that protection remains enabled.
- Cart, registration, login, checkout, and API workflows are not represented as completed coverage in the current 16-test suite.
- The framework requires an authorized, accessible application environment for repeatable end-to-end execution.

## Future Enhancements

Potential improvements include:

- Establish an authorized application environment for repeatable CI execution.
- Add shopping-cart and quantity-validation scenarios.
- Add registration and login validation.
- Add checkout and navigation workflows.
- Expand negative and boundary-value testing.
- Add API tests and UI/API integration scenarios.
- Add cross-browser execution where supported by the test environment.
- Capture screenshots and diagnostic traces on failure.
- Publish richer test reports and trend results.
- Add a test-case traceability matrix linking requirements, scenarios, and assertions.

## Author's Notes

This project is an ongoing demonstration of practical UI automation engineering: framework design, test-case selection, reusable page objects, source control, CI configuration, and evidence-based troubleshooting.

The current priority is reliable and authorized test execution—not increasing the test count for its own sake.

---

## License and Third-Party Application Notice

This repository contains the test automation project. The target Practice Software Testing application is a separate third-party project and is not included in this repository.

The target application's published license, as reviewed for this project, permits private, internal, and non-commercial use, subject to its stated restrictions. It reserves commercial use, public hosting, redistribution, and third-party service use unless prior written permission is obtained.

Before running a copy of that application in a hosted CI service or using it in any other way not clearly covered by its license, obtain clarification or written permission from the copyright holder. Do not assume that public source-code availability grants permission for public deployment or unrestricted hosted use.
