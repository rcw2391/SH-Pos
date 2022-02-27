using Common;
using DataLayer;
using Pos.ViewModels;
using System;

namespace Pos
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private IUnitOfWork _uow;
        
        public MainWindowViewModel(IUnitOfWork uow)
        {
            _uow = uow;

            SecurityManager.Instance.SetUnitOfWork(uow);

#if DEBUG
            if (Environment.MachineName == "DESKTOP-VVEM2T6")
                SecurityManager.Instance.LoginRhino();
#endif
            Tasker<bool> _ = new(DataCache.Instance.InitAsync(uow), b =>
            {
                if (b)
                {
                    CustomerVm.UpdateCustomers();
                }
            });
        }

        private CustomerViewModel _customerVm;
        public CustomerViewModel CustomerVm
        {
            get
            {
                if (_customerVm == null)
                    _customerVm = new(_uow);

                return _customerVm;
            }
        }

        private LoginViewModel _loginVm;
        public LoginViewModel LoginVm
        {
            get
            {
                if (_loginVm == null)
                    return new LoginViewModel(_uow);

                return _loginVm;
            }
        }
    }
}
