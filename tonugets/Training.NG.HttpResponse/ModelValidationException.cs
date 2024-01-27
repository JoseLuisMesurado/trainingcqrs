namespace Training.NG.HttpResponse
{
    public class ModelValidationException : Exception
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int? Status { get; set; } = 400;
        public string Instance { get; set; }
        public string Detail { get; set; }
        public IDictionary<string, string[]> ErrorsMessages { get; set; }
    }

}
