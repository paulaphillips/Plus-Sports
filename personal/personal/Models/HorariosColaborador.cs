using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace personal.Models
{
    public class HorariosColaborador
    {
        public int ID { get; set; }
        public int ID_ESPORTE { get; set; }
        public int ID_COLABORADOR { get; set; }
        public DateTime DATA { get; set; }
        public int FREQUENCIA { get; set; }
         public String DATAFORMATADA { get; set; }
        public String DIA_SEMANA { get; set; }
    }
}