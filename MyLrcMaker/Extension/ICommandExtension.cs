using System.Windows.Input;
using Prism.Commands;

namespace MyLrcMaker.Extension
{
    public static class CommandExtension
    {
        public static void ForceUpdateCanExecuteCommand(this ICommand command)
        {
            var c = command as DelegateCommandBase;
            c?.RaiseCanExecuteChanged();
        }
    }
}