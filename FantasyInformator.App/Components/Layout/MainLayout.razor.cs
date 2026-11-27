using FantasyInformator.App.Resources;
using FantasyInformator.State.Enums;
using FantasyInformator.State.Store;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.App.Components.Layout;

public partial class MainLayout(ISender<ClientPipeline> sender, IStore store)
{
    private bool AppReady => DatabaseState.DatabaseWakeUpStatus == DatabaseWakeUpStatus.Awake;

    private DatabaseState DatabaseState => store.GetState<DatabaseState>();

    protected override async Task OnInitializedAsync() =>
        await DatabaseWakeUpAsync();

    private async Task DatabaseWakeUpAsync() =>
        await sender.Send(new DatabaseState.DatabaseWakeUpAction(Texts.ConnectingToDatabase), CancellationToken.None);
}
