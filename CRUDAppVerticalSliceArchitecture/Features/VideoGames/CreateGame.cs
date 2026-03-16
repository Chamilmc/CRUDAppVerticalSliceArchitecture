using Carter;
using CRUDAppVerticalSliceArchitecture.Core.Entities;
using CRUDAppVerticalSliceArchitecture.Infrastructure.Contexts;
using MediatR;

namespace CRUDAppVerticalSliceArchitecture.Features.VideoGames;

public static class CreateGame
{
    public record Command(string Title, string Genre, int ReleaseYear) : IRequest<Response>;

    public record Response(int Id, string Title, string Genre, int ReleaseYear);

    public class Handler(VideoGameContext context) : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var videoGame = new VideoGame
            {
                Title = request.Title,
                Genre = request.Genre,
                ReleaseYear = request.ReleaseYear
            };
            context.VideoGames.Add(videoGame);

            await context.SaveChangesAsync(cancellationToken);

            return new Response(videoGame.Id, videoGame.Title, videoGame.Genre, videoGame.ReleaseYear);
        }
    }

    public class Endpont : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/games", async (ISender sender, Command command) =>
            {
                var result = await sender.Send(command);
                return Results.Created($"api/games/{result.Id}", result);
            });
        }
    }
}
