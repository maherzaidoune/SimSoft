using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PFE.Models;

namespace PFE.Services
{
    public interface IDataServices
    {
        Task<bool> addStockLigneMEAsync(StockLigne obj);
        Task<bool> addStockLigneMSAsync(StockLigne obj);
        Task<bool> addStockLigneMTAsync(StockLigne obj);

        bool addStockLigneListMSAsync(IList<StockLigne> obj);
        bool addStockLigneListMEAsync(IList<StockLigne> obj);
        bool addStockLigneListMTAsync(IList<StockLigne> obj);

        Task<bool> RemoveStockLigneMEAsync(StockLigne obj);
        Task<bool> RemoveStockLigneMSAsync(StockLigne obj);
        Task<bool> RemoveStockLigneMTAsync(StockLigne obj);



        Task<IList<StockLigne>> getStockLigneObjectsMEAsync();
        Task<IList<StockLigne>> getStockLigneObjectsMSAsync();
        Task<IList<StockLigne>> getStockLigneObjectsMTAsync();

        bool RemoveStockLigne();

    }
}
