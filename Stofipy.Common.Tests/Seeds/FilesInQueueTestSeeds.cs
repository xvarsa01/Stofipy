using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.Common.Tests.Seeds;

public static class FilesInQueueTestSeeds
{
    public static readonly FilesInQueueEntity QueueItem1 = new()
    {
        Id = Guid.Parse("22F5F86F-C452-4F38-A5B1-AEF04DC4D97F"),
        Index = 1,
        File = FileTestSeeds.File05,
        FileId = FileTestSeeds.File05.Id
    };
    public static readonly FilesInQueueEntity QueueItem2 = new()
    {
        Id = Guid.Parse("9901B4CF-F867-4368-A530-3AFD4AB9B747"),
        Index = 2,
        File = FileTestSeeds.File06,
        FileId = FileTestSeeds.File06.Id
    };
    public static readonly FilesInQueueEntity QueueItem3 = new()
    {
        Id = Guid.Parse("C89378F7-C0FC-43CE-80D9-4BC8BD3A961E"),
        Index = 3,
        File = FileTestSeeds.File07,
        FileId = FileTestSeeds.File07.Id
    };
    public static readonly FilesInQueueEntity QueueItem4 = new()
    {
        Id = Guid.Parse("B25F6429-F7EF-4FF0-BC10-426F642C1B20"),
        Index = 4,
        File = FileTestSeeds.File08,
        FileId = FileTestSeeds.File08.Id
    };
    public static DbContext SeedTestFilesInQueue(this DbContext dbx)
    {
        dbx.Set<FilesInQueueEntity>().AddRange(
            QueueItem1 with{File = null!},
            QueueItem2 with{File = null!},
            QueueItem3 with{File = null!},
            QueueItem4 with{File = null!}
        );
        return dbx;
    }
}