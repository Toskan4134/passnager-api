namespace passnager_api;

public class CategoryEntity
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public string Name { get; set; }
    public byte[] Icon { get; set; }
    public bool IsActive { get; set; }
}