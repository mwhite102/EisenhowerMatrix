using System;
using System.Collections.Generic;
using System.Linq;

namespace EisenhowerMatrix.WPF.DataAccess
{
    class DataService : IDataService
    {
        private EisenhowerMatrixEntities _Context;

        private CustomStack<MatrixTask> _UndoDeleteStack = new CustomStack<MatrixTask>(100);

        /// <summary>
        /// DataService Constructor
        /// </summary>
        /// <param name="eisenhowerMatrixEntities">The EisenhowerMatrixEntities DbContext</param>
        public DataService(EisenhowerMatrixEntities eisenhowerMatrixEntities)
        {
            _Context = eisenhowerMatrixEntities;
        }

        #region Public Properties

        /// <summary>
        /// Gets the number of items that can be undeleted
        /// </summary>
        public int UndoDeleteCount
        {
            get
            {
                return _UndoDeleteStack.Count;
            }
        }

        #endregion
        
        #region Public Mtehods

        /// <summary>
        /// Deletes a MatrixTask
        /// </summary>
        /// <returns>True if deleted, false if not</returns>
        public bool DeleteMatrixTask(int matrixTaskId)
        {
            bool result = false;
            MatrixTask matrixTask = this.GetMatrixTaskById(matrixTaskId);
            if (matrixTask != null)
            {
                // Push the item onto the Undo Stack
                _UndoDeleteStack.Push(matrixTask);
                _Context.MatrixTasks.Remove(matrixTask);
                result = true;
                _Context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Gets a list of Quadrants ordered by QuadrantDescription
        /// </summary>
        /// <returns>A list of Quadrants</returns>
        public List<Quadrant> GetQuadrants()
        {
            return _Context.Quadrants
                .OrderBy(o => o.QuadrantDescription)
                .ToList();
        }

        /// <summary>
        /// Gets a MatrixTask with a specified MatrixTaskId
        /// </summary>
        /// <param name="matrixTaskId">The MatrixTaskId value</param>
        /// <returns>A MatrixTask with a specified MatrixTaskId, or NULL if the item doesn't exist</returns>
        public MatrixTask GetMatrixTaskById(int matrixTaskId)
        {
            return _Context.MatrixTasks
                .Where(o => o.MatrixTaskId == matrixTaskId)
                .SingleOrDefault();
        }

        /// <summary>
        /// Gets a list of MatrixTask objects with the specified QuadrantId value
        /// </summary>
        /// <param name="quadrantId">The QuadrantId value</param>
        /// <returns>A list of MatrixTask objects</returns>
        public List<MatrixTask> GetMatrixTasksByQuadrant(int quadrantId)
        {
            return _Context.MatrixTasks
                .Where(o => o.QuadrantId == quadrantId)
                .OrderBy(o => o.Description)
                .ToList();
        }

        /// <summary>
        /// Creates a new MatrixTask
        /// </summary>
        /// <returns>A new MatrixTask</returns>
        public MatrixTask NewMatrixTask()
        {
            return new MatrixTask();
        }

        /// <summary>
        /// Save any changes to the underlying Database
        /// </summary>
        /// <returns>The number of objects written to the underlying database</returns>
        public int SaveChanges()
        {
            return _Context.SaveChanges();
        } 

        public bool UndoDelete()
        {
            bool result = false;

            if (UndoDeleteCount == 0)
            {
                throw new InvalidOperationException("There are no items to be undeleted");
            }

            // Get the item to be undeleted
            MatrixTask matrixTask = _UndoDeleteStack.Pop();
            // Set the MatrixTaskId to 0 (like a new item)
            matrixTask.MatrixTaskId = 0;

            _Context.MatrixTasks.Add(matrixTask);

            try
            {
                this.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        #endregion
    }
}
