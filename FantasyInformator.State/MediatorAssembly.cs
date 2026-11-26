using FantasyInformator.State.Behaviors;
using TimeWarp.Mediator;
using TimeWarp.State;

[assembly: MediatorAssembly]
[assembly: MediatorScope(typeof(ClientPipeline))]
[assembly: MediatorBehavior(typeof(TraceBehavior<,>), Scope = typeof(ClientPipeline))]
[assembly: MediatorBehavior(typeof(SpinnerBehavior<,>), order: 500, Scope = typeof(ClientPipeline))]
