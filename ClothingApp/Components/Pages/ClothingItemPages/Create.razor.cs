using ClothingApp.Data;
using ClothingApp.Models;
using Microsoft.AspNetCore.Components;

namespace ClothingApp.Components.Pages.ClothingItemPages
{
    public partial class Create
    {
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

        private static ClothingItem ClothingItem { get; set; } = new();

        private ClothingAppUser user = default!;
        private string? email;

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



        private async Task AddClothingItem()
        {
            if (await validations.ValidateAll())
            {
                using var context = DbFactory.CreateDbContext();
                ClothingItem.Username = email ?? string.Empty;
                var cType = ClothingItem.ClothingTypeSpecific;
                ClothingItem.ClothingType = "Other";
                foreach (var kvp in clothingTypeDict)
                {
                    if (kvp.Key.Contains(cType))
                    {
                        ClothingItem.ClothingType = kvp.Value;
                        break;
                    }
                }
                ClothingItem.Name = ClothingItem.Brand + " " + ClothingItem.ClothingTypeSpecific;



                context.ClothingItem.Add(ClothingItem);
                await context.SaveChangesAsync();
                ClothingItem = new ClothingItem();

                NavigationManager.NavigateTo("/clothingitems");
            }

        }

        private void ResetModel()
        {
            ClothingItem = new();
        }


        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var claimsPrincipalUser = authState.User;

            user = await UserManager.GetUserAsync(claimsPrincipalUser) ?? throw new InvalidOperationException("User is null, this page requires an authenticated user");

            email = await UserManager.GetEmailAsync(user);
        }

        Blazorise.Validations validations;


    }
}