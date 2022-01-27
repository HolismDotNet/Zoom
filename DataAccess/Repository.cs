namespace Zoom;

public class Repository
{
    public static Repository<Zoom.Meeting> Meeting
    {
        get
        {
            return new Repository<Zoom.Meeting>(new ZoomContext());
        }
    }
}
