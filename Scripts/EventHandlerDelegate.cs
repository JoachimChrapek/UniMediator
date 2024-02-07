namespace FazApp.UniMedatior
{
    public delegate void EventHandlerDelegate();
    public delegate void EventHandlerDelegate<in T>(T baseEvent) where T : BaseEvent;
}
