using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody enemyRb;
    private GameObject player;

    public AudioClip nameClip;

    public string displayName;
    private PlayerSoundEffect soundEffects;


    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        soundEffects = GetComponent<PlayerSoundEffect>();
    }

    void Update()
    {
        if (transform.position.y < -2)
        {
            GameObject.FindObjectOfType<SpawnManager>().FindOtherEnemy();
            Destroy(gameObject);
        }
    }


    void FixedUpdate()
    {
        /// challenge: set lookDirection to "enemy to player" vector
        Vector3 lookDirection = player.transform.position - transform.position;
        enemyRb.AddForce(lookDirection.normalized * speed);
    }

    void OnCollisionEnter()
    {
        // GameObject other = collision.gameObject;
        // if (other.CompareTag("Player"))
        // {

        //     soundEffects.PlayEnemyHitClip()nameClip, other);
        // }

    }
}
