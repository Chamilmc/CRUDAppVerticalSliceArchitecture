using Carter;
using CRUDAppVerticalSliceArchitecture.Infrastructure.Contexts;
using MediatR;
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

    public class Endpont : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/games", async (ISender sender) =>
            {
                var result = await sender.Send(new Query());
                return Results.Ok(result);
            });
        }
    }

}

