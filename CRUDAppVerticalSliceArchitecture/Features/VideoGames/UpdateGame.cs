using CRUDAppVerticalSliceArchitecture.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static CRUDAppVerticalSliceArchitecture.Features.VideoGames.UpdateGame;

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
}

[ApiController]
[Route("api/games")]
public class UpdateGameController(ISender sender) : ControllerBase
{
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> UpdateGame(int id, Command command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch");
        }

        var result = await sender.Send(command);

        if (result == null)
        {
            return NotFound("Video game with given Id not fount.");
        }

        return Ok(result);
    }
}