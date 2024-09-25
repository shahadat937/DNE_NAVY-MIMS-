using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
   public interface IBooksAndBrManager
    {
       List<BooksAndBr> GetAll();
       //BooksAndBr FindOne(string id);
       ResponseModel Saving(BooksAndBr booksAndBr);
       long MaxId();
       BooksAndBr FindUrl(long id);
       object Delete(long id);
       BooksAndBr GetValue(long? id, BooksAndBr booksAndBr);
    }
}
