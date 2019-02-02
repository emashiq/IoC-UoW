using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace Repository.UnitOfWork
{
    public class UnitOfWorkInjector
    {
        private IUnitOfWork _unitOfWork;
        public IUnitOfWork GetUnitOfWorkInstance() => _unitOfWork ?? (_unitOfWork = new UnitOfWork());
    }
}
