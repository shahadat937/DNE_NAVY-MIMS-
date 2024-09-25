using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
   public class BooksAndBrManager :BaseManager,IBooksAndBrManager
    {
         private IBooksAndBrRepository _booksAndBrRepository;
         public BooksAndBrManager()
        {
            _booksAndBrRepository = new BooksAndBrRepository(Instance.Context);
        }

       public List<BooksAndBr> GetAll()
       {
           return _booksAndBrRepository.All().ToList();
       }

     

       public ResponseModel Saving(BooksAndBr booksAndBr)
       {
           BooksAndBr oldData = _booksAndBrRepository.FindOne(x => x.BooksAndBrIdentity == booksAndBr.BooksAndBrIdentity);
           if (oldData != null)
           {
               oldData.BooksAndBrIdentity = booksAndBr.BooksAndBrIdentity;
               oldData.BooksAndBrName = booksAndBr.BooksAndBrName;
               oldData.UrlLink = booksAndBr.UrlLink;
             
               oldData.UserId = PortalContext.CurrentUser.UserName;
               oldData.LastUpdate = DateTime.Now;
               _booksAndBrRepository.Edit(oldData);
               ResponseModel.Message = "Information is updated successfully.";
           }
           else
           {
               
               booksAndBr.LastUpdate = DateTime.Now;
               booksAndBr.UserId = PortalContext.CurrentUser.UserName;
               _booksAndBrRepository.Save(booksAndBr);
               ResponseModel.Message = "Information is saved successfully.";
           }
           return ResponseModel;
       }

       public long MaxId()
       {
           long maxvalue = 0;
           var value = _booksAndBrRepository.All();
           if (value.Count()>0)
               maxvalue = value.Select(x => x.BooksAndBrIdentity).Max();
           return maxvalue;
       }

       public BooksAndBr FindUrl(long id)
       {
           return _booksAndBrRepository.Filter(x => x.BooksAndBrIdentity == id).FirstOrDefault();
       }

       public object Delete(long id)
       {
           return _booksAndBrRepository.Delete(x => x.BooksAndBrIdentity == id);
       }

       public BooksAndBr GetValue(long? id, BooksAndBr booksAndBr)
       {
           return _booksAndBrRepository.FindOne(x => x.BooksAndBrIdentity == id);
       }
    }
}
