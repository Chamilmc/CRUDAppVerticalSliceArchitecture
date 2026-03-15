using CRUDAppVerticalSliceArchitecture.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
}

[ApiController]
[Route("api/games")]
public class DeleteGameController(ISender sender) : ControllerBase
{
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGame(int id)
    {
        var result = await sender.Send(new DeleteGame.Command(id));
        if (!result)
        {
            return NotFound("Video game with given Id not found.");
        }

        return NoContent();
    }
}