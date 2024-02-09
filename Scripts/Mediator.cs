namespace FazApp.UniMedatior
{
    public static class Mediator
    {
        private static MediatorHandlersContainer HandlersContainer { get; } = new();
        
        public static void PublishEvent<TEvent>(object sender, TEvent eventToPublish) where TEvent : BaseEvent
        {
            HandlersContainer.PublishEvent(sender, eventToPublish);
        }
        
        public static void PublishEvent<TEvent>(TEvent eventToPublish) where TEvent : BaseEvent
        {
            PublishEvent(null, eventToPublish);
        }
        
        public static void PublishEvent<TEvent>() where TEvent : BaseEvent, new()
        {
            PublishEvent(null, new TEvent());
        }
        
        public static void PublishEvent<TEvent>(object sender) where TEvent : BaseEvent, new()
        {
            PublishEvent(sender, new TEvent());
        }

        public static void AttachToEvent<TEvent>(EventHandlerDelegate eventHandler) where TEvent : BaseEvent
        {
            HandlersContainer.AttachToEventCommon<TEvent>(eventHandler);
        }
        
        public static void DetachFromEvent<TEvent>(EventHandlerDelegate eventHandler) where TEvent : BaseEvent
        {
            HandlersContainer.DetachFromEventCommon<TEvent>(eventHandler);
        }
        
        public static void AttachToEvent<TEvent>(EventHandlerDelegate<TEvent> eventHandler) where TEvent : BaseEvent
        {
            HandlersContainer.AttachToEventCommon<TEvent>(eventHandler);
        }
        
        public static void DetachFromEvent<TEvent>(EventHandlerDelegate<TEvent> eventHandler) where TEvent : BaseEvent
        {
            HandlersContainer.DetachFromEventCommon<TEvent>(eventHandler);
        }
        
        public static void SendCommand<TCommand>(object sender, TCommand command) where TCommand : Command
        {
            HandlersContainer.SendCommand(sender, command);
        }
        
        public static void SendCommand<TCommand>(TCommand command) where TCommand : Command
        {
            SendCommand(null, command);
        }

        public static void SendCommand<TCommand>() where TCommand : Command, new()
        {
            SendCommand(null, new TCommand());
        }
        
        public static void SendCommand<TCommand>(object sender) where TCommand : Command, new()
        {
            SendCommand(sender, new TCommand());
        }
        
        public static void SendCommand<TCommand, TResult>(object sender, TCommand command, out TResult result) where TCommand : Command<TResult>
        {
            command.Sender = sender;
            HandlersContainer.SendCommand(command, out result);
        }
        
        public static void SendCommand<TCommand, TResult>(TCommand command, out TResult result) where TCommand : Command<TResult>
        {
            SendCommand(null, command, out result);
        }

        public static void SendCommand<TCommand, TResult>(out TResult result) where TCommand : Command<TResult>, new()
        {
            SendCommand(null, new TCommand(), out result);
        }
        
        public static void SendCommand<TCommand, TResult>(object sender, out TResult result) where TCommand : Command<TResult>, new()
        {
            SendCommand(sender, new TCommand(), out result);
        }

        public static void AttachCommandHandler<TCommand>(CommandHandlerDelegate handler) where TCommand : Command
        {
            HandlersContainer.AttachCommandHandlerCommon<TCommand>(handler);
        }
        
        public static void AttachCommandHandler<TCommand>(CommandHandlerDelegate<TCommand> handler) where TCommand : Command
        {
            HandlersContainer.AttachCommandHandlerCommon<TCommand>(handler);
        }
        
        public static void AttachCommandHandler<TCommand, TResult>(ResultCommandHandlerDelegate<TResult> handler) where TCommand : Command<TResult>
        {
            HandlersContainer.AttachCommandHandlerCommon<TCommand>(handler);
        }
        
        public static void AttachCommandHandler<TCommand, TResult>(ResultCommandHandlerDelegate<TCommand, TResult> handler) where TCommand : Command<TResult>
        {
            HandlersContainer.AttachCommandHandlerCommon<TCommand>(handler);
        }
        
        public static void DetachCommandHandler<TCommand>(CommandHandlerDelegate handler) where TCommand : Command
        {
            HandlersContainer.DetachCommandHandlerCommon<TCommand>(handler);
        }
        
        public static void DetachCommandHandler<TCommand>(CommandHandlerDelegate<TCommand> handler) where TCommand : Command
        {
            HandlersContainer.DetachCommandHandlerCommon<TCommand>(handler);
        }
        
        public static void DetachCommandHandler<TCommand, TResult>(ResultCommandHandlerDelegate<TResult> handler) where TCommand : Command<TResult>
        {
            HandlersContainer.DetachCommandHandlerCommon<TCommand>(handler);
        }
        
        public static void DetachCommandHandler<TCommand, TResult>(ResultCommandHandlerDelegate<TCommand, TResult> handler) where TCommand : Command<TResult>
        {
            HandlersContainer.DetachCommandHandlerCommon<TCommand>(handler);
        }
    }
}