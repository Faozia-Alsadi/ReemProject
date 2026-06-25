using MediatR;

namespace ReemProject.Application.Features.Values.Queries.GetValues;

public record GetValuesQuery : IRequest<List<ValueDto>>;

public record ValueDto(int Id, string NameAr, string NameEn, string Color, string IconClass, int DisplayOrder, int ProjectCount);
