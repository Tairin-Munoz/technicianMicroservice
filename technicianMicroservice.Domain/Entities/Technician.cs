namespace technicianMicroservice.Domain.Entities
{
    public sealed class Technician
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstLastName { get; set; } = string.Empty;
        public string? SecondLastName { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string? DocumentExtension { get; set; }
        public string Address { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public int? ModifiedByUserId { get; set; }

        public string FullDocumentNumber =>
            string.IsNullOrWhiteSpace(DocumentExtension)
                ? DocumentNumber
                : $"{DocumentNumber}-{DocumentExtension}";
    }
}

