namespace FluentValidator;

public interface IValidator<T>
{
    ValidationResult Validate(T entity);
}