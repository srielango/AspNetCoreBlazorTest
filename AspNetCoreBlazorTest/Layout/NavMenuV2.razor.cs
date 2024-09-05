using Microsoft.AspNetCore.Components;

namespace AspNetCoreBlazorTest.Layout
{
	public partial class NavMenuV2
	{
		[Parameter]
		public bool IsVisible { get; set; }
		private bool isAdminMenuOpen = false;
		private bool isProductsMenuOpen = false;

		private void ToggleAdminMenu()
		{
			isAdminMenuOpen = !isAdminMenuOpen;
		}

		private void ToggleProductsMenu()
		{
			isProductsMenuOpen = !isProductsMenuOpen;
		}

	}
}
