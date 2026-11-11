using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using SoftwareTestingExample.Pages;

namespace SoftwareTestingExample.Tests;

[Parallelizable(ParallelScope.None)]
public class ProductFilterTests : PageTest
{
    [Test]
    public async Task TC17_EcoFriendlyFilterDisplaysOnlyEcoProducts()
    {
        var homePage = new HomePage(Page);
        var listingPage = new ProductListingPage(Page);

        // Navigate to the homepage
        await homePage.NavigateAsync();

        // Capture the original product list
        var initialProducts =
            await listingPage.GetProductNamesAsync();

        Assert.That(
            initialProducts,
            Is.Not.Empty,
            "The homepage should initially display products."
        );

        // Apply the eco-friendly filter
        await listingPage.ApplyEcoFriendlyFilterAsync();

        // Capture the filtered product list
        var filteredProducts =
            await listingPage.GetProductNamesAsync();

        // Verify the filter is selected
        Assert.That(
            await listingPage.IsEcoFriendlyFilterCheckedAsync(),
            Is.True,
            "The eco-friendly filter should be selected."
        );

        // Verify products are displayed
        Assert.That(
            filteredProducts,
            Is.Not.Empty,
            "Eco-friendly products should be displayed."
        );

        // Verify every displayed product has an ECO badge
        var productCount =
            await listingPage.GetProductCountAsync();

        var ecoBadgeCount =
            await listingPage.GetEcoBadgeCountAsync();

        Assert.That(
            ecoBadgeCount,
            Is.EqualTo(productCount),
            "Every displayed product should have an ECO badge."
        );

        // Verify that filtering changed the product list
        Assert.That(
            filteredProducts,
            Is.Not.EqualTo(initialProducts),
            "The eco-friendly filter should change the displayed products."
        );
    }
}