﻿namespace Atanet.Model.Dto
{
    using System;

    public class CommentDto
    {
        public long Id { get; set; }

        public long PostId { get; set; }

        public long UserId { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; set; }
    }
}
