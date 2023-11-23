using Nazar1988.Models;
using Nazar1988.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Areas.Identity.Data.Services
{
    public interface _WalletService
    {
        #region WalletInterFace
        int BalanceUserWallet(string Id);
        List<WalletViewModel> GetWalletUser(string Id);
        int ChargeWallet(string userName, int amount, string description, bool isPay = false,string shomareCheck="0",int typeId = 1,bool etebar = false);
        int AddWallet(Wallet wallet);
        #endregion

        int GetWalletUserIdBedehi(string userId);
        List<BedehiViewModel> GetWalletUserBedehi();


    }
}
