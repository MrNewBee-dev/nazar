using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nazar1988.Models;
using Nazar1988.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Areas.Identity.Data.Services
{
    public class WalletService : _WalletService
    {
        private readonly Nazar1988Context _context;
        
        public WalletService(Nazar1988Context _context)
        {
            this._context = _context;
        
        }
        public int BalanceUserWallet(string Id)
        {
            
            var enter = _context.wallets.Where(x => x.UserId == Id && x.TypeId == 1 && x.IsPay).Select(w => w.Amount);
            var exit = _context.wallets.Where(x => x.UserId == Id && x.TypeId == 2 && x.IsPay).Select(w => w.Amount);
            return (enter.Sum() - exit.Sum());
        }

        public List<WalletViewModel> GetWalletUser(string Id)
        {
            return _context.wallets.Where(w => w.IsPay && w.UserId == Id).Select(
                x=> new WalletViewModel{
                Amount = x.Amount,
                DateTime= x.CreateDate,
                Description = x.Description,
                Type = x.TypeId

                }
                ).ToList();
        }
        public int ChargeWallet(string Id, int amount, string description, bool isPay = false, string shomare_Check = "0",int typeId = 1,bool etebar = false)
        {
            Wallet wallet = new Wallet()
            {
                Amount = amount,
                CreateDate = DateTime.Now,
                Description = description,
                IsPay = isPay,
                TypeId = typeId,
                UserId = Id,
                Shomare_Check = shomare_Check,
                Etebar = etebar
            };
           return AddWallet(wallet);
        }

        public int AddWallet(Wallet wallet)
        {

             _context.wallets.Add(wallet);
            
            _context.SaveChanges();
            return wallet.WalletId;
        }

        public List<BedehiViewModel> GetWalletUserBedehi()//bedehi kole user ha ra biron mikeshad
        {

            return _context.wallets.Include(x => x.User).Where(w => w.IsPay && w.Etebar).GroupBy(c => new { c.UserId, c.User.Email }).Select(x => new BedehiViewModel {
                UserId = x.Key.UserId,
                Email = x.Key.Email,
                Amount = x.Sum(x => x.Amount)
            }).ToList();
        }
        public int GetWalletUserIdBedehi(string userId)// bedehi har shakhs ra az data base biron mikeshad
        {

            return _context.wallets.Where(w => w.IsPay && w.Etebar && w.UserId == userId).Sum(x => x.Amount);
            
        }


    }
}
