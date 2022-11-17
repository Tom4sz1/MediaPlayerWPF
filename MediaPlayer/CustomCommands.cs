using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaPlayer
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Privacy = new RoutedUICommand(
            "Privacy",
            "Privacy",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F10, ModifierKeys.Shift)
            }
            );
    }
}
