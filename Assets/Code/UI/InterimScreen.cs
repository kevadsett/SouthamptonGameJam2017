using UnityEngine;
using UnityEngine.UI;

public class InterimScreen : MonoBehaviour
{
    public Sprite[] RoundSprites;
    public Image CurrentRound;

    void Awake()
    {
        int currentRound = 0;
        if(ViewBindings.Instance.TryGetBoundValue("CurrentRound", out currentRound))
        {
            CurrentRound.sprite = RoundSprites[currentRound];
        }
    }
}