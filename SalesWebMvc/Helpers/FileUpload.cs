using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Helpers {
    public class FileUpload : Product {

        [BindProperty]
        public FileUploadDb imageUpload { get; set; }
    }
}
