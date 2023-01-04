using Adventuring.Contexts.UserManager.Mapper.Interface.Token;
using Adventuring.Contexts.UserManager.Model.Contract.Token.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.Token.Create;
using AutoMapper;

namespace Adventuring.Contexts.UserManager.Mapper.Implementation.Mappers.Token;

/// <inheritdoc/>
public class TokenMapper : ITokenMapper
{
    private readonly IMapper Mapper;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="mapper"></param>
    public TokenMapper(IMapper mapper)
    {
        this.Mapper = mapper;
    }

    /// <inheritdoc/>
    public CreateTokenInputModel Map(CreateTokenRequestModel model)
    {
        return this.Mapper.Map<CreateTokenInputModel>(model);
    }

    /// <inheritdoc/>
    public CreateTokenResponseModel Map(CreateTokenOutputModel model)
    {
        return this.Mapper.Map<CreateTokenResponseModel>(model);
    }
}