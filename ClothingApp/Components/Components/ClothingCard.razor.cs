using ClothingApp.Components.Components.Icons;
using ClothingApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace ClothingApp.Components.Components
{
    public partial class ClothingCard
    {
        [Parameter]
        public ClothingItem ClothingItem { get; set; } = new();
        [Parameter]
        public string IconColor { get; set; } = "#FFFFFF";
        [Parameter]
        public string IconSize { get; set; } = "25px";

        //
        [Parameter]
        public bool DeleteFlag { get; set; } = false;
        [Parameter]
        public EventCallback DeleteFlagChanged { get; set; }

        //



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

        private Dictionary<string, object> GetIconParameters()
        {
            return new Dictionary<string, object>
            {
                { "Color", IconColor },
                { "Size", IconSize }
            };
        }

        private Type SafeDict(string clothingType)
        {
            Type ctype;
            if (!icons.TryGetValue(clothingType, out ctype!))
            {
                ctype = typeof(Top);
            }
            return ctype;
        }

        //
        bool modalVisible = false;

        Task ToggleModal()
        {
            if (modalVisible)
            {
                modalVisible = false;
            }
            else
            {
                modalVisible = true;
            }

            return Task.CompletedTask;
        }

        //
        protected override async Task OnInitializedAsync()
        {
            using var context = DbFactory.CreateDbContext();
            ClothingItem = await context.ClothingItem.FirstOrDefaultAsync(m => m.Id == ClothingItem.Id) ?? new();

            if (ClothingItem is null)
            {
                NavigationManager.NavigateTo("notfound");
            }
        }

        private async Task DeleteClothingItem()
        {
            await ToggleModal();
            using var context = DbFactory.CreateDbContext();
            context.ClothingItem.Remove(ClothingItem!);
            await context.SaveChangesAsync();
            DeleteFlag = true;
            visible = false;
            await DeleteFlagChanged.InvokeAsync();
            NavigationManager.NavigateTo("/clothingitems");
        }

        bool visible = false; 

        private async Task WearClicked()
        {
            visible = true;
            using var context = DbFactory.CreateDbContext();
            ClothingItem.NumberWorn++;
            ClothingItem.LastWorn = DateOnly.FromDateTime(DateTime.Now);
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

        private bool ClothingItemExists(int id)
        {
            using var context = DbFactory.CreateDbContext();
            return context.ClothingItem.Any(e => e.Id == id);
        }

    }
}