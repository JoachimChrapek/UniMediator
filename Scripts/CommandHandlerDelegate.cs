namespace FazApp.UniMedatior
{
    public delegate void CommandHandlerDelegate();
    public delegate void CommandHandlerDelegate<in TCommand>(TCommand baseCommand) where TCommand : Command;
    public delegate TResult ResultCommandHandlerDelegate<out TResult>();
    public delegate TResult ResultCommandHandlerDelegate<in TCommand, out TResult>(TCommand baseEvent) where TCommand : Command<TResult>;
}
