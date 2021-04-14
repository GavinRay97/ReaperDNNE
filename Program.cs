using System;
using System.Runtime.InteropServices;

namespace ReaperDNNE
{
  public unsafe static class MyReaperPlugin
  {

    private const string ReaperPluginInfoStructString = @"
      struct reaper_plugin_info_t
      {
        int caller_version;
        void* hwnd_main;
        int (*Register)(const char* name, void* infostruct);
        void* (*GetFunc)(const char* name);
      };
    ";

    public struct ReaperPluginInfo
    {
      public int caller_version;
      public IntPtr hwnd_main;
      public delegate* unmanaged[Cdecl]<sbyte*, void*, int> Register;
      public delegate* unmanaged[Cdecl]<sbyte*, void*> GetFunc;
    }

    [UnmanagedCallersOnly]
    [DNNE.C99DeclCode(ReaperPluginInfoStructString)]
    public static int ReaperPluginEntry(IntPtr hInstance, [DNNE.C99Type("struct reaper_plugin_info_t*")] ReaperPluginInfo* rec)
    {
      var showConsoleMsgStrPtr = Marshal.StringToHGlobalAnsi("ShowConsoleMsg");
      var ShowConsoleMsg = (delegate* unmanaged[Cdecl]<sbyte*, void>)
        rec->GetFunc((sbyte*)showConsoleMsgStrPtr);
      Marshal.FreeHGlobal(showConsoleMsgStrPtr);

      var helloMsgStrPtr = Marshal.StringToHGlobalAnsi("Hello From C#");
      ShowConsoleMsg((sbyte*)helloMsgStrPtr);
      Marshal.FreeHGlobal(helloMsgStrPtr);

      return 1;
    }
  }
}