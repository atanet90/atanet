namespace Atanet.Services.Files
{
    using Atanet.Model.Data;
    using Atanet.Model.Dto;
    using Atanet.Services.UoW;
    using Microsoft.AspNetCore.Http;

    public interface IFileCreationService
    {
        File CreateImageFile(IUnitOfWork unitOfWork, IFormFile formFile);

        File CreateImageFile(IUnitOfWork unitOfWork, byte[] data, string fileName);
    }
}
