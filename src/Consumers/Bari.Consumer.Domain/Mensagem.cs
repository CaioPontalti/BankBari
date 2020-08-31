using System;
using System.Collections.Generic;
using System.Text;

namespace Bari.Consumer.Domain
{
    public class Mensagem
    {
        public Guid Id { get; set; }
        public string Conteudo { get; set; }
        public DateTime TimesTamp { get; set; }
        public string Tipo { get; set; }

        public Mensagem()
        {
            Id = Guid.NewGuid();
            Conteudo = "Hello World";
            TimesTamp = DateTime.Now;
            Tipo = GetType().Name;
        }
    }
}
