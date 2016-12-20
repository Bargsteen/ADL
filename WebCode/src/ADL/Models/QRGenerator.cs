using System;
using ZXing.QrCode;
using System.Drawing;

namespace ADL.Models
{
    public static class QrGenerator
    {
        public static void GenerateQr(int locationID)
        {
            Bitmap bmp = PathtoBitmap(LocationIDtoPath(locationID));
            bmp.Save("wwwroot/custom/images/locationQR.bmp");
        }

        private static string LocationIDtoPath(int locationID)
        {
            return $"ADLearning;{locationID}";
        }

        private static Bitmap PathtoBitmap(string inputUrl)
        {
            QRCodeWriter qr = new QRCodeWriter();

            //Encodes the given string into a bitmatrix with the format defined by Zxing: "QR_CODE"
            var matrix = qr.encode(inputUrl, ZXing.BarcodeFormat.QR_CODE, 500, 500);

            int height = matrix.Height;
            int width = matrix.Width;

            Bitmap bmp = new Bitmap(width, height);

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
