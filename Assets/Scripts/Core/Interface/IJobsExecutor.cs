using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Core.Interface
{
    public interface IJobsExecutor
    {
        UniTask ExecuteJobsAsync(IEnumerable<IJob> jobs);
    }
}