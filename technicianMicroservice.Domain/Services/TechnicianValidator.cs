using System.Text.RegularExpressions;
using technicianMicroservice.Domain.Entities;

namespace technicianMicroservice.Domain.Services;

public class TechnicianValidator : IValidator<Technician>
{
    public Result Validate(Technician entity)
    {
        var errors = new List<string>();

        var name = entity.Name?.Trim() ?? string.Empty;
        var firstLastName = entity.FirstLastName?.Trim( ) ?? string.Empty;
        var secondLastName = entity.SecondLastName?.Trim() ?? string.Empty;
        var email = entity.Email?.Trim() ?? string.Empty;
        var documentNumber = entity.DocumentNumber?.Trim() ?? string.Empty;
        var documentExt = entity.DocumentExtension?.Trim() ?? string.Empty;
        var address = entity.Address?.Trim() ?? string.Empty;

        ValidatePersonName("Nombre", name, required: true, errors);
        ValidatePersonName("Apellido paterno", firstLastName, required: true, errors);
        ValidatePersonName("Apellido materno", secondLastName, required: false, errors);

        ValidatePhone(entity.PhoneNumber, errors);
        ValidateEmail(email, errors);

        ValidateDocumentNumber(documentNumber, errors);
        ValidateDocumentExtension(documentExt, errors);

        ValidateAddress(address, errors);
        ValidateBaseSalary(entity.BaseSalary, errors);

        return errors.Count == 0 ? Result.Success() : Result.Failure(errors);
    }

    private void ValidatePersonName(string label, string value, bool required, List<string> errors)
    {
        if (!required && string.IsNullOrWhiteSpace(value)) return;

        if (required && string.IsNullOrWhiteSpace(value))
        {
            errors.Add($"{label} es obligatorio.");
            return;
        }

        if (value.Length < 2) errors.Add($"{label} debe tener al menos 2 caracteres.");
        if (value.Length > 100) errors.Add($"{label} no puede superar 100 caracteres.");

        // Solo letras y espacios (incluye tildes y ñ/ü)
        if (!Regex.IsMatch(value, @"^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s]+$"))
            errors.Add($"{label} solo puede contener letras y espacios.");
    }

    private void ValidatePhone(int phone, List<string> errors)
    {
        var s = phone.ToString();
        if (!Regex.IsMatch(s, @"^\d{8}$"))
            errors.Add("El teléfono debe tener exactamente 8 dígitos.");
    }

    private void ValidateEmail(string email, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            errors.Add("El correo electrónico es obligatorio.");
            return;
        }

        // Regex simple y suficiente para validación de servidor
        var ok = Regex.IsMatch(email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase);

        if (!ok) errors.Add("Ingrese un correo electrónico válido.");
        if (email.Length > 255) errors.Add("El correo electrónico no puede superar 255 caracteres.");
    }

    private void ValidateDocumentNumber(string number, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            errors.Add("El CI es obligatorio.");
            return;
        }

        if (!Regex.IsMatch(number, @"^\d{6,10}$"))
            errors.Add("El CI debe contener entre 6 y 10 dígitos.");
    }

    private void ValidateDocumentExtension(string ext, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(ext)) return; // opcional

        if (!Regex.IsMatch(ext, @"^[1-9][A-Z]$"))
            errors.Add("El Complemento debe tener formato: dígito 1–9 seguido de letra mayúscula (ej. 1A).");
    }

    private void ValidateAddress(string address, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            errors.Add("La dirección es obligatoria.");
            return;
        }

        if (address.Length < 5) errors.Add("La dirección debe tener al menos 5 caracteres.");
        if (address.Length > 200) errors.Add("La dirección no puede superar 200 caracteres.");
    }

    private void ValidateBaseSalary(decimal? salary, List<string> errors)
    {
        if (!salary.HasValue)
        {
            errors.Add("El salario base es obligatorio.");
            return;
        }

        if (salary.Value <= 0) errors.Add("El salario base debe ser mayor que 0.");
        if (decimal.Round(salary.Value, 2) != salary.Value)
            errors.Add("El salario base solo puede tener hasta 2 decimales.");
    }
}
