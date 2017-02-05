using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EisenhowerMatrix.WPF.Behaviors
{
    /// <summary>
    /// Attached Property for controls that allows for the capture of the MouseDoubleClick event and executes an ICommand in the ViewModel
    /// </summary>
    public static class DoubleClickBehavior
    {
        public static DependencyProperty DoubleClickCommandProperty = DependencyProperty.RegisterAttached("DoubleClick",
            typeof(ICommand),
            typeof(DoubleClickBehavior),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DoubleClickBehavior.DoubleClickChanged)));

        public static void SetDoubleClick(DependencyObject target, ICommand value)
        {
            target.SetValue(DoubleClickBehavior.DoubleClickCommandProperty, value);
        }

        public static ICommand GetDoubleClick(DependencyObject target)
        {
            return (ICommand)target.GetValue(DoubleClickCommandProperty);
        }

        private static void DoubleClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Only allow attached properties for System.Windows.Controls.Control or it's descendants
            // that is where MouseDoubleClick is introduced
            Control ctrl = d as Control;
            if (ctrl != null)
            {
                if (e.NewValue != null && e.OldValue == null)
                {
                    ctrl.MouseDoubleClick += control_MouseDoubleClick;
                }
                else if (e.NewValue == null && e.OldValue != null)
                {
                    ctrl.MouseDoubleClick -= control_MouseDoubleClick;
                }
            }
        }

        public static DependencyProperty DoubleClickParameterProperty = DependencyProperty.RegisterAttached("DoubleClickParameter",
            typeof(object),
            typeof(DoubleClickBehavior),
            new UIPropertyMetadata(null));

        public static void SetDoubleClickParameter(DependencyObject target, object value)
        {
            target.SetValue(DoubleClickParameterProperty, value);
        }

        public static object GetDoubleClickParameter(DependencyObject target)
        {
            return target.GetValue(DoubleClickParameterProperty);
        }

        private static void control_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement element = (UIElement)sender;
            ICommand command = (ICommand)element.GetValue(DoubleClickBehavior.DoubleClickCommandProperty);
            object commandParameter = element.GetValue(DoubleClickBehavior.DoubleClickParameterProperty);
            command.Execute(commandParameter);
        }
    }
}
