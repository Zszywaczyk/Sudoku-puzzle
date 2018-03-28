using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory1
{
	public class PuzzelState : State
	{
		public PuzzelState(PuzzelState parent /*pozostale parametry*/ ) : base(parent)
		{
			//cialo konstruktora
			this.h = ComputeHeuristicGrade();
			//W stanie potomnym droga ktora przebylismy 
			//jest o jeden wieksza niz w rodzicu
			this.g = parent.g + 1;
		}

		public override string ID => throw new NotImplementedException();

		public override double ComputeHeuristicGrade()
		{
			throw new NotImplementedException();
		}
	}
}
