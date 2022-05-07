using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
	public enum LifeBarVisualType { Image, Slider}

	public LifeBarVisualType lifeBarVisualType = LifeBarVisualType.Slider;
	[Tooltip("The life component we want to display")]
	public Life lifeToDisplay;
	public Slider lifeBarSlider;
	public Image lifeBarImage;

	private void Awake ()
	{
		if (lifeToDisplay == null)
		{
			lifeToDisplay = FindObjectOfType<Life>();
			Debug.Log("LifeToDisplay n'a pas été assigné ! Pensez à drag & drop le component Life du GameObject dont vous voulez afficher la vie !", gameObject);
		}
		InitLifeBarValues();
	}

	public void InitLifeBarValues()
	{
		if(lifeBarSlider == null)
		{
			lifeBarSlider = GetComponentInChildren<Slider>();
		}

		switch (lifeBarVisualType)
		{
			case LifeBarVisualType.Slider when lifeBarSlider != null:
				
				lifeBarSlider.minValue = 0;
				lifeBarSlider.maxValue = 1;

				lifeBarSlider.value = (float)lifeToDisplay.CurrentLife / lifeToDisplay.maxLife;
				break;
			case LifeBarVisualType.Image when lifeBarImage != null:
				lifeBarImage.fillAmount = (float)lifeToDisplay.CurrentLife / lifeToDisplay.maxLife;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		lifeToDisplay.LifeChangeDelegate += UpdateValue;
	}

	private void UpdateValue()
	{
		if (lifeToDisplay == null) return;
		
		float maxValue = lifeToDisplay.maxLife;
		
		switch (lifeBarVisualType)
		{
			case LifeBarVisualType.Slider when lifeBarSlider != null:
				lifeBarSlider.value = lifeToDisplay.CurrentLife;
				break;
			case LifeBarVisualType.Image when lifeBarImage != null:
				lifeBarImage.fillAmount = lifeToDisplay.CurrentLife;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void Update ()
	{
		//UpdateValue();
	}
}
