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
    private readonly IRAGWidgetViewProvider _mongoProvider;
    IBus bus;

    public UpdateProjectScoreHandler(IBus bus)
        : this(bus, ObjectFactory.GetInstance<IProjectionWriter<RAGWidgetView>>(), ObjectFactory.GetInstance<IRAGWidgetViewProvider>())
    {
        this.bus = bus;
    }

    public UpdateProjectScoreHandler(IBus bus, IProjectionWriter<RAGWidgetView> commandWriter, IRAGWidgetViewProvider mongoProvider)
    {
        _commandWriter = commandWriter;
        _mongoProvider = mongoProvider;
    }

    public void Handle(UpdateProjectScore command)
    {
        UpdateDatabase(command);
    }

    private async void UpdateDatabase(UpdateProjectScore command)
    {
        bool viewExists = CheckIfRAGWidgetExists(command.Id);

        if (viewExists)
        {
            await _commandWriter.Update(command.Id, x =>
            {
                x.Green = command.Green;
                x.Yellow = command.Yellow;
                x.Red = command.Red;
            });
        }
        else
        {
            await _commandWriter.Add(command.Id, new RAGWidgetView
            {
                Green = command.Green,
                Yellow = command.Yellow,
                Red = command.Red,
            });
        }
    }

    private bool CheckIfRAGWidgetExists(Guid id)
    {
        Task<RAGWidgetView> dbItem = _mongoProvider.GetById(id);
        return dbItem.Result != null;
    }
}

public class UpdateProjectScore : ICommand
{
    public Guid Id { get; set; }
    public int Red { get; set; }
    public int Yellow { get; set; }
    public int Green { get; set; }
}