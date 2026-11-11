using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using SoftwareTestingExample.Pages; 

namespace SoftwareTestingExample.Tests;

public class ProductSearchTests : PageTest
{
    [Test]
    public async Task TC06_SearchReturnsMatchingProducts()  // Test case to verify that searching for a product returns matching results
    {
        var homePage = new HomePage(Page);

        // Navigate to homepage
        await homePage.NavigateAsync();

        // Search for Pliers
        await homePage.SearchForProductAsync("Pliers");

        // Wait for search results
        await homePage.WaitForSearchResultsAsync("Pliers");

        // Retrieve product names
        var productNames = await homePage.GetProductNamesAsync();

        // Verify that matching products were returned
        Assert.That(
            productNames.Count,
            Is.GreaterThan(0),
            "Search should return at least one product."
        );

        // Verify every displayed product contains Pliers
        foreach (var productName in productNames)
        {
            Assert.That(
                productName,
                Does.Contain("Pliers").IgnoreCase,
                $"Product '{productName}' should contain Pliers."
            );
        }
    }

    [Test]
    public async Task TC07_SearchReturnsNoMatchingProducts()
    {
        var homePage = new HomePage(Page);

        // Navigate to homepage
        await homePage.NavigateAsync();

        // Search for a product that does not exist
        await homePage.SearchForProductAsync("XYZ123NONEXISTENT");

        // Wait until the application returns zero results
        await homePage.WaitForNoSearchResultsAsync();

        // Retrieve the updated search-result count
        var resultCount = await homePage.GetSearchResultCountTextAsync();

        // Verify that zero products were found
        Assert.That(
            resultCount,
            Does.Contain("0 products found"),
            "Search should report zero matching products."
        );

        // Verify the no-products message
        Assert.That(
            await homePage.IsNoProductsMessageVisibleAsync(),
            Is.True,
            "The no-products message should be displayed."
        );

        // Verify that no product cards remain
        var productCount = await homePage.GetProductCardCountAfterSearchAsync();

        Assert.That(
            productCount,
            Is.EqualTo(0),
            "No product cards should be displayed for an unsuccessful search."
        );

        // Verify that the search field is cleared
        var searchValue = await homePage.GetSearchBoxValueAsync();

        Assert.That(
            searchValue,
            Is.Empty,
            "The search field should be cleared after searching."
        );
    }



    [Test]
    public async Task TC08_SearchReturnsProductsUsingPartialName()
    {
        var homePage = new HomePage(Page);

        // Navigate to homepage
        await homePage.NavigateAsync();

        // Search using a partial product name
        await homePage.SearchForProductAsync("Pli");

        // Wait for matching search results
        await homePage.WaitForSearchResultsAsync("Pli");

        // Retrieve displayed product names
        var productNames = await homePage.GetProductNamesAsync();

        // Verify that at least one product was returned
        Assert.That(
            productNames.Count,
            Is.GreaterThan(0),
            "Partial search should return at least one product."
        );

        // Verify that every product contains the partial search term
        foreach (var productName in productNames)
        {
            Assert.That(
                productName,
                Does.Contain("Pli").IgnoreCase,
                $"Product '{productName}' should contain the partial search term."
            );
        }

        // Verify that the search field clears after submission
        var searchValue = await homePage.GetSearchBoxValueAsync();

        Assert.That(
            searchValue,
            Is.Empty,
            "Search field should clear after submitting a partial search."
        );
    }

    
    [Test]
    public async Task TC09_SearchReturnsHammerProducts()
    {
        var homePage = new HomePage(Page);

        // Navigate to homepage
        await homePage.NavigateAsync();

        // Search for Hammer
        await homePage.SearchForProductAsync("Hammer");

        // Wait for matching search results
        await homePage.WaitForSearchResultsAsync("Hammer");

        // Retrieve displayed product names
        var productNames = await homePage.GetProductNamesAsync();

        // Verify that at least one product was returned
        Assert.That(
            productNames.Count,
            Is.GreaterThan(0),
            "Hammer search should return at least one product."
        );

        // Verify every displayed product contains Hammer
        foreach (var productName in productNames)
        {
            Assert.That(
                productName,
                Does.Contain("Hammer").IgnoreCase,
                $"Product '{productName}' should contain Hammer."
            );
        }

        // Verify that the search field clears after submission
        var searchValue = await homePage.GetSearchBoxValueAsync();

        Assert.That(
            searchValue,
            Is.Empty,
            "Search field should clear after submitting a Hammer search."
        );
    }

    
    [Test]
    public async Task TC10_SearchResultCountMatchesDisplayedProducts()
    {
        var homePage = new HomePage(Page);

        // Navigate to homepage
        await homePage.NavigateAsync();

        // Search for Hammer
        await homePage.SearchForProductAsync("Hammer");

        // Wait for matching products
        await homePage.WaitForSearchResultsAsync("Hammer");

        // Retrieve the count reported by the website
        var reportedCount = await homePage.GetSearchResultCountAsync();

        // Retrieve the actual number of displayed product cards
        var displayedCount = await homePage.GetProductCardCountAfterSearchAsync();

        // Verify that the website's reported count matches displayed products
        Assert.That(
            reportedCount,
            Is.EqualTo(displayedCount),
            "Search-results count should match the number of displayed product cards."
        );

        // Verify that the expected Hammer search returns six products
        Assert.That(
            reportedCount,
            Is.EqualTo(6),
            "Hammer search should return six products."
        );
    }
}





