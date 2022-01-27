namespace Zoom;

public class Meeting : IEntity
{
    public Meeting()
    {
        RelatedItems = new ExpandoObject();
    }

    public long Id { get; set; }

    public string ZoomIdentifier { get; set; }

    public dynamic RelatedItems { get; set; }
}
