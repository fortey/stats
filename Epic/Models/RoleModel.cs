using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epic.Models
{
    public class EditRoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateRoleModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class EditUserRoles
    {
        public SelectList Roles { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}