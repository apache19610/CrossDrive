using UnityEngine;
using UnityEngine.UI;

public class CheckMaps : MonoBehaviour
{
    private BuyMapCoins mapCoins;
    public Image[] maps;
    public Sprite selected, notSelected;

    private void Start()
    {
       
        whichMapSelected();
        mapCoins = GetComponent<BuyMapCoins>();
        if (PlayerPrefs.GetString("City") == "Open")
        {
            mapCoins.Buy1000CoinsButton.SetActive(false);
            mapCoins.Buy1000CoinsMoneyButton.SetActive(false);
            mapCoins.CheckCityButton.SetActive(true);
        }
        if (PlayerPrefs.GetString("Megapolis") == "Open")
        {
            mapCoins.Buy5000CoinsButton.SetActive(false);
            mapCoins.Buy5000CoinsMoneyButton.SetActive(false);
            mapCoins.CheckMegapolisButton.SetActive(true);
        }
    }

    public void whichMapSelected()
    {
        switch(PlayerPrefs.GetInt("NowMap"))
        {
            case 2:
                maps[0].sprite = notSelected;
                maps[1].sprite = selected;
                maps[2].sprite = notSelected;
                break;

            case 3:
                maps[0].sprite = notSelected;
                maps[1].sprite = notSelected;
                maps[2].sprite = selected;
                break;
            default:
                maps[0].sprite = selected;
                maps[1].sprite = notSelected;
                maps[2].sprite = notSelected;
                break;
        }
    }

}
