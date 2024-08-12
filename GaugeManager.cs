using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GaugeManager : MonoBehaviour
{
    public Image gaugeOshare;
    public Image gaugeGohan;
    public Image gaugeSuimin;
    public Image gaugeOfuro;
    public BirdImageChanger birdImageChanger;  // BirdImageChanger�̎Q�Ƃ�ǉ�
    public Image dieMessage;  // ���S���b�Z�[�W��Image
    public GameObject[] buttonObjects; // �{�^���I�u�W�F�N�g���i�[����z��
    public ParticleSystem dieParticles; // �ǉ�: ���S���̃p�[�e�B�N���V�X�e��
    public AudioSource bgmSource; // BGM�p��AudioSource
    public AudioSource seSource; // SE�p��AudioSource
    public AudioClip deathSE; // �Q�[���I�[�o�[����SE�N���b�v

    private float oshareValue = 100;
    private float gohanValue = 100;
    private float suiminValue = 100;
    private float ofuroValue = 100;

    private float oshareDecreaseRate = 10f;
    private float gohanDecreaseRate = 25f;
    private float suiminDecreaseRate = 20f;
    private float ofuroDecreaseRate = 15f;

    private bool isGameOver = false;  // �Q�[���I�[�o�[��Ԃ�ǐ�

    private void Start()
    {
        dieMessage.gameObject.SetActive(false);  // ������ԂŃQ�[���I�[�o�[�p���b�Z�[�W���\���ɂ���
        bgmSource.Play(); // BGM���Đ�
        StartCoroutine(DecreaseGauge());
    }

    private IEnumerator DecreaseGauge()
    {
        while (!isGameOver)
        {
            oshareValue = Mathf.Clamp(oshareValue - oshareDecreaseRate * Time.deltaTime, 0, 100);
            gohanValue = Mathf.Clamp(gohanValue - gohanDecreaseRate * Time.deltaTime, 0, 100);
            suiminValue = Mathf.Clamp(suiminValue - suiminDecreaseRate * Time.deltaTime, 0, 100);
            ofuroValue = Mathf.Clamp(ofuroValue - ofuroDecreaseRate * Time.deltaTime, 0, 100);

            UpdateGaugeUI();

            // �Q�[���I�[�o�[�̃`�F�b�N
            if (oshareValue == 0 || gohanValue == 0 || suiminValue == 0 || ofuroValue == 0)
            {
                GameOver();
                yield break;
            }

            yield return null;
        }
    }

    private void UpdateGaugeUI()
    {
        gaugeOshare.fillAmount = oshareValue / 100;
        gaugeGohan.fillAmount = gohanValue / 100;
        gaugeSuimin.fillAmount = suiminValue / 100;
        gaugeOfuro.fillAmount = ofuroValue / 100;
    }

    public void RecoverGauge(string gaugeType)
    {
        if (isGameOver) return;  // �Q�[���I�[�o�[���͉񕜂ł��Ȃ�

        switch (gaugeType)
        {
            case "oshare":
                oshareValue = Mathf.Clamp(oshareValue + 10, 0, 100);
                break;
            case "gohan":
                gohanValue = Mathf.Clamp(gohanValue + 10, 0, 100);
                break;
            case "suimin":
                suiminValue = Mathf.Clamp(suiminValue + 10, 0, 100);
                break;
            case "ofuro":
                ofuroValue = Mathf.Clamp(ofuroValue + 10, 0, 100);
                break;
        }

        UpdateGaugeUI();
    }

    private void GameOver()
    {
        isGameOver = true;
        birdImageChanger.SetDieImage(); // �����ɃQ�[���I�[�o�[�p�摜��ݒ肷�郁�\�b�h���Ăяo��
        dieMessage.gameObject.SetActive(true);

        // �S�Ẵ{�^���I�u�W�F�N�g��BoxCollider2D�𖳌���
        foreach (GameObject button in buttonObjects)
        {
            button.GetComponent<BoxCollider2D>().enabled = false;
        }

        // ���S���̃p�[�e�B�N�����Đ�
        if (dieParticles != null)
        {
            dieParticles.Play();
        }

        // ���S����SE���Đ�
        if (seSource != null && deathSE != null)
        {
            seSource.PlayOneShot(deathSE);
        }

        Debug.Log("Game Over");
    }
}
