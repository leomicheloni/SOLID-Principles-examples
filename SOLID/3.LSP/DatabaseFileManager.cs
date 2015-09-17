using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.LSP
{
    public class DatabaseFileManager: FileManager
    {
        string connectionString = string.Empty;

        public void SetConnectionString(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public override void SaveImage(byte[] imageBytes, string name)
        {
            //guarda en base de datos
        }
    }
}
