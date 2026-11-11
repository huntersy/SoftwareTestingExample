
using Microsoft.Playwright;

namespace SoftwareTestingExample.Pages;

public class ProductPage
{
    private readonly IPage _page;

    // Product page locators
    private ILocator ProductName =>
        _page.Locator("[data-test='product-name']");

    private ILocator ProductPrice =>
        _page.Locator("[data-test='unit-price']");

    private ILocator ProductDescription =>
        _page.Locator("[data-test='product-description']");


    private ILocator ProductImage =>
        _page.Locator("app-detail img").First;


    public ProductPage(IPage page)
    {
        _page = page;
    }

    // Retrieve product information
    public async Task<string> GetProductNameAsync()
    {
        return (await ProductName.InnerTextAsync()).Trim();
    }

    public async Task<string> GetProductPriceAsync()
    {
        return (await ProductPrice.InnerTextAsync()).Trim();
    }

    public async Task<string> GetProductDescriptionAsync()
    {
        return (await ProductDescription.InnerTextAsync()).Trim();
    }


    public async Task<bool> IsProductImageVisibleAsync()
    {
        await ProductImage.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 15000
        });

        return await ProductImage.IsVisibleAsync();
    }


    public async Task<string?> GetProductImageSourceAsync()
    {
        return await ProductImage.GetAttributeAsync("src");
    }



    public async Task<int> GetProductImageCountAsync()
    {
        // Wait until the product name is visible
        await ProductName.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 15000
        });

        // Give the product page an opportunity to render its images
        await _page.Locator("app-detail img").First.WaitForAsync(new()
        {
            State = WaitForSelectorState.Attached,
            Timeout = 15000
        });

        // Count all images within the product details component
        return await _page.Locator("app-detail img").CountAsync();
    }


    public async Task<bool> IsProductImageLoadedAsync()
    {
        return await ProductImage.First.EvaluateAsync<bool>(
            "img => img.complete && img.naturalWidth > 0"
        );
    }


    public async Task<string> GetProductImageDetailsAsync()
    {
        return await ProductImage.First.EvaluateAsync<string>(
            @"img => JSON.stringify({
            src: img.getAttribute('src'),
            complete: img.complete,
            naturalWidth: img.naturalWidth,
            naturalHeight: img.naturalHeight,
            display: getComputedStyle(img).display,
            visibility: getComputedStyle(img).visibility
        })"
        );
    }

}
