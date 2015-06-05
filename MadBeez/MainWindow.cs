using System;
using Gtk;
using System.Collections.Generic;
using MadBeez;

public partial class MainWindow: Gtk.Window {

	List<Bee> bees = new List<Bee> (0);
	Gtk.ListStore queenBeeStore;
	Gtk.ListStore workerBeeStore;
	Gtk.ListStore droneBeeStore;
	Random rand;

	public MainWindow () : base (Gtk.WindowType.Toplevel) {
		Build ();

		//create instance of random to generate numbers later
		rand = new Random ();

		//set up columns for tree views (one for each bee type)
		queenTreeView.AppendColumn ("Icon", new Gtk.CellRendererPixbuf (), "pixbuf", 0);
		queenTreeView.AppendColumn ("Health", new Gtk.CellRendererText (), "text", 1);
		queenTreeView.AppendColumn ("Dead?", new Gtk.CellRendererText (), "text", 2);

		workerTreeView.AppendColumn ("Icon", new Gtk.CellRendererPixbuf (), "pixbuf", 0);
		workerTreeView.AppendColumn ("Health", new Gtk.CellRendererText (), "text", 1);
		workerTreeView.AppendColumn ("Dead?", new Gtk.CellRendererText (), "text", 2);

		droneTreeView.AppendColumn ("Icon", new Gtk.CellRendererPixbuf (), "pixbuf", 0);
		droneTreeView.AppendColumn ("Health", new Gtk.CellRendererText (), "text", 1);
		droneTreeView.AppendColumn ("Dead?", new Gtk.CellRendererText (), "text", 2);

		//set up data store
		queenBeeStore = new Gtk.ListStore (typeof (Gdk.Pixbuf),
			typeof (string),  typeof (string));
		workerBeeStore = new Gtk.ListStore (typeof (Gdk.Pixbuf),
			typeof (string),  typeof (string));
		droneBeeStore = new Gtk.ListStore (typeof (Gdk.Pixbuf),
			typeof (string),  typeof (string));

		//assign data stores to tree views
		queenTreeView.Model = queenBeeStore;
		workerTreeView.Model = workerBeeStore;
		droneTreeView.Model = droneBeeStore;
		// set up list of bees.
		Console.WriteLine (AppDomain.CurrentDomain.BaseDirectory);
		setupBees();
	}

	public void setupBees() {

		bees.Clear();
		// create bees and dummy data for the columns before damage is clicked
		for(int i = 0; i < 10; i++) {
			bees.Add(new DroneBee());
			bees.Add(new WorkerBee());
			bees.Add(new QueenBee());
			//relative path but main app dir is in bin/debug or bin/release. Probably a better way of doing this that remains cross plat
			queenBeeStore.AppendValues (new Gdk.Pixbuf ("../../icons/queen_gb.png"),
				"100%", "Not dead yet");
			workerBeeStore.AppendValues (new Gdk.Pixbuf ("../../icons/worker.png"),
				"100%", "Not dead yet");
			droneBeeStore.AppendValues (new Gdk.Pixbuf ("../../icons/drone.png"),
				"100%", "Not dead yet");
		}
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a) {
		Application.Quit();
		a.RetVal = true;
	}

	protected virtual void OnButtonDamageClicked(object sender, System.EventArgs e) {
		//empty data stores
		queenBeeStore.Clear();
		droneBeeStore.Clear();
		workerBeeStore.Clear();
		bool allDead = true;
		//iterate bee list and add updated values to data stores based on type
		foreach (Bee b in bees) {
			int damage = rand.Next (100);
			float health = b.damage (damage);
			if (!b.isDead ())
				allDead = false;
			String dead = b.isDead() ? "You killed it!" : "Not dead yet";
			//update label based on Bee type
			switch(b.GetType().ToString()) {
				case "MadBeez.QueenBee":
					queenBeeStore.AppendValues(new Gdk.Pixbuf ("/home/encima/Development/java/MadBeez/icons/queen_gb.png"),
					health + "%", dead);
					break;
				case "MadBeez.WorkerBee":
					workerBeeStore.AppendValues (new Gdk.Pixbuf ("/home/encima/Development/java/MadBeez/icons/worker.png"),
					health + "%", dead);
					break;
				case "MadBeez.DroneBee":
					droneBeeStore.AppendValues (new Gdk.Pixbuf ("/home/encima/Development/java/MadBeez/icons/drone.png"),
					health + "%", dead);
					break;
			}
		}
		//reset if all bees are dead
		//TODO: Provide option to quit
		if (allDead) {
			MessageDialog md = new MessageDialog(this, 
				DialogFlags.DestroyWithParent, MessageType.Info, 
				ButtonsType.Close, "You..you killed them all. Wwwhhhhyyy?!? \n I guess you will have to start again.");
			md.Run();
			md.Destroy();
			queenBeeStore.Clear();
			droneBeeStore.Clear();
			workerBeeStore.Clear();
			setupBees();
		}
	}
}
