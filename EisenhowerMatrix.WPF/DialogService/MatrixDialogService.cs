using EisenhowerMatrix.WPF.Windows;

namespace EisenhowerMatrix.WPF.DialogService
{
    class MatrixDialogService : IMatrixDialogService
    {
        public void ShowAboutBox()
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Owner = App.Current.MainWindow;
            aboutBox.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;

            aboutBox.ShowDialog();
        }
    }
}
