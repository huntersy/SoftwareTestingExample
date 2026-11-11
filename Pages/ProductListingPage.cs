using Microsoft.Playwright;

namespace SoftwareTestingExample.Pages;

public class ProductListingPage
{
    private readonly IPage _page;

    private ILocator ProductCards =>
        _page.Locator("a.card[data-test^='product-']");

    private ILocator EcoFriendlyFilter =>
        _page.Locator("[data-test='eco-friendly-filter']");

    private ILocator EcoBadges =>
        ProductCards.Locator("[data-test='eco-badge']");

    public ProductListingPage(IPage page)
    {
        _page = page;
    }

    public async Task<List<string>> GetProductNamesAsync()
    {
        // Wait until at least one product card is displayed.
        await ProductCards.First.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 30000
        });

        // Retrieve the product names after rendering.
        var names = await ProductCards.Locator(
            "[data-test='product-name']"
        ).AllInnerTextsAsync();

        return names
            .Select(name => name.Trim())
            .ToList();
    }

    public async Task ApplyEcoFriendlyFilterAsync()
    {
        await EcoFriendlyFilter.CheckAsync();

        await EcoBadges.First.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 30000
        });
    }

    public async Task<bool> IsEcoFriendlyFilterCheckedAsync()
    {
        return await EcoFriendlyFilter.IsCheckedAsync();
    }

    public async Task<int> GetProductCountAsync()
    {
        return await ProductCards.CountAsync();
    }

    public async Task<int> GetEcoBadgeCountAsync()
    {
        return await EcoBadges.CountAsync();
    }
}