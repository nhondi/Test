using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    #region General Var
    [Header("General")]
    public Achievemnet achievemnet;
    public bool unlocked;
    public float displayTime;

    #endregion

    #region UI
    [Header("UI")]
    [SerializeField] private Sprite unlockIcon;
    //public Color unlockColor;
    //public Color lockColor;


    public Text titleLabel;
    public Text descriptionLabel;
    public int Progression;
    #endregion

    //Change achievement icon
    public void RefreshView()
    {
        titleLabel.text = achievemnet.title;
        descriptionLabel.text = achievemnet.description;

        //unlockedIcon.color.
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
