using EisenhowerMatrix.WPF.DataAccess;
using EisenhowerMatrix.WPF.Models;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace EisenhowerMatrix.WPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        const int IMPORTANT_AND_URGENT_QUADRANT_ID = 1;
        const int IMPORTANT_AND_NOT_URGENT_QUADRANT_ID = 2;
        const int NOT_IMPORTANT_AND_URGENT_QUADRANT_ID = 3;
        const int NOT_IMPORTANT_AND_NOT_URGENT_QUADRANT_ID = 4;


        private IDataService _DataService;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            _DataService = dataService;

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

        #region Private Methods

        private void Init()
        {
            // Create the Task ObservableCollections
            ImportantAndNotUrgentMatrixTasks = new ObservableCollection<MatrixTaskItemModel>();
            ImportantAndUrgentMatrixTasks = new ObservableCollection<MatrixTaskItemModel>();
            NotImportantAndNotUrgentMatrixTasks = new ObservableCollection<MatrixTaskItemModel>();
            NotImportantAndUrgentMatrixTasks = new ObservableCollection<MatrixTaskItemModel>();
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

        #endregion
    }
}