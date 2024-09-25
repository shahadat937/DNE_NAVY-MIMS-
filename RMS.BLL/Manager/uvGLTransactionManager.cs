using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;

namespace RMS.BLL.Manager
{
    public class uvGLTransactionManager:BaseManager,IuvGLTransactionManager
    {
        private IuvGLTransactionRepository _uvGlTransactionRepository;

        public uvGLTransactionManager()
        {
            _uvGlTransactionRepository=new uvGLTransactionRepository(Context);
        }
    }
}
