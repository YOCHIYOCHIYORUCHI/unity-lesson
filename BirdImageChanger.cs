using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BirdImageChanger : MonoBehaviour
{
    public Image birdImage;
    public Sprite birdNormal;  // �ʏ��Ԃ̉摜
    public Sprite birdGohan;
    public Sprite birdSuimin;
    public Sprite birdOshare;
    public Sprite birdOfuro;
    public Sprite birdDie;     // �ǉ�: ���S��Ԃ̉摜

    private Coroutine changeImageCoroutine;  // ���݂̃R���[�`���̎Q��

    private void Start()
    {
        // ������ԂŒʏ�̉摜��ݒ�
        birdImage.sprite = birdNormal;
        Debug.Log("Initial bird image set to birdNormal.");
    }

    public void ChangeImage(string birdState)
    {
        if (changeImageCoroutine != null)
        {
            StopCoroutine(changeImageCoroutine);  // �����̃R���[�`�����~
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

    // �����ɃQ�[���I�[�o�[�p�摜��ݒ肷�郁�\�b�h��ǉ�
    public void SetDieImage()
    {
        if (changeImageCoroutine != null)
        {
            StopCoroutine(changeImageCoroutine);  // ���݂̃R���[�`�����~
        }
        birdImage.sprite = birdDie;
        Debug.Log("Bird image changed to die.");
    }
}
