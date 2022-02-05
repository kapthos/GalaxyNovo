using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text _goldTxt;
    [SerializeField] private Text _turboTxt;
    [SerializeField] Sprite[] _livesSprite;
    [SerializeField] Image _LivesImg;
    [SerializeField] Text _GameOverTxt;
    [SerializeField] Text _ContinueTxt;
    private GameManager gm;
    private Player pl;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pl = GameObject.Find("Player").GetComponent<Player>();

        if (gm == null)
        {
            Debug.Log("GameManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameOverImg();
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _livesSprite[currentLives];
    }

    public void GameOverImg()
    {
        if (gm._isGameOver == true)
        {
            _GameOverTxt.gameObject.SetActive(true);
            _ContinueTxt.gameObject.SetActive(true);
        }
    }

    public void UpdateScore(int pScore)
    {
        scoreTxt.text = "Score: " + pScore;
    }
    public void UpdateGold(int pGold)
    {
        _goldTxt.text = "Gold: " + pGold;
    }

    public void UpdateTurbo(int pTurbo)
    {
        _turboTxt.text = "Turbo: " + pTurbo;
    }
}
