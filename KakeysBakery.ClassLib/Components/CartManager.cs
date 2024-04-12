using System.Net.Http.Json;

using KakeysBakeryClassLib.Data;

using Microsoft.AspNetCore.Components;

namespace KakeysBakeryClassLib.Components;

public class CartManager(HttpClient client)
{
    public int CurrentGoodTypeId { get; set; }
    public List<Basegood> CurrentDetails { get; set; } = new();
    public Basegood? Selected { get; set; }

    public async Task UpdateCurrentGood(int baseGoodTypeId)
    {
        CurrentGoodTypeId = baseGoodTypeId;
        CurrentDetails = await client.GetFromJsonAsync<List<Basegood>>($"api/Basegood/get_from_type/{CurrentGoodTypeId}") ?? new();
    }

    public async Task UpdateSelection(ChangeEventArgs e)
    {
        if (e.Value is null) { return; }
        if (e.Value is "---") { return; }

        int flavorId = Convert.ToInt32(e.Value.ToString());

        try
        {
            // If 404, don't do anything
            Selected = await client.GetFromJsonAsync<Basegood>($"api/Basegood/get_from_flavor/{CurrentGoodTypeId}/{flavorId}");
        }
        catch { }
    }
}
