using Carter;
using CRUDAppVerticalSliceArchitecture.Infrastructure.Contexts;
using MediatR;

namespace CRUDAppVerticalSliceArchitecture.Features.VideoGames;

public static class UpdateGame
{
    public record Command(int Id, string Title, string Genre, int ReleaseYear) : IRequest<Response?>;

    public record Response(int Id, string Title, string Genre, int ReleaseYear);

    public class Handler(VideoGameContext context) : IRequestHandler<Command, Response?>
    {
        public async Task<Response?> Handle(Command request, CancellationToken cancellationToken)
        {
            var videoGame = await context.VideoGames.FindAsync(new object[] { request.Id }, cancellationToken);
            if (videoGame == null)
            {
                return null;
            }

            videoGame.Title = request.Title;
            videoGame.Genre = request.Genre;
            videoGame.ReleaseYear = request.ReleaseYear;

            await context.SaveChangesAsync(cancellationToken);

            return new Response(videoGame.Id, videoGame.Title, videoGame.Genre, videoGame.ReleaseYear);
        }
    }

    public class Endpont : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/api/games/{id}", async (ISender sender, int id, Command command) =>
            {
                var updateGame = await sender.Send(command with { Id = id });

                return updateGame is not null ? Results.Ok(updateGame) : 
                Results.NotFound("Video game with given Id not found.");
            });
        }
    }
}
