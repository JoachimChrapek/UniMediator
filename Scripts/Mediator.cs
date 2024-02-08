namespace FazApp.UniMedatior
{
    public static class Mediator
    {
        private static MediatorHandlersContainer HandlersContainer { get; } = new();
        
        public static void PublishEvent<TEvent>(TEvent eventToPublish) where TEvent : BaseEvent
        {
            HandlersContainer.PublishEvent(eventToPublish);
        }

        public static void PublishEvent<TEvent>() where TEvent : BaseEvent, new()
        {
            HandlersContainer.PublishEvent<TEvent>();
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
        
        public static void SendCommand<TCommand>(TCommand command) where TCommand : Command
        {
            HandlersContainer.SendCommand(command);
        }

        public static void SendCommand<TCommand>() where TCommand : Command, new()
        {
            HandlersContainer.SendCommand<TCommand>();
        }
        
        public static void SendCommand<TCommand, TResult>(TCommand command, out TResult result) where TCommand : Command<TResult>
        {
            HandlersContainer.SendCommand(command, out result);
        }

        public static void SendCommand<TCommand, TResult>(out TResult result) where TCommand : Command<TResult>, new()
        {
            HandlersContainer.SendCommand<TCommand, TResult>(out result);
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