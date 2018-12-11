using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace personal.Models
{
    public class CadastroHorarios
    {
        public String esporte { get; set; }
        public IList<String> data { get; set; }
        public IList<String> frequencia { get; set; }
        public IList<String> diaSemana { get; set; }
        public IList<String> horarios { get; set; }
        public IList<int> meses { get; set; }
    }
}