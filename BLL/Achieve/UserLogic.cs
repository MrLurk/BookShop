using System;
using BLL.Interface;
using DTOModels;
using DAL.Interface;

namespace BLL.Achieve
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository = null;

        public UserLogic(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public int UserReg(DTOUserReg dto)
        {
            return _userRepository.GetValue().GetHashCode();
            // return 1;
        }
    }
}
