using System;
using System.IO;

namespace SOLID.ALL
{
    public class FileManager: IImageRecorder
    {
        public virtual void SaveImage(byte[] imageBytes, string name)
        {
            FileStream fs = new FileStream(name, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                bw.Write(imageBytes);
            }
            finally
            {
                fs.Close();
                bw.Close();
            }

        }

        public void SetConnectionString(string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
