using CommunityToolkit.Mvvm.ComponentModel;

namespace Common
{
    public class Tasker<T> : ObservableObject
    {
        public Tasker(Task<T> task, Action<T> callBack)
        {
            Task = task;
            CallBack = callBack;
        }
        
        private Action<T> _callBack;
        public Action<T> CallBack
        {
            get => _callBack;
            set => SetProperty(ref _callBack, value);
        }

        private TaskNotifier<T> _task;
        public Task<T> Task
        {
            get => _task;
            set => SetPropertyAndNotifyOnCompletion<T>(ref _task, value, OnCompleted);
        }

        private void OnCompleted(Task<T>? obj)
        {
            if (obj is null) return;

            CallBack(Task.Result);

            Task = null;
            CallBack = null;
        }
    }
}
