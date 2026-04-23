using Microsoft.JSInterop;

namespace Bookstore.Services;

public class IndexedDbService
{
    private readonly IJSRuntime _js;
    private const string DbName = "BookstoreDb";
    private const string CartStore = "cart";
    private const int DbVersion = 1;

    public IndexedDbService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task SaveCartAsync(List<CartItem> items)
    {
        await _js.InvokeVoidAsync("indexedb.saveCart", DbName, CartStore, DbVersion, items);
    }

    public async Task<List<CartItem>> LoadCartAsync()
    {
        return await _js.InvokeAsync<List<CartItem>>("indexedb.loadCart", DbName, CartStore, DbVersion);
    }

    public async Task ClearCartAsync()
    {
        await _js.InvokeVoidAsync("indexedb.clearCart", DbName, CartStore, DbVersion);
    }

    public async Task SaveWalletBalanceAsync(decimal balance)
    {
        await _js.InvokeVoidAsync("indexedb.saveWalletBalance", DbName, "wallet", DbVersion, balance);
    }

    public async Task<decimal> LoadWalletBalanceAsync()
    {
        var result = await _js.InvokeAsync<decimal?>("indexedb.loadWalletBalance", DbName, "wallet", DbVersion);
        return result ?? 100.00m;
    }
}