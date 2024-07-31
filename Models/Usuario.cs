using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesNHibernate.Models
{
    internal class Usuario
    {
        public virtual int id { get; set; }
        public virtual string nome { get; set; }
        public virtual string senha { get; set; }
        public virtual string email { get; set; }
        public virtual DateTime dataCadastro { get; set; }
    }
}
