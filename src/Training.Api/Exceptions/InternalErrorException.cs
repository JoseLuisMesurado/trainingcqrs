namespace Training.API.Exceptions
{
    public class InternalErrorException : Exception
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int? Status { get; set; }
        public string Instance { get; set; }
        public string Detail { get; set; }
    }
}
