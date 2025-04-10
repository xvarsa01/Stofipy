using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.Common.Tests.Seeds;

public static class FilesInQueueTestSeeds
{
    public static readonly FilesInQueueEntity QueueItemP1 = new()
    {
        Id = Guid.Parse("22F5F86F-C452-4F38-A5B1-AEF04DC4D97F"),
        Index = 1,
        File = FileTestSeeds.File05,
        FileId = FileTestSeeds.File05.Id,
        PriorityQueue = true
    };
    public static readonly FilesInQueueEntity QueueItemP2 = new()
    {
        Id = Guid.Parse("9901B4CF-F867-4368-A530-3AFD4AB9B747"),
        Index = 2,
        File = FileTestSeeds.File06,
        FileId = FileTestSeeds.File06.Id,
        PriorityQueue = true
    };
    public static readonly FilesInQueueEntity QueueItemP3 = new()
    {
        Id = Guid.Parse("C89378F7-C0FC-43CE-80D9-4BC8BD3A961E"),
        Index = 3,
        File = FileTestSeeds.File07,
        FileId = FileTestSeeds.File07.Id,
        PriorityQueue = true
    };
    public static readonly FilesInQueueEntity QueueItemP4 = new()
    {
        Id = Guid.Parse("B25F6429-F7EF-4FF0-BC10-426F642C1B20"),
        Index = 4,
        File = FileTestSeeds.File08,
        FileId = FileTestSeeds.File08.Id,
        PriorityQueue = true
    };
    
    public static readonly FilesInQueueEntity QueueItemN1 = new()
    {
        Id = Guid.Parse("29D59CAF-4FC1-4F73-BDE5-0CBA0E76D650"),
        Index = 1,
        File = FileTestSeeds.File01,
        FileId = FileTestSeeds.File01.Id,
        PriorityQueue = false
    };
    public static readonly FilesInQueueEntity QueueItemN2 = new()
    {
        Id = Guid.Parse("BE6C18D9-5EE1-4312-9A3C-D47AD6EE9DF6"),
        Index = 2,
        File = FileTestSeeds.File02,
        FileId = FileTestSeeds.File02.Id,
        PriorityQueue = false
    };
    public static readonly FilesInQueueEntity QueueItemN3 = new()
    {
        Id = Guid.Parse("EB8DAB7C-BE9E-4CF2-A9BA-CF0D72DFACFB"),
        Index = 3,
        File = FileTestSeeds.File03,
        FileId = FileTestSeeds.File03.Id,
        PriorityQueue = false
    };
    public static DbContext SeedTestFilesInQueue(this DbContext dbx)
    {
        dbx.Set<FilesInQueueEntity>().AddRange(
            QueueItemP1 with{File = null!},
            QueueItemP2 with{File = null!},
            QueueItemP3 with{File = null!},
            QueueItemP4 with{File = null!},
            QueueItemN1 with{File = null!},
            QueueItemN2 with{File = null!},
            QueueItemN3 with{File = null!}
        );
        return dbx;
    }
}