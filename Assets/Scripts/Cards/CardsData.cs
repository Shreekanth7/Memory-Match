using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   // <-- This is the critical fix
public class CardData
{
    [Header("Card Properties")]
    public int cardId;             // Unique identifier for matching
    public Sprite cardFrontSprite; // Image shown when flipped
    public Sprite cardBackSprite;  // Image shown when face-down
    public string cardName;        // Optional descriptive name
}

[System.Serializable]   // <-- This is the critical fix
[CreateAssetMenu(fileName = "CardsData", menuName = "MatchCards/Cards", order = 0)]
public class CardsData : ScriptableObject
{
    public List<CardData> cards = new List<CardData>();
}