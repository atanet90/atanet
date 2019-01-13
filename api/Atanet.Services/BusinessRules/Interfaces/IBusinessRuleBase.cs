namespace Atanet.Services.BusinessRules.Interfaces
{
    using System.Collections.Generic;
    using Atanet.Services.UoW;

    public interface IBusinessRuleBase
    {
        void PreSave(IList<object> added, IList<object> updated, IList<object> removed);

        void PostSave(IUnitOfWork unitOfWork);
    }
}