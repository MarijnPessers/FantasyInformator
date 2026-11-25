using FantasyInformator.State.Store;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.App.Components.Layout;

public partial class MainLayout(ISender<ClientPipeline> sender)
{
    protected override async Task OnInitializedAsync() =>
        await LoadOwnersAsync();

    private async Task LoadOwnersAsync() =>
        await sender.Send(new OwnerState.LoadAllOwnersAction(), CancellationToken.None);
}
