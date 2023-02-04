namespace Submit_Image.ImageMongoModel
{
    public class ImageDatabaseSetting : IImageDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string CollectionName { get; set; }
    }

    public interface IImageDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string CollectionName { get; set; }
    }
}
