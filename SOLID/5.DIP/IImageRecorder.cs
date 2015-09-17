namespace SOLID.DIP
{
    public interface IImageRecorder
    {
        void SaveImage(byte[] imageBytes, string name);
        void SetConnectionString(string connectionString);
    }
}