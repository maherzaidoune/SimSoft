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
        Task<bool> addStockLigneMIAsync(StockLigne obj);

        bool addStockLigneListMSAsync(IList<StockLigne> obj);
        bool addStockLigneListMEAsync(IList<StockLigne> obj);
        bool addStockLigneListMTAsync(IList<StockLigne> obj);
        bool addStockLigneListMIAsync(IList<StockLigne> obj);


        Task<bool> RemoveStockLigneMEAsync(StockLigne obj);
        Task<bool> RemoveStockLigneMSAsync(StockLigne obj);
        Task<bool> RemoveStockLigneMTAsync(StockLigne obj);
        Task<bool> RemoveStockLigneMIAsync(StockLigne obj);



        Task<IList<StockLigne>> getStockLigneObjectsMEAsync();
        Task<IList<StockLigne>> getStockLigneObjectsMSAsync();
        Task<IList<StockLigne>> getStockLigneObjectsMTAsync();
        Task<IList<StockLigne>> getStockLigneObjectsMIAsync();
        bool RemoveStockLigne();


        //sell elements
     
        Task<bool> addSellElementAsync(SellElements obj);
        bool addSellElementsAsync(IList<SellElements> obj);
        Task<IList<SellElements>> getSellElementAsync();
        Task<bool> updateAsyncSellElement(SellElements obj);
        Task<bool> removeSellElementsAsync(SellElements obj);
        bool RemoveSellElements();

        //buy element

        Task<bool> addBuyElementAsync(Buyelement obj);
        bool addBuyElementsAsync(IList<Buyelement> obj);
        Task<IList<Buyelement>> getBuyElementAsync();
        Task<bool> updateAsyncBuyElement(Buyelement obj);
        Task<bool> removeBuyElementsAsync(Buyelement obj);
        bool RemoveBuyElements();


    }
}
