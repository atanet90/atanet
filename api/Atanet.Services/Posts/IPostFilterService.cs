namespace Atanet.Services.Posts
{
    using System;
    using System.Collections.Generic;
    using Atanet.Model.Data;
    using Atanet.Model.Dto;

    public interface IPostFilterService
    {
        IList<PostDto> FilterPosts(int page, int pageSize, int commentCount);

        File GetPictureForPost(long postId);
    }
}
