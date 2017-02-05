using EisenhowerMatrix.WPF.DataAccess;
using EisenhowerMatrix.WPF.Models;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using EisenhowerMatrix.WPF.DialogService;
using GalaSoft.MvvmLight.Messaging;
using EisenhowerMatrix.WPF.Messages;

namespace EisenhowerMatrix.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        const int IMPORTANT_AND_URGENT_QUADRANT_ID = 1;
        const int IMPORTANT_AND_NOT_URGENT_QUADRANT_ID = 2;
        const int NOT_IMPORTANT_AND_URGENT_QUADRANT_ID = 3;
        const int NOT_IMPORTANT_AND_NOT_URGENT_QUADRANT_ID = 4;

        private IDataService _DataService;
        private IMatrixDialogService _DialogService;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService, IMatrixDialogService dialogService)
        {
            _DataService = dataService;
            _DialogService = dialogService;

            Init();
            LoadMatrixTask();
        }

        /// <summary>
        /// Gets the ImportantAndNotUrgentMatrixTask Collection
        /// </summary>
        public ObservableCollection<MatrixTaskItemModel> ImportantAndNotUrgentMatrixTasks { get; private set; }

        /// <summary>
        /// Gets the ImportantAndUrgentMatrixTask Collection
        /// </summary>
        public ObservableCollection<MatrixTaskItemModel> ImportantAndUrgentMatrixTasks { get; private set; }

        /// <summary>
        /// Gets the NotImportantAndNotUrgentMatrixTask Collection
        /// </summary>
        public ObservableCollection<MatrixTaskItemModel> NotImportantAndNotUrgentMatrixTasks { get; private set; }

        /// <summary>
        /// Get the NotImportantAndUrgentMatrixTask Collection
        /// </summary>
        public ObservableCollection<MatrixTaskItemModel> NotImportantAndUrgentMatrixTasks { get; private set; }

        private MatrixTaskItemModel _SelectedMatrixTaskItemModel;

        /// <summary>
        /// Gets or sets the SelectedMatrixTaskItemModel
        /// </summary>
        public MatrixTaskItemModel SelectedMatrixTaskItemModel
        {
            get { return _SelectedMatrixTaskItemModel; }
            set
            {
                if (_SelectedMatrixTaskItemModel != value)
                {
                    _SelectedMatrixTaskItemModel = value;
                    RaisePropertyChanged();
                }
            }
        }

        private MatrixTaskPropertiesViewModel _MatrixTaskPropertiesViewModel;

        public MatrixTaskPropertiesViewModel MatrixTaskPropertiesViewModel
        {
            get { return _MatrixTaskPropertiesViewModel; }
            set
            {
                if (_MatrixTaskPropertiesViewModel != value)
                {
                    _MatrixTaskPropertiesViewModel = value;
                    RaisePropertyChanged();
                }
            }
        }

        #region Private Methods        

        private void HandleEditResultMessage(EditResultMessage editResultMsg)
        {
            if (editResultMsg.IsSaved)
            {
                LoadMatrixTask();
            }
            MatrixTaskPropertiesViewModel = null;
        }

        private void Init()
        {
            // Create the Task ObservableCollections
            ImportantAndNotUrgentMatrixTasks = new ObservableCollection<MatrixTaskItemModel>();
            ImportantAndUrgentMatrixTasks = new ObservableCollection<MatrixTaskItemModel>();
            NotImportantAndNotUrgentMatrixTasks = new ObservableCollection<MatrixTaskItemModel>();
            NotImportantAndUrgentMatrixTasks = new ObservableCollection<MatrixTaskItemModel>();

            Messenger.Default.Register<EditResultMessage>(this, (result) => HandleEditResultMessage(result));
        }
        
        private void LoadMatrixTask()
        {
            LoadQuardrant(ImportantAndNotUrgentMatrixTasks, IMPORTANT_AND_NOT_URGENT_QUADRANT_ID);
            LoadQuardrant(ImportantAndUrgentMatrixTasks, IMPORTANT_AND_URGENT_QUADRANT_ID);
            LoadQuardrant(NotImportantAndNotUrgentMatrixTasks, NOT_IMPORTANT_AND_NOT_URGENT_QUADRANT_ID);
            LoadQuardrant(NotImportantAndUrgentMatrixTasks, NOT_IMPORTANT_AND_URGENT_QUADRANT_ID);
        }        
        
        private void LoadQuardrant(ObservableCollection<MatrixTaskItemModel> collection, int quadrantId)
        {
            if (collection.Count > 0)
                collection.Clear();

            foreach(var v in _DataService.GetMatrixTasksByQuadrant(quadrantId))
            {
                collection.Add(new MatrixTaskItemModel()
                {
                    Description = v.Description,
                    MatrixTaskId = v.MatrixTaskId
                });
            }
        }

        private void OpenMatrixTaskProperties(MatrixTask matrixTask)
        {
            MatrixTaskPropertiesViewModel = new MatrixTaskPropertiesViewModel(matrixTask, _DataService);
        }

        private void DoDelete()
        {
            if (_DataService.DeleteMatrixTask(SelectedMatrixTaskItemModel.MatrixTaskId))
            {
                SelectedMatrixTaskItemModel = null;
                LoadMatrixTask();
            }
        }

        #endregion

        #region Commands

        private ICommand _ExitCommand;

        /// <summary>
        /// Gets the ExitCommand
        /// </summary>
        public ICommand ExitCommand
        {
            get
            {
                if (_ExitCommand == null)
                    _ExitCommand = new RelayCommand(ExitCommandExecute);

                return _ExitCommand;
            }
        }

        private void ExitCommandExecute()
        {
            App.Current.Shutdown();
        }

        private ICommand _NewTaskCommand;        

        /// <summary>
        /// Gets the NewTaskCommand
        /// </summary>
        public ICommand NewTaskCommand
        {
            get
            {
                if (_NewTaskCommand == null)
                    _NewTaskCommand = new RelayCommand(NewTaskCommandExecute);
                return _NewTaskCommand;
            }
        }

        private void NewTaskCommandExecute()
        {
            OpenMatrixTaskProperties(_DataService.NewMatrixTask());
        }

        private ICommand _EditTaskCommand;

        public ICommand EditTaskCommand
        {
            get
            {
                if (_EditTaskCommand == null)
                    _EditTaskCommand = new RelayCommand(EditTaskCommandExecute, EditTaskCommandCanExecute);

                return _EditTaskCommand;
            }
              
        }

        private bool EditTaskCommandCanExecute()
        {
            return _SelectedMatrixTaskItemModel != null;
        }
        
        private void EditTaskCommandExecute()
        {
            MatrixTask matrixTask = _DataService.GetMatrixTaskById(_SelectedMatrixTaskItemModel.MatrixTaskId);
            OpenMatrixTaskProperties(matrixTask);
        }

        private ICommand _DeleteTaskCommand;

        public ICommand DeleteTaskCommand
        {
            get
            {
                if (_DeleteTaskCommand == null)
                    _DeleteTaskCommand = new RelayCommand(DeleteTaskCommandExecute, DeleteTaskCommandCanExecute);
                return _DeleteTaskCommand;
            }
        }

        private void DeleteTaskCommandExecute()
        {
            DoDelete();
        }

        private bool DeleteTaskCommandCanExecute()
        {
            return _SelectedMatrixTaskItemModel != null;
        }

        #endregion
    }
}