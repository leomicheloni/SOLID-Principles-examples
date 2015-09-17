using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.ALL
{
    public class ServiceLocator
    {
 
        internal static T Find<T>() where T : class
        {
            if (typeof(T) == typeof(IImageRecorder))
                return new FileManager() as T;

            if (typeof(T) == typeof(ILogger))
                return new ConsoleLogger() as T;

            if (typeof(T) == typeof(IPageRetriever))
                return new PageRetriever() as T;


            throw new TypeLoadException($"cannot find type {typeof(T).Name}");
        }
    }
}
