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


        //sell elements
        Task<bool> addSellElementBCAsync(SellElements obj);
        bool addSellElementsBCAsync(IList<SellElements> obj);
        Task<IList<SellElements>> getSellElementBCAsync();
        Task<bool> updateAsyncSellElementBC(SellElements obj);
        Task<bool> removeSellElementsBCAsync(SellElements obj);
        bool RemoveSellElementsBC();

        Task<bool> addSellElementBLAsync(SellElements obj);
        bool addSellElementsBLAsync(IList<SellElements> obj);
        Task<IList<SellElements>> getSellElementBLAsync();
        Task<bool> updateAsyncSellElementBL(SellElements obj);
        Task<bool> removeSellElementsBLAsync(SellElements obj);
        bool RemoveSellElementsBL();

        Task<bool> addSellElementBRAsync(SellElements obj);
        bool addSellElementsBRAsync(IList<SellElements> obj);
        Task<IList<SellElements>> getSellElementBRAsync();
        Task<bool> updateAsyncSellElementBR(SellElements obj);
        Task<bool> removeSellElementsBRAsync(SellElements obj);
        bool RemoveSellElementsBR();

        Task<bool> addSellElementFVAsync(SellElements obj);
        bool addSellElementsFVAsync(IList<SellElements> obj);
        Task<IList<SellElements>> getSellElementFVAsync();
        Task<bool> updateAsyncSellElementFV(SellElements obj);
        Task<bool> removeSellElementsFVAsync(SellElements obj);
        bool RemoveSellElementsFV();

        Task<bool> addSellElementFRAsync(SellElements obj);
        bool addSellElementsFRAsync(IList<SellElements> obj);
        Task<IList<SellElements>> getSellElementFRAsync();
        Task<bool> updateAsyncSellElementFR(SellElements obj);
        Task<bool> removeSellElementsFRAsync(SellElements obj);
        bool RemoveSellElementsFR();

    }
}
