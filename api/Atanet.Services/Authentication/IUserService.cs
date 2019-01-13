namespace Atanet.Services.Authentication
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Atanet.Model.Data;
    using Atanet.Model.Dto;

    public interface IUserService
    {
        IList<UserWithScoreDto> GetUsersSortedByScore();

        ShowUserDto GetUserInfo(long userId);

        long GetCurrentUserId();

        File GetUserProfilePicture(long userId);

        void DeleteUser(long userId);
    }
}
