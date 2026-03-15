using CRUDAppVerticalSliceArchitecture.Core.Entities;
using CRUDAppVerticalSliceArchitecture.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static CRUDAppVerticalSliceArchitecture.Features.VideoGames.CreateGame;

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
}

[ApiController]
[Route("api/games")]
public class CreateGameController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Response>> CreateGame(Command command)
    {
        var result = await sender.Send(command);
        return Created($"/a[i/games/{result.Id}", result);
    }
}
