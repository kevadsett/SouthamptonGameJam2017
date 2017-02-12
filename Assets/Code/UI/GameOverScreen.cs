using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Sprite BeheadedSprite;
    public Sprite WinnerSprite;

    public Image Player1;
    public Image Player2;

    public void Setup(eGameOverType gameOverType)
    {
        if(gameOverType == eGameOverType.Player1Victory)
        {
            Player1.sprite = WinnerSprite;
            Player2.sprite = BeheadedSprite;
        }
        else if(gameOverType == eGameOverType.Player2Victory)
        {
            Player1.sprite = BeheadedSprite;
            Player2.sprite = WinnerSprite;
        }
        else
        {
            Player1.sprite = BeheadedSprite;
            Player2.sprite = BeheadedSprite;
        }
    }
}