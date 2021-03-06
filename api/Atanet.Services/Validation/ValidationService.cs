﻿namespace Atanet.Services.Validation
{
    using System;
    using FluentValidation;

    public class ValidationService : ValidatorFactoryBase
    {
        private readonly IServiceProvider serviceProvider;

        public ValidationService(IServiceProvider serviceProvider) =>
            this.serviceProvider = serviceProvider;

        public override IValidator CreateInstance(Type validatorType)
        {
            if (validatorType.IsInterface)
            {
                return (IValidator)this.serviceProvider.GetService(validatorType);
            }

            var type = typeof(IValidator).MakeGenericType(validatorType);
            return (IValidator)this.serviceProvider.GetService(type);
        }
    }
}
