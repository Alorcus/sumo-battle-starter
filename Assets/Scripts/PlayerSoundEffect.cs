using UnityEngine;
using SpeechIO;
public class PlayerSoundEffect : MonoBehaviour
{
    public AudioClip dropInClip;
    public AudioClip gameOverClip;
    public AudioClip collisionClip;

    public AudioClip islandBorderClip;
    public float maxPitch = 1.2f;
    public float minPitch = 0.8f;
    private GameObject previousEnemy;
    private AudioSource audioSource;
    public SpeechOut speechOut = new SpeechOut();
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public float PlayerFellDown()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(gameOverClip);
        return gameOverClip.length;
    }
    public void PlayHit()
    {
        PlayClipPitched(collisionClip, minPitch, maxPitch);
    }

    public void PlayBorder(float distance)
    {

        //PlayClipPitched(islandBorderClip, minPitch, maxPitch);
        audioSource.volume = distance > 6.7f ? 1 : (distance / 6.7f);
        audioSource.PlayOneShot(islandBorderClip);

    }
    public void PlayDropIn()
    {
        audioSource.PlayOneShot(dropInClip);
    }
    public void PlayEnemyHitClip(AudioClip clip, GameObject go = null)
    {
        if (go)
        {
            if (previousEnemy)
            {
                if (go.Equals(previousEnemy))
                    return;
            }
            previousEnemy = go;
            sayName(go.GetComponent<Enemy>());


        }
        audioSource.clip = clip;
        audioSource.Play();

    }
    private async void sayName(Enemy e)
    {
        speechOut.Stop();
        await speechOut.Speak("I was hit by");
        await speechOut.Speak(e.displayName);
    }


    //TODO: implement async method sayName

    public void StopPlayback()
    {
        audioSource.Stop();
    }

    public void PlayClipPitched(AudioClip clip, float minPitch, float maxPitch)
    {
        // little trick to make clip sound less redundant
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        // plays same clip only once, this way no overlapping
        audioSource.PlayOneShot(clip);
        audioSource.pitch = 1f;
    }

}