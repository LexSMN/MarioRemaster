using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RescueManager : MonoBehaviour
{
    public static RescueManager instance;

    [Header("Contador")]
    public int rescuedAnimals = 0;
    public int animalsNeeded = 11;

    [Header("UI")]
    public TextMeshProUGUI rescueText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddRescue()
    {
        rescuedAnimals++;

        UpdateUI();

        if (rescuedAnimals >= animalsNeeded)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void UpdateUI()
    {
        rescueText.text = "Animales rescatados: " + rescuedAnimals + "/" + animalsNeeded;
    }
}