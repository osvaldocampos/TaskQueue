using System.Threading.Tasks;

namespace TaskQueue.Services
{
    public interface IEngineService
    {
        Task Process(int taskId);
    }
}