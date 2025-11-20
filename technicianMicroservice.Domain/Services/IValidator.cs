namespace technicianMicroservice.Domain.Services;

public interface IValidator<T>
{
    Result Validate(T entity);
}
