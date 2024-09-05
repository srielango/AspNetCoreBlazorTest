using Microsoft.AspNetCore.Components;

namespace AspNetCoreBlazorTest.SharedComponents
{
    public partial class Confirmation
    {
        [Parameter] public string Header { get; set; }
        [Parameter] public string Message { get; set; }
        [Parameter] public int RowId { get; set; }  // Row ID passed to the confirmation component

        [Parameter] public EventCallback<(bool confirmed, int rowId)> OnConfirmationResult { get; set; }

        private void Confirm()
        {
            OnConfirmationResult.InvokeAsync((true, RowId));
        }

        private void Cancel()
        {
            OnConfirmationResult.InvokeAsync((false, RowId));
        }
    }
}
