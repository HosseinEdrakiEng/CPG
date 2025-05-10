using Application.Models;
using Helper;

namespace Application.Abstraction.IService
{
    public interface IPurchaseService
    {
        Task<BaseResponse<CreatePurchaseResponseModel>> Create(CreatePurchaseRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<InquiryPurchaseResponseModel>> Inquiry(string trackingCode, CancellationToken cancellationToken);
    }
}
