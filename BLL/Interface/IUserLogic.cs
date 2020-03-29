using System;
using DTOModels;

namespace BLL.Interface
{
    public interface IUserLogic
    {
        int UserReg(DTOUserReg dto);
    }
}
