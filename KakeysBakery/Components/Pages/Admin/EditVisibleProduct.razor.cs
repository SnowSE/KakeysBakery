using Org.BouncyCastle.Bcpg.OpenPgp;

using static KakeysBakery.Components.Pages.Admin.EditVisibleProduct;

namespace KakeysBakery.Components.Pages.Admin
{
    public partial class CreateEditDelete()
    {
        public virtual async Task Create(string productString, decimal productCost, int id)
        {

        }
        public virtual async Task Edit(int id, string productString, decimal productCost)
        {

        }
        public virtual async Task Delete(int id)
        {

        }
        public virtual async Task Create(string productString, decimal productCost, int selectedId, bool isAvailabe)
        {

        }

    }

    public class CEDToppings : CreateEditDelete
    {
        readonly IAddonService addon;
        readonly IAddonFlavorService addonFlavor;
        public CEDToppings(IAddonFlavorService addonFlavorService, IAddonService addonService)
        {
            addonFlavor = addonFlavorService;
            addon = addonService;
        }

        private Addonflavor CreateAddonFlavor(string productString)
        {
            return new Addonflavor()
            {
                Flavor = productString
            };
        }

        private Addon CreateAddon(decimal productCost, int selectedId, int flavorId)
        {
            return new Addon()
            {
                Suggestedprice = productCost,
                Addontypeid = selectedId,
                Addonflavorid = flavorId
            };
        }

        public override async Task<string> Create(string productString, decimal productCost, int selectedId)
        {
            try
            {
                var newFlavor = CreateAddonFlavor(productString);
                await addonFlavor.CreateAddonFlavorAsync(newFlavor);
                var returned = await addonFlavor.GetAddonFlavorByFlavorAsync(productString);

                var newAddon = CreateAddon(productCost, selectedId, returned.Id);
                await addon.CreateAddOnAsync(newAddon);
                return "Successfully Added Topping";
            }
            catch
            {
                return "Something happened, please check connection and try again";
            }
        }

        public override async Task<string> Edit(int id, string productString, decimal productCost)
        {
            try
            {

            var addons = await addon.GetAddonAsync(id);
            addons.Addonflavor.Flavor = productString;
            addons.Suggestedprice = productCost;
            await addon.UpdateAddOnAsync(addons);
            return "Successfully edited the topping";
            }
            catch
            {
                return "something went wrong, please try again";
            }
        }

        public override async Task<string> Delete(int id)
        {
            await addon.DeleteAddOnAsync(id);
            return "Successfully deleted product";
        }
    }


    public class CEDProducts : CreateEditDelete
    {
        readonly IBaseGoodFlavorService baseGoodFlavor;
        readonly IBaseGoodService baseGood;
        readonly IBasegoodSizeService baseGoodSize;

        public CEDProducts(IBaseGoodFlavorService baseGoodFlavorService, IBaseGoodService baseGoodService, IBasegoodSizeService baseGoodSizeService)
        {
            baseGoodFlavor = baseGoodFlavorService;
            baseGood = baseGoodService;
            baseGoodSize = baseGoodSizeService;
        }

        private Basegoodflavor CreateBaseFlavor(string productString)
        {
            return new Basegoodflavor()
            {
                Flavorname = productString
            };
        }


        private Basegood CreateBaseGood(decimal productCost, int selectedId, int flavorId, bool isAvailable, int quantityId)
        {
            return new Basegood()
            {
                Suggestedprice = productCost,
                Pastryid = selectedId,
                Flavorid = flavorId,
                Isavailable = isAvailable,
                Goodsize = quantityId
            };
        }

        private BasegoodSize CreateBaseGoodSize(string quantity)
        {
            return new BasegoodSize()
            {
                Size = quantity
            };
        }
        public async Task<string> Create(string productString, decimal productCost, int selectedId, bool isAvailabe, string quantity)
        {
            try
            {
                var returned = await baseGoodFlavor.GetBaseGoodFlavorByBase(productString);
                var returnedSize = await baseGoodSize.GetBasegoodSizeByAsync(quantity);

                if (returned == null)
                {

                    await baseGoodFlavor.CreateBaseGoodFlavorAsync(CreateBaseFlavor(productString));
                    returned = await baseGoodFlavor.GetBaseGoodFlavorByBase(productString);
                }

                if (returnedSize == null)
                {

                    await baseGoodSize.CreateBasegoodSizeAsync(CreateBaseGoodSize(quantity));
                    returnedSize = await baseGoodSize.GetBasegoodSizeByAsync(quantity);
                }


                var newAddon = CreateBaseGood(productCost, selectedId, returned.Id, isAvailabe, returnedSize.Id);
                await baseGood.CreateBaseGoodAsync(newAddon);

                return "Successfully Added Product";
            }
            catch
            {
                return "Something went wrong, please check connection and try again";
            }

        }
        public override async Task<string> Edit(int id, string productString, decimal productCost)
        {
            throw new NotImplementedException();
        }

        public override async Task<string> Delete(int id)
        {
            await baseGood.DeleteBaseGoodAsync(id);
            return "Successfully deleted product";
        }
    }
}