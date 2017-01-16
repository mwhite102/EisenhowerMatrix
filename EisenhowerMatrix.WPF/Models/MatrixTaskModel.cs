using GalaSoft.MvvmLight;
using System;
using System.ComponentModel.DataAnnotations;
using EisenhowerMatrix.WPF.DataAccess;

namespace EisenhowerMatrix.WPF.Models
{
    public class MatrixTaskModel : ObservableObject
    {
        private MatrixTask _MatrixTask;

        /// <summary>
        /// MatrixTaskModel Constructor
        /// </summary>
        /// <param name="matrixTask">The MatrixTask</param>
        public MatrixTaskModel(MatrixTask matrixTask)
        {
            _MatrixTask = matrixTask;
        }

        /// <summary>
        /// Gets the enclosed MatrixTask
        /// </summary>
        public MatrixTask MatrixTask
        {
            get
            {
                return _MatrixTask;
            }
        }

        /// <summary>
        /// Gets or sets the MatrixTaskId
        /// </summary>
        public int MatrixTaskId
        {
            get { return _MatrixTask.MatrixTaskId; }
            set
            {
                if (_MatrixTask.MatrixTaskId != value)
                {
                    _MatrixTask.MatrixTaskId = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        [Required]
        public string Description
        {
            get { return _MatrixTask.Description; }
            set
            {
                if (_MatrixTask.Description != value)
                {
                    _MatrixTask.Description = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the CreatedUTC
        /// </summary>
        public DateTime CreatedUTC
        {
            get { return _MatrixTask.CreatedUTC; }
            set
            {
                if (_MatrixTask.CreatedUTC != value)
                {
                    _MatrixTask.CreatedUTC = value;
                    RaisePropertyChanged();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the CompletedUTC
        /// </summary>
        public DateTime? CompletedUTC
        {
            get { return _MatrixTask.CompletedUTC; }
            set
            {
                if (_MatrixTask.CompletedUTC != value)
                {
                    _MatrixTask.CompletedUTC = value;
                    RaisePropertyChanged();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the QuadrantId
        /// </summary>
        public int QuadrantId
        {
            get { return _MatrixTask.QuadrantId; }
            set
            {
                if (_MatrixTask.QuadrantId != value)
                {
                    _MatrixTask.QuadrantId = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
