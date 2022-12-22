namespace passnager_api;

public class SiteEntity
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Site { get; set; }
    public string Url { get; set; }
    public string User { get; set; }
    public DateTime Date { get; set; }
    public string Password { get; set; }
    public string Description { get; set; }
    public bool isActive { get; set; }
}