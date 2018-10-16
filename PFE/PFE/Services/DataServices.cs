using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using PFE.Models;
using System.Reactive.Linq;


namespace PFE.Services
{
    public class DataServices : IDataServices
    {
        public async Task<bool> addStockLigneMEAsync(StockLigne obj)
        {
            if (obj == null)
                return false;
            try
            {
                var stocks = await getStockLigneObjectsMEAsync();
                if (stocks == null)
                {
                    stocks = new List<StockLigne>();
                }
                for (int i = 0; i < stocks.Count; i++)
                {

                    if (obj.code.Equals(stocks[i].code) && obj.prix.Equals(stocks[i].prix) && (obj.sense == 1) && obj.depin.DEPID == stocks[i].depin.DEPID && (int.Parse(obj.quantite) > 0))
                    {
                        obj.quantite = (int.Parse(stocks[i].quantite) + int.Parse(obj.quantite)).ToString();
                        if (await RemoveStockLigneMEAsync(stocks[i]))
                        {
                            stocks.RemoveAt(i);
                            stocks.Add(obj);
                            return addStockLigneListMEAsync(stocks);
                        }
                    }
                }
                if (int.Parse(obj.quantite) > 0)
                {
                    stocks.Add(obj);
                    return addStockLigneListMEAsync(stocks);
                }
                return false;

            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error adding to stock element list" + e.StackTrace);
                return false;
            }
        }

