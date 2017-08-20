using System;
using System.Windows;
using System.Windows.Controls;

namespace MyLrcMaker.Controls
{
    public class MediaElementHelper
    {
        public static TimeSpan GetBindablePosition(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }
            return (TimeSpan) element.GetValue(BindablePositionProperty);
        }

        public static void SetBindablePosition(UIElement element, TimeSpan value)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }
            element.SetValue(BindablePositionProperty, value);
        }

        #region Private methods

        private static void PostionPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var richEditControl = obj as MediaElement;

            if (richEditControl != null)
            {
                richEditControl.Position = (TimeSpan) e.NewValue;
            }
        }

        #endregion

        #region Fields

        public static readonly DependencyProperty BindablePositionProperty =
            DependencyProperty.RegisterAttached("BindablePosition",
                typeof(TimeSpan), typeof(MediaElementHelper),
                new FrameworkPropertyMetadata(new TimeSpan(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PostionPropertyChanged));

        #endregion
    }
}