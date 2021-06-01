using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexandriaVer._2.Models
{
    public class UsersBooks
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public IdentityUser User { get; set; }
        public bool HasRead { get; set; }
    }
}