        public bool addStockLigneListMEAsync(IList<StockLigne> obj)
        {
            try
            {
                BlobCache.UserAccount.Invalidate("stockLigneME");
                BlobCache.UserAccount.InsertObject("stockLigneME", obj);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error saving list of stock ligne elements");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public async Task<IList<StockLigne>> getStockLigneObjectsMEAsync()
        {
            try
            {
                // need much test
                var Stocks = await BlobCache.UserAccount.GetObject<IList<StockLigne>>("stockLigneME");
                return Stocks;
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error getting list of stock ligne elements");
                Console.WriteLine(e.StackTrace);
                return null;

            }
        }

        public async Task<bool> RemoveStockLigneMEAsync(StockLigne obj)
        {
            if (obj == null)
                return false;
            IList<StockLigne> stocks = new List<StockLigne>();
            try
            {
                stocks = await getStockLigneObjectsMEAsync();
                if (stocks == null)
                {
                    return false;
                }

                for (int i = 0; i < stocks.Count; i++)
                {
                    if (obj.code.Equals(stocks[i].code) && obj.prix.Equals(stocks[i].prix) && (obj.sense == 1) && obj.depin.DEPID == stocks[i].depin.DEPID)
                    {
                        stocks.RemoveAt(i);
                    }
                }
                return addStockLigneListMEAsync(stocks);
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error deleting to stock element list" + e.StackTrace);
                return false;
            }

        }

        public async Task<bool> addStockLigneMSAsync(StockLigne obj)
        {
            if (obj == null)
                return false;
            try
            {
                var stocks = await getStockLigneObjectsMSAsync();
                if (stocks == null)
                {
                    stocks = new List<StockLigne>();
                }
                for (int i = 0; i < stocks.Count; i++)
                {

                    if (obj.code.Equals(stocks[i].code) && obj.prix.Equals(stocks[i].prix) && (obj.sense == -1) && obj.depout.DEPID == stocks[i].depout.DEPID && (int.Parse(obj.quantite) > 0))
                    {
                        obj.quantite = (int.Parse(stocks[i].quantite) + int.Parse(obj.quantite)).ToString();
                        if (await RemoveStockLigneMSAsync(stocks[i]))
                        {
                            stocks.RemoveAt(i);
                            stocks.Add(obj);
                            return addStockLigneListMSAsync(stocks);
                        }
                    }
                }
                if (int.Parse(obj.quantite) > 0)
                {
                    stocks.Add(obj);
                    return addStockLigneListMSAsync(stocks);
                }
                return false;

            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error adding to stock element list" + e.StackTrace);
                return false;
            }
        }

        public async Task<bool> RemoveStockLigneMSAsync(StockLigne obj)
        {
            if (obj == null)
                return false;
            IList<StockLigne> stocks = new List<StockLigne>();
            try
            {
                stocks = await getStockLigneObjectsMSAsync();
                if (stocks == null)
                {
                    return false;
                }

                for (int i = 0; i < stocks.Count; i++)
                {
                    if (obj.code.Equals(stocks[i].code) && obj.prix.Equals(stocks[i].prix) && (obj.sense == -1) && obj.depout.DEPID == stocks[i].depout.DEPID)
                    {
                        stocks.RemoveAt(i);
                    }
                }
                return addStockLigneListMSAsync(stocks);
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error deleting to stock element list" + e.StackTrace);
                return false;
            }
        }

        public bool addStockLigneListMSAsync(IList<StockLigne> obj)
        {
            try
            {
                BlobCache.UserAccount.Invalidate("stockLigneMS");
                BlobCache.UserAccount.InsertObject("stockLigneMS", obj);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error saving list of stock ligne elements");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public async Task<IList<StockLigne>> getStockLigneObjectsMSAsync()
        {
            try
            {
                // need much test
                var Stocks = await BlobCache.UserAccount.GetObject<IList<StockLigne>>("stockLigneMS");
                return Stocks;
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error getting list of stock ligne elements");
                Console.WriteLine(e.StackTrace);
                return null;

            }
        }

        public bool RemoveStockLigne()
        {
            var stocks = new List<StockLigne>();
            try
            {
                BlobCache.UserAccount.Invalidate("stockLigneME");
                BlobCache.UserAccount.InsertObject("stockLigneME", stocks);
                BlobCache.UserAccount.Invalidate("stockLigneMS");
                BlobCache.UserAccount.InsertObject("stockLigneMS", stocks);
                BlobCache.UserAccount.Invalidate("stockLigneMT");
                BlobCache.UserAccount.InsertObject("stockLigneMT", stocks);
                return true;
            }
            catch
            {

                return false;
            }
        }

        public async Task<bool> addStockLigneMTAsync(StockLigne obj)
        {
            if (obj == null)
                return false;
            try
            {
                var stocks = await getStockLigneObjectsMTAsync();
                if (stocks == null)
                {
                    stocks = new List<StockLigne>();
                }
                for (int i = 0; i < stocks.Count; i++)
                {

                    if (obj.code.Equals(stocks[i].code) && obj.prix.Equals(stocks[i].prix) && (obj.sense == 0) && obj.depin.DEPID == stocks[i].depin.DEPID && obj.depout.DEPID == stocks[i].depout.DEPID && (int.Parse(obj.quantite) > 0))
                    {
                        obj.quantite = (int.Parse(stocks[i].quantite) + int.Parse(obj.quantite)).ToString();
                        if (await RemoveStockLigneMTAsync(stocks[i]))
                        {
                            stocks.RemoveAt(i);
                            stocks.Add(obj);
                            return addStockLigneListMTAsync(stocks);
                        }
                    }
                }
                if (int.Parse(obj.quantite) > 0)
                {
                    stocks.Add(obj);
                    return addStockLigneListMTAsync(stocks);
                }
                return false;

            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error adding to stock element list" + e.StackTrace);
                return false;
            }
        }

        public bool addStockLigneListMTAsync(IList<StockLigne> obj)
        {
            try
            {
                BlobCache.UserAccount.Invalidate("stockLigneMT");
                BlobCache.UserAccount.InsertObject("stockLigneMT", obj);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error saving list of stock ligne elements");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public async Task<bool> RemoveStockLigneMTAsync(StockLigne obj)
        {
            if (obj == null)
                return false;
            IList<StockLigne> stocks = new List<StockLigne>();
            try
            {
                stocks = await getStockLigneObjectsMTAsync();
                if (stocks == null)
                {
                    return false;
                }

                for (int i = 0; i < stocks.Count; i++)
                {
                    if (obj.code.Equals(stocks[i].code) && obj.prix.Equals(stocks[i].prix) && (obj.sense == 0) && obj.depin.DEPID == stocks[i].depin.DEPID && obj.depout.DEPID == stocks[i].depout.DEPID)
                    {
                        stocks.RemoveAt(i);
                    }
                }
                return addStockLigneListMTAsync(stocks);
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error deleting to stock element list" + e.StackTrace);
                return false;
            }
        }

        public async Task<IList<StockLigne>> getStockLigneObjectsMTAsync()
        {
            try
            {
                // need much test
                var Stocks = await BlobCache.UserAccount.GetObject<IList<StockLigne>>("stockLigneMT");
                return Stocks;
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error getting list of stock ligne elements");
                Console.WriteLine(e.StackTrace);
                return null;

            }
        }




        // sell elements

        public async Task<bool> addSellElementAsync(SellElements obj)
        {
            if (obj == null)
                return false;
            try
            {
                var stocks = await getSellElementAsync();
                if (stocks == null)
                {
                    stocks = new List<SellElements>();
                }
                else
                    if (stocks.Count > 0)
                {
                    if (!stocks[0].type.Equals(obj.type))
                    {
                        RemoveSellElements();
                        stocks = new List<SellElements>();
                    }
                }
                stocks.Add(obj);
                return addSellElementsAsync(stocks);
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error adding to sell element list" + e.StackTrace);
                return false;
            }
        }

        public bool addSellElementsAsync(IList<SellElements> obj)
        {
            try
            {
                BlobCache.UserAccount.Invalidate("SellElements");
                BlobCache.UserAccount.InsertObject("SellElements", obj);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error saving list of sell elements");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public async Task<IList<SellElements>> getSellElementAsync()
        {
            try
            {
                // need much test
                var Stocks = await BlobCache.UserAccount.GetObject<IList<SellElements>>("SellElements");
                return Stocks;
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error getting list of sell elements BL");
                Console.WriteLine(e.StackTrace);
                return null;

            }
        }

        public async Task<bool> updateAsyncSellElement(SellElements obj)
        {
            if (obj == null)
                return false;
            IList<SellElements> stocks = new List<SellElements>();
            try
            {
                stocks = await getSellElementAsync();
                if (stocks == null)
                {
                    return false;
                }

                for (int i = 0; i < stocks.Count; i++)
                {
                    if(!stocks[i].ligneUpdated == true){
                        stocks[i].depot = obj.depot;
                        stocks[i].tva = obj.tva;
                        stocks[i].articles = obj.articles;
                        stocks[i].artarifligne = obj.artarifligne;
                        stocks[i].LivredQuantity = obj.LivredQuantity;
                        stocks[i].mutht = obj.mutht;
                        stocks[i].mtht = obj.mtht;
                        stocks[i].mttc = obj.mttc;
                        stocks[i].artarifligne = obj.artarifligne;
                    }

                }
                return addSellElementsAsync(stocks);
            }
            catch (Exception e)
            {
                Console.WriteLine("update bl" + e.StackTrace);
                return false;
            }
        }

        public async Task<bool> removeSellElementsAsync(SellElements obj)
        {
            if (obj == null)
                return false;
            IList<SellElements> stocks = new List<SellElements>();
            try
            {
                stocks = await getSellElementAsync();
                if (stocks == null)
                {
                    return false;
                }

                for (int i = 0; i < stocks.Count; i++)
                {
                    if (obj.articles.ARTID.Equals(stocks[i].articles.ARTID) && obj.LivredQuantity.Equals(stocks[i].LivredQuantity) && obj.depot.DEPID == stocks[i].depot.DEPID)
                    {
                        stocks.RemoveAt(i);
                    }
                }
                return addSellElementsAsync(stocks);
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error deleting to sell list" + e.StackTrace);
                return false;
            }
        }

        public bool RemoveSellElements()
        {
            var stocks = new List<SellElements>();
            try
            {
                BlobCache.UserAccount.Invalidate("SellElements");
                BlobCache.UserAccount.InsertObject("SellElements", stocks);
                return true;
            }
            catch
            {

                return false;
            }
        }

    }
}
