namespace Atanet.Services.BusinessRules.Registry.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Atanet.Services.BusinessRules.Interfaces;
    using Atanet.Services.UoW;

    public interface IBusinessRuleRegistry
    {
        IDictionary<Type, IList<Type>> RegisteredEntries { get; }

        IEnumerable<Type> GetBusinessRulesFor<TEntity>();

        IEnumerable<Type> GetBusinessRulesFor(Type type);

        IBusinessRuleBase InstantiateBusinessRule(Type type, IUnitOfWork unitOfWork);

        void RegisterEntries();
    }
}
