namespace Atanet.Services.UoW
{
    using System;
    using Atanet.Services.Repository;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> CreateEntityRepository<T>();

        void Save();
    }
}
