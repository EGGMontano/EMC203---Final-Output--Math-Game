using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathDoor : MonoBehaviour
{
    //Kill player

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Player died");
            SceneManager.LoadScene("DeathScene");      //Change scene
        }
    }
}
