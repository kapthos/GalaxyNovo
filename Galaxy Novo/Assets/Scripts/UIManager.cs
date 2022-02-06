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
    [SerializeField] private Slider heavyShotBar;
    [SerializeField] public Slider _turboBar;

    private float maxHeavyHold = 0.56f;
    private float currentHeavyHold;
    private float _totalTimePressed;

    private float _maxTurboAmount = 1.0f;
    [SerializeField] public float _currentTurboAmount;
    private float _timeHolding;


    //References
    private GameManager gm;
    private PlayerShots ps;
    private Player pl;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ps = GameObject.Find("Player").GetComponent<PlayerShots>();
        pl = GameObject.Find("Player").GetComponent<Player>();

        if (gm == null)
        {
            Debug.Log("GameManager is NULL");
        }

        currentHeavyHold = 0;
        heavyShotBar.maxValue = maxHeavyHold;
        heavyShotBar.value = 0;
        _turboBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameOverImg();
        HeavyShotBarCharge();
        TurboBarCharge();
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
    public void HeavyShotBarCharge()
    {
        if (ps.canHeavyShot == true)
        {
            currentHeavyHold = _totalTimePressed;
            heavyShotBar.value = currentHeavyHold;

            if (Input.GetKey(KeyCode.Space))
            {
                _totalTimePressed += Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                heavyShotBar.value = currentHeavyHold;
                _totalTimePressed = 0;
            }
        }
    }
    public void TurboBarCharge()
    {
        if(pl._extraTurbo < 3)
        {
            _turboBar.value = _maxTurboAmount;
            _currentTurboAmount = _timeHolding;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _timeHolding += Time.deltaTime;
                Debug.Log(_currentTurboAmount);
                _turboBar.value -= _currentTurboAmount;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _timeHolding = 0;
            }
        }
    }
}
