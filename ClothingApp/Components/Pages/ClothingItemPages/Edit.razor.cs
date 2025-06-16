using ClothingApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace ClothingApp.Components.Pages.ClothingItemPages
{
    public partial class Edit
    {
        [SupplyParameterFromQuery]
        private int Id { get; set; }

        [SupplyParameterFromForm]
        private ClothingItem? ClothingItem { get; set; }

        private Dictionary<string[], string> clothingTypeDict = new()
        {
            [ClothingTypes.Tops] = "Top",
            [ClothingTypes.Bottoms] = "Bottoms",
            [ClothingTypes.Outerwears] = "Outerwear",
            [ClothingTypes.One_pieces] = "One-Piece",
            [ClothingTypes.Activewears] = "ActiveWear",
            [ClothingTypes.Underwear_loungewears] = "Underwear & Loungewear",
            [ClothingTypes.Swimwears] = "Swimwear",
            [ClothingTypes.Accessories] = "Accessories",
            [ClothingTypes.Others] = "Other"
        };


        protected override async Task OnInitializedAsync()
        {
            using var context = DbFactory.CreateDbContext();
            ClothingItem ??= await context.ClothingItem.FirstOrDefaultAsync(m => m.Id == Id);

            if (ClothingItem is null)
            {
                NavigationManager.NavigateTo("notfound");
            }
        }

        private async Task UpdateClothingItem()
        {
            if (await validations.ValidateAll())
            {
                using var context = DbFactory.CreateDbContext();
                var cType = ClothingItem!.ClothingTypeSpecific;
                ClothingItem.ClothingType = "Other";
                foreach (var kvp in clothingTypeDict)
                {
                    if (kvp.Key.Contains(cType))
                    {
                        ClothingItem.ClothingType = kvp.Value;
                        break;
                    }
                }
                ClothingItem!.Name = ClothingItem.Brand + " " + ClothingItem.ClothingTypeSpecific;
                context.Attach(ClothingItem!).State = EntityState.Modified;

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingItemExists(ClothingItem!.Id))
                    {
                        NavigationManager.NavigateTo("notfound");
                    }
                    else
                    {
                        throw;
                    }
                }

                NavigationManager.NavigateTo("/clothingitems");
            }
        }

        private bool ClothingItemExists(int id)
        {
            using var context = DbFactory.CreateDbContext();
            return context.ClothingItem.Any(e => e.Id == id);
        }

        private static readonly string[] Tops = ClothingTypes.Tops;
        private static readonly string[] Bottoms = ClothingTypes.Bottoms;
        private static readonly string[] Outerwears = ClothingTypes.Outerwears;
        private static readonly string[] One_pieces = ClothingTypes.One_pieces;
        private static readonly string[] Activewears = ClothingTypes.Activewears;
        private static readonly string[] Underwear_loungewears = ClothingTypes.Underwear_loungewears;
        private static readonly string[] Swimwears = ClothingTypes.Swimwears;
        private static readonly string[] Accessories = ClothingTypes.Accessories;
        private static readonly string[] Others = ClothingTypes.Others;
        private static readonly string[] Conditions = ClothingConditions.Conditions;

        Blazorise.Validations validations;
    }
}