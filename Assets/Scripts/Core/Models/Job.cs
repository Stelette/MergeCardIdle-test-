using Core.Interface;
using Cysharp.Threading.Tasks;

namespace Core.Models
{
    public abstract class Job : IJob
    {
        public int ExecutionOrder { get; }
        
        protected Job(int executionOrder)
        {
            ExecutionOrder = executionOrder;
        }
        
        public abstract UniTask ExecuteAsync();
    }
}