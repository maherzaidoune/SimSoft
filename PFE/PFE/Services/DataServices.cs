﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using PFE.Models;
using System.Reactive.Linq;


namespace PFE.Services
{
    public class DataServices : IDataServices
    {

        public async Task<bool> addStockLigneMIAsync(StockLigne obj)
        {
            if (obj == null)
                return false;
            try
            {
                var stocks = await getStockLigneObjectsMIAsync();
                if (stocks == null)
                {
                    stocks = new List<StockLigne>();
                }
                for (int i = 0; i < stocks.Count; i++)
                {

                    if (obj.code.Equals(stocks[i].code) && obj.prix.Equals(stocks[i].prix) && (obj.sense == 1) && obj.depin.DEPID == stocks[i].depin.DEPID && (int.Parse(obj.quantite) > 0))
                    {
                        obj.quantite = (int.Parse(stocks[i].quantite) + int.Parse(obj.quantite)).ToString();
                        if (await RemoveStockLigneMIAsync(stocks[i]))
                        {
                            stocks.RemoveAt(i);
                            stocks.Add(obj);
                            return addStockLigneListMIAsync(stocks);
                        }
                    }
                }
                if (int.Parse(obj.quantite) > 0)
                {
                    stocks.Add(obj);
                    return addStockLigneListMIAsync(stocks);
                }
                return false;

            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error adding to stock element list" + e.StackTrace);
                return false;
            }
        }

