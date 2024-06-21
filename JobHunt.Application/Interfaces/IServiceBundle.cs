using JobHunt.Application.Interfaces.UserInterface;
using JobHunt.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Application.Interfaces
{
    public interface IServiceBundle
    {
        public IAuthService AuthService { get; }

        public IRegistrationService RegistrationService { get; }
    }
}
