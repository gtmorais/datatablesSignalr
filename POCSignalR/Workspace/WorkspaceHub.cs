using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNet.SignalR.Messaging;

namespace POCSignalR.Workspace
{
    [HubName("liveWorkspace")]
    public class WorkspaceHub : Hub
    {
        private readonly LiveWorkspace _liveWorkspace;

        public WorkspaceHub() : this(LiveWorkspace.Instance)
        {

        }

        public WorkspaceHub(LiveWorkspace liveWorkspace)
        {
            _liveWorkspace = liveWorkspace;
            //_liveWorkspace.workspaceHub = this;
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            //register current user groups/profiles
            UserVm user = GetUser.User1();
            
            foreach (var grupo in user.Groups)
                Groups.Add(Context.ConnectionId, grupo);    

            return base.OnConnected();
        }


        public List<Task> GetAllTasks()
        {
            //calls the workspace
            return _liveWorkspace.GetAllTasks();
        }

        //Broadcast
        public void Send()
        {
            Clients.All.notifyNewContent();
        }

        ////Broadcast
        //public void Send(string name, string message)
        //{
        //    //_liveWorkspace.AddTask();
        //    Clients.All.notifyNewContent();
        //}

        //public void Receive(string name, string message)
        //{
        //    // Call the notifyNewContent method to update clients.
        //   Send(name, message);
        //}
    }
}