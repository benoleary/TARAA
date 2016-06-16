using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TARAA
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      //Application.EnableVisualStyles();
      // Just checking Git through Visual Studio 2015.
      Application.SetCompatibleTextRenderingDefault( false );
      Application.Run( new MainForm() );
    }
  }
}
