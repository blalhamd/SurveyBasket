
namespace Survey.DataAccess.Seed
{
    public static class SeedData
    {
        public static async Task Seeding(AppDbContext dbContext)
        {
            if(dbContext is not null)
            {
                if(!await dbContext.polls.AnyAsync())
                {
                    var ReadFromJson = File.ReadAllText("../Survey.DataAccess/Seed/jsons/Polls.json");
                    var Polls = JsonSerializer.Deserialize<List<Poll>>(ReadFromJson);

                    foreach (var item in Polls)
                    {
                        await dbContext.polls.AddAsync(item);
                    }

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
