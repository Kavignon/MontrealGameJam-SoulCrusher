using UnityEngine;
using XInputDotNetPure;

namespace XboxCtrlrInput
{
	
	// ================= Enumerations ==================== //
	
	/// <summary>
	/// List of enumerated identifiers for Xbox buttons.
	/// </summary>
	public enum XboxButton
	{
		A,
		B,
		X,
		Y,
		Start,
		Back,
		LeftStick,
		RightStick,
		LeftBumper,
		RightBumper
	}
	
	/// <summary>
	/// List of enumerated identifiers for Xbox D-Pad directions.
	/// </summary>
	public enum XboxDPad
	{
		Up,
		Down,
		Left,
		Right
	}
	
	/// <summary>
	/// List of enumerated identifiers for Xbox axis.
	/// </summary>
	public enum XboxAxis
	{
		LeftStickX,
		LeftStickY,
		RightStickX,
		RightStickY,
		LeftTrigger,
		RightTrigger
	}
	
	// ================= Class ==================== //
	
	public sealed class XCI 
	{
		// ------------ Members --------------- //
		
		private static GamePadState[] xInputCtrlrs = new GamePadState[4];
		private static GamePadState[] xInputCtrlrsPrev = new GamePadState[4];
		private static int xiPrevFrameCount = 0;
		private static bool xiUpdateAlreadyCalled = false;
		private static bool xiNumOfCtrlrsQueried = false;
		
		// ------------ Methods --------------- //
		
		// >>> For Buttons <<< //
		
		/// <summary> Returns <c>true</c> if the specified button is held down by any controller. </summary>
		/// <param name='button'> Identifier for the Xbox button to be tested. </param>
		public static bool GetButton(XboxButton button)
		{
			if(OnWindowsNative())
			{
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdateSingleState();
				}
				GamePadState ctrlrState = XInputGetSingleState();
				
				if( XInputGetButtonState(ctrlrState.Buttons, button) == ButtonState.Pressed )
				{
					return true;
				}
			}
			
			else
			{	
				string btnCode = DetermineButtonCode(button, 0);
				
				if(Input.GetKey(btnCode))
				{
					return true;
				}
			}
				
			return false;
		}
		
		/// <summary> Returns <c>true</c> if the specified button is held down by a specified controller. </summary>
		/// <param name='button'> Identifier for the Xbox button to be tested. </param>
		/// <param name='controllerNumber'> An identifier for the specific controller on which to test the button. An int between 1 and 4. </param>
		public static bool GetButton(XboxButton button, int controllerNumber)
		{
			if(!IsControllerNumberValid(controllerNumber))  return false;
			
			if(OnWindowsNative())
			{
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdatePaticularState(controllerNumber);
				}
				
				GamePadState ctrlrState = XInputGetPaticularState(controllerNumber);
				
				if( XInputGetButtonState(ctrlrState.Buttons, button) == ButtonState.Pressed )
				{
					return true;
				}
			}
			
			else
			{
				string btnCode = DetermineButtonCode(button, controllerNumber);
				
				if(Input.GetKey(btnCode))
				{
					return true;
				}
			}
			
