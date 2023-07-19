using Microsoft.AspNetCore.Http;

public class EditCategoryDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int? ParentId { get; set; }
    public IFormFile? Image { get; set; }
}