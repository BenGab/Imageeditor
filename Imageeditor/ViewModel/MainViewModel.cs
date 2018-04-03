using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Imageditor.Contracts;
using Imageditor.Contracts.Dialog;
using Imageditor.Contracts.Lockbits;
using Imageditor.Contracts.Maybe;
using Imageditor.Contracts.Processing;
using Imageeditor.Extensions;
using System;
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
        private readonly ILockBitmapFactory _lockbitmapFactory;
        private readonly Action<ILockBitmap, int, int, IMaybe<int>> _brightnessFunction;
        private readonly Action<ILockBitmap, int, int, IMaybe<double>> _contrastFunction;
        private readonly Action<ILockBitmap, int, int, IMaybe<object>> _grayscaleFunction;
        private readonly Action<ILockBitmap, int, int, IMaybe<object>> _negativescaleFunction;

        private RelayCommand _openFileCommand;
        private RelayCommand _originalCommand;
        private RelayCommand _grayscaleCommand;
        private RelayCommand _negativeScaleCommand;
        private RelayCommand _brightNessCommand;
        private RelayCommand _contrastCommand;

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

        private double _brightNessValue;

        public double BrightNessValue
        {
            get { return _brightNessValue; }
            set
            {
                _brightNessValue = value;
                RaisePropertyChanged();
            }
        }

        private double _contrastValue;

        public double ContrastValue
        {
            get { return _contrastValue; }
            set
            {
                _contrastValue = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        public MainViewModel(IDialogService dialogService, IImageProcessing imageProcessing, IAdjustProvider adjustProvider, ILockBitmapFactory lockbitmapFactory)
        {
            _dialogService = dialogService;
            _imageProcessing = imageProcessing;
            _lockbitmapFactory = lockbitmapFactory;
            _contrastFunction = adjustProvider.CreateAdjustFunction<double>(AdjustType.Contrast);
            _brightnessFunction = adjustProvider.CreateAdjustFunction<int>(AdjustType.Brightness);
            _grayscaleFunction = adjustProvider.CreateAdjustFunction<object>(AdjustType.GrayScale);
            _negativescaleFunction = adjustProvider.CreateAdjustFunction<object>(AdjustType.NegativeScale);

            _brightNessValue = 0;
            _contrastValue = 0.0;

            InitCommands();
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

        public RelayCommand BrightNessCommand
        {
            get { return _brightNessCommand; }
        }

        public RelayCommand ContrastCommand
        {
            get { return _contrastCommand; }
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
            var bitmap = _lockbitmapFactory.CreateLockBitmap(_bitmapClone);
            bitmap.LockBits();
            _imageProcessing.AdjustImage(bitmap, new None<object>(), _grayscaleFunction);
            bitmap.UnlockBits();
            ImageSource = _bitmapClone.ToBitmapSource();
        }

        private void NegativeScale()
        {
            var bitmap = _lockbitmapFactory.CreateLockBitmap(_bitmapClone);
            bitmap.LockBits();
            _imageProcessing.AdjustImage(bitmap, new None<object>(), _negativescaleFunction);
            bitmap.UnlockBits();
            ImageSource = _bitmapClone.ToBitmapSource();
        }

        private void BrightNess()
        {
            _bitmapClone = (Bitmap)_original.Clone();
            int brightness = (int)_brightNessValue;
            if (brightness < -255) brightness = -255;
            if (brightness > 255) brightness = 255;
            var bitmap = _lockbitmapFactory.CreateLockBitmap(_bitmapClone);
            bitmap.LockBits();
            _imageProcessing.AdjustImage(bitmap, brightness.ToMaybe(), _brightnessFunction);
            bitmap.UnlockBits();
            ImageSource = _bitmapClone.ToBitmapSource();
        }

        private void Contrast()
        {
            _bitmapClone = (Bitmap)_original.Clone();
            double contrast = _contrastValue;
            if (contrast < -100) contrast = -100;
            if (contrast > 100) contrast = 100;
            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;
            var bitmap = _lockbitmapFactory.CreateLockBitmap(_bitmapClone);
            bitmap.LockBits();
            _imageProcessing.AdjustImage(bitmap, contrast.ToMaybe(), _contrastFunction);
            bitmap.UnlockBits();
            ImageSource = _bitmapClone.ToBitmapSource();
        }

        private void InitCommands()
        {
            _openFileCommand = new RelayCommand(OpenFile);
            _originalCommand = new RelayCommand(BackToOriginal);
            _grayscaleCommand = new RelayCommand(GrayScale);
            _negativeScaleCommand = new RelayCommand(NegativeScale);
            _brightNessCommand = new RelayCommand(BrightNess);
            _contrastCommand = new RelayCommand(Contrast);
        }
    }
}