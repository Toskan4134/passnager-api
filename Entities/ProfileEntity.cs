namespace passnager_api;

public class ProfileEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] Icon { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
}