			return false;
		}
		
		/// <summary> Returns <c>true</c> at the frame the specified button starts to press down (not held down) by any controller. </summary>
		/// <param name='button'> Identifier for the Xbox button to be tested. </param>
		public static bool GetButtonDown(XboxButton button)
		{
			if(OnWindowsNative())
			{
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdateSingleState();
				}
				
				GamePadState ctrlrState = XInputGetSingleState();
				GamePadState ctrlrStatePrev = XInputGetSingleStatePrev();
				
				if( ( XInputGetButtonState(ctrlrState.Buttons, button) == ButtonState.Pressed ) &&
					( XInputGetButtonState(ctrlrStatePrev.Buttons, button) == ButtonState.Released ) )
				{
					return true;
				}
			}
			
			else
			{
				string btnCode = DetermineButtonCode(button, 0);
				
				if(Input.GetKeyDown(btnCode))
				{
					return true;
				}
			}
				
			return false;
		}
		
		/// <summary> Returns <c>true</c> at the frame the specified button starts to press down (not held down) by a specified controller. </summary>
		/// <param name='button'> Identifier for the Xbox button to be tested. </param>
		/// <param name='controllerNumber'> An identifier for the specific controller on which to test the button. An int between 1 and 4. </param>
		public static bool GetButtonDown(XboxButton button, int controllerNumber)
		{
			if(!IsControllerNumberValid(controllerNumber))  return false;
			
			if(OnWindowsNative())
			{
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdatePaticularState(controllerNumber);
				}
				
				GamePadState ctrlrState = XInputGetPaticularState(controllerNumber);
				GamePadState ctrlrStatePrev = XInputGetPaticularStatePrev(controllerNumber);
				
				if( ( XInputGetButtonState(ctrlrState.Buttons, button) == ButtonState.Pressed ) &&
					( XInputGetButtonState(ctrlrStatePrev.Buttons, button) == ButtonState.Released ) )
				{
					return true;
				}
			}
			
			else
			{
				string btnCode = DetermineButtonCode(button, controllerNumber);
				
				if(Input.GetKeyDown(btnCode))
				{
					return true;
				}
			}
				
			return false;
		}
		
		/// <summary> Returns <c>true</c> at the frame the specified button is released by any controller. </summary>
		/// <param name='button'> Identifier for the Xbox button to be tested. </param>
		public static bool GetButtonUp(XboxButton button)
		{
			if(OnWindowsNative())
			{
				if(Time.frameCount < 2)
				{
					return false;
				}
				
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdateSingleState();
				}
				
				GamePadState ctrlrState = XInputGetSingleState();
				GamePadState ctrlrStatePrev = XInputGetSingleStatePrev();
				
				if( ( XInputGetButtonState(ctrlrState.Buttons, button) == ButtonState.Released ) &&
					( XInputGetButtonState(ctrlrStatePrev.Buttons, button) == ButtonState.Pressed ) )
				{
					return true;
				}
			}
			
			else
			{
				string btnCode = DetermineButtonCode(button, 0);
				
				if(Input.GetKeyUp(btnCode))
				{
					return true;
				}
			}
			
			return false;
		}
		
		/// <summary> Returns <c>true</c> at the frame the specified button is released by a specified controller. </summary>
		/// <param name='button'> Identifier for the Xbox button to be tested. </param>
		/// <param name='controllerNumber'> An identifier for the specific controller on which to test the button. An int between 1 and 4. </param>
		public static bool GetButtonUp(XboxButton button, int controllerNumber)
		{
			if(!IsControllerNumberValid(controllerNumber))  return false;
			
			if(OnWindowsNative())
			{
				if(Time.frameCount < 2)
				{
					return false;
				}
				
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdatePaticularState(controllerNumber);
				}
				
				GamePadState ctrlrState = XInputGetPaticularState(controllerNumber);
				GamePadState ctrlrStatePrev = XInputGetPaticularStatePrev(controllerNumber);
				
				if( ( XInputGetButtonState(ctrlrState.Buttons, button) == ButtonState.Released ) &&
				    ( XInputGetButtonState(ctrlrStatePrev.Buttons, button) == ButtonState.Pressed ) )
				{
					return true;
				}
			}
			
			else
			{
				string btnCode = DetermineButtonCode(button, controllerNumber);
				
				if(Input.GetKeyUp(btnCode))
				{
					return true;
				}
			}
			
			return false;
		}
		
		// >>> For D-Pad <<< //
		
		/// <summary> Returns <c>true</c> if the specified D-Pad direction is pressed down by any controller. </summary>
		/// <param name='padDirection'> An identifier for the specified D-Pad direction to be tested. </param>
		public static bool GetDPad(XboxDPad padDirection)
		{
			bool r = false;
			
			if(OnWindowsNative())
			{
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdateSingleState();
				}
				
				GamePadState ctrlrState = XInputGetSingleState();
				
				if( XInputGetDPadState(ctrlrState.DPad, padDirection) == ButtonState.Pressed )
				{
					return true;
				}
			}
			
			else
			{				
				string inputCode = "";
				
				if(OnMac())
				{
					inputCode = DetermineDPadMac(padDirection, 0);
					r = Input.GetKey(inputCode);
				}
				else if(OnLinux() && IsControllerWireless())
				{
					inputCode = DetermineDPadWirelessLinux(padDirection, 0);
					r = Input.GetKey(inputCode);
				}
				else
				{
					inputCode = DetermineDPad(padDirection, 0);
					
					switch(padDirection)
					{
						case XboxDPad.Up: 		r = Input.GetAxis(inputCode) > 0; break;
						case XboxDPad.Down: 	r = Input.GetAxis(inputCode) < 0; break;
						case XboxDPad.Left: 	r = Input.GetAxis(inputCode) < 0; break;
						case XboxDPad.Right:	r = Input.GetAxis(inputCode) > 0; break;
						
						default: r = false; break;
					}
				}
			}
				
			return r;
		}
		
		/// <summary> Returns <c>true</c> if the specified D-Pad direction is pressed down by a specified controller. </summary>
		/// <param name='padDirection'> An identifier for the specified D-Pad direction to be tested. </param>
		/// <param name='controllerNumber'> An identifier for the specific controller on which to test the D-Pad. An int between 1 and 4. </param>
		public static bool GetDPad(XboxDPad padDirection, int controllerNumber)
		{
			bool r = false;
			
			if(OnWindowsNative())
			{
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdatePaticularState(controllerNumber);
				}
				
				GamePadState ctrlrState = XInputGetPaticularState(controllerNumber);

				if( XInputGetDPadState(ctrlrState.DPad, padDirection) == ButtonState.Pressed )
				{
					return true;
				}
			}
			
			else
			{
				string inputCode = "";
				
				if(OnMac())
				{
					inputCode = DetermineDPadMac(padDirection, controllerNumber);
					r = Input.GetKey(inputCode);
				}
				else if(OnLinux() && IsControllerWireless(controllerNumber))
				{
					inputCode = DetermineDPadWirelessLinux(padDirection, controllerNumber);
					r = Input.GetKey(inputCode);
				}
				else
				{
					inputCode = DetermineDPad(padDirection, controllerNumber);
					
					switch(padDirection)
					{
						case XboxDPad.Up: 		r = Input.GetAxis(inputCode) > 0; break;
						case XboxDPad.Down: 	r = Input.GetAxis(inputCode) < 0; break;
						case XboxDPad.Left: 	r = Input.GetAxis(inputCode) < 0; break;
						case XboxDPad.Right:	r = Input.GetAxis(inputCode) > 0; break;
						
						default: r = false; break;
					}
				}
			}
			
			return r;
		}
		
		/// <summary> Returns <c>true</c> at the frame the specified button is released. </summary>
		/// <param name='button'> Identifier for the Xbox button to be tested. </param>
		/// <param name='controllerNumber'> An identifier for the specific controller on which to test the button. An int between 1 and 4. </param>
		public static bool GetDPadUp(XboxDPad padDirection)
		{
			
			bool r = false;
			
			if(OnWindowsNative())
			{
				if(Time.frameCount < 2)
				{
					return false;
				}
				
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdatePaticularState(controllerNumber);
				}
				
				GamePadState ctrlrState = XInputGetSingleState();
				GamePadState ctrlrStatePrev = XInputGetSingleStatePrev();
				
				if( ( XInputGetDPadState(ctrlrState.DPad, padDirection) == ButtonState.Released ) &&
				   ( XInputGetDPadState(ctrlrStatePrev.DPad, padDirection) == ButtonState.Pressed ) )
				{
					return true;
				}
			}
			
			else
			{
				string inputCode = "";
				
				if(OnMac())
				{
					inputCode = DetermineDPadMac(padDirection, 0);
					r = Input.GetKeyUp(inputCode);
				}
				else if(OnLinux() && IsControllerWireless())
				{
					inputCode = DetermineDPadWirelessLinux(padDirection, 0);
					r = Input.GetKeyUp(inputCode);
				}
				else
				{
					//Place Holder for Wired Linux
					r = false;
				}					
			}
			
			return r;
		}
		
		/// <summary> Returns <c>true</c> at the frame the specified button is released by a specified controller. </summary>
		/// <param name='button'> Identifier for the Xbox button to be tested. </param>
		/// <param name='controllerNumber'> An identifier for the specific controller on which to test the button. An int between 1 and 4. </param>
		public static bool GetDPadUp(XboxDPad padDirection, int controllerNumber)
		{
			
			bool r = false;
			
			if(OnWindowsNative())
			{
				if(Time.frameCount < 2)
				{
					return false;
				}
				
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdatePaticularState(controllerNumber);
				}
				
				GamePadState ctrlrState = XInputGetPaticularState(controllerNumber);
				GamePadState ctrlrStatePrev = XInputGetPaticularStatePrev(controllerNumber);
				
				if( ( XInputGetDPadState(ctrlrState.DPad, padDirection) == ButtonState.Released ) &&
				   ( XInputGetDPadState(ctrlrStatePrev.DPad, padDirection) == ButtonState.Pressed ) )
				{
					return true;
				}
			}
			
			else
			{
				string inputCode = "";
				
				if(OnMac())
				{
					inputCode = DetermineDPadMac(padDirection, controllerNumber);
					r = Input.GetKeyUp(inputCode);
				}
				else if(OnLinux() && IsControllerWireless(controllerNumber))
				{
					inputCode = DetermineDPadWirelessLinux(padDirection, controllerNumber);
					r = Input.GetKeyUp(inputCode);
				}
				else
				{
					//Place Holder for Wired Linux
					r = false;
				}
			}
			
			return r;
		}
		
		/// <summary> Returns <c>true</c> at the frame the specified button is Pressed. </summary>
		/// <param name='button'> Identifier for the Xbox button to be tested. </param>
		/// <param name='controllerNumber'> An identifier for the specific controller on which to test the button. An int between 1 and 4. </param>
		public static bool GetDPadDown(XboxDPad padDirection)
		{
			
			bool r = false;
			
			if(OnWindowsNative())
			{
				if(Time.frameCount < 2)
				{
					return false;
				}
				
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdatePaticularState(controllerNumber);
				}
				
				GamePadState ctrlrState = XInputGetSingleState();
				GamePadState ctrlrStatePrev = XInputGetSingleStatePrev();
				
				if( ( XInputGetDPadState(ctrlrState.DPad, padDirection) == ButtonState.Pressed ) &&
				   ( XInputGetDPadState(ctrlrStatePrev.DPad, padDirection) == ButtonState.Released ) )
				{
					return true;
				}
			}
			
			else
			{
				string inputCode = "";
				
				if(OnMac())
				{
					inputCode = DetermineDPadMac(padDirection, 0);
					r = Input.GetKeyDown(inputCode);
				}
				else if(OnLinux() && IsControllerWireless())
				{
					inputCode = DetermineDPadWirelessLinux(padDirection, 0);
					r = Input.GetKeyDown(inputCode);
				}
				else
				{
					//Place Holder for Wired Linux
					r = false;
				}
			}
			
			return r;
		}
		
		/// <summary> Returns <c>true</c> at the frame the specified button is Pressed by a specified controller. </summary>
		/// <param name='button'> Identifier for the Xbox button to be tested. </param>
		/// <param name='controllerNumber'> An identifier for the specific controller on which to test the button. An int between 1 and 4. </param>
		public static bool GetDPadDown(XboxDPad padDirection, int controllerNumber)
		{
			
			bool r = false;
			
			if(OnWindowsNative())
			{
				if(Time.frameCount < 2)
				{
					return false;
				}
				
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdatePaticularState(controllerNumber);
				}
				
				GamePadState ctrlrState = XInputGetPaticularState(controllerNumber);
				GamePadState ctrlrStatePrev = XInputGetPaticularStatePrev(controllerNumber);
				
				if( ( XInputGetDPadState(ctrlrState.DPad, padDirection) == ButtonState.Pressed ) &&
				   ( XInputGetDPadState(ctrlrStatePrev.DPad, padDirection) == ButtonState.Released ) )
				{
					return true;
				}
			}
			
			else
			{
				string inputCode = "";
				
				if(OnMac())
				{
					inputCode = DetermineDPadMac(padDirection, controllerNumber);
					r = Input.GetKeyDown(inputCode);
				}
				else if(OnLinux() && IsControllerWireless(controllerNumber))
				{
					inputCode = DetermineDPadWirelessLinux(padDirection, controllerNumber);
					r = Input.GetKeyDown(inputCode);
				}
				else
				{
					//Place Holder for Wired Linux
					r = false;
				}
			}
			
			return r;
		}
		
		// >>> For Axis <<< //
		
		/// <summary> Returns the analog number of the specified axis from any controller. </summary>
		/// <param name='axis'> An identifier for the specified Xbox axis to be tested. </param>
		public static float GetAxis(XboxAxis axis)
		{
			float r = 0.0f;
			
			if(OnWindowsNative())
			{
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdateSingleState();
				}
				
				GamePadState ctrlrState = XInputGetSingleState();
				
				if(axis == XboxAxis.LeftTrigger || axis == XboxAxis.RightTrigger)
				{
					r = XInputGetAxisState(ctrlrState.Triggers, axis);
				}
				else
				{
					r = XInputGetAxisState(ctrlrState.ThumbSticks, axis);
				}
			}
			
			else
			{
				string axisCode = DetermineAxisCode(axis, 0);
				
				r = Input.GetAxis(axisCode);
				r = AdjustAxisValues(r, axis);
			}
				
			return r;
		}
		
		/// <summary> Returns the float number of the specified axis from a specified controller. </summary>
		/// <param name='axis'> An identifier for the specified Xbox axis to be tested. </param>
		/// <param name='controllerNumber'> An identifier for the specific controller on which to test the axis. An int between 1 and 4. </param>
		public static float GetAxis(XboxAxis axis, int controllerNumber)
		{
			float r = 0.0f;
			
			if(OnWindowsNative())
			{
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdatePaticularState(controllerNumber);
				}
				
				GamePadState ctrlrState = XInputGetPaticularState(controllerNumber);
				
				if(axis == XboxAxis.LeftTrigger || axis == XboxAxis.RightTrigger)
				{
					r = XInputGetAxisState(ctrlrState.Triggers, axis);
				}
				else
				{
					r = XInputGetAxisState(ctrlrState.ThumbSticks, axis);
				}
			}
			
			else
			{
				string axisCode = DetermineAxisCode(axis, controllerNumber);
				
				r = Input.GetAxis(axisCode);
				r = AdjustAxisValues(r, axis);
			}
			
			return r;
		}
		
		/// <summary> Returns the float number of the specified axis from any controller without Unity's smoothing filter. </summary>
		/// <param name='axis'> An identifier for the specified Xbox axis to be tested. </param>
		public static float GetAxisRaw(XboxAxis axis)
		{
			float r = 0.0f;
			
			if(OnWindowsNative())
			{
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdateSingleStateRaw();
				}
				
				GamePadState ctrlrState = XInputGetSingleState();
				
				if(axis == XboxAxis.LeftTrigger || axis == XboxAxis.RightTrigger)
				{
					r = XInputGetAxisState(ctrlrState.Triggers, axis);
				}
				else
				{
					r = XInputGetAxisState(ctrlrState.ThumbSticks, axis);
				}
			}
			
			else
			{
				string axisCode = DetermineAxisCode(axis, 0);
				
				r = Input.GetAxisRaw(axisCode);
				r = AdjustAxisValues(r, axis);
			}
				
			return r;
		}
		
		/// <summary> Returns the float number of the specified axis from a specified controller without Unity's smoothing filter. </summary>
		/// <param name='axis'> An identifier for the specified Xbox axis to be tested. </param>
		/// <param name='controllerNumber'> An identifier for the specific controller on which to test the axis. An int between 1 and 4. </param>
		public static float GetAxisRaw(XboxAxis axis, int controllerNumber)
		{
			float r = 0.0f;
			
			if(OnWindowsNative())
			{
				if(!XInputStillInCurrFrame())
				{
					XInputUpdateAllStates(); //XInputUpdatePaticularStateRaw(controllerNumber);
				}
					
				GamePadState ctrlrState = XInputGetPaticularState(controllerNumber);
				
				if(axis == XboxAxis.LeftTrigger || axis == XboxAxis.RightTrigger)
				{
					r = XInputGetAxisState(ctrlrState.Triggers, axis);
				}
				else
				{
					r = XInputGetAxisState(ctrlrState.ThumbSticks, axis);
				}
			}
			
			else
			{
				string axisCode = DetermineAxisCode(axis, controllerNumber);
				
				r = Input.GetAxisRaw(axisCode);
				r = AdjustAxisValues(r, axis);
			}
				
			return r;
		}
		
		// >>> Other important functions <<< //
		
		/// <summary> Returns the number of Xbox controllers plugged to the computer. </summary>
		public static int GetNumPluggedCtrlrs()
		{
			int r = 0;
			
			if(OnWindowsNative())
			{
				if(!xiNumOfCtrlrsQueried || !XInputStillInCurrFrame())
				{
					xiNumOfCtrlrsQueried = true;
					XInputUpdateAllStates();
				}
				
				for(int i = 0; i < 4; i++)
				{
					if(xInputCtrlrs[i].IsConnected)
					{
						r++;
					}
				}
			}
			
			else
			{
				string[] ctrlrNames = Input.GetJoystickNames();
				for(int i = 0; i < ctrlrNames.Length; i++)
				{
					if(ctrlrNames[i].Contains("Xbox") || ctrlrNames[i].Contains("XBOX") || ctrlrNames[i].Contains("Microsoft"))
					{
						r++;
					}
				}
			}
			
			return r;
		}
		
		/// <summary> DEBUG function. Log all controller names to Unity's console. </summary>
		public static void DEBUG_LogControllerNames()
		{
			string[] cNames = Input.GetJoystickNames();
			
			for(int i = 0; i < cNames.Length; i++)
			{
				Debug.Log("Ctrlr " + i.ToString() + ": " + cNames[i]);
			}
		}

        public static bool SetVibration(int controllerNumber, float leftMotor, float rightMotor)
        {
            if (!IsControllerNumberValid(controllerNumber)) return false;

            if (Time.frameCount < 2) return false;
            if (!XInputStillInCurrFrame()) XInputUpdateAllStates();

            PlayerIndex index = (PlayerIndex)(controllerNumber -1);

            GamePad.SetVibration(index, leftMotor, rightMotor);

            return true;
        }

		// ------------- Private Methods -------------- //
		
		
		private static bool OnMac()
		{
			// All Mac mappings are based off TattieBogle Xbox Controller drivers
			// http://tattiebogle.net/index.php/ProjectRoot/Xbox360Controller/OsxDriver
			// http://wiki.unity3d.com/index.php?title=Xbox360Controller
			return (Application.platform == RuntimePlatform.OSXEditor || 
				    Application.platform == RuntimePlatform.OSXPlayer ||
				    Application.platform == RuntimePlatform.OSXWebPlayer);
		}
		
		private static bool OnWindows()
		{
			return (Application.platform == RuntimePlatform.WindowsEditor || 
				    Application.platform == RuntimePlatform.WindowsPlayer ||
				    Application.platform == RuntimePlatform.WindowsWebPlayer);
		}
		
		private static bool OnWindowsWebPlayer()
		{
			return (Application.platform == RuntimePlatform.WindowsWebPlayer);
		}
		
		private static bool OnWindowsNative()
		{
			return (Application.platform == RuntimePlatform.WindowsEditor || 
				    Application.platform == RuntimePlatform.WindowsPlayer);
		}
		
		private static bool OnLinux()
		{
			// Linux mapping based on observation of mapping from default drivers on Ubuntu 13.04
			return Application.platform == RuntimePlatform.LinuxPlayer;
		}
		
		private static bool IsControllerWireless()
		{
			// 0 means for any controller
			return IsControllerWireless(0);
		}
		
		private static bool IsControllerWireless(int ctrlNum)
		{	
			if (ctrlNum < 0 || ctrlNum > 4) return false;
			
			// If 0 is passed in, that assumes that only 1 controller is plugged in.
			if(ctrlNum == 0)
			{
				return ( (string) Input.GetJoystickNames()[0]).Contains("Wireless");
			}
			
			return ( (string) Input.GetJoystickNames()[ctrlNum-1]).Contains("Wireless");
		}
		
		private static bool IsControllerNumberValid(int ctrlrNum)
		{
			if(ctrlrNum > 0 && ctrlrNum <= 4)
			{
				return true;
			}
			else
			{
				Debug.LogError("XCI.IsControllerNumberValid(): " + 
							   "Invalid contoller number! Should be between 1 and 4.");
			}
			return false;
		}
		
		private static float RefactorRange(float oldRangeValue)
		{
			// Assumes you want to take a number from -1 to 1 range
			// And turn it into a number from a 0 to 1 range
			
			return ((oldRangeValue + 1.0f) / 2.0f );
		}
		
		private static string DetermineButtonCode(XboxButton btn, int ctrlrNum)
		{
			string r = "";
			string sJoyCode = "";
			string sKeyCode = "";
			bool invalidCode = false;
			
			if(ctrlrNum == 0)
			{
				sJoyCode = "";
			}
			else
			{
				sJoyCode = " " + ctrlrNum.ToString();
			}
			
			if(OnMac())
			{
				switch(btn)
				{
					case XboxButton.A: 				sKeyCode = "16"; break;
					case XboxButton.B:				sKeyCode = "17"; break;
					case XboxButton.X:				sKeyCode = "18"; break;
					case XboxButton.Y:				sKeyCode = "19"; break;
					case XboxButton.Start:			sKeyCode = "9"; break;
					case XboxButton.Back:			sKeyCode = "10"; break;
					case XboxButton.LeftStick:		sKeyCode = "11"; break;
					case XboxButton.RightStick:		sKeyCode = "12"; break;
					case XboxButton.LeftBumper:		sKeyCode = "13"; break;
					case XboxButton.RightBumper:	sKeyCode = "14"; break;
					
					default: invalidCode = true; break;
				}
			}
			else if (OnLinux())
			{
				switch(btn)
				{
					case XboxButton.A: 				sKeyCode = "0"; break;
					case XboxButton.B:				sKeyCode = "1"; break;
					case XboxButton.X:				sKeyCode = "2"; break;
					case XboxButton.Y:				sKeyCode = "3"; break;
					case XboxButton.Start:			sKeyCode = "7"; break;
					case XboxButton.Back:			sKeyCode = "6"; break;
					case XboxButton.LeftStick:		sKeyCode = "9"; break;
					case XboxButton.RightStick:		sKeyCode = "10"; break;
					case XboxButton.LeftBumper:		sKeyCode = "4"; break;
					case XboxButton.RightBumper:	sKeyCode = "5"; break;
					
					default: invalidCode = true; break;
				}
			}
			else
			{
				switch(btn)
				{
					case XboxButton.A: 				sKeyCode = "0"; break;
					case XboxButton.B:				sKeyCode = "1"; break;
					case XboxButton.X:				sKeyCode = "2"; break;
					case XboxButton.Y:				sKeyCode = "3"; break;
					case XboxButton.Start:			sKeyCode = "7"; break;
					case XboxButton.Back:			sKeyCode = "6"; break;
					case XboxButton.LeftStick:		sKeyCode = "8"; break;
					case XboxButton.RightStick:		sKeyCode = "9"; break;
					case XboxButton.LeftBumper:		sKeyCode = "4"; break;
					case XboxButton.RightBumper:	sKeyCode = "5"; break;
					
					default: invalidCode = true; break;
				}
			}
			
			r = "joystick" + sJoyCode + " button " + sKeyCode;
			
			if(invalidCode)
			{
				r = "";
			}
			
			return r;
		}
		
		private static string DetermineAxisCode(XboxAxis axs, int ctrlrNum)
		{
			string r = "";
			string sJoyCode = ctrlrNum.ToString();
			string sAxisCode = "";
			bool invalidCode = false;
			
			
			if(OnMac())
			{
				switch(axs)
				{
					case XboxAxis.LeftStickX: 		sAxisCode = "X"; break;
					case XboxAxis.LeftStickY:		sAxisCode = "Y"; break;
					case XboxAxis.RightStickX:		sAxisCode = "3"; break;
					case XboxAxis.RightStickY:		sAxisCode = "4"; break;
					case XboxAxis.LeftTrigger:		sAxisCode = "5"; break;
					case XboxAxis.RightTrigger:		sAxisCode = "6"; break;
					
					default: invalidCode = true; break;
				}
			}
			else if(OnLinux())
			{
				switch(axs)
				{
					case XboxAxis.LeftStickX: 		sAxisCode = "X"; break;
					case XboxAxis.LeftStickY:		sAxisCode = "Y"; break;
					case XboxAxis.RightStickX:		sAxisCode = "4"; break;
					case XboxAxis.RightStickY:		sAxisCode = "5"; break;
					case XboxAxis.LeftTrigger:		sAxisCode = "3"; break;
					case XboxAxis.RightTrigger:		sAxisCode = "6"; break;
					
					default: invalidCode = true; break;
				}
			}
			else
			{
				switch(axs)
				{
					case XboxAxis.LeftStickX: 		sAxisCode = "X"; break;
					case XboxAxis.LeftStickY:		sAxisCode = "Y"; break;
					case XboxAxis.RightStickX:		sAxisCode = "4"; break;
					case XboxAxis.RightStickY:		sAxisCode = "5"; break;
					case XboxAxis.LeftTrigger:		sAxisCode = "9"; break;
					case XboxAxis.RightTrigger:		sAxisCode = "10"; break;
					
					default: invalidCode = true; break;
				}
			}
			
			r = "XboxAxis" + sAxisCode + "Joy" + sJoyCode;
			
			if(invalidCode)
			{
				r = "";
			}
			
			return r;
		}
		
		private static float AdjustAxisValues(float axisValue, XboxAxis axis)
		{
			float newAxisValue = axisValue;
			
			if(OnMac())
			{
				if(axis == XboxAxis.LeftTrigger)
				{
					newAxisValue = -newAxisValue;
					newAxisValue = RefactorRange(newAxisValue);
				}
				else if(axis == XboxAxis.RightTrigger)
				{
					newAxisValue = RefactorRange(newAxisValue);
				}
				else if(axis == XboxAxis.RightStickY)
				{
					newAxisValue = -newAxisValue;
				}
			}
			else if(OnLinux())
			{
				if(axis == XboxAxis.RightTrigger)
				{
					newAxisValue = RefactorRange(newAxisValue);
				}
				else if(axis == XboxAxis.LeftTrigger)
				{
					newAxisValue = RefactorRange(newAxisValue);
				}
			}
			
			return newAxisValue;
		}
		
		private static string DetermineDPad(XboxDPad padDir, int ctrlrNum)
		{
			string r = "";
			string sJoyCode = ctrlrNum.ToString();
			string sPadCode = "";
			bool invalidCode = false;
			
			if(OnLinux())
			{
				switch(padDir)
				{
					case XboxDPad.Up: 		sPadCode = "8"; break;
					case XboxDPad.Down:		sPadCode = "8"; break;
					case XboxDPad.Left:		sPadCode = "7"; break;
					case XboxDPad.Right:	sPadCode = "7"; break;
					
					default: invalidCode = true; break;
				}
			}
			else
			{
				switch(padDir)
				{
					case XboxDPad.Up: 		sPadCode = "7"; break;
					case XboxDPad.Down:		sPadCode = "7"; break;
					case XboxDPad.Left:		sPadCode = "6"; break;
					case XboxDPad.Right:	sPadCode = "6"; break;
					
					default: invalidCode = true; break;
				}
			}
			
			r = "XboxAxis" + sPadCode + "Joy" + sJoyCode;
			
			if(invalidCode)
			{
				r = "";
			}
			
			return r;
		}
		
		private static string DetermineDPadMac(XboxDPad padDir, int ctrlrNum)
		{
			string r = "";
			string sJoyCode = "";
			string sPadCode = "";
			bool invalidCode = false;
			
			if(ctrlrNum == 0)
			{
				sJoyCode = "";
			}
			else
			{
				sJoyCode = " " + ctrlrNum.ToString();
			}
			
			switch(padDir)
			{
				case XboxDPad.Up: 		sPadCode = "5"; break;
				case XboxDPad.Down:		sPadCode = "6"; break;
				case XboxDPad.Left:		sPadCode = "7"; break;
				case XboxDPad.Right:	sPadCode = "8"; break;
				
				default: invalidCode = true; break;
			}
			
			r = "joystick" + sJoyCode + " button " + sPadCode;
			
			if(invalidCode)
			{
				r = "";
			}
			
			return r;
		}
		
		private static string DetermineDPadWirelessLinux(XboxDPad padDir, int ctrlrNum)
		{
			string r = "";
			string sJoyCode = "";
			string sPadCode = "";
			bool invalidCode = false;
			
			if(ctrlrNum == 0)
			{
				sJoyCode = "";
			}
			else
			{
				sJoyCode = " " + ctrlrNum.ToString();
			}
			
			switch(padDir)
			{
				case XboxDPad.Up: 		sPadCode = "13"; break;
				case XboxDPad.Down:		sPadCode = "14"; break;
				case XboxDPad.Left:		sPadCode = "11"; break;
				case XboxDPad.Right:	sPadCode = "12"; break;
				
				default: invalidCode = true; break;
			}
			
			r = "joystick" + sJoyCode + " button " + sPadCode;
			
			if(invalidCode)
			{
				r = "";
			}
			
			return r;
		}
		
		
		// ------------- Private XInput Wrappers (for Windows Native player and editor only) -------------- //
		
		
		//>> For updating states <<
		
		private static void XInputUpdateAllStates()
		{
			if(xiUpdateAlreadyCalled) return;
			
			for(int i = 0; i < 4; i++)
			{
				PlayerIndex plyNum = (PlayerIndex) i;
				xInputCtrlrsPrev[i] = xInputCtrlrs[i];
				xInputCtrlrs[i] = GamePad.GetState(plyNum, GamePadDeadZone.IndependentAxes);
			}
			
			xiUpdateAlreadyCalled = true;
		}
		
		
		//>> For getting states <<
		private static GamePadState XInputGetSingleState()
		{
			return xInputCtrlrs[0];
		}
		
		private static GamePadState XInputGetPaticularState(int ctrlNum)
		{
			if (!IsControllerNumberValid(ctrlNum)) return xInputCtrlrs[0];
			
			return xInputCtrlrs[ctrlNum-1];
		}
		
		private static GamePadState XInputGetSingleStatePrev()
		{
			return xInputCtrlrsPrev[0];
		}
		
		private static GamePadState XInputGetPaticularStatePrev(int ctrlNum)
		{
			if (!IsControllerNumberValid(ctrlNum)) return xInputCtrlrsPrev[0];
			
			return xInputCtrlrsPrev[ctrlNum-1];
		}
		
		//>> For getting input <<
		private static ButtonState XInputGetButtonState(GamePadButtons xiButtons, XboxButton xciBtn)
		{
			ButtonState stateToReturn = ButtonState.Pressed;
			
			switch(xciBtn)
			{
				case XboxButton.A: 				stateToReturn = xiButtons.A; break;
				case XboxButton.B: 				stateToReturn = xiButtons.B; break;
				case XboxButton.X: 				stateToReturn = xiButtons.X; break;
				case XboxButton.Y: 				stateToReturn = xiButtons.Y; break;
				case XboxButton.Start: 			stateToReturn = xiButtons.Start; break;
				case XboxButton.Back: 			stateToReturn = xiButtons.Back; break;
				case XboxButton.LeftBumper: 	stateToReturn = xiButtons.LeftShoulder; break;
				case XboxButton.RightBumper: 	stateToReturn = xiButtons.RightShoulder; break;
				case XboxButton.LeftStick: 		stateToReturn = xiButtons.LeftStick; break;
				case XboxButton.RightStick: 	stateToReturn = xiButtons.RightStick; break;
			}
			
			return stateToReturn;
		}
		
		private static ButtonState XInputGetDPadState(GamePadDPad xiDPad, XboxDPad xciDPad)
		{
			ButtonState stateToReturn = ButtonState.Released;
			
			switch(xciDPad)
			{
				case XboxDPad.Up: 				stateToReturn = xiDPad.Up; break;
				case XboxDPad.Down: 			stateToReturn = xiDPad.Down; break;
				case XboxDPad.Left: 			stateToReturn = xiDPad.Left; break;
				case XboxDPad.Right: 			stateToReturn = xiDPad.Right; break;
			}
			
			return stateToReturn;
		}
		
		private static float XInputGetAxisState(GamePadTriggers xiTriggers, XboxAxis xciAxis)
		{
			float stateToReturn = 0.0f;
			
			switch(xciAxis)
			{
				case XboxAxis.LeftTrigger: 		stateToReturn = xiTriggers.Left; break;
				case XboxAxis.RightTrigger: 	stateToReturn = xiTriggers.Right; break;
				default:						stateToReturn = 0.0f; break;
			}
			
			return stateToReturn;
		}
		
		private static float XInputGetAxisState(GamePadThumbSticks xiThumbSticks, XboxAxis xciAxis)
		{
			float stateToReturn = 0.0f;
			
			switch(xciAxis)
			{
				case XboxAxis.LeftStickX: 		stateToReturn = xiThumbSticks.Left.X; break;
				case XboxAxis.LeftStickY: 		stateToReturn = xiThumbSticks.Left.Y; break;
				case XboxAxis.RightStickX: 		stateToReturn = xiThumbSticks.Right.X; break;
				case XboxAxis.RightStickY: 		stateToReturn = xiThumbSticks.Right.Y; break;
				default:						stateToReturn = 0.0f; break;
			}
			
			return stateToReturn;
		}
		
		private static bool XInputStillInCurrFrame()
		{
			bool r = false;
			
			// Get the current frame
			int currFrame = Time.frameCount;
			
			// Are we still in the current frame?
			if(xiPrevFrameCount == currFrame)
			{
				r = true;
			}
			else
			{
				r = false;
				xiUpdateAlreadyCalled = false;
			}
			
			// Assign the previous frame count regardless of whether it's the same or not.
			xiPrevFrameCount = currFrame;
			
			return r;
		}

	}
}
