# UniMediator
Mediator pattern implementation for Unity, supports commands and events. It uses a simple approach without the use of advanced patterns and practices such as DependencyInjection. It is intended for use in less advanced projects where we do not create separate classes for each event/command handler.

## How to
### Events

Events are used to notify different modules of your app that something has happened.

Each event can have multiple handlers (or none) and it does not return value.

#### Event declaration
Events derive from **BaseEvent** class. If your event required parameters, you can add them as public property
```
public class TestEvent1 : BaseEvent { }

public class TestEvent2 : BaseEvent
{
    public int P { get; }

    public TestEvent2(int p)
    {
        P = p;
    }
} 
```

#### Event handlers
These are methods are invoked when an event is published. You can use the event as parameter if you want to use passed values or leave it empty.
```
private void EmptyEventHandler()
{
    Debug.Log("Empty event handler");
}

private void TestEvent2Handler(TestEvent2 testEvent)
{
    Debug.Log($"TestEvent2 handler. P value: {testEvent.P}");
}
```

#### Event subscription
There are Attach and Detach methods for events. It takes generic parameter which is type of the event you want to subscribe and handler method delegate.
```
private void AttachToEvents()
{
    Mediator.AttachToEvent<TestEvent1>(EmptyEventHandler);
    Mediator.AttachToEvent<TestEvent2>(EmptyEventHandler);
    Mediator.AttachToEvent<TestEvent2>(TestEvent2Handler);
}

private void DetachFromEvents()
{
    Mediator.DetachFromEvent<TestEvent1>(EmptyEventHandler);
    Mediator.DetachFromEvent<TestEvent2>(EmptyEventHandler);
    Mediator.DetachFromEvent<TestEvent2>(TestEvent2Handler);
}
```

#### Event publishing
Before publishing an event you need to create new instance of it and pass it in **Mediator.PublishEvent** method
```
private void PublishEvent1()
{
    Mediator.PublishEvent(new TestEvent1());
}

private void PublishEvent2()
{
    Mediator.PublishEvent(new TestEvent2(222));
}
```

### Commands

Commands differ from events in several ways:
- a command can only have one handler, it throws an error if you try to add more than one
- if you call a command that does not have a handler it will throw an error
- a command can return a value (in this case the handler must also return a value)

Commands are used to send "do something" requests.

#### Commands declaration
Command derive from **Command** class. If you want to return a value from command use generic Command<TResult> class. 

In the example below **TestReturnValueCommand** will return **string**
```
public class TestCommand : Command
{
    public int Value { get; }

    public TestCommand(int value)
    {
        Value = value;
    }
}

public class TestReturnValueCommand : Command<string>
{
    public int Value { get; }

    public TestReturnValueCommand(int value)
    {
        Value = value;
    }
}
```

#### Command handlers
Similiar to events. They can take command as parameter or you can leave it empty. Handler used in commands with return value should also return value with same type.
```
private void EmptyCommandHandler()
{
    Debug.Log("Empty command handler");
}

private string EmptyCommandHandlerWithReturnValue()
{
    Debug.Log($"Empty command handler with return value");
    return "some text";
}

private void TestCommandHandler(TestCommand command)
{
    Debug.Log($"TestCommand handler. Command value: {command.Value}");
}

private string TestReturnValueCommandHandler(TestReturnValueCommand command)
{
    Debug.Log($"TestReturnValueCommand handler. Command value: {command.Value}");
    return command.Value.ToString();
}
```

#### Commands subscription
Similiar to events. Subscribing two handlers to one command will throw an error. When command return a value you have to specify its type as second genering parameter.
```
private void AttachCommands()
{
    Mediator.AttachCommandHandler<TestCommand>(EmptyCommandHandler);
    //Mediator.DetachCommandHandler<TestCommand>(TestCommandHandler); <- this will throw an error
    
    Mediator.AttachCommandHandler<TestReturnValueCommand, string>(TestReturnValueCommandHandler);
    //Mediator.DetachCommandHandler<TestReturnValueCommand, string>(EmptyCommandHandlerWithReturnValue); <- this will throw an error
}

private void DetachCommands()
{
    Mediator.DetachCommandHandler<TestCommand>(EmptyCommandHandler);
    
    Mediator.DetachCommandHandler<TestReturnValueCommand, string>(TestReturnValueCommandHandler);
}
```

#### Command sending
Before sending a command you need to create new instance of it and pass it in **Mediator.SendCommand** method. If you want to return a value from command use method with **out** parameter
```
private void SendCommand()
{
    Mediator.SendCommand(new TestCommand(9));
}

private void SendValueCommand()
{
    Mediator.SendCommand(new TestReturnValueCommand(999), out string value);
    Debug.Log($"TestReturnValueCommand returned {value}");
}
```
