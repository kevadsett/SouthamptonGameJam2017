using UnityEngine;

public class JudgingScreen : MonoBehaviour
{
    public RectTransform Player1Parent;
    public RectTransform Player2Parent;

    public GameObject CrossPrefab;
    public GameObject TickPrefab;

    private void DisplayResult(bool success, RectTransform parent)
    {
        GameObject instance = GameObject.Instantiate(success ? TickPrefab : CrossPrefab);
        instance.transform.SetParent(parent, false);
    }

    public void DisplayResults(bool player1Success, bool player2Success)
    {
        DisplayResult(player1Success, Player1Parent);
        DisplayResult(player2Success, Player2Parent);
    }
}