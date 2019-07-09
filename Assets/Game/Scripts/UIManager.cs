using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite[] potionImages;

    //public Sprite[] levelImages;

    public Image PotionDisplay;
	public AudioSource flag_sound;

    public GameObject titleScreen;
    public GameObject startText;
    void Start ()
	{
		AudioSource[] audios = GetComponents<AudioSource> ();
		flag_sound = audios [0];
	}

    public void UpdatePotions(int potions){

        PotionDisplay.sprite = potionImages[potions];

    }

    public void EndTitle(){
        titleScreen.SetActive(true);
		flag_sound.Play ();
    }

    public void HideTitle(){
        titleScreen.SetActive(false);
        startText.SetActive(false);
    }
}