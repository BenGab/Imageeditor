using Imageditor.Contracts.Dialog;
using Microsoft.Win32;

namespace Imageeditor.Services.Dialog
{
    public class DialogService : IDialogService
    {
        private const string dialogFilter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

        public string OpenFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = dialogFilter;
            var dlgResult = dlg.ShowDialog();

            if (dlgResult.HasValue && dlgResult.Value)
            {
                return dlg.FileName;
            }

            return string.Empty;
        }
    }
}
