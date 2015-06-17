using System;
using NServiceBus;

public class UpdateProjectScoreHandler : IHandleMessages<UpdateProjectScore>
{
    IBus bus;

    public UpdateProjectScoreHandler(IBus bus)
    {
        
        this.bus = bus;
    }

    public void Handle(UpdateProjectScore message)
    {
        //Update the database
        Console.WriteLine("Updating project score in the db...");
    }
}

public class UpdateProjectScore : ICommand
{
    public Guid Id { get; set; }
    public int Red { get; set; }
    public int Yellow { get; set; }
    public int Green { get; set; }
}