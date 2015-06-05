using System;

namespace MadBeez {
	public abstract class Bee {
		private int deathThreshold { get; set;}
		private float health { get; set; }

		public Bee (int deathThreshold) {
			this.health = 100;
			this.deathThreshold = deathThreshold;
		}

		public bool isDead() {
			if (this.health < this.deathThreshold)
				return true;
			else
				return false;
		}

		public float damage(int damPercent) {
			//only hurt the poor thing if it is still alive
			if (!isDead ()) {
				this.health = this.health - damPercent;
				//if health drops below 0, reset it back.
				if (this.health < 0)
					this.health = 0;
			}
			return this.health;
		}


	}
}

