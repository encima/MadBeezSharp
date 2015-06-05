using System;
using Gtk;

namespace MadBeez {
	class MainClass {
		public static void Main (string[] args) {
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Console.WriteLine (AppDomain.CurrentDomain.BaseDirectory);
			Application.Run ();
		}
	}
}
