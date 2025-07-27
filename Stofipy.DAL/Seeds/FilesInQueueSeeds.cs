using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Seeds;

public static class FilesInQueueSeeds
{
    public static readonly FilesInQueueEntity QueueItem0 = new()
    {
        Id = Guid.Parse("bac96347-ee3a-4bb8-9127-eae77dfa6ed4"),
        Index = 0,
        File = FileSeeds.KomisarRex,
        FileId = FileSeeds.KomisarRex.Id,
        PriorityQueue = true
    };
    public static readonly FilesInQueueEntity QueueItemP1 = new()
    {
        Id = Guid.Parse("6A452CF6-4375-4C5F-9D4B-FB7F8BD2AD7E"),
        Index = 1,
        File = FileSeeds.Intro,
        FileId = FileSeeds.Intro.Id,
        PriorityQueue = true
    };
    public static readonly FilesInQueueEntity QueueItemP2 = new()
    {
        Id = Guid.Parse("8991371f-4313-4287-a9cd-f87fc21a0600"),
        Index = 2,
        File = FileSeeds.Vlak,
        FileId = FileSeeds.Vlak.Id,
        PriorityQueue = true
    };
    public static readonly FilesInQueueEntity QueueItemP3 = new()
    {
        Id = Guid.Parse("fdb5d4dd-edf5-405d-a514-f4d2b7d804f3"),
        Index = 3,
        File = FileSeeds.ToOkoloNas,
        FileId = FileSeeds.ToOkoloNas.Id,
        PriorityQueue = true
    };
    public static readonly FilesInQueueEntity QueueItemP4 = new()
    {
        Id = Guid.Parse("f6321b22-b600-4896-a497-a10c4311ce8e"),
        Index = 4,
        File = FileSeeds.Abstinent,
        FileId = FileSeeds.Abstinent.Id,
        PriorityQueue = true
    };
    
    public static readonly FilesInQueueEntity QueueItemN1 = new()
    {
        Id = Guid.Parse("0c7d487c-d940-42ef-bc8c-aad62545537d"),
        Index = 1,
        File = FileSeeds.Svadobna,
        FileId = FileSeeds.Svadobna.Id,
        PriorityQueue = false
    };
    public static readonly FilesInQueueEntity QueueItemN2 = new()
    {
        Id = Guid.Parse("efb550bb-27cf-4320-93ee-69ce0899b81e"),
        Index = 2,
        File = FileSeeds.NasouKrajinou,
        FileId = FileSeeds.NasouKrajinou.Id,
        PriorityQueue = false
    };
    public static readonly FilesInQueueEntity QueueItemN3 = new()
    {
        Id = Guid.Parse("0e067e99-22a9-4adf-8777-7d5f20af94e9"),
        Index = 3,
        File = FileSeeds.Nostalgia,
        FileId = FileSeeds.Nostalgia.Id,
        PriorityQueue = false
    };
    public static readonly FilesInQueueEntity QueueItemN4 = new()
    {
        Id = Guid.Parse("936d5eaf-9040-474c-be1b-1958085c8340"),
        Index = 4,
        File = FileSeeds.Miesta,
        FileId = FileSeeds.Miesta.Id,
        PriorityQueue = false
    };
    public static DbContext SeedFilesInQueue(this DbContext dbx)
    {
        dbx.Set<FilesInQueueEntity>().AddRange(
            QueueItem0 with{File = null!},
            QueueItemP1 with{File = null!},
            QueueItemP2 with{File = null!},
            QueueItemP3 with{File = null!},
            QueueItemP4 with{File = null!},
            QueueItemN1 with{File = null!},
            QueueItemN2 with{File = null!},
            QueueItemN3 with{File = null!},
            QueueItemN4 with{File = null!}
        );
        return dbx;
    }
}