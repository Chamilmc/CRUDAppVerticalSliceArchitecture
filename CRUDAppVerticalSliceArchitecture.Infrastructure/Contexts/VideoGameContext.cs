using Microsoft.EntityFrameworkCore;

namespace CRUDAppVerticalSliceArchitecture.Infrastructure.Contexts;

public class VideoGameContext: 
    DbContext
{
    public VideoGameContext(DbContextOptions<VideoGameContext> options) : 
        base(options)
    {

    }

    public DbSet<Core.Entities.VideoGame> VideoGames { get; set; }

}
