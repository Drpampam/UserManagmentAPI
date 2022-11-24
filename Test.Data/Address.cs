using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Data
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
