using MediatR;
using Microsoft.EntityFrameworkCore;
using ReemProject.Application.Common.Interfaces;

namespace ReemProject.Application.Features.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await db.Projects.FirstAsync(p => p.Id == request.Id && !p.IsDeleted, cancellationToken);
        project.IsDeleted = true;
        project.DeletedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
    }
}
