using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace POCSignalR.Workspace
{
    public class Task
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Responsavel { get; set; }
        
        public string Prioridade { get; set; }
        public string Origem { get; set; }
        public string TipoProcesso { get; set; }

        public DateTime DataCriado { get; set; }
        public DateTime DataFim { get; set; }
        
        public string PrazoRestante { get; set; }

        public long PrazoRestanteTicks
        {
            get { return DataFim.Ticks; }
        }

        public string DataFimTexto
        {
            get { return DataFim.ToString("MM/dd/yyyy HH:mm:ss"); }
        }
    }
}

