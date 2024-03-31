using System.Collections.Generic;
using System.Linq;
using Core.Interface;
using Cysharp.Threading.Tasks;

namespace Core
{
    public class JobsExecutor : IJobsExecutor
    {
        public async UniTask ExecuteJobsAsync(IEnumerable<IJob> jobs)
        {
            var jobGroups = jobs.GroupBy(job => job.ExecutionOrder).OrderBy(group => group.Key);

            foreach (var jobGroup in jobGroups)
            {
                await UniTask.WhenAll(jobGroup.Select(job => job.ExecuteAsync()));
            }
        }
    }
}