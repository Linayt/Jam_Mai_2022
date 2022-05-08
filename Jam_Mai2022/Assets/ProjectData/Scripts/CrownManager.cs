using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CrownManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugCrownAmountText;
    public int crownAmount = 0;
    
    public void CrownCollect(GameObject crown)
    {
        crownAmount++;
        DisplayCrownAmount();
    }

    private void DisplayCrownAmount()
    {
        debugCrownAmountText.text = crownAmount.ToString();
    }

    public void LoseAllCrowns()
    {
        DisplayCrownAmount();
        crownAmount = 0;
    }
}
