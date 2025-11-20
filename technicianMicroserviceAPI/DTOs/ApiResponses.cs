namespace technicianMicroservice.DTOs
{
    public class CreateTechnicianDto
    {
        public string Name { get; set; } = string.Empty;
        public string FirstLastName { get; set; } = string.Empty;
        public string? SecondLastName { get; set; }
        public int PhoneNumber { get; set; }
        public required string Email { get; set; } 
        public string DocumentNumber { get; set; } = string.Empty;
        public string? DocumentExtension { get; set; }
        public string Address { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }
    }

    public class UpdateTechnicianDto
    {
        public string Name { get; set; } = string.Empty;
        public string FirstLastName { get; set; } = string.Empty;
        public string? SecondLastName { get; set; }
        public int PhoneNumber { get; set; }
        public required string Email { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string? DocumentExtension { get; set; }
        public string Address { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }
    }

    public class ValidationErrorResponse
    {
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
    }

    public class SuccessResponse
    {
        public string Message { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
