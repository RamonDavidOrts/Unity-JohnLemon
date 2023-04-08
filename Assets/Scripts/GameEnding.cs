using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField] float fadeDuration = 1f;
    [SerializeField] float displayImageDuration = 1f;
    [SerializeField] GameObject player;
    [SerializeField] CanvasGroup exitBackgroundImageCanvasGroup;
    [SerializeField] CanvasGroup caughtBackgroundImageCanvasGroup;
    [SerializeField] AudioSource exitAudio;
    [SerializeField] AudioSource caughtAudio;

    bool isPlayerAtExit;
    bool isPlayerCaught;
    float timer;
    bool hasAudioPlayed;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }

    private void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }

        timer += Time.deltaTime;

        imageCanvasGroup.alpha = timer / fadeDuration;

        if (timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }

}
