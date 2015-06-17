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
        var id = new Guid("cf629fac-0cdb-4eeb-901c-b6e04ca0acc8");
        await _commandWriter.Update(id, x =>
        {
            x.GreenValue = command.Green;
            x.AmberValue = command.Yellow;
            x.RedValue = command.Red;
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