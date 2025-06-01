using Btg.BrownianMotion.Core.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;

namespace Btg.BrownianMotion.UI.Views.Charts;

public class Model(double y)
{
    public double X { get; set; }
    public double Y { get; set; } = y;
}

public class LinearChartPageViewModel : ViewModelBase
{
    private readonly IBrownianMotionService _brownianMotionService;

    public LinearChartPageViewModel(IBrownianMotionService brownianMotionService)
    {
        _brownianMotionService = brownianMotionService;
        Sigma = new ReactiveProperty<double>().AddTo(Disposables);
        Mean = new ReactiveProperty<double>().AddTo(Disposables);
        InitialPrice = new ReactiveProperty<double>().AddTo(Disposables);
        NumberOfDays = new ReactiveProperty<int>().AddTo(Disposables);
        Values = new ReactiveProperty<double[]>().AddTo(Disposables);

        var canGenerateSimulation = Sigma.CombineLatest(Mean)
                                         .CombineLatest(InitialPrice)
                                         .CombineLatest(NumberOfDays)
                                         .Select(_ => Sigma.Value != 0 &&
                                                      Mean.Value != 0 &&
                                                      InitialPrice.Value != 0 &&
                                                      NumberOfDays.Value != 0);

        GenerateSimulationCommand = new ReactiveCommand(canGenerateSimulation)
            .WithSubscribe(GenerateSimulation)
            .AddTo(Disposables);
    }

    public ReactiveProperty<double[]> Values { get; }
    public ReactiveProperty<double> Sigma { get; }
    public ReactiveProperty<double> Mean { get; }
    public ReactiveProperty<double> InitialPrice { get; }
    public ReactiveProperty<int> NumberOfDays { get; }
    public ReactiveCommand GenerateSimulationCommand { get; set; }

    private void GenerateSimulation()
    {
        Values.Value = _brownianMotionService.GenerateBrownianMotion(Sigma.Value, Mean.Value, InitialPrice.Value, NumberOfDays.Value);
    }
}