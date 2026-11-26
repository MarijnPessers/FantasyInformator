using FantasyInformator.App.Resources;
using FantasyInformator.State.Enums;
using FantasyInformator.State.Store;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.App.Components.Layout;

public partial class MainLayout(ISender<ClientPipeline> sender, IStore store)
{
    private bool IsDatabaseAwake => DatabaseState.DatabaseWakeUpStatus == DatabaseWakeUpStatus.Awake;

    private bool IsDatabaseUnavailable => DatabaseState.DatabaseWakeUpStatus == DatabaseWakeUpStatus.Unavailable;

    private DatabaseState DatabaseState => store.GetState<DatabaseState>();

    protected override async Task OnInitializedAsync()
    {
        await DatabaseWakeUpAsync();

        if (IsDatabaseAwake)
        {
            await LoadAllOwnersAsync();
        }
    }

    private async Task DatabaseWakeUpAsync()
    {
        await sender.Send(new SpinnerState.SpinnerStartAction(Texts.ConnectingToDatabase), CancellationToken.None);
        await sender.Send(new DatabaseState.DatabaseWakeUpAction(), CancellationToken.None);
    }

    private async Task LoadAllOwnersAsync() =>
        await sender.Send(new OwnerState.LoadAllOwnersAction(), CancellationToken.None);
}
