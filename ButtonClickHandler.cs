using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{
    public BirdImageChanger birdImageChanger;
    public GaugeManager gaugeManager;

    public void OnButtonClick(string birdState)
    {
        birdImageChanger.ChangeImage(birdState);
        gaugeManager.RecoverGauge(birdState);  // �N���b�N���ɃQ�[�W���񕜂���
    }
}