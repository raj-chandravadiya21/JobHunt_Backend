using JobHunt.Domain.DataModels.Request.UserRequest.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Application.Interfaces.UserInterface
{
    public interface IApplicationService
    {
        Task ApplyForJob(ApplyJobRequest model);
    }
}
