using Flurl.Http;
using PFE.Helper;
using PFE.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PFE.Services
{
    public class RestServices : IRestServices
    {
        public Task<IList<UTILISATEURSGRP>> GetGroupAsync()
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                var l =  c.group_uri.GetJsonAsync<IList<UTILISATEURSGRP>>();
                return l;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Task<IList<UTILISATEUR>> GetUserByGroupIdAsync(string groupId)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                var user =  (c.user_uri + "?filter[where][USERGRP]=" + groupId).GetJsonAsync<IList<UTILISATEUR>>();
                return user;
                
            }
            catch (FlurlHttpException e)
            {
                //userNotAuth();
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Task<IList<UTILISATEUR>> GetUserAsync()
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return c.user_uri.GetJsonAsync<IList<UTILISATEUR>>();
            }
            catch (FlurlHttpException)
            {
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool Login(UTILISATEUR user)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.user_uri + "count?where={\"and\":[{\"USRLOGIN\":" + user.USRLOGIN + "},{\"USRPWD\":\" " + user.USRPWD + "}]}").GetJsonAsync<login>().Result.count > 0;
            }
            catch (FlurlHttpException)
            {
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public Task<IList<TIERS>> GetTiers(string info)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");

                if (string.IsNullOrEmpty(info))
                {
                    return c.tiers_uri.GetJsonAsync<IList<TIERS>>();
                }
                return (c.tiers_uri + "?filter[where][or][0][TIRSOCIETE]=" + info + "&filter[where][or][1][TIRCODE]=" + info ).GetJsonAsync<IList<TIERS>>();
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Task<IList<PIECE_NATURE>> GetPieceNature(string PICCODE = null, string PITCODE = null, string PINLIBELLE = null, string PINSENSSTOCK = null, bool like = false )
        {
            Constant c = new Constant("http://192.168.43.174:3000");
            try
            {
                if(string.IsNullOrEmpty(PINLIBELLE) && string.IsNullOrEmpty(PINSENSSTOCK)) 
                    return (c.piece_nature_uri + "?filter[where][and][0][PICCODE]=" + PICCODE + "&filter[where][and][1][PITCODE]=" + PITCODE).GetJsonAsync<IList<PIECE_NATURE>>();
                if (string.IsNullOrEmpty(PINLIBELLE))
                    return (c.piece_nature_uri + "?filter[where][and][0][PICCODE]=" + PICCODE + "&filter[where][and][1][PITCODE]=" + PITCODE + "&filter[where][and][2][PINSENSSTOCK]=" + PINSENSSTOCK).GetJsonAsync<IList<PIECE_NATURE>>();

                if(like){
                    return (c.piece_nature_uri + "?filter[where][and][0][PICCODE]=" + PICCODE + "&filter[where][and][1][PITCODE]=" + PITCODE +  "&filter[where][and][2][PINLIBELLE][like]=" + PINLIBELLE).GetJsonAsync<IList<PIECE_NATURE>>();
                }
                return (c.piece_nature_uri + "?filter[where][and][0][PICCODE]=" + PICCODE + "&filter[where][and][1][PITCODE]=" + PITCODE + "&filter[where][and][2][PINLIBELLE][nlike]=" + PINLIBELLE).GetJsonAsync<IList<PIECE_NATURE>>();
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Task<IList<PIECE_NATURE>> GetPieceNaturebyPINID(String PINID)
        {
            Constant c = new Constant("http://192.168.43.174:3000");
            try
            {
                return (c.piece_nature_uri + "?filter[where][PINID]=" + PINID ).GetJsonAsync<IList<PIECE_NATURE>>();
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }


        public Task<IList<depot>> GetDepot(string DEPISACTIF, string DEPISPRINCIPAL = null)
        {
            Constant c = new Constant("http://192.168.43.174:3000");
            try
            {
                if (string.IsNullOrEmpty(DEPISPRINCIPAL)){
                    return  (c.depot_url + "?filter[where][DEPISACTIF]=" + DEPISACTIF).GetJsonAsync<IList<depot>>();
                    }
                return (c.depot_url + "?filter[where][and][0][DEPISACTIF]=" + DEPISACTIF + "&filter[where][and][1][DEPISPRINCIPAL]=" + DEPISPRINCIPAL ).GetJsonAsync<IList<depot>>();
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Task<IList<AFFAIRE>> GetAffaire(string info)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");

                if (string.IsNullOrEmpty(info))
                {
                    return c.affaire_uri.GetJsonAsync<IList<AFFAIRE>>();
                }
                return (c.affaire_uri + "?filter[where][or][0][AFFCODE]=" + info + "&filter[where][or][1][AFFINTITULE]=" + info).GetJsonAsync<IList<AFFAIRE>>();
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ARTICLE getArticlebyBC(string bc)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.article_url + "?filter[where][ARTCODEBARRE]=" + bc).GetJsonAsync<IList<ARTICLE>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public NUMAUTO getNumPiecenyNature(string nature)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                PIECE_PREF  prefs = GetPIECE_PREF(nature);
                var numauto =  (c.numauto_url + "?filter[where][NUMID]=" + prefs.NUMID ).GetJsonAsync<IList<NUMAUTO>>().Result;
                return numauto[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int userNumber()
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                var count =  (c.user_uri + "/count").GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public bool addUser(UTILISATEUR user)
        {
            try{
                Constant c = new Constant("http://192.168.43.174:3000");
                var id = userNumber();

                user.USRID = id + 1;
                if (user.USERGRP == 50)
                    user.PROID = 1;
                else
                    user.PROID = 2;
                return (c.user_uri).PostJsonAsync(new {
                    USRID = user.USRID,
                    USRLOGIN =  user.USRLOGIN,
                    PROID =  user.PROID,
                    USRRESERVE =  "N",
                    USRPWD = user.USRPWD,
                    USERGRP = user.USERGRP
                }).Result.IsSuccessStatusCode;
            }
            catch(Exception e){
                Console.WriteLine(e.StackTrace);
                return false;
            }

        }

        public bool addUsers(IList<UTILISATEUR> users)
        {
            try{
                foreach (UTILISATEUR u in users)
                {
                    if (!addUser(u))
                        return false;
                }
                return true;
            }
            catch(Exception e){
                Console.WriteLine(e.StackTrace);
                return false;
            }

        }

        public ARTICLE getArticlebyid(string id)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.article_url + "?filter[where][ARTID]=" + id).GetJsonAsync<IList<ARTICLE>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ARTFAMILLES_CPT GetARTFAMILLES_CPTbyARFID(string ARFID)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.ARTFAMILLES_CPTs_url + "?filter[where][ARFID]=" + ARFID).GetJsonAsync<IList<ARTFAMILLES_CPT>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ARTTARIFLIGNE GetRTTARIFLIGNEbyARTID(string ARTID)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.ARTTARIFLIGNEs_url + "?filter[where][ARTID]=" + ARTID).GetJsonAsync<IList<ARTTARIFLIGNE>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public TVA GetTVAbyTVACODE(string TVACODE)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.TVAs_url + "?filter[where][TVACODE]=" + TVACODE).GetJsonAsync<IList<TVA>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ARTUNITE GetRTUNITE(string type)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.ARTUNITEs_url + "?filter[where][AUTCODE]=" + type).GetJsonAsync<IList<ARTUNITE>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ARTDEPOT GetARTDEPOTbyDepid(string DEPID)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.ARTDEPOTs_url + "?filter[where][DEPID]=" + DEPID).GetJsonAsync<IList<ARTDEPOT>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ARTDEPOT GetARTDEPOTbyDepArtid(string ARTID)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.ARTDEPOTs_url + "?filter[where][ARTID]=" + ARTID).GetJsonAsync<IList<ARTDEPOT>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool PostToStock(IList<StockLigne> stocks)
        {
            try{
                foreach(StockLigne s in stocks){
                    if(PostStockElement(s, stocks.Count))
                        Console.WriteLine(s.designation + "post succefully ");
                }
                return true;
            }catch(Exception ex){
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public bool PostStockElement(StockLigne obj, int num)
        {
            try{
                    MEMOS m = new MEMOS
                    {
                        MEMOISALERTE = "N"
                    };
                string PCDNUMEXT = "";
                string type = "";
                int PCDID = getPieceDiversNumber() + 1;
                if (obj.depin != null && obj.depout != null){
                    PCDNUMEXT = "Bon de transfert de dépot";
                    type = "BTR";
                }
                else if (obj.depin != null){
                    PCDNUMEXT = "Bon entrée en stock";
                    type = "SIN";
                }
                else{
                    PCDNUMEXT = "Bon sortir en stock";
                    type = "SOUT";
                }


                ARTDEPOT depo = GetARTDEPOTbyDepArtid(obj.article.ARTID.ToString());

                // artdepot problem of multiple id


                PIECE_PREF prefs = GetPIECE_PREF(obj.pIECE_NATURE.PINID.ToString() , "o");

                PIECEDIVERS p = new PIECEDIVERS
                {
                        PCDID = PCDID,
                        PCDNUM = Helper.Strings.getNum(PCDID,type),
                        PCDMNTHT = int.Parse(obj.prix),
                        PCDNUMEXT = PCDNUMEXT,
                        DEPID_IN =   (obj.depin!=null )? (int)obj.depin.DEPID : 0,
                        DEPID_OUT = (obj.depout != null) ? (int)obj.depout.DEPID : 0,
                        PCDPOIDS = int.Parse(obj.quantite),
                        PITCODE = "B",
                        PINID = obj.pIECE_NATURE.PINID,
                        PINCODE = obj.designation,
                        EXEID = 1,
                        TRFID = 1,
                        PCDNBIMPRESSION = 0,
                        PCDUNITEPOIDS = 1000,
                        PCDDATEEFFET = DateTime.Now,
                        PCDDATE_FIN = DateTime.Now,
                        PCDISSOLDE = "N",
                        PCDISACTIVE = "N",
                        PCDISCLOS = "N",
                        PCDISPRINT = "N",
                        PCDNBPRINT = 1,
                        PCDMNTTTC = 0,
                        USRMODIF = "",
                        DATECREATE = DateTime.Now,
                        DATEUPDATE = DateTime.Now,
                        PCDDATEPRINT = DateTime.Now,
                        PCDDATE_DEBUT = DateTime.Now,
                        MEMOID = getMemosNumber() + 1,
                        MODID = prefs.MODID,
                        NUMID = (int)obj.numauto.NUMID,
                        SOCID=1
                };

                PIECEDIVERSLIGNES pl = new PIECEDIVERSLIGNES
                {

                    PLDNUMLIGNE = num,
                    PLDTYPE = "L",
                    PLDDATE = DateTime.Now,
                    DEPID = obj.depin == null ? obj.depout.DEPID : obj.depin.DEPID,
                    TIRIDFOU = 0,
                    PLDISSOUMISESC = "N",
                    PLDDESIGNATION = obj.article.ARTDESIGNATION,
                    TPFCODE = 0,
                    PCDID = (int)p.PCDID,
                    PLDSTOTID = 1,
                    PLDCOEFFUV = 100000,
                    ARTTYPE = "A",
                    PLDFRAIS1T = "P",
                    PLDFRAIS2T = "P",
                    PLDFRAIS3T = "P",
                    PLDREMISE_T = "P",
                    PLDQTE = int.Parse(obj.quantite),
                    PLDQTEUS = int.Parse(obj.quantite),
                    ARTID = obj.article.ARTID,
                    PLDISFORFAIT = "N",
                    PLDMNTNETHT = int.Parse(obj.prix),
                    TVACODE = (int?)GetTVAbyTVACODE(obj.artfamilles_cpt.TVACODE_FR.ToString()).TVACODE,
                    PLDID = getPieceDiversLignesNumber() + 1
                };

                OPERATIONSTOCK o = new OPERATIONSTOCK
                {
                    OPEID = getOperationStockNumber() + 20,
                    DATECREATE = DateTime.Now,
                    DATEUPDATE = DateTime.Now,
                    //USRMODIF = "ADM",
                    OPEDATE = DateTime.Now,
                    ARTID = obj.article.ARTID,
                    DEPID = obj.depin.DEPID,
                    USRMODIF = Helper.Session.user.USRNOM,
                    PICCODE = "S",
                    PINID = obj.pIECE_NATURE.PINID,
                    OPENATURESTOCK = "R",
                    OPEQUANTITE = short.Parse(obj.quantite),
                    OPESENS = short.Parse(obj.sense.ToString()),
                    OPETYPE = "N",
                    OPEISMAJPA = "O",
                    OPEISBLOQUE = "N",
                    SOCID = 1,
                    OPEISCLOS = "N",
                    PCID = pl.PCDID,
                    PLID = pl.PLDID,
                    CTMID=0,
                    //tirid
                    //opuint
                };


                return PostPiecedivers(p) &&
                       PostPiecediversLigne(pl) &&
                       PostOperationStock(o) &&
                       PostIdentityTable("operationstock") &&
                       PostIdentityTable("piecedivers");

            }catch(Exception e){

                    Console.WriteLine(e.StackTrace);
                    return false;
                  
            } 

        }

        public bool PostPiecedivers(PIECEDIVERS piecediverse)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.PIECEDIVERs_url).PostJsonAsync(piecediverse).Result.IsSuccessStatusCode;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool PostOperationStock(OPERATIONSTOCK operationStock)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.OPERATIONSTOCKs_url).PostJsonAsync(operationStock).Result.IsSuccessStatusCode;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool PostIdentityTable(string idTable)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                var table = GetIDENTIFIANTTABLE(idTable);

                return (c.IDENTIFIANTTABLEs_url + "/" + idTable).PatchJsonAsync(new
                {
                    CTNTABLE = idTable,
                    CTNLASTID = table.CTNLASTID + 1
                }).Result.IsSuccessStatusCode;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public IDENTIFIANTTABLE GetIDENTIFIANTTABLE(string idTable)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return  (c.IDENTIFIANTTABLEs_url + "/" + idTable).GetJsonAsync<IDENTIFIANTTABLE>().Result;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int getPieceDiversNumber()
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                var count = (c.PIECEDIVERs_url + "/count").GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public int getOperationStockNumber()
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                var count = (c.OPERATIONSTOCKs_url + "/count").GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public bool PostMemos(MEMOS memos)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.MEMOS_url).PostJsonAsync(memos).Result.IsSuccessStatusCode;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public int getMemosNumber()
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                var count = (c.MEMOS_url + "/count").GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public bool PostStockElement(StockLigne obj)
        {
            throw new NotImplementedException();
        }

        public PIECE_PREF GetPIECE_PREF(string id , string PIPISDEFAULT = null)
        {
            try{
                Constant c = new Constant("http://192.168.43.174:3000");
                if(PIPISDEFAULT != null){
                    return (c.pieceprefs_url + "?filter[where][and][0][PINID] = " + id + " & filter[where][and][1][PIPISDEFAULT] = " + PIPISDEFAULT).GetJsonAsync<IList<PIECE_PREF>>().Result[0];
                }
                return (c.pieceprefs_url + "?filter[where][PINID]=" + id).GetJsonAsync<IList<PIECE_PREF>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int getPieceDiversLignesNumber()
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                var count = (c.PIECEDIVERSLIGNE_url + "/count").GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public bool PostPiecediversLigne(PIECEDIVERSLIGNES piecediverseligne)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.PIECEDIVERSLIGNE_url).PostJsonAsync(piecediverseligne).Result.IsSuccessStatusCode;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool PatchArtdepot(ARTDEPOT artdepot, string artid)
        {
            try
            {
                Constant c = new Constant("http://192.168.43.174:3000");
                return (c.ARTDEPOTs_url + "/" + artid).PatchJsonAsync(artdepot).Result.IsSuccessStatusCode;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /*
        public PIECEVENTELIGNE GetPIECEVENTELIGNEbyARTID(string artid)
        {
            Constant c = new Constant("http://192.168.43.174:3000");
            try
            {
                return (c.PIECEDIVERSLIGNE_url + "?filter[where][ARTID]=" + artid).GetJsonAsync<IList<PIECEVENTELIGNE>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
            }
            catch (Exception ex)
            {
                //noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        } */
    }

}
