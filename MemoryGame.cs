using System.Collections;
using UnityEngine;

public class MemoryGame : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject cardHolder;
    public GameObject winPanel;
    public int totalPairs = 4;

    private Card firstCard;
    private Card secondCard;
    private bool isChecking = false;
    private int matchedPairs = 0;

    private void Start()
    {
        if (winPanel != null)
            winPanel.SetActive(false);

        if (cardHolder != null)
            cardHolder.SetActive(false);

        if (startPanel != null)
            startPanel.SetActive(true);
    }

    public void StartGame()
    {
        if (startPanel != null)
            startPanel.SetActive(false);

        if (cardHolder != null)
            cardHolder.SetActive(true);

        SetupCards();
    }

    private void SetupCards()
    {
        Card[] cards = FindObjectsByType<Card>(FindObjectsSortMode.None);

        foreach (Card card in cards)
        {
            card.Setup(this);
        }
    }

    public void SelectCard(Card card)
    {
        if (isChecking) return;

        card.ShowCard();

        if (firstCard == null)
        {
            firstCard = card;
            return;
        }

        secondCard = card;
        StartCoroutine(CheckCards());
    }

    private IEnumerator CheckCards()
    {
        isChecking = true;

        yield return new WaitForSeconds(0.8f);

        if (firstCard.cardID == secondCard.cardID)
        {
            firstCard.MatchCard();
            secondCard.MatchCard();
            matchedPairs++;

            if (matchedPairs >= totalPairs)
            {
                Debug.Log("You Win!");

                if (winPanel != null)
                    winPanel.SetActive(true);
            }
        }
        else
        {
            firstCard.HideCard();
            secondCard.HideCard();
        }

        firstCard = null;
        secondCard = null;
        isChecking = false;
    }
}
