using System;
namespace GenerateImagePagesFromCSV
{
    public class ImageData
    {
        public string FileName { get; set; }
        public string ImageURL { get; set; }
        public string ProductURL { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string ImageCaption { get; set; }
        public string ImageViewName { get; set; }

        public static ImageData FromCSV(string csvLine)
        {

            string[] values = csvLine.Split(',');

            ImageData imageData = new ImageData();

            imageData.FileName = values[0];
            imageData.ImageURL = values[1];
            imageData.ProductURL = values[2];
            imageData.ProductName = values[3];
            imageData.Price =values[4];
            imageData.Description = values[5];
            imageData.ImageCaption = values[6];
            imageData.ImageViewName = values[7];

            return imageData;
        }

    }
}
