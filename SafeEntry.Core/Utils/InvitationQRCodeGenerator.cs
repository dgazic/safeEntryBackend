using Microsoft.AspNetCore.WebUtilities;
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
        public static string GenerateQRCodeInvitation(PeopleRegistrationEventDto request)
        {
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

            bitmap.Save(@"C:\Users\dinog\Desktop\qr-code.png", ImageFormat.Jpeg);
            return shaDataEncoded;
        }
    }
}
