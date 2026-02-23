using UnityEngine;
using UnityEngine.UI;
using System;

public class CardController : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Image flipImage;
    [SerializeField] private Animator animator;

    private CardData cardData;
    private bool isMatched = false;
    private bool isFlipped = false;

    public event Action<CardController> OnCardFlipped;
    public static bool InputLocked = false;
    public void Initialize(CardData data)
    {
        cardData = data;
        cardImage.sprite = cardData.cardBackSprite;
        isMatched = false;
        isFlipped = false;
    }

    public int GetCardId() => cardData.cardId;
    public bool IsMatched() => isMatched;

    public void SetMatched()
    {
        isMatched = true;
        animator.SetTrigger("Matched");
    }

    public void ResetCard()
    {
        isFlipped = false;
        cardImage.sprite = cardData.cardBackSprite;
        animator.SetTrigger("Reset");
    }
    
    public void ForceFlipFront()
    {
        isFlipped = true;
        cardImage.sprite = cardData.cardFrontSprite;
    }
    
    public void OnClick()
    {
        if (InputLocked || isMatched || isFlipped) return;

        isFlipped = true;
        cardImage.sprite = cardData.cardFrontSprite;
        Debug.Log("Flip trigger fired on " + gameObject.name);
        animator.SetTrigger("Flip");

        OnCardFlipped?.Invoke(this);
    }
}