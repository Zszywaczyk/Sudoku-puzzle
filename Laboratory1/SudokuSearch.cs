using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory1
{
	public class SudokuSearch : BestFirstSearch
	{
        private bool useAdvancedHeuristic;


        public SudokuSearch(SudokuState state, bool advancedHeuristic = false) : base(state)
        {
            useAdvancedHeuristic = advancedHeuristic;

			state.findBeginField(useAdvancedHeuristic);
		}

		protected override void buildChildren(IState parent)
        {
            SudokuState state = (SudokuState)parent;

			state.findBeginField(useAdvancedHeuristic);

            for (int k = 1; k < SudokuState.GRID_SIZE + 1; k++)
            {
				//Console.Write(String.Format("k: {0}, xy({1},{2})\n", k, state.xyBegin[0], state.xyBegin[1]));
				if (state.isNumberCorrect(k, state.xyBegin[0], state.xyBegin[1]))
                {
                    SudokuState child = new SudokuState(state, k, state.xyBegin[0], state.xyBegin[1]);
                    parent.Children.Add(child);
				}
            }
            return;
        }

        

        

        protected override bool isSolution(IState state)
        {
            return state.H == 0.0;
        }
    }
}
