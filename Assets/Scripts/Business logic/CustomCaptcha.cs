using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCaptcha : MonoBehaviour {

    [SerializeField]
	Text [] answers;

    [SerializeField]
	Text quest;

	int rightButtonId;

    Action<bool> OnCaptchaResult;

	[SerializeField]
	ApearAnimation apearAnimation;

	private bool IsAnimating;

	public void Open(Action<bool> OnRightAction)
    {
		if(!IsAnimating)
        {
			gameObject.SetActive(true);
			IsAnimating = true;
			Generate(OnRightAction);
			apearAnimation.Show(() => {  IsAnimating = false; });
		}

	}

	public void Generate(Action<bool> OnRightAction)
	{
		int a = UnityEngine.Random.Range (1, 10);
		int b = UnityEngine.Random.Range (1, 10);
		int answer = a + b;
		quest.text = a.ToString()+" + "+b.ToString()+" = ?";
        rightButtonId = UnityEngine.Random.Range (0, 4);

		for (int i = 0; i < answers.Length; i++) 
		{
			if (i == rightButtonId) 
			{
				answers[i].text = answer.ToString();
			} 
			else 
			{
                int c = UnityEngine.Random.Range (1, 20);

                for (int j = 0; j < answers.Length; j++)
                {
                    if (answers[j].text == c.ToString() || c == answer)
                    {
                        c = UnityEngine.Random.Range (1, 20);
                        j = 0;
                    }
                }
				answers [i].text = c.ToString ();
			}
		}

        this.OnCaptchaResult = OnRightAction;
	}

	public void GetAnswer(int buttonId)
	{
		if (IsAnimating) return;

		Close(buttonId == rightButtonId);
	}

	public void Close(bool result=false)
    {
		if (!IsAnimating)
		{
			IsAnimating = true;
			apearAnimation.Hide(() => { gameObject.SetActive(false); IsAnimating = false; });
			OnCaptchaResult?.Invoke(result);
		}
	}

}
