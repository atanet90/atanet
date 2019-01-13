namespace Atanet.Services.UoW
{
    using System.Linq;
    using Atanet.Model.Interfaces;

    public interface IQueryService
    {
        IQueryable<T> Query<T>() where T : class, IIdentifiable;
    }
}
