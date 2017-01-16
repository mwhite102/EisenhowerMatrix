using GalaSoft.MvvmLight;
using System;
using System.ComponentModel.DataAnnotations;

namespace EisenhowerMatrix.WPF.Models
{
    public class MatrixTaskModel : ObservableObject
    {
        private int _MatrixTaskId;

        /// <summary>
        /// Gets or sets the MatrixTaskId
        /// </summary>
        public int MatrixTaskId
        {
            get { return _MatrixTaskId; }
            set
            {
                if (_MatrixTaskId != value)
                {
                    _MatrixTaskId = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _Description;

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        [Required]
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    RaisePropertyChanged();
                }
            }
        }

        private DateTime _CreatedUTC;

        /// <summary>
        /// Gets or sets the CreatedUTC
        /// </summary>
        public DateTime CreatedUTC
        {
            get { return _CreatedUTC; }
            set
            {
                if (_CreatedUTC != value)
                {
                    _CreatedUTC = value;
                    RaisePropertyChanged();
                }
            }
        }

        private DateTime _CompletedUTC;

        /// <summary>
        /// Gets or sets the CompletedUTC
        /// </summary>
        public DateTime CompletedUTC
        {
            get { return _CompletedUTC; }
            set
            {
                if (_CompletedUTC != value)
                {
                    _CompletedUTC = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _QuadrantId;

        /// <summary>
        /// Gets or sets the QuadrantId
        /// </summary>
        public int QuadrantId
        {
            get { return _QuadrantId; }
            set
            {
                if (_QuadrantId != value)
                {
                    _QuadrantId = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
