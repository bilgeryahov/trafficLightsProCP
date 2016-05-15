using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    /// <summary>
    /// represents the NxN grid within the system
    /// </summary>
    [Serializable]
    public class Grid
    {
        public System.Drawing.Point PointFromSlotID(int slotID) { return new System.Drawing.Point(slotID % 3, slotID / 3); }
        public Crossing CrossingAt(int slotID)
        {
            System.Drawing.Point point = PointFromSlotID(slotID);
            return Crossings[point.Y][point.X];
        }

        /// <summary>
        /// Delegate GridAltered
        /// </summary>
        /// <param name="alteredCrossing">The altered crossing.</param>
        /// <param name="xPosition">The x position.</param>
        /// <param name="yPosition">The y position.</param>
        public delegate void GridAltered(Crossing alteredCrossing, int xPosition, int yPosition);
        /// <summary>
        /// Occurs when [on crossing added].
        /// </summary>
        public event GridAltered OnCrossingAdded = (x, y, z) => { };
        /// <summary>
        /// Occurs when [on crossing removed].
        /// </summary>
        public event GridAltered OnCrossingRemoved = (x, y, z) => { };

        /// <summary>
        /// Gets the crossings.
        /// </summary>
        /// <value>The crossings.</value>
        public Crossing[][] Crossings { get; private set; }
        /// <summary>
        /// Gets all crossings.
        /// </summary>
        /// <value>All crossings.</value>
        public IEnumerable<Crossing> AllCrossings
        {
            get
            {
                List<Crossing> crossings = new List<Crossing>();

                foreach (Crossing[] row in this.Crossings)
                    crossings.AddRange(row);

                return crossings;
            }
        }

        /// <summary>
        /// The rows size
        /// </summary>
        private int rowsSize = -1;
        /// <summary>
        /// Gets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public int Rows { get { return rowsSize; } }
        /// <summary>
        /// The columns size
        /// </summary>
        private int columnsSize = -1;
        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <value>The columns.</value>
        public int Columns { get { return this.columnsSize; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="matrixSize">Size of the matrix.</param>
        public Grid(int matrixSize) : this(matrixSize, matrixSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
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

        /// <summary>
        /// Adds at.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="crossing">The crossing.</param>
        public void AddAt(int row, int column, Crossing crossing)
        {
            ValidateCanUse(row, column);

            if (this[row][column] != null)
                RemoveAt(row, column);

            crossing = crossing.CreateCopy();
            crossing.AssignGridLocation(row, column);
            this.Crossings[row][column] = crossing;

            OnCrossingAdded(crossing, row, column);
        }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public void RemoveAt(int row, int column)
        {
            ValidateCanUse(row, column);

            Crossing crossing = this[row][column];
            this.Crossings[row][column] = null;
            crossing.RemoveFromGrid();
            OnCrossingRemoved(crossing, row, column);
        }

        /// <summary>
        /// Gets the <see cref="Crossing[]"/> with the specified row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>Crossing[].</returns>
        public Crossing[] this[int row]
        {
            get
            {
                if (row < 0) return null;
                if (row >= this.Rows) return null;
                return this.Crossings[row];
            }
        }

        /// <summary>
        /// checks if a row and column are within the bounds of the grid
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        private void ValidateCanUse(int row, int column)
        {
            if (row < 0) throw new InvalidOperationException("Cannot use a value < 0 for a row");
            if (column < 0) throw new InvalidOperationException("Cannot use a value < 0 for a column");
            if (this.Rows != -1) if (this.rowsSize <= row) throw new InvalidOperationException("Row specified is out of Range");
            if (this.Columns != -1) if (this.Columns <= row) throw new InvalidOperationException("Column specified is out of Range");
        }

        /// <summary>
        /// Checks the availability.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public bool CheckAvailability(int row,int column)
        {
            throw new System.NotImplementedException();
        }
    }
}
