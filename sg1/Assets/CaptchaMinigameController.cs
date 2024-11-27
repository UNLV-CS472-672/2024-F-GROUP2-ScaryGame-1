using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class CaptchaMinigameController : MonoBehaviour, IMiniGame
{
    public GameObject CaptchaMinigameCanvas;
    public GameObject FailCanvas;
    public GameObject WinCanvas;
    public GameObject button_verify;

    private string[] button_names = {"c_button1",
                                        "c_button2",
                                        "c_button3",
                                        "c_button4",
                                        "c_button5",
                                        "c_button6",
                                        "c_button7",
                                        "c_button8",
                                        "c_button9"};

    private string[] alien_sprite_names = {"CaptchaSprites/alien1",
                                            "CaptchaSprites/alien2",
                                            "CaptchaSprites/alien3",
                                            "CaptchaSprites/alien4",
                                            "CaptchaSprites/alien5",
                                            "CaptchaSprites/alien6",
                                            "CaptchaSprites/alien7",
                                            "CaptchaSprites/alien8",
                                            "CaptchaSprites/alien9"};

    private string[] ghost_sprite_names = {"CaptchaSprites/ghost1",
                                            "CaptchaSprites/ghost2",
                                            "CaptchaSprites/ghost3",
                                            "CaptchaSprites/ghost4",
                                            "CaptchaSprites/ghost5",
                                            "CaptchaSprites/ghost6",
                                            "CaptchaSprites/ghost7",
                                            "CaptchaSprites/ghost8",
                                            "CaptchaSprites/ghost9"};

    private GameObject[] o_button = new GameObject[9];
    private Button[] b_button = new Button[9];

    private int aliens_in_captcha = 0;
    int alien_idx = 0;
    private bool[] idx_is_alien = new bool[9];

    private Sprite[] alien_sprites = new Sprite[9];
    private Sprite[] ghost_sprites = new Sprite[9];
    private Sprite border_sprite;

    private List<int> alien_indexes = new List<int>();
    private List<int> ghost_indexes = new List<int>();
    private List<int> alien_position_indexes = new List<int>();
    private float fade_seconds = 2.8f;
    bool stop_coroutine = false;
    bool first_exec = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupGame()
    {
        // You know what, I'm not even going to write actual documentation for this because nobody wants to read this. This is horrifying
        alien_indexes.Clear();
        ghost_indexes.Clear();
        alien_position_indexes.Clear();
        alien_idx = 0;
        aliens_in_captcha = 3;
        stop_coroutine = false;
        button_verify.GetComponent<Button>().enabled = true;
        for (int i = 0; i < 9; i++)
        {
            o_button[i] = GameObject.Find(button_names[i]);
            o_button[i].GetComponent<Image>().color = new Vector4(1f,1f,1f,1f);
            b_button[i] = o_button[i].GetComponent<Button>();
            b_button[i].enabled = true;
            int button_idx = i;
            if (first_exec == true) b_button[i].onClick.AddListener(() => {HandleButtonClick(button_idx);});
            button_verify.GetComponent<Button>().onClick.AddListener(HandleVerifyClick);

            idx_is_alien[i] = false;
            alien_indexes.Add(i);
            ghost_indexes.Add(i);
        }
        for(int i = 0; i < 9; i++)
        {
            int alien_randidx = Random.Range(0,alien_indexes.Count);
            int ghost_randidx = Random.Range(0,ghost_indexes.Count);
            if (i < 3)
            {
                alien_position_indexes.Add(ghost_indexes[ghost_randidx]);
                idx_is_alien[ghost_indexes[ghost_randidx]] = true;
            }
            alien_sprites[i] = Resources.Load<Sprite>(alien_sprite_names[alien_indexes[alien_randidx]]);
            ghost_sprites[i] = Resources.Load<Sprite>(ghost_sprite_names[ghost_indexes[ghost_randidx]]);
            alien_indexes.RemoveAt(alien_randidx);
            ghost_indexes.RemoveAt(ghost_randidx);

            o_button[i].GetComponent<Image>().sprite = ghost_sprites[i];
        }
        for (int i = 0; i < 3; i++)
        {
            o_button[alien_position_indexes[i]].GetComponent<Image>().sprite = alien_sprites[i];
            alien_idx++;
        }
        first_exec = false;


        if (alien_indexes[0] == 55)
        {
            CompleteMiniGame();
        }
    }

    private void HandleButtonClick(int b_idx)
    {
        if (idx_is_alien[b_idx] == false)
        {
            StartCoroutine(LoseGame());
        }
        else
        {
            if (aliens_in_captcha > 1)
            {
                StartCoroutine(SwapButtonImageAtIdx(b_idx, false));
                aliens_in_captcha--;
            }
            else
            {
                StartCoroutine(SwapButtonImageAtIdx(b_idx, true));
            }
        }
    }
    private void HandleVerifyClick()
    {
        Debug.Log("You have clicked the button!");
        button_verify.GetComponent<Button>().enabled = false;
        if (aliens_in_captcha == 0)
        {
            StartCoroutine(WinGame());
        }
        else
        {
            StartCoroutine(LoseGame());
        }
    }

    private IEnumerator SwapButtonImageAtIdx(int b_idx, bool one_left)
    {
        b_button[b_idx].enabled = false;
        float yield_wait_time = fade_seconds / 100f;
        for (int i = 0; i < 100; i++)
        {
            if (stop_coroutine) yield break;
            o_button[b_idx].GetComponent<Image>().color = new Vector4(1f,1f,1f,(1f-(float)i/100f));
            yield return new WaitForSeconds(yield_wait_time);
        }

        if (one_left)
        {
            if (alien_idx < 8)
            {
                o_button[b_idx].GetComponent<Image>().sprite = alien_sprites[alien_idx];
                alien_idx++;
            }
            else
            {
                o_button[b_idx].GetComponent<Image>().sprite = ghost_sprites[b_idx];
                idx_is_alien[b_idx] = false;
                aliens_in_captcha--;

            }
        }
        else
        {
            o_button[b_idx].GetComponent<Image>().sprite = ghost_sprites[b_idx];
            idx_is_alien[b_idx] = false;

        }
        for (int i = 0; i < 100; i++)
        {
            if (stop_coroutine) yield break;
            o_button[b_idx].GetComponent<Image>().color = new Vector4(1f,1f,1f,(float)i/100f);
            yield return new WaitForSeconds(yield_wait_time);
        }
        b_button[b_idx].enabled = true;

    }
    private IEnumerator WinGame()
    {
        yield return new WaitForSeconds(0.2f);
        CaptchaMinigameCanvas.SetActive(false);
        FailCanvas.SetActive(false);
        WinCanvas.SetActive(true);
        CompleteMiniGame();
    }
    
    private IEnumerator LoseGame()
    {
        stop_coroutine = true;
        yield return new WaitForSeconds(0.2f);
        CaptchaMinigameCanvas.SetActive(false);
        FailCanvas.SetActive(true);
        WinCanvas.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        CaptchaMinigameCanvas.SetActive(true);
        FailCanvas.SetActive(false);
        WinCanvas.SetActive(false);
        SetupGame();
    }

    public void CompleteMiniGame()
    {
        MinigameManager.instance.MiniGameCompleted();
    }
}
