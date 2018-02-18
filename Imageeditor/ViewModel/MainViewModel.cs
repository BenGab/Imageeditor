using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Imageditor.Contracts.Dialog;
using Imageditor.Contracts.Processing;
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
        private readonly IImageProcessing _imageProcessing; 
        private RelayCommand _openFileCommand;
        private RelayCommand _originalCommand;
        private RelayCommand _grayscaleCommand;
        private RelayCommand _negativeScaleCommand;
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
        public MainViewModel(IDialogService dialogService, IImageProcessing imageProcessing)
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
            _imageProcessing = imageProcessing;
            _openFileCommand = new RelayCommand(OpenFile);
            _originalCommand = new RelayCommand(BackToOriginal);
            _grayscaleCommand = new RelayCommand(GrayScale);
            _negativeScaleCommand = new RelayCommand(NegativeScale);
        }

        public RelayCommand OpenFileCommand
        {
            get
            {
                return _openFileCommand;
            }
        }

        public RelayCommand OriginalCommand
        {
            get { return _originalCommand; }
        }

        public RelayCommand GrayScaleCommand
        {
            get { return _grayscaleCommand; }
        }

        public RelayCommand NegativeScaleCommand
        {
            get { return _negativeScaleCommand; }
            set { _negativeScaleCommand = value; }
        }


        private void OpenFile()
        {
            var filePath = _dialogService.OpenFile();
            _original = new Bitmap(filePath);
            _bitmapClone = (Bitmap)_original.Clone();
            ImageSource = _bitmapClone.ToBitmapSource();
        }

        private void BackToOriginal()
        {
            _bitmapClone = (Bitmap)_original.Clone();
            ImageSource = _bitmapClone.ToBitmapSource();
        }

        private void GrayScale()
        {
            _imageProcessing.ConverToGray(_bitmapClone);
            ImageSource = _bitmapClone.ToBitmapSource();
        }

        private void NegativeScale()
        {
            _imageProcessing.ConvertToNegative(_bitmapClone);
            ImageSource = _bitmapClone.ToBitmapSource();
        }
    }
}