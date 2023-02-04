using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Collections.Generic;
using MongoDB.Driver.Linq;
using Submit_Image.ImageMongoModel;
using System.Linq;
using System.Threading;

namespace Submit_Image.Services
{
    public class ImageService
    {
        private readonly IMongoCollection<Image> _images;
        public ImageService(IImageDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _images = database.GetCollection<Image>(settings.CollectionName);
        }

        
        public List<Image> GetAllImages() =>
                _images.Find(Img => true).ToList();


        public List<Image> GetImageById(List<string> id)
        {
           return  _images.AsQueryable().Where(_images => id.Contains(_images.Id)).ToList();
        }

        public List<Image> GetImageByGUId(List<string> guid)
        {
            return _images.AsQueryable().Where(_images => guid.Contains(_images.GUID)).ToList();
        }


        public bool CreateImage(Image Img)
        {
            try
            {
                _images.InsertOne(Img);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool UpdateImage(string id, Image ImgIn)
        {
            try
            {
                _images.ReplaceOne(Img => Img.Id == id, ImgIn);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public bool RemoveImage(Image ImgIn)
        {
            try
            {
                _images.DeleteOne(Img => Img.Id == ImgIn.Id);
            }
            catch { return false; }

            return true;
        }

        public bool RemoveImage(string id)
        {
            try
            {
                _images.DeleteOne(Img => Img.Id == id);
            }
            catch { return false; }
            return true;
        }


    }
}
