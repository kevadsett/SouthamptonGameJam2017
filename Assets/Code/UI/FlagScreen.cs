using UnityEngine;
using UnityEngine.UI;

public class FlagScreen : MonoBehaviour
{
    public Sprite[] RoundSprites;
    public Image CurrentRound;

    private Animator animator;

    void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    public void SetVisible(bool visible)
    {
        animator.SetBool("Visible", visible);

        if(visible)
        {
            int currentRound;
            if(ViewBindings.Instance.TryGetBoundValue("CurrentRound", out currentRound))
            {
                CurrentRound.sprite = RoundSprites[currentRound];
            }
        }
    }
}