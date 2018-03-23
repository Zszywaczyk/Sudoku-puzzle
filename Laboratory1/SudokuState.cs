using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory1
{
	public class SudokuState : State
	{
		public const int SMALL_GRID_SIZE = 3;
		public const int GRID_SIZE = SMALL_GRID_SIZE * SMALL_GRID_SIZE;
		private int[,] table;
		public int[,] Table {
			get { return this.table; }
			set { this.table = value; } 11 } 12 13 // reszta implementacj


		private string id;


		public override string ID
		{
			get { return this.id; }
		}

		public override double ComputeHeuristicGrade()
		{
			 throw new NotImplementedException();
		 }
		public void Print() {

		}

	}
}
