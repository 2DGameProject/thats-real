using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class MobileInteractButton : MonoBehaviour
{
    // Import the necessary Windows API functions
    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

    
    private const uint KEYEVENTF_KEYDOWN = 0x0000; // Key press
    private const uint KEYEVENTF_KEYUP = 0x0002;   // Key release

    [Tooltip("Set the virtual key code (e.g., 0x1B for ESC, 0x45 for E)")]
    public int virtualKeyCode = 0x45; // Default to ESC key
    public Canvas canvas;
    public void SimulateEKeyPress()
    {
        byte key = (byte)virtualKeyCode;

        // Simulate the key down
        keybd_event(key, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);

        // Simulate the key up
        keybd_event(key, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);

    }
}
