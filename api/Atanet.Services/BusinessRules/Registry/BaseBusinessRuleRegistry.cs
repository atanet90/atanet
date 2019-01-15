namespace Atanet.Services.BusinessRules.Registry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Atanet.Services.Assembly;
    using Atanet.Services.BusinessRules.Interfaces;
    using Atanet.Services.BusinessRules.Registry.Interfaces;
    using Atanet.Services.UoW;

    public class BaseBusinessRuleRegistry : IBusinessRuleRegistry
    {
        private readonly IServiceProvider serviceProvider;

        private readonly IAssemblyContainer assemblyContainer;

        private IDictionary<Type, IList<Type>> registeredEntries = new Dictionary<Type, IList<Type>>();

        public BaseBusinessRuleRegistry(IServiceProvider serviceProvider, IAssemblyContainer assemblyContainer)
        {
            this.serviceProvider = serviceProvider;
            this.assemblyContainer = assemblyContainer;
            this.RegisterEntries();
        }

        public IDictionary<Type, IList<Type>> RegisteredEntries => this.registeredEntries;

        public void RegisterEntries()
        {
            var atanetAssemblies = this.assemblyContainer.GetAssemblies();
            foreach (var atanetAssembly in atanetAssemblies)
            {
                var types = atanetAssembly.GetTypes();
                var businessRules = types.Where(x => x.GetInterfaces().Contains(typeof(IBusinessRuleBase)) && !x.IsInterface);
                foreach (var businessRule in businessRules)
                {
                    if (!businessRule.IsAbstract && !businessRule.IsGenericType)
                    {
                        var type = businessRule.BaseType.GetGenericArguments().SingleOrDefault();
                        if (businessRule.BaseType == typeof(object))
                        {
                            type = typeof(object);
                        }

                        this.RegisterEntry(type, businessRule);
                    }
                }
            }
        }

        public IEnumerable<Type> GetBusinessRulesFor<TEntity>() =>
            this.GetBusinessRulesFor(typeof(TEntity));

        public IEnumerable<Type> GetBusinessRulesFor(Type type)
        {
            var list = new List<Type>();
            foreach (var businessRuleGroup in this.RegisteredEntries)
            {
                if (businessRuleGroup.Key.GetTypeInfo().IsAssignableFrom(type))
                {
                    foreach (var value in businessRuleGroup.Value)
                    {
                        list.Add(value);
                    }
                }
            }

            // Execute least specific business rule first
            return list.Distinct().OrderBy(x => x.GetTypeInfo().IsInterface);
        }

        public IBusinessRuleBase InstantiateBusinessRule(Type type, IUnitOfWork unitOfWork)
        {
            ThrowIfInvalidBusinessRule(type);
            var instantiated = this.serviceProvider.GetService(type);
            return (IBusinessRuleBase)instantiated;
        }

        private static void ThrowIfInvalidBusinessRule(Type t)
        {
            if (!t.GetInterfaces().Contains(typeof(IBusinessRuleBase)))
            {
                throw new ArgumentException($"The business rule '{t.FullName}' must implement '{typeof(IBusinessRuleBase).FullName}'.");
            }
        }

        private void RegisterEntry(Type entityType, Type businessRuleType)
        {
            ThrowIfInvalidBusinessRule(businessRuleType);
            if (!this.registeredEntries.ContainsKey(entityType))
            {
                this.registeredEntries.Add(entityType, new List<Type>());
                this.registeredEntries[entityType].Add(businessRuleType);
                return;
            }

            this.registeredEntries[entityType].Add(businessRuleType);
        }
    }
}
