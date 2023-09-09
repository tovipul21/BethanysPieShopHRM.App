namespace BethanysPieShopHRM.App.Components
{
    public partial class QuickNewPopUp
    {
        public bool ShowNewViewPopUp = false;
        public void Show()
        {
            //ResetDialog();
            ShowNewViewPopUp = true;
            StateHasChanged();
        }

        public void ClosePopUp() 
        {
            ShowNewViewPopUp = false;
        }
    }
}
