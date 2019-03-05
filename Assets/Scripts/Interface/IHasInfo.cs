using System.Collections.Generic;
namespace NodeVR
{
    public interface IHasInfo
    {
        List<(string, string)> ReportInfo();
    }
}