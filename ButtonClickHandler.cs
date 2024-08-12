using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{
    public BirdImageChanger birdImageChanger;
    public GaugeManager gaugeManager;

    public void OnButtonClick(string birdState)
    {
        birdImageChanger.ChangeImage(birdState);
        gaugeManager.RecoverGauge(birdState);  // クリック時にゲージを回復する
    }
}