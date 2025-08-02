using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coins : MonoBehaviour
{
    //Make on trigger coin score keeper

    public int Coin = 0;

    public TextMeshProUGUI coinText;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Coin")
        {
            Coin++;
            coinText.text = "Coin: " + Coin.ToString();
            Destroy(other.gameObject);
            //Debug.Log("ChaChing");
        }
    }
}
