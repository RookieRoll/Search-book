﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook.Service.Model
{
    public class BookQuery
    {
        public IEnumerable<BookInfo> books { get; set; }
    }
}
