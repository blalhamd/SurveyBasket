namespace Survey.Business.Services.Email.Helper
{
    public static class EmailBodyBuilder
    {
        public static string GenerateEmailBody(string template, Dictionary<string, string> placeholders)
        {
            var templatePath = $"../Survey.Business/Templates/{template}.html";
            var reader = new StreamReader(templatePath);
            var body = reader.ReadToEnd();
            reader.Close();

            foreach (var item in placeholders)
                body = body.Replace(item.Key, item.Value);

            return body;
        }
    }
}
