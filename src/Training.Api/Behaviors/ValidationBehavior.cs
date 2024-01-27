using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Training.NG.HttpResponse;

namespace Training.API.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToke)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            ValidationContext<TRequest> context = new(request);
            Dictionary<string, string[]> errorsDictionary = (from x in _validators.Select((IValidator<TRequest> x) => x.Validate(context)).SelectMany((ValidationResult x) => x.Errors)
                                                             where x != null
                                                             select x).GroupBy((ValidationFailure x) => x.PropertyName, (ValidationFailure x) => x.ErrorMessage, (string propertyName, IEnumerable<string> errorMessages) => new
                                                             {
                                                                 Key = propertyName,
                                                                 Values = errorMessages.Distinct().ToArray()
                                                             }).ToDictionary(x => x.Key, x => x.Values);
            if (errorsDictionary.Any())
            {
                throw new ModelValidationException
                {
                    Type = "Validations",
                    Title = "Validation Failure ",
                    Detail = "One or more validation erros occurred",
                    Status = 400,
                    ErrorsMessages = errorsDictionary
                };
            }

            return await next();
        }
       
    }
}
