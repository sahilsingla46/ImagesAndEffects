using ImagesAndEffects.BusinessLogic;
using ImagesAndEffects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace ImagesAndEffects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploaderController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public ImageUploaderController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        [Route("SaveImage")]
        public Response SaveImage(List<FileModel> fileModels)
        {
            Response response = new Response();
            try
            {

                foreach (var fileModel in fileModels)
                {
                    Bitmap bitmap;

                    //string strFileName = @"C:\Users\Public\Pictures\Sample Pictures\Koala.jpg";
                    if (System.IO.File.Exists(fileModel.filePath))
                    {


                        bitmap = new Bitmap(fileModel.filePath);

                        //Bitmap resized = new Bitmap(bitmap, new Size(original.Width / 4, original.Height / 4));

                        bitmap = new Bitmap(bitmap,fileModel.size, fileModel.size); //------------Resize---------------//

                        foreach (var items in fileModel.Effects)
                        {
                            switch (items.ToUpper())
                            {
                                case "EFFECT1":

                                    bitmap = BlurImage.Blur(bitmap, new Rectangle(0, 0, fileModel.size, fileModel.size), 2); //-----------------------Blur Effect-------------//
                                    break;
                                case "EFFECT2":
                                    bitmap = GreyImage.MakeGrayscale3(bitmap); //-----------------------Grey Effect-------------//
                                    break;
                                default:
                                    //code block
                                    break;
                            }
                        }
                        string path1 = Path.Combine(_environment.ContentRootPath, "Processed_Images", fileModel.fileName);
                        bitmap.Save(path1);
                    }
                    response.statusCode = 200;
                    response.errorMessage = "Image has been successfully uploaded.";
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 100;
                response.errorMessage = "Image Upload has some errors." + ex.Message;
            }

            return response;
        }
    }
}
