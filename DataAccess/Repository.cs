namespace Zoom;

public class Repository
{
    public static Write<Zoom.Meeting> Meeting
    {
        get
        {
            return new Write<Zoom.Meeting>(new ZoomContext());
        }
    }
}
