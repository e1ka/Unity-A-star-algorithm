using UnityEngine;
using UnityEngine.UI;

public class BoardSizeInputController : MonoBehaviour
{
    InputField input;
    private void Start() {
        input = this.GetComponent<InputField>();
    }
    public void CheckRange()
    {
		if (input.text == "0") 
		{
			Debug.Log("0");
			input.text = "1";
		}
        else
        {
            int newValue;
            bool isInt = int.TryParse(input.text, out newValue);
            if(isInt)
            {
                if(!BoardManager.Instance.LimitedRange)
                {
                    return;
                }
                if (newValue > BoardManager.Instance.maxRange)
                {
                    input.text = BoardManager.Instance.maxRange.ToString();
                }
            }
            else
            {
                input.text = "1";
            }
        }
    }
}
