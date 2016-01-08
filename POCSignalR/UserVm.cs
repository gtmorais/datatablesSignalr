using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POCSignalR
{
    public class UserVm
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Area { get; set; }
        public string[] Groups { get; set; }
        public bool Admin { get; set; }
    }

    public static class GetUser
    {
        public static UserVm User1()
        {
            UserVm vm = new UserVm();
            vm.Name = "User 1";
            vm.Groups = new string[] { "Group1", "Group2" };;
            return vm;
        }

        public static UserVm User2()
        {
            UserVm vm = new UserVm();
            vm.Name = "User 2";
            string[] grupos = { "Group1", "Group3" };
            vm.Groups = grupos;
            return vm;
        }
    }
}