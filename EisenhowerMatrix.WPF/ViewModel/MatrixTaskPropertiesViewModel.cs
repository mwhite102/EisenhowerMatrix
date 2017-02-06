using EisenhowerMatrix.WPF.DataAccess;
using EisenhowerMatrix.WPF.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace EisenhowerMatrix.WPF.ViewModel
{
    public class MatrixTaskPropertiesViewModel : ViewModelBase, IDataErrorInfo
    {
        private IDataService _DataService;
        private MatrixTask _MatrixTask;

        /// <summary>
        /// MatrixTaskPropertiesViewModel Constructor
        /// </summary>
        /// <param name="matrixTaskModel">The MatrixTask</param>
        public MatrixTaskPropertiesViewModel(MatrixTask matrixTask, IDataService dataService)
        {
            _DataService = dataService;
            _MatrixTask = matrixTask;
            Init();
        }

        /// <summary>
        /// Gets the ObservableCollection of Quadrants
        /// </summary>
        public ObservableCollection<Quadrant> Quadrants { get; } = new ObservableCollection<Quadrant>();

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

        #region Private Methods

        private void Init()
        {
            if (Quadrants.Count > 0)
            {
                Quadrants.Clear();
            }

            foreach(Quadrant q in _DataService.GetQuadrants().OrderBy(o => o.QuadrantDescription))
            {
                Quadrants.Add(q);
            }
        }


        private void DoSave()
        {
            try
            {
                // Is the item new and needs to be added to a quadrant?
                if (_MatrixTask.MatrixTaskId == 0)
                {
                    var quadrant = Quadrants.Where(o => o.QuadrantId == _MatrixTask.QuadrantId).FirstOrDefault();
                    if (quadrant != null)
                    {
                        quadrant.MatrixTasks.Add(_MatrixTask);
                    }

                    _MatrixTask.CreatedUTC = DateTime.UtcNow;
                }

                _DataService.SaveChanges();
                EditResultMessage msg = new EditResultMessage() { IsSaved = true };
                Messenger.Default.Send<EditResultMessage>(msg);
            }
            catch (Exception ex)
            {
                // TODO: Display Error Message
            }
        }

        #endregion

        #region Commands

        private ICommand _CancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand(CancelCommandExecuted);
                }
                return _CancelCommand;
            }
        }

        private void CancelCommandExecuted()
        {
            // Send message to MainViewModel to cancel editing
            EditResultMessage msg = new EditResultMessage();
            Messenger.Default.Send<EditResultMessage>(msg);
        }

        private ICommand _SaveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                    _SaveCommand = new RelayCommand(SaveCommandExecute, SaveCommandCanExecute);

                return _SaveCommand;
            }
        }

        private bool SaveCommandCanExecute()
        {
            return IsValid();
        }

        private void SaveCommandExecute()
        {
            DoSave();
        }

        #endregion

        #region IDataErrorInfo Implementation

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                if (columnName == "Description")
                {
                    if (String.IsNullOrWhiteSpace(this.Description))
                    {
                        result = "Please enter a task description";
                    }
                }

                if (columnName == "QuadrantId")
                {
                    if (this.QuadrantId == 0)
                    {
                        result = "Please select a quadrant";
                    }
                }

                return result;
            }
        }

        string[] FieldsToValidate = new string[]
        {
            "Description", "QuadrantId"
        };

        private bool IsValid()
        {
            foreach(var s in FieldsToValidate)
            {
                if (!string.IsNullOrEmpty(this[s]))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion        
    }
}
