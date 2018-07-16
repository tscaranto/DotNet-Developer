using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CMDApproval
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Pending transaction to approve");

            IEnumerable<Models.Transaction> transactions = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/api/transaction");
                //HTTP GET
                var responseTask = client.GetAsync("transaction");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Models.Transaction>>();
                    readTask.Wait();

                    transactions = readTask.Result.Where(x => x.Status == Models.StatusEnum.Pending);
                }
            }

            if(transactions.ToList().Count == 0)
            {
                Console.WriteLine("There are no transactions to approve");
                return;
            }

            foreach (var item in transactions)
            {
                Console.WriteLine(String.Format("Id: {0}, Account From: {1}, Account To: {2}, Amout: {3}, Requisition Date: {4}", item.Id, item.AccountFrom, item.AccountTo, item.Amount, item.DtInserted));
            }

            Console.WriteLine("Press S to approve");

            ConsoleKeyInfo keyPressed = Console.ReadKey(false);

            if (keyPressed.Key.ToString().Equals("S") || keyPressed.Key.ToString().Equals("s"))
            {
                foreach (var item in transactions)
                {
                    using (SuperbidContext db = new SuperbidContext())
                    {
                        var trans = db.Transactions.Find(item.Id);
                        var accFrom = db.Accounts.Find(item.AccountFrom);
                        var accTo = db.Accounts.Find(item.AccountTo);

                        accFrom.Balance = accFrom.Balance - item.Amount;
                        accTo.Balance = accTo.Balance + item.Amount;
                        trans.Status = Models.StatusEnum.Aproved;

                        db.Entry(accFrom).State = EntityState.Modified;

                        db.Entry(accTo).State = EntityState.Modified;

                        db.Entry(trans).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                }

                Console.WriteLine("Transactions approved!");
            }
            else
            {
                Console.WriteLine("Transactions not aprroved, quitting app");
            }


        }
    }
}
