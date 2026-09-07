using CafeOrderingSystem.Forms;

namespace CafeOrderingSystem;

internal static class Program
{
    /// <summary>
    /// Entry point. [STAThread] sets a Single-Threaded Apartment, which
    /// WinForms requires for COM interop (clipboard, dialogs, drag-drop).
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Application.Run starts the Windows MESSAGE LOOP: the loop that pulls
        // events from the OS queue and dispatches them to controls. In an
        // event-driven program this IS the repetition construct — you never
        // write the WHILE loop yourself.
        Application.Run(new MainForm());
    }
}
