using System.Collections.Generic;

namespace IISTools
{
    public class IISRewriteMaps
    {
        public Dictionary<string, IList<IISRewriteMapElement>> Maps = new Dictionary<string, IList<IISRewriteMapElement>>();

        public static IISRewriteMaps Read(string fileName)
        {
            return new IISRewriteMapsParser().Parse(fileName);
        }

        public void Write(string fileName)
        {
            new IISRewriteMapsWriter().Write(fileName, this);
        }
    }
}
