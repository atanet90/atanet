namespace Atanet.Model.Data
{
    using System;
    using Atanet.Model.Interfaces;

    public class File : IIdentifiable, ICreated
    {
        public long Id { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }

        public DateTime Created { get; set; }
    }
}
