using EisenhowerMatrix.WPF.Models;
using EisenhowerMatrix.WPF.ViewModel;
using System.Windows.Controls;

namespace EisenhowerMatrix.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for Matrix.xaml
    /// </summary>
    public partial class Matrix : UserControl
    {
        public Matrix()
        {
            InitializeComponent();
        }

        private void MatrixBlock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If e.AddedItems is empty, then this is a SelectionChanged event from 
            // a ListBox that had it's SelectedIndex set to -1. Just return
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            // All MatrixBlocks implement this event handler
            // Clear any selected items all but the sender block
            MatrixBlock matrixBlock = sender as MatrixBlock;
            if (matrixBlock != null)
            {
                if (!sender.Equals(ImportantAndUrgentMatrixBlock))
                {
                    ImportantAndUrgentMatrixBlock.ClearSelection();
                }

                if (!sender.Equals(ImportantAndNotUrgentMatrixBlock))
                {
                    ImportantAndNotUrgentMatrixBlock.ClearSelection();
                }

                if (!sender.Equals(NotImportantAndUrgentMatrixBlock))
                {
                    NotImportantAndUrgentMatrixBlock.ClearSelection();
                }

                if (!sender.Equals(NotImportantAndNotUrgentMatrixBlock))
                {
                    NotImportantAndNotUrgentMatrixBlock.ClearSelection();
                }
            }

            // Set the SelectedMatrixTaskItemModel value
            MainViewModel viewModel = this.DataContext as MainViewModel;
            if (viewModel != null)
            {
                viewModel.SelectedMatrixTaskItemModel = e.AddedItems[0] as MatrixTaskItemModel;
            }
            
            e.Handled = true;
        }
    }
}
