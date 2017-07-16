using System.Threading.Tasks;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.View
{
    public interface IViewWriter
    {
        Task UpdateView(IRoot root);
    }
}