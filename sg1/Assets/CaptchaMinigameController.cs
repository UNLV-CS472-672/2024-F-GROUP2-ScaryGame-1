using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CaptchaMinigameController : MonoBehaviour
{
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

    private List<int> alien_indexes = new List<int>();
    private List<int> ghost_indexes = new List<int>();
    private List<int> alien_position_indexes = new List<int>();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // You know what, I'm not even going to write actual documentation for this because nobody wants to read this. This is horrifying
        for (int i = 0; i < 9; i++)
        {
            o_button[i] = GameObject.Find(button_names[i]);
            b_button[i] = o_button[i].GetComponent<Button>();
            b_button[i].onClick.AddListener(delegate{HandleButtonClick(i);});

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

        aliens_in_captcha = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleButtonClick(int b_idx)
    {
        return;
    }
}
