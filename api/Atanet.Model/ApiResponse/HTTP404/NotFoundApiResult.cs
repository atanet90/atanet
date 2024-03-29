﻿namespace Atanet.Model.ApiResponse.HTTP404
{
    using System;
    using System.Net;
    using Atanet.Model.Validation;

    public class NotFoundApiResult : ApiResultBase, INotFoundApiResult
    {
        public NotFoundApiResult(AtanetEntityName accessedEntity, long entityId)
        {
            this.AccessedEntityType = accessedEntity;
            this.AccessedEntityId = entityId;
            this.Message = $"{this.AccessedEntityType.ToString()} does not exist. The owner may have deleted it";
        }

        public AtanetEntityName AccessedEntityType { get; set; }

        public long AccessedEntityId { get; set; }

        public override HttpStatusCode Code => HttpStatusCode.NotFound;

        public override bool Success => false;

        public override string Message { get; set; }

        public override object GetJsonObject() => new
        {
            Success = false,
            Message = this.Message,
            AccessedEntityType = this.AccessedEntityType.ToString(),
            AccessedEntityId = this.AccessedEntityId
        };
    }
}
