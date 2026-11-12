using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using SoftwareTestingExample.Utilities;
using SoftwareTestingExample.Pages;

namespace SoftwareTestingExample.Tests;

public class HomePageTests : PageTest
{
    [Test]
    public async Task Homepage_ShouldLoadSuccessfully()
    {
        var homePage = new HomePage(Page);

        await homePage.NavigateAsync();

        var title = await homePage.GetPageTitleAsync();  //retrieves the page title.

        Assert.That(title, Is.Not.Empty,
            "Homepage should have a page title.");   //verifies that the homepage is not empty.
    }

    [Test]
    public async Task TC02_SearchBoxIsVisible()
    {
        var homePage = new HomePage(Page);

        await homePage.NavigateAsync();

        Assert.That(
            await homePage.IsSearchBoxVisibleAsync(),
            Is.True,
            "Search box should be visible on the homepage."
        );
    }

    [Test]
    public async Task TC03_CategoriesMenuOpensSuccessfully()
    {
        var homePage = new HomePage(Page);

        await homePage.NavigateAsync();

        await homePage.OpenCategoriesMenuAsync();

        Assert.That(
            await homePage.IsCategoriesMenuExpandedAsync(),
            Is.True,
            "Categories menu should expand after clicking."
        );
    }

    [Test]
    public async Task TC04_HomepageUrlIsCorrect()
    {
        var homePage = new HomePage(Page);

        await homePage.NavigateAsync();

        var expectedUrl = new Uri(TestConfig.BaseUrl).AbsoluteUri;
        var actualUrl = new Uri(await homePage.GetCurrentUrlAsync()).AbsoluteUri;

        Assert.That(
            actualUrl,
            Is.EqualTo(expectedUrl),
            "Homepage URL should match the configured BASE_URL."
        );
    }

    [Test]
    public async Task TC05_HomepageDisplaysProducts()
    {
        var homePage = new HomePage(Page);

        await homePage.NavigateAsync();

        var productCount = await homePage.GetProductCardCountAsync();

        Assert.That(
            productCount,
            Is.GreaterThan(0),
            "Homepage should display at least one product."
        );
    }
}