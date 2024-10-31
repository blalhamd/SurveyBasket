namespace Survey.DataAccess.Seed
{
    public static class SeedData
    {
        public static async Task Seeding(AppDbContext dbContext)
        {
            if (dbContext is not null)
            {
                if (!await dbContext.polls.AnyAsync())
                {
                    var ReadFromJson = File.ReadAllText("../Survey.DataAccess/Seed/jsons/Polls.json");
                    var Polls = JsonSerializer.Deserialize<List<Poll>>(ReadFromJson);

                    if(Polls != null)
                    {
                        foreach (var item in Polls)
                        {
                            await dbContext.polls.AddAsync(item);
                        }

                        await dbContext.SaveChangesAsync();
                    }
                }

                if(!await dbContext.Questions.AnyAsync())
                {
                    var ReadFromJson = File.ReadAllText("../Survey.DataAccess/Seed/jsons/Questions.json");
                    var Questions = JsonSerializer.Deserialize<List<Question>>(ReadFromJson);

                    if(Questions != null)
                    {
                        foreach (var question in Questions)
                        {
                            await dbContext.Questions.AddAsync(question);
                        }

                        await dbContext.SaveChangesAsync();
                    }
                }

                if (!await dbContext.Answers.AnyAsync())
                {
                    var ReadFromJson = File.ReadAllText("../Survey.DataAccess/Seed/jsons/Answers.json");
                    var Answers = JsonSerializer.Deserialize<List<Answer>>(ReadFromJson);

                    if (Answers != null)
                    {
                        foreach (var answer in Answers)
                        {
                            await dbContext.Answers.AddAsync(answer);
                        }

                        await dbContext.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
