namespace Training.NG.HttpResponse
{
    public class NotFoundEntityException : Exception
    {
        public string Title { get; set; } = "Entity not found";
        public string Type { get; set; } = "Wrong entity requested";
        public int? Status { get; set; } = 404;
        public string Instance { get; set; }
        public string Detail { get; set; }

    }
}
