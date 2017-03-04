using System.IO;

namespace O2.Common.Utility
{
    public class Assembly
    {
        public string GetTextResource<TLocation>(string name)
        {
            var location = typeof (TLocation);

            using(var rdr = new StreamReader(location.Assembly.GetManifestResourceStream(location, name)))
            {
                return rdr.ReadToEnd();
            }
        }
    }
}
