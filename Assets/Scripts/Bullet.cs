using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float life = 3;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
