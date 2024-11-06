using Microsoft.AspNetCore.Identity;

namespace Aetherworks_Victuz.Models
{
    public class ManageAccountsViewModel
    {
        public List<UserRoleViewModel> AllUsers { get; set; }
        public List<UserRoleViewModel> PendingAccounts { get; set; }
    }

}
