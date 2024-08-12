using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GaugeManager : MonoBehaviour
{
    public Image gaugeOshare;
    public Image gaugeGohan;
    public Image gaugeSuimin;
    public Image gaugeOfuro;
    public BirdImageChanger birdImageChanger;  // BirdImageChangerの参照を追加
    public Image dieMessage;  // 死亡メッセージのImage
    public GameObject[] buttonObjects; // ボタンオブジェクトを格納する配列
    public ParticleSystem dieParticles; // 追加: 死亡時のパーティクルシステム
    public AudioSource bgmSource; // BGM用のAudioSource
    public AudioSource seSource; // SE用のAudioSource
    public AudioClip deathSE; // ゲームオーバー時のSEクリップ

    private float oshareValue = 100;
    private float gohanValue = 100;
    private float suiminValue = 100;
    private float ofuroValue = 100;

    private float oshareDecreaseRate = 10f;
    private float gohanDecreaseRate = 25f;
    private float suiminDecreaseRate = 20f;
    private float ofuroDecreaseRate = 15f;

    private bool isGameOver = false;  // ゲームオーバー状態を追跡

    private void Start()
    {
        dieMessage.gameObject.SetActive(false);  // 初期状態でゲームオーバー用メッセージを非表示にする
        bgmSource.Play(); // BGMを再生
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

            // ゲームオーバーのチェック
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
        if (isGameOver) return;  // ゲームオーバー時は回復できない

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
        birdImageChanger.SetDieImage(); // 即座にゲームオーバー用画像を設定するメソッドを呼び出す
        dieMessage.gameObject.SetActive(true);

        // 全てのボタンオブジェクトのBoxCollider2Dを無効化
        foreach (GameObject button in buttonObjects)
        {
            button.GetComponent<BoxCollider2D>().enabled = false;
        }

        // 死亡時のパーティクルを再生
        if (dieParticles != null)
        {
            dieParticles.Play();
        }

        // 死亡時のSEを再生
        if (seSource != null && deathSE != null)
        {
            seSource.PlayOneShot(deathSE);
        }

        Debug.Log("Game Over");
    }
}
