using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    public class Grid
    {
        public delegate void GridAltered(Crossing alteredCrossing, int xPosition, int yPosition);
        public event GridAltered OnCrossingAdded = (x, y, z) => { };
        public event GridAltered OnCrossingRemoved = (x, y, z) => { };

        public Crossing[][] Crossings { get; private set; }
        public IEnumerable<Crossing> AllCrossings
        {
            get
            {
                List<Crossing> crossings = new List<Crossing>();

                foreach (Crossing[] row in this.Crossings)
                {
                    crossings.AddRange(row);
                }

                return crossings;
            }
        }

        private int rowsSize = -1;
        public int Rows { get { return rowsSize; } }
        private int columnsSize = -1;
        public int Columns { get { return this.columnsSize; } }

        public Grid(int matrixSize) : this(matrixSize, matrixSize)
        {
        }

        public Grid(int rows, int columns)
        {
            ValidateCanUse(rows, columns);

            this.rowsSize = rows;
            this.columnsSize = columns;
            this.Crossings = new Crossing[rows][];
            for (int i = 0; i < rows; i++)
            {
                this.Crossings[i] = new Crossing[columns];
            }
        }

        public void AddAt(int row, int column, Crossing crossing)
        {
            ValidateCanUse(row, column);

            if (this[row][column] != null)
                RemoveAt(row, column);
            throw new NotImplementedException();

            crossing = crossing.CreateCopy();
            crossing.AssignGridLocation(row, column);
            this.Crossings[row][column] = crossing;

            OnCrossingAdded(crossing, row, column);
        }

        public void RemoveAt(int row, int column)
        {
            ValidateCanUse(row, column);

            throw new NotImplementedException();

            Crossing crossing = this[row][column];
            crossing.RemoveFromGrid();
            OnCrossingRemoved(crossing, row, column);
        }

        public Crossing[] this[int row]
        {
            get
            {
                if (row < 0) return null;
                if (row >= this.Rows) return null;
                return this.Crossings[row];
            }
        }

        /// <summary>checks if a row and column are within the bounds of the grid</summary>
        private void ValidateCanUse(int row, int column)
        {
            if (row < 0) throw new InvalidOperationException("Cannot use a value < 0 for a row");
            if (column < 0) throw new InvalidOperationException("Cannot use a value < 0 for a column");
            if (this.Rows != -1) if (this.rowsSize <= row) throw new InvalidOperationException("Row specified is out of Range");
            if (this.Columns != -1) if (this.Columns <= row) throw new InvalidOperationException("Column specified is out of Range");
        }
    }
}
