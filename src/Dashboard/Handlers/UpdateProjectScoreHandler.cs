using System;
using System.Threading.Tasks;
using Dashboard;
using Dashboard.ReadModel.Providers;
using NServiceBus;
using StructureMap;

public class UpdateProjectScoreHandler : IHandleMessages<UpdateProjectScore>
{
    private readonly IRAGWidgetViewProvider _ragWidgetViewProvider;
    IBus bus;
        
    public UpdateProjectScoreHandler(IBus bus)
        : this(bus, ObjectFactory.GetInstance<IRAGWidgetViewProvider>())
    {
        this.bus = bus;
    }

    public UpdateProjectScoreHandler(IBus bus, IRAGWidgetViewProvider ragWidgetViewProvider)
    {;
        _ragWidgetViewProvider = ragWidgetViewProvider;
    }

    public void Handle(UpdateProjectScore command)
    {
        HandleAsync(command).Wait();
    }

    private async Task HandleAsync(UpdateProjectScore command)
    {
        new MongoDbModifier(_ragWidgetViewProvider).UpdateDatabase(command);
    }
}

public class UpdateProjectScore : ICommand
{
    public Guid Id { get; set; }
    public int Red { get; set; }
    public int Yellow { get; set; }
    public int Green { get; set; }
}