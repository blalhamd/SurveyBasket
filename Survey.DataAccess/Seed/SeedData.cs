namespace Survey.DataAccess.Seed
{
    public static class SeedData
    {
        public static void Seeding(AppDbContext dbContext)
        {
            if (dbContext is not null)
            {
                if (!dbContext.polls.Any())
                {
                    var ReadFromJson = File.ReadAllText("../Survey.DataAccess/Seed/jsons/Polls.json");
                    var Polls = JsonSerializer.Deserialize<List<Poll>>(ReadFromJson);

                    if(Polls != null)
                    {
                        foreach (var item in Polls)
                        {
                             dbContext.polls.Add(item);
                        }

                         dbContext.SaveChanges();
                    }
                }

                if(!dbContext.Questions.Any())
                {
                    var ReadFromJson = File.ReadAllText("../Survey.DataAccess/Seed/jsons/Questions.json");
                    var Questions = JsonSerializer.Deserialize<List<Question>>(ReadFromJson);

                    if(Questions != null)
                    {
                        foreach (var question in Questions)
                        {
                            dbContext.Questions.Add(question);
                        }

                         dbContext.SaveChanges();
                    }
                }

                if (!dbContext.Answers.Any())
                {
                    var ReadFromJson = File.ReadAllText("../Survey.DataAccess/Seed/jsons/Answers.json");
                    var Answers = JsonSerializer.Deserialize<List<Answer>>(ReadFromJson);

                    if (Answers != null)
                    {
                        foreach (var answer in Answers)
                        {
                             dbContext.Answers.Add(answer);
                        }

                         dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
