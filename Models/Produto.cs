using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesNHibernate.Models
{
    internal class Produto
    {
        public virtual int id { get; set; }
        public virtual string grupo { get; set; }
        public virtual string nome { get; set; }
        public virtual string especificacao { get; set; }
        public virtual string unidade { get; set; }
        public virtual double preco { get; set; }
        public virtual int quantidade { get; set; }
    }
}

