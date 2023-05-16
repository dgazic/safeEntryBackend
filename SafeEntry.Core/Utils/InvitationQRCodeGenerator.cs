using Microsoft.AspNetCore.WebUtilities;
using SafeEntry.Core.Models;
using SafeEntry.Core.Models.DtoModels;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;
using ZXing;
using ZXing.QrCode;

namespace SafeEntry.Core.Utils
{
    public static class InvitationQRCodeGenerator
    {
        public static QRCodeModel GenerateQRCodeInvitation(PeopleRegistrationEventDto request)
        {
            QRCodeModel qrCodeModel = new QRCodeModel();
            var sha256 = SHA256.Create();
            var shaData = sha256.ComputeHash(Encoding.UTF8.GetBytes(request.Email.ToString() + request.Name.ToString() + request.Surname.ToString() + request.BirthDate.ToString()));
            string shaDataEncoded = WebEncoders.Base64UrlEncode(shaData);

            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = 300,
                    Width = 300
                }
            };

            var pixelData = writer.Write(shaDataEncoded);

            var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            try
            {
                System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }

            string base64Image = BitmapToBase64(bitmap);

            qrCodeModel.ShaDataEncoded = shaDataEncoded;
            qrCodeModel.Base64Image = base64Image;
            return qrCodeModel;
        }

        public static string BitmapToBase64(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                byte[] byteArr = memoryStream.ToArray();

                string base64String = Convert.ToBase64String(byteArr);

                return base64String;
            }
        }
    }
}
