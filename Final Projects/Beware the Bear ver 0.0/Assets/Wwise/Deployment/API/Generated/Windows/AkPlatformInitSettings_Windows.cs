#if UNITY_STANDALONE_WIN || (UNITY_EDITOR_WIN && UNITY_STANDALONE_WIN) || (UNITY_EDITOR_WIN && UNITY_XBOX360) || (UNITY_EDITOR_WIN && UNITY_XBOXONE) || (UNITY_EDITOR_WIN && UNITY_PS3) || (UNITY_EDITOR_WIN && UNITY_PS4) || (UNITY_EDITOR_WIN && UNITY_ANDROID) || (UNITY_EDITOR_WIN && UNITY_METRO) || (UNITY_EDITOR_WIN && UNITY_PSP2) || (UNITY_EDITOR_WIN && UNITY_STANDALONE_LINUX) || (UNITY_EDITOR_WIN && UNITY_WP8) || (UNITY_EDITOR_WIN && UNITY_WIIU)
/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.11
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class AkPlatformInitSettings : IDisposable {
  private IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal AkPlatformInitSettings(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static IntPtr getCPtr(AkPlatformInitSettings obj) {
    return (obj == null) ? IntPtr.Zero : obj.swigCPtr;
  }

  ~AkPlatformInitSettings() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkPlatformInitSettings(swigCPtr);
        }
        swigCPtr = IntPtr.Zero;
      }
      GC.SuppressFinalize(this);
    }
  }

  public AkThreadProperties threadLEngine {
    set {
      AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_threadLEngine_set(swigCPtr, AkThreadProperties.getCPtr(value));

    } 
    get {
      IntPtr cPtr = AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_threadLEngine_get(swigCPtr);
      AkThreadProperties ret = (cPtr == IntPtr.Zero) ? null : new AkThreadProperties(cPtr, false);

      return ret;
    } 
  }

  public AkThreadProperties threadBankManager {
    set {
      AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_threadBankManager_set(swigCPtr, AkThreadProperties.getCPtr(value));

    } 
    get {
      IntPtr cPtr = AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_threadBankManager_get(swigCPtr);
      AkThreadProperties ret = (cPtr == IntPtr.Zero) ? null : new AkThreadProperties(cPtr, false);

      return ret;
    } 
  }

  public AkThreadProperties threadMonitor {
    set {
      AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_threadMonitor_set(swigCPtr, AkThreadProperties.getCPtr(value));

    } 
    get {
      IntPtr cPtr = AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_threadMonitor_get(swigCPtr);
      AkThreadProperties ret = (cPtr == IntPtr.Zero) ? null : new AkThreadProperties(cPtr, false);

      return ret;
    } 
  }

  public uint uLEngineDefaultPoolSize {
    set {
      AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_uLEngineDefaultPoolSize_set(swigCPtr, value);

    } 
    get {
      uint ret = AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_uLEngineDefaultPoolSize_get(swigCPtr);

      return ret;
    } 
  }

  public float fLEngineDefaultPoolRatioThreshold {
    set {
      AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_fLEngineDefaultPoolRatioThreshold_set(swigCPtr, value);

    } 
    get {
      float ret = AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_fLEngineDefaultPoolRatioThreshold_get(swigCPtr);

      return ret;
    } 
  }

  public ushort uNumRefillsInVoice {
    set {
      AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_uNumRefillsInVoice_set(swigCPtr, value);

    } 
    get {
      ushort ret = AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_uNumRefillsInVoice_get(swigCPtr);

      return ret;
    } 
  }

  public AkSoundQuality eAudioQuality {
    set {
      AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_eAudioQuality_set(swigCPtr, (int)value);

    } 
    get {
      AkSoundQuality ret = (AkSoundQuality)AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_eAudioQuality_get(swigCPtr);

      return ret;
    } 
  }

  public bool bGlobalFocus {
    set {
      AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_bGlobalFocus_set(swigCPtr, value);

    } 
    get {
      bool ret = AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_bGlobalFocus_get(swigCPtr);

      return ret;
    } 
  }

  public AkPlatformInitSettings() : this(AkSoundEnginePINVOKE.CSharp_new_AkPlatformInitSettings(), true) {

  }

}
#endif // #if UNITY_STANDALONE_WIN || (UNITY_EDITOR_WIN && UNITY_STANDALONE_WIN) || (UNITY_EDITOR_WIN && UNITY_XBOX360) || (UNITY_EDITOR_WIN && UNITY_XBOXONE) || (UNITY_EDITOR_WIN && UNITY_PS3) || (UNITY_EDITOR_WIN && UNITY_PS4) || (UNITY_EDITOR_WIN && UNITY_ANDROID) || (UNITY_EDITOR_WIN && UNITY_METRO) || (UNITY_EDITOR_WIN && UNITY_PSP2) || (UNITY_EDITOR_WIN && UNITY_STANDALONE_LINUX) || (UNITY_EDITOR_WIN && UNITY_WP8) || (UNITY_EDITOR_WIN && UNITY_WIIU)
