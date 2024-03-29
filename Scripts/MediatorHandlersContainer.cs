using System;
using System.Collections.Generic;
using UnityEngine;

namespace FazApp.UniMedatior
{
    public class MediatorHandlersContainer
    {
        private Dictionary<Type, List<Delegate>> EventHandlersCollection { get; } = new();
        private Dictionary<Type, Delegate> CommandHandlersCollection { get; } = new();
        
        public void PublishEvent<TEvent>(object sender, TEvent eventToPublish) where TEvent : BaseEvent
        {
            if (eventToPublish != null)
            {
                eventToPublish.Sender = sender;
            }
            
            Type eventType = typeof(TEvent);
            
            if (EventHandlersCollection.TryGetValue(eventType, out List<Delegate> handlersList) == false)
            {
                Debug.Log($"Event {typeof(TEvent).FullName} does not have any handlers registered");
                return;
            }

            for (int index = handlersList.Count - 1; index >= 0; index--)
            {
                Delegate handler = handlersList[index];

                if (handler is EventHandlerDelegate<TEvent> genericEventHandler)
                {
                    genericEventHandler.Invoke(eventToPublish);
                    continue;
                }

                if (handler is EventHandlerDelegate eventHandler)
                {
                    eventHandler.Invoke();
                }
            }
        }
        
        public void AttachToEventCommon<TEvent>(Delegate eventHandler) where TEvent : BaseEvent
        {
            Type eventType = typeof(TEvent);

            if (EventHandlersCollection.TryGetValue(eventType, out List<Delegate> handlersList) == false)
            {
                handlersList = new List<Delegate>();
                EventHandlersCollection[eventType] = handlersList;
            }

            if (handlersList.Contains(eventHandler) == false)
            {
                handlersList.Add(eventHandler);
            }
        }
        
        public void DetachFromEventCommon<TEvent>(Delegate eventHandler) where TEvent : BaseEvent
        {
            Type eventType = typeof(TEvent);

            if (EventHandlersCollection.TryGetValue(eventType, out List<Delegate> handlersList) == true)
            {
                handlersList.Remove(eventHandler);
            }
        }

        public void SendCommand<TCommand>(object sender, TCommand command) where TCommand : Command
        {
            if (command != null)
            {
                command.Sender = sender;
            }
            
            Type commandType = typeof(TCommand);

            if (CommandHandlersCollection.TryGetValue(commandType, out Delegate commandHandler) == false)
            {
                Debug.LogError($"Command {typeof(TCommand).FullName} does not have any handler registered");
                return;
            }

            if (commandHandler is CommandHandlerDelegate<TCommand> valueHandler)
            {
                valueHandler.Invoke(command);
                return;
            }
            
            if (commandHandler is CommandHandlerDelegate handler)
            {
                handler.Invoke();
            }
        }

        public void SendCommand<TCommand, TResult>(object sender, TCommand command, out TResult result) where TCommand : Command<TResult>
        {
            if (command != null)
            {
                command.Sender = sender;
            }
            
            result = default;
            Type commandType = typeof(TCommand);

            if (CommandHandlersCollection.TryGetValue(commandType, out Delegate commandHandler) == false)
            {
                Debug.LogError($"Command {typeof(TCommand).FullName} does not have any handler registered");
                return;
            }

            if (commandHandler is ResultCommandHandlerDelegate<TCommand, TResult> valueHandler)
            {
                result = valueHandler.Invoke(command);
                return;
            }
            
            if (commandHandler is ResultCommandHandlerDelegate<TResult> handler)
            {
                result = handler.Invoke();
            }
        }

        public void AttachCommandHandlerCommon<TCommand>(Delegate commandHandler) where TCommand : BaseCommand
        {
            Type commandType = typeof(TCommand);
            
            if (CommandHandlersCollection.TryGetValue(commandType, out Delegate handler) == true)
            {
                Debug.LogError($"Can not attach new command handler for {commandType.FullName} command - handler is already registered.\n" +
                                $"Current handler: {handler.Target.GetType().FullName}, method {handler.Method.Name}\n" +
                                $"New handler: {commandHandler.Target.GetType().FullName}, method {commandHandler.Method.Name}");
                return;
            }

            CommandHandlersCollection[commandType] = commandHandler;
        }
        
        public void DetachCommandHandlerCommon<TCommand>(Delegate commandHandler) where TCommand : BaseCommand
        {
            Type commandType = typeof(TCommand);
            
            if (CommandHandlersCollection.TryGetValue(commandType, out Delegate handler) == false)
            {
                return;
            }

            if (handler != commandHandler)
            {
                Debug.LogError($"Command {typeof(TCommand).FullName} has different handler registered\n" +
                               $"Handler to detach: {commandHandler.Target.GetType().FullName}, method {commandHandler.Method.Name}\n" +
                               $"Current registered handler: {handler.Target.GetType().FullName}, method {handler.Method.Name}");
                return;
            }

            CommandHandlersCollection.Remove(commandType);
        }
    }
}
