using FantasyInformator.Shared.Progress;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public partial class ProgressReportState
{
    public record AddProgressReportAction(ProgressReport ProgressReport) : IAction;

    public class AddProgressReportHandler(IStore store) : ActionHandler<AddProgressReportAction>
    {
        private ProgressReportState ProgressReportState => store.GetState<ProgressReportState>();

        public override ValueTask Handle(AddProgressReportAction request, CancellationToken cancellationToken)
        {
            ProgressReportState.ProgressReports.Add(request.ProgressReport);

            return ValueTask.CompletedTask;
        }
    }
}
