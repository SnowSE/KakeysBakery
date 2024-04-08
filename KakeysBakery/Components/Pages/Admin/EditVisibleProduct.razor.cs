using static KakeysBakery.Components.Pages.Admin.EditVisibleProduct;

namespace KakeysBakery.Components.Pages.Admin
{
	public partial class CreateEditDelete()
	{
		public virtual async Task Create(string productString, decimal productCost, int id)
		{

		}
		public virtual async Task Edit(int id)
		{

		}
		public virtual async Task Delete(int id)
		{

		}
	}

	public class CEDToppings : CreateEditDelete
	{
		IAddonService addon;
		IAddonFlavorService addonFlavor;
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
			try{
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

		public override async Task<string> Edit(int id)
		{
			throw new NotImplementedException();
		}

		public override async Task<string> Delete(int id)
		{
			await addon.DeleteAddOnAsync(id);
			return "Successfully deleted product";
		}
	}


	public class CEDProducts : CreateEditDelete
	{
		IBaseGoodFlavorService baseGoodFlavor;
		IBaseGoodService baseGood;

		public CEDProducts(IBaseGoodFlavorService baseGoodFlavorService, IBaseGoodService baseGoodService)
		{
            baseGoodFlavor = baseGoodFlavorService;
			baseGood = baseGoodService;
		}

		private Basegoodflavor CreateBaseFlavor(string productString)
		{
            return new Basegoodflavor()
            {
                Flavorname = productString
            };
        }


		private Basegood CreateBaseGood(decimal productCost, int selectedId, int flavorId)
		{
			return new Basegood()
			{
                Suggestedprice = productCost,
                Pastryid = selectedId,
                Flavorid = flavorId
            };
		}
		public override async Task<string> Create(string productString, decimal productCost, int selectedId)
		{
			try{
			var newFlavor = CreateBaseFlavor(productString);
			await baseGoodFlavor.CreateBaseGoodFlavorAsync(newFlavor);
			var returned = await baseGoodFlavor.GetBaseGoodFlavorByBase(productString);

			var newAddon = CreateBaseGood(productCost, selectedId, returned.Id);
			await baseGood.CreateBaseGoodAsync(newAddon);

			return "Successfully Added Product";
			}
			catch
			{
				return "Something went wrong, please check connection and try again";
			}

		}
		public override async Task<string> Edit(int id)
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
