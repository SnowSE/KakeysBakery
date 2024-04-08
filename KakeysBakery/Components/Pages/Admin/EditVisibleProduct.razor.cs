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

		public override async Task<string> Create(string productString, decimal productCost, int selectedId)
		{
			Addonflavor newType = new Addonflavor()
			{
				Flavor = productString
			};
			await addonFlavor.CreateAddonFlavorAsync(newType);
			var returned = await addonFlavor.GetAddonFlavorByFlavorAsync(productString);

			Addon newAddon = new Addon()
			{
				Suggestedprice = productCost,
				Addontypeid = selectedId,
				Addonflavorid = returned?.Id
			};
			await addon.CreateAddOnAsync(newAddon);
			return "Successfully Added Topping";
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

		}
		public override async Task<string> Create(string productString, decimal productCost, int selectedId)
		{
			Basegoodflavor newType = new Basegoodflavor()
			{
				Flavorname = productString
			};
			await baseGoodFlavor.CreateBaseGoodFlavorAsync(newType);
			var returned = await baseGoodFlavor.GetBaseGoodFlavorByBase(productString);

			Basegood newAddon = new Basegood()
			{
				Suggestedprice = productCost,
				Pastryid = selectedId,
				Flavorid = returned?.Id
			};
			await baseGood.CreateBaseGoodAsync(newAddon);

			return "Successfully Added Product";

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
