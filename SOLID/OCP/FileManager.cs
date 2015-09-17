using System.IO;

namespace SOLID.OCP
{
    public class FileManager
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
    }
}
