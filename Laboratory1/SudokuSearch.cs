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

        private int numOfPossibilities;
        private int[] xyBegin = { 0, 0 };

        public SudokuSearch(SudokuState state, bool advancedHeuristic = false) : base(state)
        {
            useAdvancedHeuristic = advancedHeuristic;

            setBeginning(0, 0, SudokuState.GRID_SIZE);
        }

        protected override void buildChildren(IState parent)
        {
            SudokuState state = (SudokuState)parent;

            findBeginField(state);

            for (int k = 1; k < SudokuState.GRID_SIZE + 1; k++)
            {
                if (state.isNumberCorrect(k, xyBegin[0], xyBegin[1]))
                {
                    SudokuState child = new SudokuState(state, k, xyBegin[0], xyBegin[1]);
                    parent.Children.Add(child);

                    if (useAdvancedHeuristic)
                    {
                        setBeginning(0, 0, SudokuState.GRID_SIZE);
                    }
                }
            }
            return;
        }

        private void findBeginField(SudokuState state)
        {
            for (int i = 0; i < SudokuState.GRID_SIZE; i++)
            {
                for (int j = 0; j < SudokuState.GRID_SIZE; j++)
                {
                    if (state.Table[i, j] == 0)
                    {
                        if (!useAdvancedHeuristic)
                        {
                            setBeginning(i, j, SudokuState.GRID_SIZE);
                        }
                        else
                        {
                            int possibilities = 0;
                            for (int k = 1; k < SudokuState.GRID_SIZE + 1; k++)
                            {
                                if (state.isNumberCorrect(k, i, j))
                                {
                                    possibilities++;
                                }
                            }
                            if (possibilities < numOfPossibilities)
                            {
                                setBeginning(i, j, possibilities);
                            }
                        }
                    }
                }
            }
        }

        private void setBeginning(int x, int y, int numOfPossibilities)
        {
            xyBegin[0] = x;
            xyBegin[1] = y;

            this.numOfPossibilities = numOfPossibilities;
        }

        protected override bool isSolution(IState state)
        {
            return state.H == 0.0;
        }
    }
}
