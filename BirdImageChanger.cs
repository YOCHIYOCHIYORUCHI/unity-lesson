using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BirdImageChanger : MonoBehaviour
{
    public Image birdImage;
    public Sprite birdNormal;  // 通常状態の画像
    public Sprite birdGohan;
    public Sprite birdSuimin;
    public Sprite birdOshare;
    public Sprite birdOfuro;
    public Sprite birdDie;     // 追加: 死亡状態の画像

    private Coroutine changeImageCoroutine;  // 現在のコルーチンの参照

    private void Start()
    {
        // 初期状態で通常の画像を設定
        birdImage.sprite = birdNormal;
        Debug.Log("Initial bird image set to birdNormal.");
    }

    public void ChangeImage(string birdState)
    {
        if (changeImageCoroutine != null)
        {
            StopCoroutine(changeImageCoroutine);  // 既存のコルーチンを停止
        }

        Debug.Log("ChangeImage called with state: " + birdState);

        Sprite newImage = birdNormal;

        switch (birdState)
        {
            case "gohan":
                newImage = birdGohan;
                Debug.Log("Bird state changed to gohan.");
                break;
            case "suimin":
                newImage = birdSuimin;
                Debug.Log("Bird state changed to suimin.");
                break;
            case "oshare":
                newImage = birdOshare;
                Debug.Log("Bird state changed to oshare.");
                break;
            case "ofuro":
                newImage = birdOfuro;
                Debug.Log("Bird state changed to ofuro.");
                break;
            default:
                Debug.Log("Unknown bird state: " + birdState);
                break;
        }

        changeImageCoroutine = StartCoroutine(ChangeImageCoroutine(newImage));
    }

    private IEnumerator ChangeImageCoroutine(Sprite newImage)
    {
        Debug.Log("Starting image change coroutine.");
        birdImage.sprite = newImage;
        Debug.Log("Image changed to: " + newImage.name);
        yield return new WaitForSeconds(0.2f);
        birdImage.sprite = birdNormal;
        Debug.Log("Image reverted to birdNormal.");
    }

    // 即座にゲームオーバー用画像を設定するメソッドを追加
    public void SetDieImage()
    {
        if (changeImageCoroutine != null)
        {
            StopCoroutine(changeImageCoroutine);  // 現在のコルーチンを停止
        }
        birdImage.sprite = birdDie;
        Debug.Log("Bird image changed to die.");
    }
}
