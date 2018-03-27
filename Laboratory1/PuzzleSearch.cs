using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory1
{
    class PuzzleSearch : AStarSearch
    {
        public PuzzleSearch(IState initialState, int numberOfSolutionsToFind = 1) : base(initialState, numberOfSolutionsToFind)
        {
        }

        protected override void buildChildren(IState parent)
        {
            PuzzleState state = (PuzzleState)parent;

            state.checkDirections();

            for (int i = 0; i < 4; i++)
            {
                if (state.Directions_LTRB[i])
                {
                    PuzzleState child = new PuzzleState(state, i);
                    parent.Children.Add(child);
                }
            }
            //return;
        }

        protected override bool isSolution(IState state)
        {
            return state.H == 0.0;
        }
    }
}
