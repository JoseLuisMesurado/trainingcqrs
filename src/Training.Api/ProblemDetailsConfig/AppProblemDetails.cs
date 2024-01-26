namespace Training.API.ProblemDetailsConfig
{
    public class AppProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public IDictionary<string, string[]> ErrorsMessages { get; set; }
    }

}
