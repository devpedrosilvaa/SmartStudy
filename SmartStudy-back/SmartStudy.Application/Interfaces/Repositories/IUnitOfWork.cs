using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
