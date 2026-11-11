using Microsoft.Playwright;
using SoftwareTestingExample.Utilities;

namespace SoftwareTestingExample.Pages;

public class HomePage
{
    private readonly IPage _page;

    // Locators. The locators are private because they are only used within the HomePage class. The names are unique so no comment is needed to explain what they are. 
    private ILocator SearchBox =>
        _page.Locator("[data-test='search-query']");

    private ILocator CategoriesButton =>
        _page.Locator("[data-test='nav-categories']");

    private ILocator ProductCards =>
    _page.Locator("a[data-test^='product-']");

    private ILocator ProductNames =>
    _page.Locator("[data-test='product-name']");

    //The text reporting how many products were found.
    private ILocator SearchResultCount =>
        _page.Locator("[data-test='search-result-count']");

    // Locator for the "No products found" message
    private ILocator NoProductsMessage =>
        _page.GetByText("There are no products found.", new()
        {
            Exact = true
        });

    public HomePage(IPage page)
    {
        _page = page;
    }



    public async Task NavigateAsync()
    {
        // Navigate until the browser confirms that the response has committed.
        await _page.GotoAsync(TestConfig.BaseUrl, new PageGotoOptions
        {
            WaitUntil = WaitUntilState.Commit,
            Timeout = 30000
        });

        // Wait until the homepage search box is visible and usable.
        await SearchBox.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 30000
        });
    }


    // Retrieve homepage title
    public async Task<string> GetPageTitleAsync()       // Retrieves the title of the current page
    {
        return await _page.TitleAsync();
    }

   public Task<string> GetCurrentUrlAsync()     // Retrieves the current URL of the page
    {
        return Task.FromResult(_page.Url);
    }

    // Search for a product
    public async Task SearchForProductAsync(string searchTerm)
    {
        await SearchBox.FillAsync(searchTerm);
        await SearchBox.PressAsync("Enter");
    }

    // Open Categories dropdown
    public async Task OpenCategoriesMenuAsync()
    {
        await CategoriesButton.ClickAsync();
    }

    // Verify search field visibility
    public async Task<bool> IsSearchBoxVisibleAsync()
    {
        await SearchBox.WaitForAsync(new()      // Wait for the search box to be visible
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });

        return await SearchBox.IsVisibleAsync();
    }

    public async Task<bool> IsCategoriesMenuExpandedAsync()     // Verify Categories dropdown state
    {
        return await CategoriesButton.GetAttributeAsync("aria-expanded") == "true";
    }
    
    public async Task<int> GetProductCardCountAsync()  // Retrieves the number of products displayed
    {
        await ProductCards.First.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000         //waits at most 10 seconds for the first product card to be visible before counting the total number of product cards.
        });

        return await ProductCards.CountAsync();
    }
    
   // Wait until the displayed products match the search term

// Wait until all visible products match the search term
    public async Task WaitForSearchResultsAsync(string searchTerm)
    {
        await _page.WaitForFunctionAsync(
            @"(term) =>
            {
                const productNames = Array.from(
                    document.querySelectorAll('[data-test=""product-name""]')
                ).filter(product => product.getClientRects().length > 0);

                return productNames.length > 0 &&
                    productNames.every(product =>
                        (product.textContent || '')
                            .trim()
                            .toLowerCase()
                            .includes(term.toLowerCase())
                    );
            }",
            searchTerm,
            new PageWaitForFunctionOptions
            {
                Timeout = 15000
            }
        );
    }

    // Wait until a search returns zero products
    public async Task WaitForNoSearchResultsAsync()
    {
        await _page.WaitForFunctionAsync(
            @"() => {
            const resultCount = document.querySelector(
                '[data-test=""search-result-count""]'
            );

            return resultCount &&
                resultCount.textContent.trim().startsWith('0 products found');
        }",
            null,
            new PageWaitForFunctionOptions
            {
                Timeout = 60000
            }
        );
    }

    // Retrieve displayed product names
    public async Task<IReadOnlyList<string>> GetProductNamesAsync()
    {
        return await ProductNames.AllTextContentsAsync();
    }

    // Open a product by its exact name
    public async Task OpenProductAsync(string productName)
    {
        var product = ProductCards.Filter(new()
        {
            Has = _page.GetByText(productName, new()
            {
            Exact = true
            })
        });

        await product.First.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });

        await product.First.ClickAsync();
    }

    // Retrieve the search-results count message
    public async Task<string> GetSearchResultCountTextAsync()
    {
        await SearchResultCount.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 15000
        });

        return (await SearchResultCount.InnerTextAsync()).Trim();
    }

    // Extract the numeric product count from the search-results message
    public async Task<int> GetSearchResultCountAsync()
    {
        var resultText = await GetSearchResultCountTextAsync();

        var match = System.Text.RegularExpressions.Regex.Match(
            resultText,
            @"^\s*(\d+)\s+products found"
        );

        if (!match.Success)
        {
            throw new InvalidOperationException(
                $"Unable to extract product count from: {resultText}"
            );
        }

        return int.Parse(match.Groups[1].Value);
    }

    // Verify that the no-products message is visible
    public async Task<bool> IsNoProductsMessageVisibleAsync()
    {
        await NoProductsMessage.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 15000
        });

        return await NoProductsMessage.IsVisibleAsync();
    }

    // Retrieve the current search-field value
    public async Task<string> GetSearchBoxValueAsync()
    {
        return await SearchBox.InputValueAsync();
    }

    // Retrieve the number of product cards without waiting for one to appear
    public async Task<int> GetProductCardCountAfterSearchAsync()
    {
        return await ProductCards.CountAsync();
    }

}
