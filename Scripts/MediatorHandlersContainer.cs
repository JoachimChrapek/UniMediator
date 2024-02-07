using System;
using System.Collections.Generic;
using UnityEngine;

namespace FazApp.UniMedatior
{
    public class MediatorHandlersContainer
    {
        private Dictionary<Type, List<Delegate>> EventHandlersCollection { get; } = new();
        private Dictionary<Type, Delegate> CommandHandlersCollection { get; } = new();
        
        public void PublishEvent<TEvent>(TEvent eventToPublish) where TEvent : BaseEvent
        {
            Type eventType = typeof(TEvent);
            
            if (EventHandlersCollection.TryGetValue(eventType, out List<Delegate> handlersList) == false)
            {
                //TODO log?
                return;
            }

            foreach (Delegate handler in handlersList)
            {
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

        public void SendCommand<TCommand>(TCommand command) where TCommand : Command
        {
            Type commandType = typeof(TCommand);
            //Type commandType = command.GetType();

            if (CommandHandlersCollection.TryGetValue(commandType, out Delegate commandHandler) == false)
            {
                //TODO log error/throw
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
        
        public void SendCommand<TCommand, TResult>(TCommand command, out TResult result) where TCommand : Command<TResult>
        {
            result = default;
            Type commandType = typeof(TCommand);
            //Type commandType = command.GetType();

            if (CommandHandlersCollection.TryGetValue(commandType, out Delegate commandHandler) == false)
            {
                //TODO log error/throw
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
                return;
            }
            
            //TODO log?
            Debug.LogError("RETURN");
        }
        
        public void AttachCommandHandlerCommon<TCommand>(Delegate commandHandler) where TCommand : BaseCommand
        {
            Type commandType = typeof(TCommand);
            
            if (CommandHandlersCollection.TryGetValue(commandType, out Delegate handler) == true)
            {
                //TODO better logs
                Debug.LogError($"Can not attach new command handler for {commandType} command - handler is already registered.\n" +
                                $"Current handler: {handler}\n" +
                                $"New handler: {commandHandler}");
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
                //TODO log error
                return;
            }

            CommandHandlersCollection.Remove(commandType);
        }
    }
}
