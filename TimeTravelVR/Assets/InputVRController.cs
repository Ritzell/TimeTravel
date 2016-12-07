using UnityEngine;
using System.Collections;

public enum HandType{
	right,
	left,
	Both
}
public enum InputPress
{
	PressTrigger,
	PressPad,
	PressGrip,
	PressMenu,
	UpMenu,
	UpPad,
	UpGrip,
	TouchPadPosition
}


public class InputVRController : MonoBehaviour {
	[SerializeField]
	public GameObject[] Controllers;
	public static SteamVR_TrackedObject[] trackedObject = new SteamVR_TrackedObject[2];



	void Start()
	{
		trackedObject[0] = Controllers[0].GetComponent<SteamVR_TrackedObject>();
		trackedObject[1] = Controllers[1].GetComponent<SteamVR_TrackedObject>();
	}

	public static bool GetPress(InputPress input,HandType type)
	{
		if (type == HandType.Both) {
			return GetPress (input, HandType.left) || GetPress (input, HandType.right);
		}
		SteamVR_Controller.Device device;
		device = SteamVR_Controller.Input((int)trackedObject[(int)type].index);
		switch (input)
		{
		case InputPress.PressTrigger:
			return device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger);
		case InputPress.PressPad:
			return device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad);
		case InputPress.PressGrip:
			return device.GetPressDown(SteamVR_Controller.ButtonMask.Grip);
		case InputPress.PressMenu:
			return device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu);
		case InputPress.UpPad:
			return device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad);
		case InputPress.UpGrip:
			return device.GetPressUp(SteamVR_Controller.ButtonMask.Grip);
		default:
			return false;
		}
	}

	public static bool GetPressStay(InputPress input, HandType type)
	{
		if (type == HandType.Both) {
			return GetPressStay (input, HandType.left) || GetPress (input, HandType.right);
		}
		SteamVR_Controller.Device device;
		device = SteamVR_Controller.Input((int)trackedObject[(int)type].index);
		switch (input)
		{
		case InputPress.PressTrigger:
			return device.GetPress(SteamVR_Controller.ButtonMask.Trigger);
		case InputPress.PressPad:
			return device.GetPress(SteamVR_Controller.ButtonMask.Touchpad);
		case InputPress.PressGrip:
			return device.GetPress(SteamVR_Controller.ButtonMask.Grip);
		default:
			return false;
		}
	}

	/// <summary>
	/// can not be use Both. only right or left.
	/// </summary>
	/// <returns>The analog press trigger.</returns>
	/// <param name="type">Type.</param>
	public static float GetAnalogPressTrigger(HandType type){
		if (HandType.Both == type) {
			return 0;
		}
		SteamVR_Controller.Device device;
		device = SteamVR_Controller.Input((int)trackedObject[(int)type].index);

		return device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
	}



	public static bool GetUp(InputPress input, HandType type)
	{
		SteamVR_Controller.Device device;
		device = SteamVR_Controller.Input((int)trackedObject[(int)type].index);
		switch (input)
		{
		case InputPress.UpMenu:
			return device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu);
		default:
			return false;
		}
	}

	public static bool GetPressStay(InputPress input)
	{
		SteamVR_Controller.Device device, device2;
		device = SteamVR_Controller.Input((int)trackedObject[0].index);
		device2 = SteamVR_Controller.Input((int)trackedObject[1].index);

		if (input == InputPress.PressGrip)
		{
			return device.GetPress(SteamVR_Controller.ButtonMask.Grip) || device2.GetPress(SteamVR_Controller.ButtonMask.Grip);
		}
		return false;
	}

	public static bool GetUp(InputPress input)
	{
		SteamVR_Controller.Device device, device2;
		device = SteamVR_Controller.Input((int)trackedObject[0].index);
		device2 = SteamVR_Controller.Input((int)trackedObject[1].index);

		switch (input)
		{
		case InputPress.UpGrip:
			return device.GetPressUp(SteamVR_Controller.ButtonMask.Grip) || device2.GetPressUp(SteamVR_Controller.ButtonMask.Grip);
		}
		return false;
	}

	public static Vector2 GetAxis(HandType type)
	{
		var device = SteamVR_Controller.Input((int)trackedObject[(int) type].index);
		return device.GetAxis();
	}

	/// <summary>
	/// コントローラーを振動させます。パワーは100～2000迄です。
	/// </summary>
	/// <param name="Power"></param>
	/// <param name="type"></param>
	public static IEnumerator ControllerPulse(ushort Power,HandType type)
	{
		if(!(Power >= 100 && Power <= 2000))
		{
			Debug.Log("ControllerPulseのPowerは100~2000までの間で指定してください");
			yield return null;
			yield break;
		}
		if (!(type == HandType.Both))
		{
			SteamVR_Controller.Device device;
			device = SteamVR_Controller.Input((int)trackedObject[(int)type].index);
			for (float time = 0; time < 1; time += Time.deltaTime)
			{
				device.TriggerHapticPulse(Power);
				yield return null;
			}
		}
		else
		{
			SteamVR_Controller.Device device,device2;

				device = SteamVR_Controller.Input((int)trackedObject[0].index);
				device2 = SteamVR_Controller.Input((int)trackedObject[1].index);

			
			for (float time = 0; time < 1; time += Time.deltaTime)
			{
				device.TriggerHapticPulse(Power);
				device2.TriggerHapticPulse(Power);
				yield return null;
			}
		}
	}
}