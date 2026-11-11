# Practice Software Testing - Playwright Automation Framework

## Project Overview

A UI automation framework built with C#, Microsoft Playwright for .NET, NUnit, and the Page Object Model (POM) design pattern.

Target application: https://practicesoftwaretesting.com/

## Technology Stack

- C#
- .NET 8
- Microsoft Playwright for .NET 1.64.1
- NUnit 3.14
- Page Object Model (POM)
- Visual Studio Code
- Git and GitHub

## Project Structure

- `Pages/` - Page Object Model classes and reusable page interactions.
- `Tests/` - NUnit automated test cases.
- `TestData/` - Test data.
- `Utilities/` - Shared configuration and utilities.

## Test Coverage

The project targets 40 automated test cases across eight functional areas:

1. Homepage
2. Product Search
3. Product Details
4. Product Filtering and Sorting
5. Shopping Cart
6. User Registration
7. User Login
8. Checkout and Navigation

## Setup

Prerequisites: .NET 8 SDK and Git.

Restore dependencies:

```powershell
dotnet restore
```

Build the project:

```powershell
dotnet build
```

Install Chromium:

```powershell
.\bin\Debug\net8.0\playwright.ps1 install chromium
```

Run automated tests:

```powershell
dotnet test
```

## Project Status

In progress - expanding test coverage and preparing GitHub Actions CI/CD integration.
