using System.Collections.Generic;

namespace Core.Interface
{
    public interface IBoardFillStrategy
    {
        string Name { get; }
        IEnumerable<IJob> GetFillJobs();
    }
}