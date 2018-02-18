using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Imageeditor.Behaviors
{
    public class SliderBehavior
    {


        public static ICommand GetValueChanged(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ValueChangedProperty);
        }

        public static void SetValueChanged(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ValueChangedProperty, value);
        }

        // Using a DependencyProperty as the backing store for ValueChanged.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueChangedProperty =
            DependencyProperty.RegisterAttached("ValueChanged", typeof(ICommand), typeof(SliderBehavior), new FrameworkPropertyMetadata(new PropertyChangedCallback(SliderChangedCallback)));

        private static void SliderChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           var element = d as Slider;
            if (element != null)
            {
                // If we're putting in a new command and there wasn't one already
                // hook the event
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    element.ValueChanged += Element_ValueChanged; ;
                }
                // If we're clearing the command and it wasn't already null
                // unhook the event
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    element.ValueChanged -= Element_ValueChanged;
                }
            }
        }

        private static void Element_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UIElement target = sender as UIElement;
            ICommand command = (ICommand)target.GetValue(ValueChangedProperty);
            command.Execute(null);
        }
    }
}