        public bool addStockLigneListMIAsync(IList<StockLigne> obj)
        {
            try
            {
                BlobCache.UserAccount.Invalidate("stockLigneMI");
                BlobCache.UserAccount.InsertObject("stockLigneMI", obj);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error saving list of stock ligne elements");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public async Task<IList<StockLigne>> getStockLigneObjectsMIAsync()
        {
            try
            {
                // need much test
                var Stocks = await BlobCache.UserAccount.GetObject<IList<StockLigne>>("stockLigneMI");
                return Stocks;
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error getting list of stock ligne elements");
                Console.WriteLine(e.StackTrace);
                return null;

            }
        }

        public async Task<bool> RemoveStockLigneMIAsync(StockLigne obj)
        {
            if (obj == null)
                return false;
            IList<StockLigne> stocks = new List<StockLigne>();
            try
            {
                stocks = await getStockLigneObjectsMIAsync();
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
                return addStockLigneListMIAsync(stocks);
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error deleting to stock element list" + e.StackTrace);
                return false;
            }

        }


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
                BlobCache.UserAccount.Invalidate("stockLigneMI");
                BlobCache.UserAccount.InsertObject("stockLigneMI", stocks);
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
                //for (int i = 0; i < stocks.Count; i++)
                //{

                //    if (obj.code.Equals(stocks[i].code) && obj.prix.Equals(stocks[i].prix) && (obj.sense == 0) && obj.depin.DEPID == stocks[i].depin.DEPID && obj.depout.DEPID == stocks[i].depout.DEPID && (int.Parse(obj.quantite) > 0))
                //    {
                //        obj.quantite = (int.Parse(stocks[i].quantite) + int.Parse(obj.quantite)).ToString();
                //        if (await RemoveStockLigneMTAsync(stocks[i]))
                //        {
                //            stocks.RemoveAt(i);
                //            stocks.Add(obj);
                //            return addStockLigneListMTAsync(stocks);
                //        }
                //    }
                //}
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
                //var stocks = await getSellElementAsync();
                //if (stocks == null)
                //{
                //    stocks = new List<SellElements>();
                //}
                //else
                //    if (stocks.Count > 0)
                //{
                //    if (!stocks[0].type.Equals(obj.type))
                //    {
                //        RemoveSellElements();
                //        stocks = new List<SellElements>();
                //    }
                //}
                var stocks = new List<SellElements>();
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


                if (stocks.Count > 0)
                {

                    if (stocks[0].ligneUpdated == false)
                    {
                        stocks[0].depot = obj.depot;
                        stocks[0].tva = obj.tva;
                        stocks[0].articles = obj.articles;
                        stocks[0].artarifligne = obj.artarifligne;
                        stocks[0].LivredQuantity = obj.LivredQuantity;
                        stocks[0].mutht = obj.mutht;
                        stocks[0].mtht = obj.mtht;
                        stocks[0].mttc = obj.mttc;
                        stocks[0].artarifligne = obj.artarifligne;
                        stocks[0].ligneUpdated = obj.ligneUpdated;
                        stocks[0].remise = obj.remise;
                        stocks[0].numpiece = stocks[0].numauto.NUMSOUCHE + "000" + (stocks[0].count).ToString();
                    }
                    else
                    {
                        SellElements s = new SellElements();
                        s.pIECE_NATURE = stocks[0].pIECE_NATURE;
                        s.type = stocks[0].type;
                        //s.affaire = stocks[0].affaire;
                        s.tiers = stocks[0].tiers;
                        s.numpiece = stocks[0].numpiece;
                        s.numauto = stocks[0].numauto;
                        s.count = stocks[0].count;

                        s.remise = obj.remise;
                        s.depot = obj.depot;
                        s.tva = obj.tva;
                        s.articles = obj.articles;
                        s.artarifligne = obj.artarifligne;
                        s.LivredQuantity = obj.LivredQuantity;
                        s.mutht = obj.mutht;
                        s.mtht = obj.mtht;
                        s.mttc = obj.mttc;
                        s.artarifligne = obj.artarifligne;
                        s.ligneUpdated = obj.ligneUpdated;
                        s.numpiece = stocks[0].numauto.NUMSOUCHE + "000" + (stocks.Count).ToString();
                        stocks.Add(s);
                    }

                    Console.WriteLine(stocks);
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



        public async Task<bool> addBuyElementAsync(Buyelement obj)
        {
            if (obj == null)
                return false;
            try
            {
                //var stocks = await getBuyElementAsync();
                //if (stocks == null)
                //{
                //    stocks = new List<Buyelement>();
                //}
                //else
                //    if (stocks.Count > 0)
                //{
                //    if (!stocks[0].type.Equals(obj.type))
                //    {
                //        RemoveSellElements();
                //        stocks = new List<Buyelement>();
                //    }
                //}
                var stocks = new List<Buyelement>();
                stocks.Add(obj);
                return addBuyElementsAsync(stocks);
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error adding to sell element list" + e.StackTrace);
                return false;
            }
        }

        public bool addBuyElementsAsync(IList<Buyelement> obj)
        {
            try
            {
                BlobCache.UserAccount.Invalidate("BuyElements");
                BlobCache.UserAccount.InsertObject("BuyElements", obj);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error saving list of BUY elements");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public async Task<IList<Buyelement>> getBuyElementAsync()
        {
            try
            {
                // need much test
                var Stocks = await BlobCache.UserAccount.GetObject<IList<Buyelement>>("BuyElements");
                return Stocks;
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error getting list of buy elements ");
                Console.WriteLine(e.StackTrace);
                return null;

            }
        }

        public async Task<bool> updateAsyncBuyElement(Buyelement obj)
        {
            if (obj == null)
                return false;
            IList<Buyelement> stocks = new List<Buyelement>();
            try
            {
                stocks = await getBuyElementAsync();

                if (stocks == null)
                {
                    return false;
                }
                if (stocks.Count > 0)
                {

                    if (stocks[0].ligneUpdated == false)
                    {
                        stocks[0].depot = obj.depot;
                        stocks[0].tva = obj.tva;
                        stocks[0].articles = obj.articles;
                        stocks[0].artarifligne = obj.artarifligne;
                        stocks[0].LivredQuantity = obj.LivredQuantity;
                        stocks[0].mutht = obj.mutht;
                        stocks[0].mtht = obj.mtht;
                        stocks[0].mttc = obj.mttc;
                        stocks[0].remise = obj.remise;
                        stocks[0].artarifligne = obj.artarifligne;
                        stocks[0].ligneUpdated = obj.ligneUpdated;
                        stocks[0].numpiece = stocks[0].numauto.NUMSOUCHE + "000" + (stocks[0].count).ToString();
                    }
                    else
                    {
                        Buyelement s = new Buyelement();
                        s.pIECE_NATURE = stocks[0].pIECE_NATURE;
                        s.type = stocks[0].type;
                        //s.affaire = stocks[0].affaire;
                        s.tiers = stocks[0].tiers;
                        s.numpiece = stocks[0].numpiece;
                        s.numauto = stocks[0].numauto;
                        s.count = stocks[0].count;

                        s.remise = obj.remise;
                        s.depot = obj.depot;
                        s.tva = obj.tva;
                        s.articles = obj.articles;
                        s.artarifligne = obj.artarifligne;
                        s.LivredQuantity = obj.LivredQuantity;
                        s.mutht = obj.mutht;
                        s.mtht = obj.mtht;
                        s.mttc = obj.mttc;
                        s.artarifligne = obj.artarifligne;
                        s.ligneUpdated = obj.ligneUpdated;
                        s.numpiece = stocks[0].numauto.NUMSOUCHE + "000" + ( stocks.Count).ToString();
                        stocks.Add(s);
                    }

                }
                return addBuyElementsAsync(stocks);
            }
            catch (Exception e)
            {
                Console.WriteLine("update bl" + e.StackTrace);
                return false;
            }
        }

        public async Task<bool> removeBuyElementsAsync(Buyelement obj)
        {
            if (obj == null)
                return false;
            IList<Buyelement> stocks = new List<Buyelement>();
            try
            {
                stocks = await getBuyElementAsync();
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
                return addBuyElementsAsync(stocks);
            }
            catch (Exception e)
            {
                Console.WriteLine("Data service ==== error deleting to sell list" + e.StackTrace);
                return false;
            }
        }

        public bool RemoveBuyElements()
        {
            var stocks = new List<SellElements>();
            try
            {
                BlobCache.UserAccount.Invalidate("BuyElements");
                BlobCache.UserAccount.InsertObject("BuyElements", stocks);
                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}