using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTables.Mvc;
using Microsoft.AspNet.SignalR;
using POCSignalR.Models;
using POCSignalR.Workspace;

namespace POCSignalR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListTasks([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var data = LiveWorkspace.Instance.GetAllTasksByProfile(GetUser.User1());

            var dataOrdenada = data.OrderBy(o => o.DataFim.Ticks);

            var paged = dataOrdenada.Skip(requestModel.Start).Take(requestModel.Length);

            var sortedColumns = requestModel.Columns.GetSortedColumns();
            foreach (var column in sortedColumns)
            {
                if (requestModel.Draw > 1)
                {
                    if (column.Orderable && column.IsOrdered)
                    {

                        if (column.SortDirection == DataTables.Mvc.Column.OrderDirection.Ascendant)
                            paged = paged.OrderBy(o => o.GetType()
                                .GetProperty(column.Data)
                                .GetValue(o, null));
                        else
                            paged = paged.OrderByDescending(o => o.GetType()
                            .GetProperty(column.Data)
                            .GetValue(o, null));

                    }
                }
            }

            var resp = new DataTablesResponse(
                requestModel.Draw,
                paged,
                dataOrdenada.Count(),
                data.Count);

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateNewTask()
        {
            Task task = new Task();

            task = new Task();
            task.ID = 2;
            task.Responsavel = "Group2";
            task.Titulo = "New Task Created";
            task.DataCriado = DateTime.Now;
            task.DataFim = DateTime.Now.AddHours(0.5);
            task.PrazoRestante = (task.DataFim - task.DataCriado).ToString();

            task.Prioridade = "Medium";
            task.Origem = "Manual";
            task.TipoProcesso = "";

            LiveWorkspace.Instance.AddTask(task);

            return View("Index");
        }
    }
}