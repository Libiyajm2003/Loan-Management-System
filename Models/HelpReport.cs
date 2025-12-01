using System.ComponentModel.DataAnnotations;

public class HelpReport
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    // Add these
    public string Status { get; set; }
    public string? Remarks { get; set; }
}
