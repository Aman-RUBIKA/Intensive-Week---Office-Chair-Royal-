using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public Canvas MainCanvas;
    public GameObject shopPanel, hudPanel, goldPanel;
    public TextMeshProUGUI timerText, goldText;
    public TextMeshProUGUI price1Text, price2Text, price3Text;
    public UnityEngine.UI.Image shopImageLeft, shopImageCenter, shopImageRight;
    public UnityEngine.UI.Button shopLeft, shopCenter, shopRight;
    private void Awake()
    {
        #region Simpleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion Simpleton
    }
    // Start is called before the first frame update
    void Start()
    {
        MainCanvas.enabled = true;
        if (gameManager.instance.currentGameState == gameManager.GAMESTATE.PLAY)
        {
            shopPanel.SetActive(false);
            hudPanel.SetActive(true);
        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
