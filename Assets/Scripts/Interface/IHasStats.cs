using System.Collections.Generic;
namespace NodeVR
{
    public interface IHasStats
    {
        List<(string, string)> ReportStats();
    }
}