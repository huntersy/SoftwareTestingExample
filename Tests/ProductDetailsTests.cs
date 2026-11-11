
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using SoftwareTestingExample.Pages;

namespace SoftwareTestingExample.Tests;

public class ProductDetailsTests : PageTest
{
    [Test]
    public async Task TC11_OpenProductFromHomepage()
    {
        // Arrange: Create the page objects
        var homePage = new HomePage(Page);
        var productPage = new ProductPage(Page);

        // Act: Navigate to the homepage
        await homePage.NavigateAsync();

        // Open the product named exactly "Pliers"
        await homePage.OpenProductAsync("Pliers");

        // Assert: Verify the product details page
        var productName = await productPage.GetProductNameAsync();

        Assert.That(
            productName,
            Is.EqualTo("Pliers"),
            "The product details page should display the Pliers product."
        );
    }

    
    [Test]
    public async Task TC12_ProductNameIsDisplayedCorrectly()
    {
        // Arrange
        var homePage = new HomePage(Page);
        var productPage = new ProductPage(Page);

        // Act
        await homePage.NavigateAsync();
        await homePage.OpenProductAsync("Pliers");

        // Retrieve the displayed product name
        var productName = await productPage.GetProductNameAsync();

        // Assert
        Assert.That(
            productName,
            Is.EqualTo("Pliers"),
            "The product name should be displayed correctly."
        );
    }


    [Test]
    public async Task TC13_ProductPriceIsDisplayedCorrectly()
    {
        // Arrange
        var homePage = new HomePage(Page);
        var productPage = new ProductPage(Page);

        // Act
        await homePage.NavigateAsync();
        await homePage.OpenProductAsync("Pliers");

        // Retrieve the displayed product price
        var productPrice = await productPage.GetProductPriceAsync();

        // Assert
        Assert.That(
            productPrice,
            Is.EqualTo("12.01"),
            "The Pliers product price should be displayed as 12.01."
        );
    }




    [Test]
    public async Task TC14_ProductImageIsDisplayedCorrectly()
    {
        var homePage = new HomePage(Page);
        var productPage = new ProductPage(Page);

        await homePage.NavigateAsync();
        await homePage.OpenProductAsync("Pliers");

        var imageIsVisible = await productPage.IsProductImageVisibleAsync();
        var imageIsLoaded = await productPage.IsProductImageLoadedAsync();

        Assert.That(
            imageIsVisible,
            Is.True,
            "The Pliers product image should be visible."
        );

        Assert.That(
            imageIsLoaded,
            Is.True,
            "The Pliers product image should load successfully."
        );
    }


    [Test]
    public async Task TC15_ProductDescriptionIsDisplayedCorrectly()
    {
        // Arrange
        var homePage = new HomePage(Page);
        var productPage = new ProductPage(Page);

        // Act
        await homePage.NavigateAsync();
        await homePage.OpenProductAsync("Pliers");

        var productDescription =
            await productPage.GetProductDescriptionAsync();

        // Assert
        Assert.That(
            productDescription,
            Is.Not.Null.And.Not.Empty,
            "The product description should be displayed."
        );
    }




}



