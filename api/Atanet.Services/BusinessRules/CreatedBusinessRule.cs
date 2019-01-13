namespace Atanet.Services.BusinessRule
{
    using System;
    using System.Collections.Generic;
    using Atanet.Model.Interfaces;
    using Atanet.Services.BusinessRules;

    public class CreatedBusinessRule : BusinessRuleBase<ICreated>
    {
        public override void PreSave(IList<ICreated> added, IList<ICreated> updated, IList<ICreated> removed)
        {
            foreach (var addedItem in added)
            {
                addedItem.Created = DateTime.Now;
            }
        }
    }
}
