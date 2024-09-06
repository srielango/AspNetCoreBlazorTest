using Microsoft.AspNetCore.Components;

namespace AspNetCoreBlazorTest.Layout
{
	public partial class NavMenu
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
