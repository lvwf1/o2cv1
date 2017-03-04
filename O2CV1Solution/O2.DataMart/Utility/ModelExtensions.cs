using O2.Common.Utility;
using O2.DataMart.Models.EasyQuery;

namespace O2.DataMart.Utility
{
    public static class ModelExtensions
    {
        public static string GetO2DataMartModel(this Model model)
        {
            return Util.Assembly.GetTextResource<O2DataMartModelFolder>("O2DataMartModel.xml");
        }
    }
}
