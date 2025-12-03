namespace SqlBuilderLab.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public bool IsActive { get; set; }
}
