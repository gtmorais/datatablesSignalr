using System;
using System.Threading;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Mvc;
using System.Web.Mvc;
using DataTables.Mvc;
using System.Globalization;

namespace POCSignalR.Workspace
{
    /// <summary>
    /// Broadcaster
    /// </summary>
    public class LiveWorkspace 
    {
        //Singleton instance
        private readonly static Lazy<LiveWorkspace> _instance = new Lazy<LiveWorkspace>(
            () => new LiveWorkspace(GlobalHost.ConnectionManager.GetHubContext<WorkspaceHub>().Clients));

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        private List<Task> _tasks;
        public List<Task> Tarefas
        {
            get
            {
                if (_tasks == null)
                    _tasks = new List<Task>();
                return _tasks;
            }
            set { _tasks = value; }
        }

        private LiveWorkspace(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            LoadDefaultData();
        }

        private void LoadDefaultData()
        {
            _tasks = GetAllTasks();
        }

        public List<Task> GetAllTasksByProfile(UserVm usuario)
        {
            List<Task> tarefasGrupos = new List<Task>();

            foreach (string grupo in usuario.Groups)
                tarefasGrupos.AddRange(Tarefas.Where(o => o.Responsavel == grupo));

            return tarefasGrupos;
        }

        public List<Task> GetAllTasks()
        {
            if (Tarefas.Count < 1)
            {
                Task task = new Task();
                task.ID = 1;
                task.Responsavel = "Group1";
                task.Titulo = "Identify Document";
                task.DataCriado = DateTime.Now;
                task.PrazoRestante = (task.DataFim - task.DataCriado).ToString();
                task.DataFim = DateTime.Now.AddHours(4);
                task.Prioridade = "High";
                task.Origem = "FAX";
                task.TipoProcesso = "Regular";
                Tarefas.Add(task);
                
                task = new Task();
                task.ID = 2;
                task.Responsavel = "Group2";
                task.Titulo = "Identify Document";
                task.DataCriado = DateTime.Now;
                task.DataFim = DateTime.Now.AddHours(2);
                task.PrazoRestante = (task.DataFim - task.DataCriado).ToString();
                task.Prioridade = "Medium";
                task.Origem = "Integration";
                task.TipoProcesso = "";
                Tarefas.Add(task);

                task = new Task();
                task.ID = 3;
                task.Responsavel = "Group3";
                task.Titulo = "Review Request";
                task.DataCriado = DateTime.Now;
                task.DataFim = DateTime.Now.AddMinutes(15);
                task.PrazoRestante = (task.DataFim - task.DataCriado).ToString();
                task.Prioridade = "Low";
                task.Origem = "Portal";
                task.TipoProcesso = "";
                Tarefas.Add(task);


                task = new Task();
                task.ID = 4;
                task.Responsavel = "Group1";
                task.Titulo = "Identify Document";
                task.DataCriado = DateTime.Now;
                task.DataFim = DateTime.Now.AddHours(2);
                task.PrazoRestante = (task.DataFim - task.DataCriado).ToString();
                task.Prioridade = "Medium";
                task.Origem = "Integration";
                task.TipoProcesso = "";
                Tarefas.Add(task);

                task = new Task();
                task.ID = 5;
                task.Responsavel = "Group2";
                task.Titulo = "Review Request";
                task.DataCriado = DateTime.Now;
                task.DataFim = DateTime.Now.AddMinutes(0);
                task.PrazoRestante = (task.DataFim - task.DataCriado).ToString();
                task.Prioridade = "Low";
                task.Origem = "Portal";
                task.TipoProcesso = "";
                Tarefas.Add(task);

                task = new Task();
                task.ID = 6;
                task.Responsavel = "Group2";
                task.Titulo = "Review Request";
                task.DataCriado = DateTime.Now;
                task.DataFim = DateTime.Now.AddMinutes(10);
                task.PrazoRestante = (task.DataFim - task.DataCriado).ToString();
                task.Prioridade = "High";
                task.Origem = "Portal";
                task.TipoProcesso = "";
                Tarefas.Add(task);

                task = new Task();
                task.ID = 3;
                task.Responsavel = "Group1";
                task.Titulo = "Review Request";
                task.DataCriado = DateTime.Now;
                task.DataFim = DateTime.Now.AddMinutes(1);
                task.PrazoRestante = (task.DataFim - task.DataCriado).ToString();
                task.Prioridade = "Low";
                task.Origem = "Portal";
                task.TipoProcesso = "";
                Tarefas.Add(task);
            }

            return Tarefas;
        }

        public void AddTask(Task task)
        {
            Tarefas.Add(task);

            Clients.Group(task.Responsavel).notifyNewContent();
        }

        public static LiveWorkspace Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}