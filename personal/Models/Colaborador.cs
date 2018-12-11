using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace personal.Models
{
    public class Colaborador
    {
        public int ID_COLABORADOR {get; set;}
        public int ID_ESPORTE { get; set; }
        public String NOME {get; set;}
        public String EMAIL { get; set; }
        public String CPF { get; set; }
        public String CREF { get; set; }
        public String UF_CREF { get; set; }
        public DateTime DATA_NASCIMENTO { get; set; }
        public String ESPECIALIDADE { get; set; }
        public String METODOLOGIA { get; set; }
        public String FORMACAO { get; set; }
        public String SOBRE { get; set; }
        public String STATUS { get; set; }
        public String IMAGE_NAME { get; set; }
        public String DATA_NASCIMENTO_FORMATADA { get; set; }
        public String RATING_STARS { get; set; }
    }
}