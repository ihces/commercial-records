using System;
using CommercialRecordSystem.Models;
using System.IO;
using Windows.Storage;

namespace CommercialRecordSystem.ViewModels.DataVMs
{
    class InfoDataVMBase<E> : DataVMBase<E> where E:InfoModelBase,new()
    {
        private StorageFolder imageSourceFolder = App.CommonImgFolder;
        public StorageFolder ImageSourceFolder
        {
            get
            {
                return imageSourceFolder;
            }
        }

        private string imageFileName = string.Empty;
        public string ImageFileName
        {
            get
            {
                return imageFileName;
            }
            set
            {
                imageFileName = value;
                RaisePropertyChanged("ImageFileName");
            }
        }

        private Uri imageFileSource;
        public Uri ImageFileSource
        {
            get
            {
                return imageFileSource;
            }
            set
            {
                imageFileSource = value;
                RaisePropertyChanged("ImageFileSource");
            }
        }

        public override void initWithModel(E model)
        {
            base.initWithModel(model);
            if (!string.IsNullOrWhiteSpace(model.ImageFileName))
                ImageFileSource = new Uri(Path.Combine(imageSourceFolder.Path, model.ImageFileName));
        }

        public InfoDataVMBase(E model, StorageFolder imageSourceFolder)
            : base(model)
        {
            this.imageSourceFolder = imageSourceFolder;
            if (!string.IsNullOrWhiteSpace(model.ImageFileName))
                ImageFileSource = new Uri(Path.Combine(imageSourceFolder.Path, model.ImageFileName));
        }

        public InfoDataVMBase(StorageFolder imageSourceFolder)
        {
            this.imageSourceFolder = imageSourceFolder;
        }
    }
}
