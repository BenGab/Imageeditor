using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Imageditor.Contracts.Dialog;
using Imageeditor.Extensions;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Imageeditor.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private RelayCommand _openFileCommand;
        private Bitmap _original = null;
        private Bitmap _bitmapClone = null;

        private BitmapSource _imagesource;

        public BitmapSource ImageSource
        {
            get { return _imagesource; }
            set
            {
                _imagesource = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        public MainViewModel(IDialogService dialogService)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            _dialogService = dialogService;
            _openFileCommand = new RelayCommand(OpenFile);
        }

        public RelayCommand OpenFileCommand
        {
            get
            {
                return _openFileCommand;
            }
        }

        private void OpenFile()
        {
            var filePath = _dialogService.OpenFile();
            _original = new Bitmap(filePath);
            _bitmapClone = (Bitmap)_original.Clone();
            ImageSource = _bitmapClone.ToBitmapSource();
        }
    }
}