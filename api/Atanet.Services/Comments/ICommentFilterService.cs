namespace Atanet.Services.Comments
{
    using System;
    using System.Collections.Generic;
    using Atanet.Model.Dto;

    public interface ICommentFilterService
    {
        IEnumerable<CommentDto> GetCommentsForPost(long postId, int page, int pageSize);
    }
}
