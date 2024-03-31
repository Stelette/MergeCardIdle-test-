using Cysharp.Threading.Tasks;

namespace Core.Interface
{
    public interface IJob
    {
        int ExecutionOrder { get; }
        
        UniTask ExecuteAsync();
    }
}