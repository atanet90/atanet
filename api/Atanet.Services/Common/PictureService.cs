namespace Atanet.Services.Common
{
    using System;
    using System.Net;
    using Atanet.Model.Data;
    using Atanet.Services.Files;
    using Atanet.Services.UoW;
    using ImageMagick;

    public class PictureService : IPictureService
    {
        private const string PictureSource = "https://atanet90.github.io/expression-pack/img/{0}.jpg";

        private const int ImageSize = 128;

        private readonly int maxImages = int.Parse(Environment.GetEnvironmentVariable("MAX_IMAGE_INDEX"));

        private readonly IFileCreationService fileCreationService;

        private readonly Random random = new Random();

        public PictureService(IFileCreationService fileCreationService)
        {
            this.fileCreationService = fileCreationService;
        }

        public File GetAtanetPicture(IUnitOfWork unitOfWork)
        {
            using (var client = new WebClient())
            {
                var url = this.GetPictureUrl();
                var data = client.DownloadData(url);
                var fileName = System.IO.Path.GetFileName(url);

                using (var image = new MagickImage(data))
                {
                    image.Format = MagickFormat.Jpeg;
                    var geometry = new MagickGeometry($"{ImageSize}x");
                    geometry.IgnoreAspectRatio = true;
                    image.AdaptiveResize(geometry);
                    image.Extent(ImageSize, ImageSize, Gravity.Center, MagickColor.FromRgb(255, 255, 255));
                    data = image.ToByteArray();
                }

                return this.fileCreationService.CreateImageFile(unitOfWork, data, fileName);
            }
        }

        public string GetPictureUrl()
        {
            var pictureId = this.random.Next(0, this.maxImages);
            var url = string.Format(PictureSource, pictureId.ToString());
            return url;
        }
    }
}
