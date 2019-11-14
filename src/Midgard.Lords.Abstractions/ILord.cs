using System.Threading.Tasks;

namespace Midgard.Lords.Abstractions
{
    public interface ILord
    {
        Task StartAsync();

        Task StopAsync();
    }
}
