using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardID;

    private GameObject frontImage;
    private GameObject backImage;
    private MemoryGame gameManager;
    private bool isOpen;
    private bool isMatched;
    private bool isInitialized;

    private void Awake()
    {
        Transform front = transform.Find("frontImage");
        Transform back = transform.Find("backImage");
        Button button = GetComponent<Button>();

        if (front == null || back == null || button == null)
        {
            Debug.LogWarning($"Card on '{name}' is missing frontImage/backImage children or a Button; ignoring this object.", this);
            return;
        }

        frontImage = front.gameObject;
        backImage = back.gameObject;
        isInitialized = true;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    public void Setup(MemoryGame manager)
    {
        if (!isInitialized) return;

        gameManager = manager;
        HideCard();

        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (isOpen || isMatched || gameManager == null) return;
        gameManager.SelectCard(this);
    }

    public void ShowCard()
    {
        isOpen = true;
        frontImage.SetActive(true);
        backImage.SetActive(false);
    }

    public void HideCard()
    {
        isOpen = false;
        frontImage.SetActive(false);
        backImage.SetActive(true);
    }

    public void MatchCard()
    {
        isMatched = true;

        frontImage.SetActive(false);
        backImage.SetActive(false);

        Button button = GetComponent<Button>();
        if (button != null)
    {
        button.interactable = false;
    }
    }
}
