namespace NetCorePattern.Models
{
    public class WebHookResponse
    {
        public int Id { get; set; }
        public string Svid { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime SubmitTime { get; set; }
        public IList<Result>? Result { get; set; }
    }

    public class Result
    {
        public string Subject { get; set; } = null!;
        public string Type { get; set; } = null!;
        public IList<string>? Answer { get; set; }
    }
}
