using FantasyInformator.State.Abstractions;
using FantasyInformator.State.Store;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Behaviors;

/// <summary>
/// Runs around every action. For an <see cref="ISpinnerAction"/>, it starts a spinner before the handler runs and ends
/// it when the handler is done, also when the handler throws. Other actions pass straight through.
/// </summary>
public sealed class SpinnerBehavior<TAction, TResponse>(ISender<ClientPipeline> sender)
    : IPipelineBehavior<TAction, TResponse>
    where TAction : IAction
{
    public async Task<TResponse> Handle(
        TAction request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ISpinnerAction spinnerAction)
        {
            return await next(cancellationToken);
        }

        var spinnerId = Guid.NewGuid();
        await sender.Send(
            new SpinnerState.SpinnerStartAction(spinnerId, spinnerAction.SpinnerMessage),
            cancellationToken);

        try
        {
            return await next(cancellationToken);
        }
        finally
        {
            await sender.Send(new SpinnerState.SpinnerEndAction(spinnerId), CancellationToken.None);
        }
    }
}
