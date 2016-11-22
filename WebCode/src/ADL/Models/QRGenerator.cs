using System;
using ZXing.QrCode;
using System.Drawing;

namespace ADL.Models
{
    public static class QrGenerator
    {
        public static void GenerateQR(int locationID)
        {
            /*Call the two QR Generation methods and save the result in a Bitmap*/
            Bitmap bmp = PathtoBitmap(LocationIDtoPath(locationID));

            /*Save the resulting Bitmap in the project; ready for display*/
            bmp.Save("wwwroot/custom/images/locationQR.bmp");

            return;
        }

        private static string LocationIDtoPath(int locationID)
        {
            /*Combine the locationID and the path into a single string*/
            string path = $"ADLearning;{locationID}";

            return path;
        }

        private static Bitmap PathtoBitmap(string inputURL)
        {
            /*Create a codewriter instance*/
            QRCodeWriter qr = new QRCodeWriter();

            /*Encodes the given string into a bitmatrix with the format defined by Zxing: "QR_CODE"*/
            var matrix = qr.encode(inputURL, ZXing.BarcodeFormat.QR_CODE, 500, 500);

            /*Gets the dimensions of the matrix*/
            int height = matrix.Height;
            int width = matrix.Width;

            /*Creates a new bitmap with the ssame dimensions as the matrix*/            Bitmap bmp = new Bitmap(width, height);

            /*Fill out the new BMP with the details of the matrix*/
            for (int x = 0; x < width; x++)
            {

                for (int y = 0; y < height; y++)
                {

                    bmp.SetPixel(x, y, matrix[x, y] ? Color.Black : Color.White);

                }
            }
            return bmp;
        }
    }
}
