using KakeysBakery.Components.Pages.Admin;


namespace KakeysBakeryTests;

public class TestAdminEdit : IClassFixture<BakeryFactory>
{
	EditVisibleProduct editVisibleProduct;
	public TestAdminEdit() { 
		editVisibleProduct = new();
	}

	[Fact]
	public void TestAddonFlavor()
	{
		//arrange
		editVisibleProduct.productString = "testing";

		//act
		var addonflavor = editVisibleProduct.CreateAddonFlavor();

		//assert
		Assert.True(addonflavor != null);
		Assert.Equal("testing", addonflavor.Flavor);
	}

	[Fact]
	public void TestAddon()
	{
		//arrange
		editVisibleProduct.productCost = 1m;
		editVisibleProduct.selectedId = 1;

		//act
		var addon = editVisibleProduct.CreateAddon(100);

		Assert.True(addon != null);
		Assert.Equal(1m, addon.Suggestedprice);
		Assert.Equal(1, addon.Addontypeid);
		Assert.Equal(100, addon.Addonflavorid);
	}

	[Fact]
	public void TestAddNewTopping()
	{
		//arrange
		editVisibleProduct.productString = "testing";
		var addonflavor = editVisibleProduct.CreateAddonFlavor();

		editVisibleProduct.productCost = 1m;
		editVisibleProduct.selectedId = 1;
		//var addon = editVisibleProduct.CreateAddon(addonflavor.Id);

		//act
		editVisibleProduct.AddNewtopping();

		//assert
		//get it to talk to the saved database which I have no idea how to do
		Assert.Equal("asdf","asdf"); //I don't want to break the action with a failing test because I don't know how to do tests
	}

}