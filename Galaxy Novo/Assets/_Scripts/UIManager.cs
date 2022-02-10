using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text _goldTxt;
    [SerializeField] private Text _turboTxt;
    [SerializeField] private Text _heavyTxt;
    [SerializeField] Sprite[] _livesSprite;
    [SerializeField] Image _LivesImg;
    [SerializeField] Text _GameOverTxt;
    [SerializeField] Text _ContinueTxt;
    [SerializeField] Text _interacaoTxt;

    [SerializeField] Image _lojaImg;
    [SerializeField] GameObject loja;

    public Slider heavyShotBar;
    public Slider _turboBar;
    public Slider timeBar;
    AudioSource aplausos;

    private float maxHeavyHold = 0.56f;
    private float currentHeavyHold;
    private float _totalTimePressed;
    private bool lojaUp = false;

    public float _currentTurbo;
    public float _timeHolding;

    //References
    private GameManager gm;
    private PlayerShots ps;
    private Player pl;
    
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ps = GameObject.Find("Player").GetComponent<PlayerShots>();
        pl = GameObject.Find("Player").GetComponent<Player>();

        currentHeavyHold = 0;
        heavyShotBar.maxValue = maxHeavyHold;
        heavyShotBar.value = 0;
        timeBar.value = 0;
        aplausos = GetComponent<AudioSource>();
    }

    void Update()
    {
        GameOverImg();
        HeavyShotBarCharge();
        TurboBarCharge();
        TimeBar();
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
        _goldTxt.text = "Money: " + pGold;
    }
    public void UpdateTurbo(int pTurbo)
    {
        _turboTxt.text = "Turbo: " + pTurbo;
    }
    public void UpdateHeavyCount(int hCount)
    {
        _heavyTxt.text = hCount.ToString();
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
                currentHeavyHold = 0;
                heavyShotBar.value = currentHeavyHold;
                _totalTimePressed = 0;
            }
        }
        else
        {
            heavyShotBar.value = 0;
        }
    }
    public void TurboBarCharge()
    {
        if(pl.canTurbo == true && pl._speedLives > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _timeHolding += Time.deltaTime * 10;
                _turboBar.value = _turboBar.maxValue - _timeHolding;
                _currentTurbo = _turboBar.value;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (_currentTurbo <= 0)
                {
                    _timeHolding = 0;
                    _turboBar.value = _turboBar.maxValue;
                }
            }
        }
        else
        {
            _turboBar.value = 0;
        }
    }
    public void TimeBar()
    {
        timeBar.value += Time.deltaTime;
    }
    public void SpawnLoja()
    {
        if (gm.levelComplete == true)
        {
            if(lojaUp == false)
            {
                Instantiate(loja, new Vector3(0, 2.2f, 0), Quaternion.identity);
                _interacaoTxt.gameObject.SetActive(true);
                lojaUp = true;
            }
        }
    }
    public void ActiveLojaUI()
    {
        AudioSource audioManager = GameObject.FindWithTag("BackGroundSound").GetComponent<AudioSource>();
        _lojaImg.gameObject.SetActive(true);
        _interacaoTxt.gameObject.SetActive(false);
        pl.canMove = false;
        ps.numShotType = 0;
        if (audioManager.isPlaying)
        {
            audioManager.Pause();
        }
        aplausos.Play();
    }
}