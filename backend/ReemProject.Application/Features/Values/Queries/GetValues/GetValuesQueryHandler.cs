using MediatR;
using Microsoft.EntityFrameworkCore;
using ReemProject.Application.Common.Interfaces;

namespace ReemProject.Application.Features.Values.Queries.GetValues;

public class GetValuesQueryHandler(IApplicationDbContext db) : IRequestHandler<GetValuesQuery, List<ValueDto>>
{
    public async Task<List<ValueDto>> Handle(GetValuesQuery request, CancellationToken cancellationToken)
    {
        return await db.Values
            .Where(v => !v.IsDeleted)
            .OrderBy(v => v.DisplayOrder)
            .Select(v => new ValueDto(
                v.Id, v.NameAr, v.NameEn, v.Color, v.IconClass,
                v.DisplayOrder,
                v.Projects.Count(p => !p.IsDeleted)))
            .ToListAsync(cancellationToken);
    }
}
