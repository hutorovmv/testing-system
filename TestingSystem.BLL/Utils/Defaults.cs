using System.Configuration;

namespace TestingSystem.BLL.Utils
{
    public static class Defaults
    {
        public static int GetPageSize()
        {
            int pageSize;
            
            string pageSizeStr = ConfigurationManager.AppSettings["pageSize"];
            if (!int.TryParse(pageSizeStr, out pageSize))
                pageSize = 1;

            return pageSize;
        }   
    }
}
