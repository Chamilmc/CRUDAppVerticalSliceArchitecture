using CRUDAppVerticalSliceArchitecture.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDAppVerticalSliceArchitecture.Features.VideoGames;

public static class GetAllGames
{
    public record Query : IRequest<IEnumerable<Response>>;

    public record Response(int It, string Title, string Genre, int ReleaseYear);

    public class Handler(VideoGameContext context) : IRequestHandler<Query, IEnumerable<Response>>
    {

        public async Task<IEnumerable<Response>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            var videoGames = await context.VideoGames.ToListAsync(cancellationToken);
            return videoGames.Select(v => new Response(v.Id, v.Title, v.Genre, v.ReleaseYear));
        }

    }

}

[ApiController]
[Route("api/games")]
public class GetAllGamesController(ISender sender) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllGames.Response>>> GetAllGames()
    {
        var result = await sender.Send(new GetAllGames.Query());
        return Ok(result);
    }
}
