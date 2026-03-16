using Carter;
using CRUDAppVerticalSliceArchitecture.Infrastructure.Contexts;
using MediatR;

namespace CRUDAppVerticalSliceArchitecture.Features.VideoGames;

public static class DeleteGame
{
    public record Command(int Id) : IRequest<bool>;

    public class Handler(VideoGameContext context) : IRequestHandler<Command, bool>
    {
        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var videoGame = await context.VideoGames.FindAsync(new object[] { request.Id }, cancellationToken);
            if (videoGame == null)
            {
                return false;
            }

            context.VideoGames.Remove(videoGame);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public class Endpont : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/api/games/{id}", async (ISender sender, int id) =>
            {
                return await sender.Send(new Command(id)) ? Results.NoContent() :
                Results.NotFound("Video game with given Id not found.");
            });
        }
    }
}
