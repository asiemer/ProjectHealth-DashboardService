using System;
using Dashboard;
using NServiceBus;

public class UpdateProjectScoreHandler : IHandleMessages<UpdateProjectScore>
{
    private readonly IMongoDbWriter _mongoDbWriter;

    public UpdateProjectScoreHandler(IMongoDbWriter mongoDbWriter)
    {
        _mongoDbWriter = mongoDbWriter;
    }

    public void Handle(UpdateProjectScore command)
    {
        _mongoDbWriter.AddOrUpdate(command).Wait();
    }
}

public class UpdateProjectScore : ICommand
{
    public Guid Id { get; set; }
    public int Red { get; set; }
    public int Yellow { get; set; }
    public int Green { get; set; }
}