using System.Collections.Generic;

namespace EisenhowerMatrix.WPF.DataAccess
{
    public interface IDataService
    {
        int UndoDeleteCount { get; }

        bool DeleteMatrixTask(int matrixTaskId);

        List<Quadrant> GetQuadrants();

        MatrixTask GetMatrixTaskById(int matrixTaskId);

        List<MatrixTask> GetMatrixTasksByQuadrant(int quadrantId);

        MatrixTask NewMatrixTask();

        int SaveChanges();

        bool UndoDelete();
    }
}
