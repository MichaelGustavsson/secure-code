using System.ComponentModel.DataAnnotations;

namespace secure_api.ViewModels;

public class TodoItemPostViewModel
{
    [Required]
    [MaxLength(30, ErrorMessage = "Titel får endast vara 30 tecken")]
    public required string Title { get; set; }
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(128, ErrorMessage = "Beskrivning får endast vara 128 tecken")]
    public required string Description { get; set; }
}