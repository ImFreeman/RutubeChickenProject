using Features.Player;
using TMPro;
using UnityEngine;
using Zenject;

public class UIHud : UIWindow
{   
    [SerializeField] private TMP_Text scoreValueText;    

    private SignalBus _signalBus;    

    [Inject]
    public void Inject(
        SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<ScoreChangeSignal>(ScoreChangeSignalHandler);
    }

    public override void Hide()
    {
        
    }

    public override void Show()
    {
        
    }

    private void ScoreChangeSignalHandler(ScoreChangeSignal signal)
    {
        scoreValueText.text = signal.CurrentValue.ToString();
    }
}
