using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace personal.Models
{
    public class Agendamentos
    {
        public int ID{ get; set; }
        public int ID_USUARIO { get; set; }
        public String DS_USUARIO { get; set; }
        public int ID_COLABORADOR { get; set; }
        public String DS_COLABORADOR { get; set; }
        public int ID_ESPORTE { get; set; }
        public String DS_ESPORTE { get; set; }
        public DateTime DATA { get; set; }
        public String DATAFORMATADA { get; set; }
    }
}