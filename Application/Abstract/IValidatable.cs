using FluentValidation;

namespace Application.Abstract;

public interface IValidatable<T>
{
    AbstractValidator<T> Validator { get; }
}