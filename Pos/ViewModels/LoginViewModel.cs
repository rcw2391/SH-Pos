using Common;
using DataLayer;

namespace Pos.ViewModels
{
    public class LoginViewModel : PropertyChangedBase
    {
        private IUnitOfWork _uow;
        
        public LoginViewModel(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
