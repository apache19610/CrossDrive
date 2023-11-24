using UnityEngine;
using UnityEngine.UI;

public class BuyMapCoins : MonoBehaviour
{
    public AudioClip success, fail;
    public GameObject Buy1000CoinsButton, Buy5000CoinsButton, Buy1000CoinsMoneyButton, Buy5000CoinsMoneyButton, CheckCityButton, CheckMegapolisButton;
    public Animation coinsText;
    public Text coinsCount;
    public int nowCoins = 0;

   public void BuyNewMap(int needCoins)
    {

        int coins = PlayerPrefs.GetInt("Coins");
        if (coins < needCoins)
        {
            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = fail;
                GetComponent<AudioSource>().Play();
            }
            coinsText.Play();
        }
        else
        {
            //Buy map
            switch(needCoins)
            {
                case 1000:
                    PlayerPrefs.SetString("City", "Open");
                    PlayerPrefs.SetInt("NowMap", 2);
                    GetComponent<CheckMaps>().whichMapSelected();
                    Buy1000CoinsButton.SetActive(false);
                    Buy1000CoinsMoneyButton.SetActive(false);
                    CheckCityButton.SetActive(true);
                    break;
                case 5000:
                    PlayerPrefs.SetString("Megapolis", "Open");
                    PlayerPrefs.SetInt("NowMap", 3);
                    GetComponent<CheckMaps>().whichMapSelected();
                    Buy5000CoinsButton.SetActive(false);
                    Buy5000CoinsMoneyButton.SetActive(false);
                    CheckMegapolisButton.SetActive(true);
                    break;
            }
            nowCoins = coins - needCoins;
            coinsCount.text = nowCoins.ToString();
            PlayerPrefs.SetInt("Coins", nowCoins);

            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = success;
                GetComponent<AudioSource>().Play();
            }
        }
    }

}
