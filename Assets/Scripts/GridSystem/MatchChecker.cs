using System.Collections.Generic;
using TMPro;

namespace GridSystem
{
    public class MatchChecker
    {
        private Dictionary<Coordinate, Cell> _cellsWithCross = new();

        private readonly TextMeshProUGUI _matchCountText;

        private int _matchCount;

        public MatchChecker(TextMeshProUGUI matchCountText)
        {
            _matchCountText = matchCountText;
        }

        public void AddCrossedCell(Cell cell)
        {
            if(_cellsWithCross.ContainsKey(cell.Coordinate))
                return;
            
            _cellsWithCross.Add(cell.Coordinate, cell);
            ConnectToNeighbours(cell);
        }

        //If there is a match resets cells
        public void CheckForAMatch(Cell cellCrossed)
        {
            var connectedCellCount = cellCrossed.GetConnectedCellCount();
            if (connectedCellCount >= 3)
            {
                cellCrossed.RemoveConnections(ref _cellsWithCross);
                _matchCount++;
                SetMatchCountText();
            }
        }
        
        public void Reset()
        {
            _matchCount = 0;
            _cellsWithCross.Clear();
            SetMatchCountText();
        }
        
        private void ConnectToNeighbours(Cell cellCrossed)
        {
            var neighbourCoordinates = cellCrossed.NeighbourCoordinates;
            for (int i = 0; i < neighbourCoordinates.Length; i++)
            {
                var neighbourCoordinate = neighbourCoordinates[i];
                if (_cellsWithCross.TryGetValue(neighbourCoordinate, out var neighbourCell))
                {
                    cellCrossed.AddNeighbour(neighbourCell);
                    neighbourCell.AddNeighbour(cellCrossed);
                }
            }
        }

        private void SetMatchCountText()
        {
            _matchCountText.text = "Match Count : " + _matchCount;
        }
    }
}
