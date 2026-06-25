using MediatR;
using Microsoft.EntityFrameworkCore;
using ReemProject.Application.Common.Interfaces;
using ReemProject.Domain.Entities;

namespace ReemProject.Application.Features.Projects.Commands.UpsertProject;

public class UpsertProjectCommandHandler(IApplicationDbContext db) : IRequestHandler<UpsertProjectCommand, int>
{
    public async Task<int> Handle(UpsertProjectCommand request, CancellationToken cancellationToken)
    {
        Project project;

        if (request.Id.HasValue)
        {
            project = await db.Projects.FirstAsync(p => p.Id == request.Id && !p.IsDeleted, cancellationToken);
        }
        else
        {
            project = new Project();
            await db.Projects.AddAsync(project, cancellationToken);
        }

        project.NameAr = request.NameAr;
        project.NameEn = request.NameEn;
        project.DescriptionAr = request.DescriptionAr;
        project.DescriptionEn = request.DescriptionEn;
        project.Status = request.Status;
        project.ValueId = request.ValueId;
        project.ManagerId = request.ManagerId;
        project.StartDate = request.StartDate;
        project.ExpectedEndDate = request.ExpectedEndDate;
        project.ActualEndDate = request.ActualEndDate;
        project.Notes = request.Notes;

        await db.SaveChangesAsync(cancellationToken);
        return project.Id;
    }
}
