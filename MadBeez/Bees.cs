using System;

namespace MadBeez {
	public sealed class WorkerBee : Bee {
		public WorkerBee() : base(70) { }
	}

	public sealed class DroneBee : Bee {
		public DroneBee() : base(50) { }
	}

	public sealed class QueenBee : Bee {
		public QueenBee() : base(20) { }
	}
}

