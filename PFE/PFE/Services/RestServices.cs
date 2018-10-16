using Flurl.Http;
using PFE.Helper;
using PFE.Models;
using Plugin.Connectivity;
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
                
                var l =  Constant.group_uri.WithTimeout(7).GetJsonAsync<IList<UTILISATEURSGRP>>();
                return l;
            }
            catch (FlurlHttpException e)
            {
                noInternetConnection();
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Task<IList<UTILISATEUR>> GetUserByGroupIdAsync(string groupId)
        {
            try
            {
                
                var user =  (Constant.user_uri + "?filter[where][USERGRP]=" + groupId).WithTimeout(7).GetJsonAsync<IList<UTILISATEUR>>();
                return user;
                
            }
            catch (FlurlHttpException e)
            {
                noInternetConnection();
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Task<IList<UTILISATEUR>> GetUserAsync()
        {
            try
            {
                
                return Constant.user_uri.WithTimeout(7).GetJsonAsync<IList<UTILISATEUR>>();
            }
            catch (FlurlHttpException)
            {
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool Login(UTILISATEUR user)
        {
            try
            {
                
                return (Constant.user_uri + "count?where={\"and\":[{\"USRLOGIN\":" + user.USRLOGIN + "},{\"USRPWD\":\" " + user.USRPWD + "}]}").WithTimeout(7).GetJsonAsync<login>().Result.count > 0;
            }
            catch (FlurlHttpException)
            {
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public Task<IList<TIERS>> GetTiers(string info)
        {
            try
            {
                

                if (string.IsNullOrEmpty(info))
                {
                    return Constant.tiers_uri.WithTimeout(7).GetJsonAsync<IList<TIERS>>();
                }
                return (Constant.tiers_uri + "?filter[where][or][0][TIRSOCIETE]=" + info + "&filter[where][or][1][TIRCODE]=" + info ).WithTimeout(7).GetJsonAsync<IList<TIERS>>();
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Task<IList<PIECE_NATURE>> GetPieceNature(string PICCODE = null, string PITCODE = null, string PINLIBELLE = null, string PINSENSSTOCK = null, bool like = false )
        {
            
            try
            {
                if(string.IsNullOrEmpty(PINLIBELLE) && string.IsNullOrEmpty(PINSENSSTOCK)) 
                    return (Constant.piece_nature_uri + "?filter[where][and][0][PICCODE]=" + PICCODE + "&filter[where][and][1][PITCODE]=" + PITCODE).WithTimeout(7).GetJsonAsync<IList<PIECE_NATURE>>();
                if (string.IsNullOrEmpty(PINLIBELLE))
                    return (Constant.piece_nature_uri + "?filter[where][and][0][PICCODE]=" + PICCODE + "&filter[where][and][1][PITCODE]=" + PITCODE + "&filter[where][and][2][PINSENSSTOCK]=" + PINSENSSTOCK).WithTimeout(7).GetJsonAsync<IList<PIECE_NATURE>>();

                if(like){
                    return (Constant.piece_nature_uri + "?filter[where][and][0][PICCODE]=" + PICCODE + "&filter[where][and][1][PITCODE]=" + PITCODE +  "&filter[where][and][2][PINLIBELLE][like]=" + PINLIBELLE).WithTimeout(7).GetJsonAsync<IList<PIECE_NATURE>>();
                }
                return (Constant.piece_nature_uri + "?filter[where][and][0][PICCODE]=" + PICCODE + "&filter[where][and][1][PITCODE]=" + PITCODE + "&filter[where][and][2][PINLIBELLE][nlike]=" + PINLIBELLE).WithTimeout(7).GetJsonAsync<IList<PIECE_NATURE>>();
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Task<IList<PIECE_NATURE>> GetPieceNaturebyPINID(String PINID)
        {
            
            try
            {
                return (Constant.piece_nature_uri + "?filter[where][PINID]=" + PINID ).WithTimeout(7).GetJsonAsync<IList<PIECE_NATURE>>();
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                //userNotAuth();
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Task<IList<depot>> GetDepot(string DEPISACTIF, string DEPISPRINCIPAL = null)
        {
            
            try
            {
                if (string.IsNullOrEmpty(DEPISPRINCIPAL)){
                    return  (Constant.depot_url + "?filter[where][DEPISACTIF]=" + DEPISACTIF).WithTimeout(7).GetJsonAsync<IList<depot>>();
                    }
                return (Constant.depot_url + "?filter[where][and][0][DEPISACTIF]=" + DEPISACTIF + "&filter[where][and][1][DEPISPRINCIPAL]=" + DEPISPRINCIPAL ).WithTimeout(7).GetJsonAsync<IList<depot>>();
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Task<IList<AFFAIRE>> GetAffaire(string info)
        {
            try
            {
                

                if (string.IsNullOrEmpty(info))
                {
                    return Constant.affaire_uri.WithTimeout(7).GetJsonAsync<IList<AFFAIRE>>();
                }
                return (Constant.affaire_uri + "?filter[where][or][0][AFFCODE]=" + info + "&filter[where][or][1][AFFINTITULE]=" + info).WithTimeout(7).GetJsonAsync<IList<AFFAIRE>>();
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ARTICLE getArticlebyBC(string bc)
        {
            try
            {
                
                return (Constant.article_url + "?filter[where][ARTCODEBARRE]=" + bc).WithTimeout(7).GetJsonAsync<IList<ARTICLE>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public NUMAUTO getNumPiecenyNature(string nature)
        {
            try
            {
                
                PIECE_PREF  prefs = GetPIECE_PREF(nature);
                var numauto =  (Constant.numauto_url + "?filter[where][NUMID]=" + prefs.NUMID ).WithTimeout(7).GetJsonAsync<IList<NUMAUTO>>().Result;
                return numauto[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int userNumber()
        {
            try
            {
                
                var count =  (Constant.user_uri + "/count").WithTimeout(7).GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public bool addUser(UTILISATEUR user)
        {
            try{
                
                var id = userNumber();

                user.USRID = id + 1;
                if (user.USERGRP == 50)
                    user.PROID = 1;
                else
                    user.PROID = 2;
                return (Constant.user_uri).WithTimeout(7).WithTimeout(7).PostJsonAsync(new {
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
                noInternetConnection();
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
                noInternetConnection();
                return false;
            }

        }

        public ARTICLE getArticlebyid(string id)
        {
            try
            {
                
                return (Constant.article_url + "?filter[where][ARTID]=" + id).WithTimeout(7).GetJsonAsync<IList<ARTICLE>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ARTFAMILLES_CPT GetARTFAMILLES_CPTbyARFID(string ARFID , string ARFCLASS = null)
        {
            try
            {
                
                if (string.IsNullOrEmpty(ARFCLASS)){
                    return (Constant.ARTFAMILLES_CPTs_url + "?filter[where][ARFID]=" + ARFID).WithTimeout(7).GetJsonAsync<IList<ARTFAMILLES_CPT>>().Result[0];
                }
                return (Constant.ARTFAMILLES_CPTs_url + "?filter[where][and][0][ARFID] = " + ARFID + " & filter[where][and][1][ARFCLASS] = " + ARFCLASS).WithTimeout(7).GetJsonAsync<IList<ARTFAMILLES_CPT>>().Result[0];

            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ARTTARIFLIGNE GetRTTARIFLIGNEbyARTID(string ARTID)
        {
            try
            {
                
                return (Constant.ARTTARIFLIGNEs_url + "?filter[where][ARTID]=" + ARTID).WithTimeout(7).GetJsonAsync<IList<ARTTARIFLIGNE>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public TVA GetTVAbyTVACODE(string TVACODE)
        {
            try
            {
                
                return (Constant.TVAs_url + "?filter[where][TVACODE]=" + TVACODE).WithTimeout(7).GetJsonAsync<IList<TVA>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ARTUNITE GetRTUNITE(string type)
        {
            try
            {
                
                return (Constant.ARTUNITEs_url + "?filter[where][AUTCODE]=" + type).WithTimeout(7).GetJsonAsync<IList<ARTUNITE>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ARTDEPOT GetARTDEPOTbyDepid(string ARTID , string DEPID)
        {
            try
            {
                
                var artdep =  (Constant.ARTDEPOTs_url + "?filter[where][and][0][DEPID] = " + DEPID + " & filter[where][and][1][ARTID] = " + ARTID).WithTimeout(7).GetJsonAsync<IList<ARTDEPOT>>().Result[0];
                return artdep;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public IList<ARTDEPOT> GetARTDEPOTbyDepArtid(string ARTID)
        {
            try
            {
                
                return (Constant.ARTDEPOTs_url + "?filter[where][ARTID]=" + ARTID).WithTimeout(7).GetJsonAsync<IList<ARTDEPOT>>().Result;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
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
                noInternetConnection();
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
                ARTDEPOT artdepo = null ;
                ARTDEPOT deptransfer = null;

                if (obj.depin != null && obj.depout != null){
                    PCDNUMEXT = "Bon de transfert de dépot";
                    type = "BTR";
                    artdepo = GetARTDEPOTbyDepid(obj.article.ARTID.ToString(), obj.depin.DEPID.ToString());
                    deptransfer = GetARTDEPOTbyDepid(obj.article.ARTID.ToString(), obj.depin.DEPID.ToString());

                }
                else if (obj.depin != null){
                    PCDNUMEXT = "Bon entrée en stock";
                    type = "SIN";
                    artdepo = GetARTDEPOTbyDepid(obj.article.ARTID.ToString(), obj.depin.DEPID.ToString());
                }
                else if(obj.depout != null) {
                    PCDNUMEXT = "Bon sortir en stock";
                    type = "SOUT";
                    artdepo = GetARTDEPOTbyDepid(obj.article.ARTID.ToString(), obj.depout.DEPID.ToString());
                }


                if (type.Equals( "SIN")){
                    artdepo.DEPID = obj.depin.DEPID;
                    artdepo.ARDSTOCKREEL += int.Parse(obj.quantite);
                    if(PatchArtdepot(artdepo, obj.article.ARTID.ToString(), obj.depin.DEPID.ToString())){
                        Console.WriteLine("artdepot updated");
                    }

                }
                else if (type.Equals("SOUT"))
                {
                    try{
                        artdepo.DEPID = obj.depout.DEPID;
                        artdepo.ARDSTOCKREEL -= int.Parse(obj.quantite);
                        PatchArtdepot(artdepo, obj.article.ARTID.ToString(), obj.depout.DEPID.ToString());
                    }catch(Exception e){
                        Console.WriteLine(e.Message);
                    }

                }
                else if (type.Equals("BTR")){

                    artdepo.DEPID = obj.depout.DEPID;
                    artdepo.ARDSTOCKREEL -= int.Parse(obj.quantite);
                    PatchArtdepot(artdepo, obj.article.ARTID.ToString() , obj.depout.DEPID.ToString());

                    deptransfer.DEPID = obj.depin.DEPID;
                    artdepo.ARDSTOCKREEL +=  int.Parse(obj.quantite);
                    PatchArtdepot(artdepo, obj.article.ARTID.ToString() , obj.depin.DEPID.ToString());
                }


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
                    DEPID = (obj.depin == null) ? obj.depout.DEPID : obj.depin.DEPID,
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

                return  PostPiecedivers(p) &&
                       PostPiecediversLigne(pl) &&
                       //PostOperationStock(o) &&
                       PostIdentityTable("operationstock") &&
                       PostIdentityTable("piecedivers");

            }catch(Exception e){
                noInternetConnection();
                Console.WriteLine(e.StackTrace);
                    return false;
                  
            } 

        }

        public bool PostPiecedivers(PIECEDIVERS piecediverse)
        {
            try
            {
                
                return (Constant.PIECEDIVERs_url).WithTimeout(7).PostJsonAsync(piecediverse).Result.IsSuccessStatusCode;
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
                return (Constant.OPERATIONSTOCKs_url).WithTimeout(7).PostJsonAsync(operationStock).Result.IsSuccessStatusCode;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                noInternetConnection();
            }
            return false;
        }

        public bool PostIdentityTable(string idTable)
        {
            try
            {
                
                var table = GetIDENTIFIANTTABLE(idTable);

                return (Constant.IDENTIFIANTTABLEs_url + "/" + idTable).PatchJsonAsync(new
                {
                    CTNTABLE = idTable,
                    CTNLASTID = table.CTNLASTID + 1
                }).Result.IsSuccessStatusCode;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public IDENTIFIANTTABLE GetIDENTIFIANTTABLE(string idTable)
        {
            try
            {
                
                return  (Constant.IDENTIFIANTTABLEs_url + "/" + idTable).WithTimeout(7).GetJsonAsync<IDENTIFIANTTABLE>().Result;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                noInternetConnection();
            }
            return null;
        }

        public int getPieceDiversNumber()
        {
            try
            {
                
                var count = (Constant.PIECEDIVERs_url + "/count").WithTimeout(7).GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public int getOperationStockNumber()
        {
            try
            {
                
                var count = (Constant.OPERATIONSTOCKs_url + "/count").WithTimeout(7).GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public bool PostMemos(MEMOS memos)
        {
            try
            {
                
                return (Constant.MEMOS_url).WithTimeout(7).PostJsonAsync(memos).Result.IsSuccessStatusCode;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public int getMemosNumber()
        {
            try
            {
                
                var count = (Constant.MEMOS_url + "/count").WithTimeout(7).GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

      

        public PIECE_PREF GetPIECE_PREF(string id , string PIPISDEFAULT = null)
        {
            try{
                
                if(PIPISDEFAULT != null){
                    return (Constant.pieceprefs_url + "?filter[where][and][0][PINID] = " + id + " & filter[where][and][1][PIPISDEFAULT] = " + PIPISDEFAULT).WithTimeout(7).GetJsonAsync<IList<PIECE_PREF>>().Result[0];
                }
                return (Constant.pieceprefs_url + "?filter[where][PINID]=" + id).WithTimeout(7).GetJsonAsync<IList<PIECE_PREF>>().Result[0];
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int getPieceDiversLignesNumber()
        {
            try
            {
                
                var count = (Constant.PIECEDIVERSLIGNE_url + "/count").WithTimeout(7).GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public bool PostPiecediversLigne(PIECEDIVERSLIGNES piecediverseligne)
        {
            try
            {
                
                return (Constant.PIECEDIVERSLIGNE_url).WithTimeout(7).PostJsonAsync(piecediverseligne).Result.IsSuccessStatusCode;
            }
            catch (FlurlHttpException e)
            {
                noInternetConnection();
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool PatchArtdepot(ARTDEPOT artdepot, string artid , string depid)
        {
            try
            {
                var update = new { ARDSTOCKREEL = artdepot.ARDSTOCKREEL };
                var result = (Constant.ARTDEPOTs_url + "/update?[where][and][0][DEPID]=" + depid + "&[where][and][1][ARTID]=" + artid).PostJsonAsync(update).Result.IsSuccessStatusCode;
                return result;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /*
        public PIECEVENTELIGNE GetPIECEVENTELIGNEbyARTID(string artid)
        {
            
            try
            {
                return (Constant.PIECEDIVERSLIGNE_url + "?filter[where][ARTID]=" + artid).WithTimeout(7).GetJsonAsync<IList<PIECEVENTELIGNE>>().Result[0];
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

        public void noInternetConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                var _dialog = new DialogService();
                _dialog.ShowMessage("Verifier votre connection internet", true);
            }
        }

        public bool PostStockElement(StockLigne obj)
        {
            throw new NotImplementedException();
        }

        public bool PostSellLignes(IList<SellElements> sells)
        {
            try
            {
                foreach (SellElements s in sells)
                {
                    if (PostSellLigne(s, sells.Count))
                        Console.WriteLine(s.articles.ARTDESIGNATION + " post succefully ");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                noInternetConnection();
                return false;
            }
        }

        public bool PostSellLigne(SellElements sell, int num)
        {
            try{
                PIECEVENTE pv = new PIECEVENTE
                {
                    PCVID = getPieceVente() + 1,
                    PCVNUM = "", // a partir de piece nature 
                                 //"PCVNUMEXT": "string",
                    PITCODE = sell.pIECE_NATURE.PITCODE,
                    PINID = sell.pIECE_NATURE.PINID,
                    PINCODE = sell.pIECE_NATURE.PINCODE,
                    EXEID = getEXERCICE() + 1, // need to add exercice 
                    NUMID = GetPIECE_PREF(sell.pIECE_NATURE.PINID.ToString()).NUMID,
                    //"AFFID": 0,
                    TRFID = int.Parse(sell.tiers.TRFID),
                    TIRID = sell.tiers.TIRID,
                    TIRID_FAC = int.Parse(sell.tiers.TIRID_FAC),
                    TIRID_LIV = int.Parse(sell.tiers.TIRID_LIV),
                    TIRID_REP = null,
                    //ADRID_FAC = sell.tiers.ADRID
                    //"ADRID_LIV": 0,
                    DEPID = sell.depot.DEPID,
                    //"EXPID": 0,
                    PCVCOLISAGE = 1, // fix ?
                    PCVPOIDS = 0, // fix ?
                    PCVUNITEPOIDS = 1000, // fix ?
                    JORID = 2, // fix ?
                    ECRSEQUENCE = 0, // fix ?
                    DEVID = 1, // fix ?
                    PCVCOURSDEV = 1, // fix ?
                    PCVDATEEFFET = DateTime.Now,
                    PCVISSOLDE = "N", // fix ?
                    PCVISCOMPTABILISE = "N", // fix ?
                    PCVISCLOS = "N", // fix ?
                    PCVISHT = "o", // fix ?
                    PCVISPRINT = "N",// fix ?
                    PCVNBPRINT = 1,
                    PCVDATEPRint = DateTime.Now, // fix ?
                    MODID = 15, // fix ?
                    PCVMNTHT = (int?)sell.mtht,
                    PCVMNTTTC = (int?)sell.mttc,
                    PCVMNTAREGLER = (int?)sell.mttc,
                    PCVMNTACOMPTE = 0,
                    PCVMNTTVA = ((int?)(sell.mttc - sell.mtht)),
                    PCVMNTTPF = 0,
                    PCVMNTESCOMPTE = 0,
                    PCVMNTPORT = 0,
                    PCVTAUXESCOMPTE = 0,
                    PCVCPTRELANCE = 0,
                    PCVCONDREGLEMENT = "Chèque à réception de facture",
                    USRMODIF = Helper.Session.user.USRNOM,
                    DATEUPDATE = DateTime.Now,
                    DATECREATE = DateTime.Now,
                    MEMOID = sell.tiers.MEMOID,
                    PCVNBIMPRESSION = 0,
                    PABID = 0,
                    SOCID = 67,
                    PCVISEDI = "N",
                    PCVEDIETAT = "A",
                    //"PCVEDIDATE": "2018-09-20T19:12:03.830Z",
                    //"PCVEDICODELIV": "string",
                    //"PCVDATELIVRAISON": "2018-09-20T19:12:03.830Z",
                    PCVISDEB = "N",
                    //"PCVDEBREGIME": "string",
                    //"PCVDEBTRANSACTION": "string",
                    //"PCVDEBLIVRAISON": "string",
                    //"PCVDEBTRANSPORT": "string",
                    //"PCVCRITREGROUPE": "string",
                    //"CAPID": 0,
                    PCVVOLUME = 0,
                    PCVUNITEVOLUME = 1,
                    PCVISPIECEFRAIS = "N",
                    PCVREMISEPIED = 0,
                    //TPVID = 1,
                    //"TYNCODE": "string",
                    //"PCVNBPTSCARTE": 0,
                    //"ANSID": 0,
                    USRCREATE = Session.user.USRLOGIN,
                    PCVISECOM = "N",
                    PRFID = 0,
                    OXID = 0,
                    //"PCVOBJET": "string",
                    TIRID_CPT = sell.tiers.TIRID
                };
                PRODUIT produit = getProduitbyARTID(sell.articles.ARTID.ToString(), "O");
                ARTFAMILLES_CPT artfamilles = GetARTFAMILLES_CPTbyARFID("36", "ART");
                PIECEVENTELIGNE pvl = new PIECEVENTELIGNE
                {
                    PLVID = getPieceVenteLigne() + 1, // to be verified
                    PCVID = getPieceVenteLigne() + 1,
                    //"PLVNUMLIGNE": 0,
                    PLVTYPE = "L",
                    PLVDATE = DateTime.Now,
                    DEPID = sell.depot.DEPID,
                    AFFID = sell.affaire.AFFID,
                    TIRIDREP = null,
                    TIRIDFOU = produit.TIRID,
                    PROID = produit.PROID,
                    ARTID = sell.articles.ARTID,
                    ARTTYPE = "A",
                    PLVISFORFAIT = "N",
                    PLVISSOUMISESC = "N",
                    PLVDESIGNATION = sell.articles.ARTDESIGNATION,
                    TVACODE = artfamilles.TVACODE_FR,
                    TPFCODE = 0,
                    CPTID = artfamilles.CPTID_FR,
                    ANSID = 7,
                    PLVQTE = sell.LivredQuantity,
                    PLVQTEUS = sell.LivredQuantity,
                    PLVQTETRANSFO = 0,
                    PLVCOEFFUV = 1,
                    //"PLVIDORG": 0,
                    PLVPUBRUT = (int?)sell.mutht,
                    PLVPUNET = (int?)(sell.mtht / sell.LivredQuantity),
                    PLVMNTNET = (int?)(sell.mutht * sell.LivredQuantity),
                    PLVMNTNETHT = (int?)sell.mtht,
                    //"PLVLASTPA": 0,
                    //"PLVPMP": 0,
                    //"PLVCUMP": 0,
                    PLVREMISE_F = sell.remise,
                    PLVREMISE_T = "P",
                    PLVREMISE_MNT = 0,
                    //"PLVSTOTID": 0,
                    PLVFRAIS1 = 0,
                    PLVFRAIS1T = "P",
                    PLVFRAIS2 = 0,
                    PLVFRAIS2T = "P",
                    PLVFRAIS3 = 0,
                    PLVFRAIS3T = "P",
                    PLVFRAISTOTAL = 0,
                    PLVPOIDS = 0,
                    PLVUNITEPOIDS = 0,
                    //"PLVDIVERS": "string",
                    //"PLVCOMMENTAIRE": "string",
                    //"PLVNUMLOT": "string",
                    //"PLVNUMSERIE": "string",
                    PLVPUBRUTREF = (int?)sell.mutht,
                    PLVARTCODE = sell.articles.ARTCODE,
                    PLVISIMPRIMABLE = "o",
                    PVOID = 0,
                    PLVSTYLEISGRAS = "N",
                    PLVSTYLEISITALIC = "N",
                    PLVSTYLEISIMPPARTIEL = "N",
                    PLVSTYLEISSOULIGNE = "N",
                    TRFID = 1,
                    //"PLVFEFOFABRICATION": "2018-09-30T15:55:15.053Z",
                    //"PLVFEFOPEREMPTION": "2018-09-30T15:55:15.053Z",
                    //"PLVFEFODIVERS": "string",
                    TPFCODE1 = 0,
                    TPFCODE2 = 0,
                    TPFCODE3 = 0,
                    TPFCODE4 = 0,
                    TPFCODE5 = 0,
                    TPFCODE6 = 0,
                    TPFCODE7 = 0,
                    TPFCODE8 = 0,
                    TPFCODE9 = 0,
                    PLVD1 = 0,
                    PLVD2 = 0,
                    PLVD3 = 0,
                    PLVD4 = 0,
                    PLVD5 = 0,
                    PLVD6 = 0,
                    PLVD7 = 0,
                    PLVD8 = 0,
                    PLVVOLUME = 0,
                    PLVUNITEVOLUME = 1,
                    PLVDENSITE = 0,
                    CTMID = 0,
                    PIFID = 0,
                    //"PLVLASTPR": 0,
                    //"PLVPRMP": 0,
                    //"PLVCRUMP": 0,
                    //"PLVFEFODIVERS1": "string",
                    //"PLVFEFODIVERS2": "string",
                    //"PLVFEFODIVERS3": "string",
                    PLVIDNOMC = 0,
                    PLVIDNOMCPERE = 0,
                    //"TCKID": 0,
                    PLVISSTK = "O",
                    //"PLVCLF": "string"
                };
                OPERATIONSTOCK o = new OPERATIONSTOCK
                {
                    OPEID = getOperationStockNumber() + 1,
                    DATECREATE = DateTime.Now,
                    DATEUPDATE = DateTime.Now,
                    //USRMODIF = "ADM",
                    OPEDATE = DateTime.Now,
                    ARTID = sell.articles.ARTID,
                    DEPID = sell.depot.DEPID,
                    USRMODIF = Helper.Session.user.USRNOM,
                    PICCODE = "S",
                    PINID = pv.PINID,
                    OPENATURESTOCK = "R",
                    OPEQUANTITE = sell.type.Equals("BR")? sell.LivredQuantity : -sell.LivredQuantity,
                    OPESENS = (short)(sell.type.Equals("BR") ? 1 : -1),
                    OPETYPE = "N",
                    OPEISMAJPA = "O",
                    OPEISBLOQUE = "N",
                    SOCID = 1,
                    OPEISCLOS = "N",
                    PCID = pvl.PCVID,
                    PLID = pvl.PLVID,
                    CTMID = 0,
                    TIRID = pv.TIRID,
                    OPEINTITULE = sell.tiers.TIRSOCIETE,
                    //opuint

                };

                PIECEVENTEECHEANCE pve = new PIECEVENTEECHEANCE
                {
                    PCVID = pvl.PCVID,
                    PEVID = getPIECEVENTEECHEANCE_PEVID() + 1 ,
                    PEVDATE = DateTime.Now,
                    PEVMONTANT = (int?)get_PCVMNTTTC(pvl.PCVID.ToString()),
                    PEVTAUX = 1,
                    RGTID = 2,
                    PEVISREGLE = "N",
                    PEVMNTREGLE = 0,
                    PITCODE = "F"
                };

                PIECEVENTETAXES pvt = new PIECEVENTETAXES
                {
                    PCVID = pvl.PCVID,
                    CODETAXE = 10,
                    PTVBASETVA = pvl.PLVMNTNETHT,
                    PTVBASETVAESC = 0,
                    //PTVMNTTVA = ,
                    PTVTAUXTVA = (int?)sell.tva.TVATAUX,
                    PTVBASETPF = 0,
                    PTVBASETPFESC = 0,
                    PTVMNTTPF = 0,
                    PTVTAUXTPF = 0
                };
                REGLEMENTECHEANCE re = new REGLEMENTECHEANCE
                {
                    ECHID = getREGLEMENTECHEANCE_ECHID() + 1,
                    PEREID = pvl.PCVID,
                    PERECLASSE = "PCV",
                    ECHNUMERO = 1,
                    RGTID = 2,
                    ECHLIBELLE = "Chèque à réception de facture",
                    ECHISLIBELLEAUTO = "N",
                    ECHJOUR = 0,
                    //ECHTYPE": "string",
                    ECHLE = 0,
                    ECHTAUX = 1,
                    ECHDATE = DateTime.Now,
                    PITCODE = "F"
                };




            }
            catch(Exception e){
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
                return false;
            }


            return true;
        }

        public bool PostPIECEVENTELIGNE(PIECEVENTELIGNE pIECEVENTELIGNE)
        {
            throw new NotImplementedException();
        }

        public bool PostPIECEVENTE(PIECEVENTE pIECEVENTE)
        {
            throw new NotImplementedException();
        }

        public int getPieceVente()
        {
            try
            {
                
                var count = (Constant.PIECEVENTE_url + "/count").WithTimeout(7).GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public int getEXERCICE()
        {
            try
            {
                
                var count = (Constant.EXERCICE_url + "/count").WithTimeout(7).GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public int getPieceVenteLigne()
        {
            try
            {
                
                var count = (Constant.PIECEVENTELIGNE_url + "/count").WithTimeout(7).GetJsonAsync<Count>();
                return count.Result.count;
            }
            catch (FlurlHttpException e)

            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public PRODUIT getProduit(string PROCODE, string PROISPRINCIPAL)
        {
            try
            {
                
                return (Constant.PRODUIT_url + "?filter[where][and][0][PROCODE]=" + PROCODE + "&filter[where][and][1][PROISPRINCIPAL]=" + PROISPRINCIPAL).WithTimeout(7).GetJsonAsync<PRODUIT>().Result;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                noInternetConnection();
            }
            return null;
        }
        public PRODUIT getProduitbyARTID(string ARTID, string PROISPRINCIPAL)
        {
            try
            {

                return (Constant.PRODUIT_url + "?filter[where][and][0][ARTID]=" + ARTID + "&filter[where][and][1][PROISPRINCIPAL]=" + PROISPRINCIPAL).WithTimeout(7).GetJsonAsync<PRODUIT>().Result;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                noInternetConnection();
            }
            return null;
        }

        public bool testServer(string uri = null){
            try{
                if(string.IsNullOrEmpty(uri)){
                    uri = Config.URL + ':' + Config.port;
                }
                ServerResponce res = uri.WithTimeout(3).GetJsonAsync<ServerResponce>().Result;
                if(res != null){
                    var _dialog = new DialogService();
                    _dialog.ShowMessage("started : " + res.started + "uptime : " + res.uptime, false);
                    return true;
                }else{
                    var _dialog = new DialogService();
                    _dialog.ShowMessage("Can't reach server ", true);
                    return false;
                }
            }
            catch{
                var _dialog = new DialogService();
                _dialog.ShowMessage("Error make sure you use http:// and you type a valid port " , true);
                return false;
            }
		}

        public int getPIECEVENTEECHEANCE_PEVID()
        {
            try
            {

                return (int)(Constant.PIECEVENTEECHEANCE_url + "filter[order]=PEVID DESC").WithTimeout(7).GetJsonAsync<IList<PIECEVENTEECHEANCE>>().Result[0].PEVID;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
        public int getREGLEMENTECHEANCE_ECHID()
        {
            try
            {

                return (int)(Constant.REGLEMENTECHEANCE_url + "filter[order]=ECHID DESC").WithTimeout(7).GetJsonAsync<IList<REGLEMENTECHEANCE>>().Result[0].ECHID;
            }
            catch (FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public float get_PCVMNTTTC(string PCVID)
        {
            try{
                var list = (Constant.PIECEVENTE_url + "?filter[where][PCVID]=" + PCVID).WithTimeout(10).GetJsonAsync<IList<PIECEVENTE>>().Result;
                float sum = 0;
                foreach (PIECEVENTE P in list)
                {
                    sum += (float)P.PCVMNTTTC;
                }
                return sum;
            }
            catch(FlurlHttpException e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
            }
            catch (Exception ex)
            {
                noInternetConnection();
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
    }

}
