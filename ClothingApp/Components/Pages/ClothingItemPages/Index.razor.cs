using Blazorise;
using ClothingApp.Components.Components.Icons;
using ClothingApp.Data;
using ClothingApp.Models;

namespace ClothingApp.Components.Pages.ClothingItemPages
{
    public partial class Index
    {
        private ClothingAppContext context = default!;
        private ClothingAppUser user = default!;
        private string? email;
        private IQueryable<ClothingItem> Clothes { get; set; } = Enumerable.Empty<ClothingItem>().AsQueryable();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var claimsPrincipalUser = authState.User;

            user = await UserManager.GetUserAsync(claimsPrincipalUser) ?? throw new InvalidOperationException("User is null, this page requires an authenticated user");

            email = await UserManager.GetEmailAsync(user);
        }

        protected override void OnInitialized()
        {
            context = DbFactory.CreateDbContext();
            Clothes = context.ClothingItem.Where(c => c.Username == email);
        }


        public async ValueTask DisposeAsync() => await context.DisposeAsync();

        public string IconColor { get; set; } = "#0DCAF0";
        public string IconSize { get; set; } = "25px";

        private Dictionary<string, object> GetIconParameters()
        {
            return new Dictionary<string, object>
            {
                { "Color", IconColor },
                { "Size", IconSize }
            };
        }
        

        private Dictionary<string, Type> icons = new()
        {
            ["Top"] = typeof(Top),
            ["Bottoms"] = typeof(Bottoms),
            ["Outerwear"] = typeof(Outerwear),
            ["One-Piece"] = typeof(One_Piece),
            ["ActiveWear"] = typeof(ActiveWear),
            ["Underwear & Loungewear"] = typeof(Underwear_Loungewear),
            ["Swimwear"] = typeof(Swimwear),
            ["Accessories"] = typeof(Accessories),
            ["Other"] = typeof(Other)
        };

        private Type SafeDict(string clothingType)
        {
            Type ctype;
            if (!icons.TryGetValue(clothingType, out ctype!))
            {
                ctype = typeof(Other);
            }
            return ctype;
        }

        // private List<bool> bools = new() { true, false, false, false, false, false, false, false, false };

    }
}