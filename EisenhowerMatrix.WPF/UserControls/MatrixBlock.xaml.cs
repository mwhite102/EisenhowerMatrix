using System.Windows.Controls;

namespace EisenhowerMatrix.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for MatrixBlock.xaml
    /// </summary>
    public partial class MatrixBlock : UserControl
    {
        public MatrixBlock()
        {
            InitializeComponent();
        }

        #region Public Methods

        /// <summary>
        /// Clears the SelectedItem in the MatrixBlock
        /// </summary>
        public void ClearSelection()
        {
            MatrixBlock_ListBox.SelectedIndex = -1;
        }

        #endregion

        #region SelectionChangedEvent

        /// <summary>
        /// Defines a new SelectionChangedEventHandler for the MatrixBlock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SelectionChangedEventHandler(object sender, SelectionChangedEventArgs e);

        /// <summary>
        /// Gets or sets the SelectionChanged event
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged;

        /// <summary>
        /// Raises the SelectedChanged event
        /// </summary>
        /// <param name="args">The SelectionChangedEventArgs</param>
        private void RaiseSelectionChangedEvent(SelectionChangedEventArgs args)
        {
            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this, args);
            }
        }

        #endregion

        private void MatrixBlock_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Raise the local SectionChanged event
            RaiseSelectionChangedEvent(e);
        }
    }
}
