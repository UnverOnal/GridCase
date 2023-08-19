using System.Collections.Generic;

namespace GridSystem
{
    public class Connection
    {
        private Coordinate Coordinate => _cell.Coordinate;
        private readonly List<Cell> _crossedNeighbours = new ();

        private readonly Cell _cell;

        public Connection(Cell cell)
        {
            _cell = cell;
        }

        public void AddNeighbour(Cell neighbour)
        {
            if(!_crossedNeighbours.Contains(neighbour))
                _crossedNeighbours.Add(neighbour);
        }

        public void Remove(ref Dictionary<Coordinate, Cell> cellsWithCross, Cell exception = null)
        {
            cellsWithCross.Remove(Coordinate);
            for (int i = 0; i < _crossedNeighbours.Count; i++)
            {
                var neighbour = _crossedNeighbours[i];
                
                if(exception == neighbour)
                    continue;
                
                neighbour.RemoveConnections(ref cellsWithCross, _cell);
            }
            _crossedNeighbours.Clear();
        }

        public int GetConnectedCellCount(Cell exception = null)
        {
            int count = 1;//Starts with 1 because there is itself.
            for (int i = 0; i < _crossedNeighbours.Count; i++)
            {
                var neighbour = _crossedNeighbours[i];
                
                if(exception == neighbour)
                    continue;
                
                count += neighbour.GetConnectedCellCount( _cell);
            }

            return count;
        }

        public void Reset()
        {
            _crossedNeighbours.Clear();
        }
        
        public Coordinate[] GetNeighbourCellCoordinates()
        {
            var coordinates = new Coordinate[4];
            
            coordinates[0] = new Coordinate { Row = Coordinate.Row, Column = Coordinate.Column + 1};
            coordinates[1] = new Coordinate { Row = Coordinate.Row, Column = Coordinate.Column - 1};
            coordinates[2] = new Coordinate { Row = Coordinate.Row + 1, Column = Coordinate.Column};
            coordinates[3] = new Coordinate { Row = Coordinate.Row - 1, Column = Coordinate.Column};

            return coordinates;
        }
    }
}
