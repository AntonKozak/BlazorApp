using Microsoft.JSInterop;

namespace BlazorApp.Data.Services.Extensions;

public static class IUSRuntimeExtension
{
    public static async Task ToasterSuccess(this IJSRuntime jsRuntime, string message)
    {
        await jsRuntime.InvokeVoidAsync("ShowToastr", "success", message);
    }

    public static async Task ToasterError(this IJSRuntime jsRuntime, string message)
    {
        await jsRuntime.InvokeVoidAsync("ShowToastr", "error", message);
    }

    public static async Task ToasterInfo(this IJSRuntime jsRuntime, string message)
    {
        await jsRuntime.InvokeVoidAsync("ShowToastr", "info", message);
    }
    public static async Task ToasterWarning(this IJSRuntime jsRuntime, string message)
    {
        await jsRuntime.InvokeVoidAsync("ShowToastr", "warning", message);
    }
}
