using Microsoft.AspNetCore.Mvc.Rendering;


namespace Mango.Web.Models.ViewModel
{
    public class RegisterUserViewModel
    {
        public CreateUserDto User { get; set; }
        public IList<SelectListItem> RoleList
        {
            get
            {
                var roleList = new List<SelectListItem>();

                foreach (var role in Constants.Constant.RoleName)
                {
                    roleList.Add(new SelectListItem
                    {
                        Text = role.Value.Name,
                        Value = role.Value.Name
                    });
                }

                return roleList;
            }
            set { }
        }



    }

}
