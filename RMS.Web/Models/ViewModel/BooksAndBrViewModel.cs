using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class BooksAndBrViewModel
    {
        public BooksAndBr BooksAndBr { get; set; }
        public List<BooksAndBr> BooksAndBrs { get; set; }

        public BooksAndBrViewModel()
        {
            BooksAndBr =new BooksAndBr();
            BooksAndBrs =new List<BooksAndBr>();
        }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        public string SearchKey { get; set; }
    }
}