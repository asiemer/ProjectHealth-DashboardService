using System;
using System.Threading.Tasks;
using Dashboard;
using Dashboard.Infrastructure;
using Dashboard.ReadModel.Providers;
using Dashboard.ReadModel.Views;
using NServiceBus;
using StructureMap;

public class UpdateProjectScoreHandler : IHandleMessages<UpdateProjectScore>
{
    private readonly IProjectionWriter<RAGWidgetView> _commandWriter;
    private readonly IRAGWidgetViewProvider _ragWidgetViewProvider;
    IBus bus;
        
    public UpdateProjectScoreHandler(IBus bus)
        : this(bus, ObjectFactory.GetInstance<IProjectionWriter<RAGWidgetView>>(), ObjectFactory.GetInstance<IRAGWidgetViewProvider>())
    {
        this.bus = bus;
    }

    public UpdateProjectScoreHandler(IBus bus, IProjectionWriter<RAGWidgetView> commandWriter, IRAGWidgetViewProvider ragWidgetViewProvider)
    {
        _commandWriter = commandWriter;
        _ragWidgetViewProvider = ragWidgetViewProvider;
    }

    public void Handle(UpdateProjectScore command)
    {
        HandleAsync(command).Wait();
    }

    private async Task HandleAsync(UpdateProjectScore command)
    {
        UpdateDatabase(command);
    }

    private async void UpdateDatabase(UpdateProjectScore command)
    {
        var dbitem = await CheckIfRAGWidgetExists(command.Id);
        var exists = (dbitem != null);

        var ragWidgetView = new RAGWidgetView
        {
            Green = command.Green,
            Yellow = command.Yellow,
            Red = command.Red,
        };
        
        if (exists)
        {
            new Test().UpdateRecord(command);
        }
        else
        {
            new Test().CreateRecord(command, ragWidgetView);
        }
    }



    private async Task<RAGWidgetView> CheckIfRAGWidgetExists(Guid id)
    {
        RAGWidgetView dbItem =  await _ragWidgetViewProvider.GetById(id);
        return dbItem;
    }
}

public class UpdateProjectScore : ICommand
{
    public Guid Id { get; set; }
    public int Red { get; set; }
    public int Yellow { get; set; }
    public int Green { get; set; }
}