using System;
using Dashboard.Infrastructure;
using Dashboard.ReadModel.Views;
using NServiceBus;
using StructureMap;

public class UpdateProjectScoreHandler : IHandleMessages<UpdateProjectScore>
{
    private readonly IProjectionWriter<RAGWidgetView> _commandWriter;
    IBus bus;

    public UpdateProjectScoreHandler(IBus bus)
        : this(bus, ObjectFactory.GetInstance<IProjectionWriter<RAGWidgetView>>())
    {
        this.bus = bus;
    }

    public UpdateProjectScoreHandler(IBus bus, IProjectionWriter<RAGWidgetView> commandWriter)
    {
        _commandWriter = commandWriter;
    }

    public void Handle(UpdateProjectScore command)
    {
        UpdateDatabase(command);
    }

    private async void UpdateDatabase(UpdateProjectScore command)
    {
        var id = new Guid("8f0c2009-ebc6-40e5-9130-989ae03bfc98");
        await _commandWriter.Update(id, x =>
        {
            x.Green = command.Green;
            x.Yellow = command.Yellow;
            x.Red = command.Red;
        });
    }
}

public class UpdateProjectScore : ICommand
{
    public Guid Id { get; set; }
    public int Red { get; set; }
    public int Yellow { get; set; }
    public int Green { get; set; }
}