
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using SoftwareTestingExample.Pages;

namespace SoftwareTestingExample.Tests;

public class ProductDetailsTests : PageTest
{
    [Test]
    public async Task TC11_OpenProductFromHomepage()
    {
        var homePage = new HomePage(Page);
        var productPage = new ProductPage(Page);

        await homePage.NavigateAsync();

        var homepageUrl = Page.Url;

        await homePage.OpenProductAsync("Pliers");

        Assert.That(
            Page.Url,
            Is.Not.EqualTo(homepageUrl),
            "Selecting a product should navigate away from the homepage."
        );

        Assert.That(
            await productPage.GetProductNameAsync(),
            Is.Not.Empty,
            "The product details page should display a product name."
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



