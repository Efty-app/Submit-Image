using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Submit_Image.Services;
using System.Threading.Tasks;
using Submit_Image.ImageMongoModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Submit_Image.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        private readonly ImageService _imageservice;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ImageController> _logger;

        public ImageController(ImageService imageservice, IWebHostEnvironment environment , ILogger<ImageController> logger)
        {
            _imageservice = imageservice;
            _environment = environment;
            _logger = logger;
        }

        // do we need to check the uploaded file extension?

        [HttpPost("uploadImage")]
        public async Task<IActionResult> OnPostUploadAsync(IFormFile file, string GUID)
        {
            Image img = new Image();
            if (file.Length > 0)
            {
                try
                {
                    var filePath = Path.GetTempFileName();
                    MemoryStream ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    img.Image_ = ms.ToArray();
                    img.GUID = GUID;
                    var result_ = _imageservice.CreateImage(img);
                    if (!result_) return BadRequest("Not Inserted successfully");                   
                }
                catch
                {
                    return BadRequest("Not Inserted successfully");
                }
            }
            else return BadRequest();

            return Ok(new { GUID_ = img.GUID, Resut = "Inserted successfully" });
        }

        [HttpGet("GetAllImages")]
        public ActionResult<List<Image>> Get() =>
            _imageservice.GetAllImages();

        [HttpPost("GetImageByGUID")]
        public ActionResult<List<Image>> GetImageByGUID(string[] GUID)
        {

            if (GUID.Count() == 0)
            {
                return BadRequest();                   
            }
            try
            {
                var images_ = _imageservice.GetImageByGUId(GUID.ToList());
                if (images_.Count == 0) return NotFound();
                else
                    return images_;
            }
            catch
            {
                return BadRequest("Something Wrong happened");
            }

        }

        [HttpPost("GetImageByID")]
        public ActionResult<List<Image>> GetImageByID(string[] ID)
        {

            if (ID.Count() == 0)
            {
                return BadRequest();
            }
            try
            {
                var images_ = _imageservice.GetImageById(ID.ToList());
                if (images_.Count == 0) return NotFound();
                else
                    return images_;
            }
            catch
            {
                return BadRequest("Something Wrong happened");
            }

        }


    }
}
