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
        public async Task<IList<UTILISATEURSGRP>> GetGroupAsync()
        {
            try
            {
                
                var l = await Constant.group_uri.WithTimeout(10).GetJsonAsync<IList<UTILISATEURSGRP>>();
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

        public async Task<IList<UTILISATEUR>> GetUserByGroupIdAsync(string groupId)
        {
            try
            {
                
                var user =  await (Constant.user_uri + "?filter[where][USERGRP]=" + groupId).WithTimeout(10).GetJsonAsync<IList<UTILISATEUR>>();
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

        public async Task<IList<UTILISATEUR>> GetUserAsync()
        {
            try
            {
                return await Constant.user_uri.WithTimeout(10).GetJsonAsync<IList<UTILISATEUR>>();
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

        public async Task<bool> Login(UTILISATEUR user)
        {
            try
            {
                return  (Constant.user_uri + "count?where={\"and\":[{\"USRLOGIN\":" + user.USRLOGIN + "},{\"USRPWD\":\" " + user.USRPWD + "}]}").WithTimeout(10).GetJsonAsync<login>().Result.count > 0;
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

        public async Task<IList<TIERS>> GetTiers(string info , string type)
        {
            try
            {
                if (string.IsNullOrEmpty(info))
                {
                    return await (Constant.tiers_uri + "?filter[where][TIRTYPE]=" + type).WithTimeout(10).GetJsonAsync<IList<TIERS>>();
                }
                return await (Constant.tiers_uri + "?filter[where][and][0][TIRSOCIETE]=" + info + "&filter[where][and][1][TIRTYPE]=" + type).WithTimeout(10).GetJsonAsync<IList<TIERS>>();
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

        public async Task<IList<PIECE_NATURE>> GetPieceNature(string PICCODE = null, string PITCODE = null, string PINLIBELLE = null, string PINSENSSTOCK = null, bool like = false )
        {
            
            try
            {
                if(string.IsNullOrEmpty(PINLIBELLE) && string.IsNullOrEmpty(PINSENSSTOCK)) 
                    return await (Constant.piece_nature_uri + "?filter[where][and][0][PICCODE]=" + PICCODE + "&filter[where][and][1][PITCODE]=" + PITCODE).WithTimeout(10).GetJsonAsync<IList<PIECE_NATURE>>();
                if (string.IsNullOrEmpty(PINLIBELLE))
                    return await (Constant.piece_nature_uri + "?filter[where][and][0][PICCODE]=" + PICCODE + "&filter[where][and][1][PITCODE]=" + PITCODE + "&filter[where][and][2][PINSENSSTOCK]=" + PINSENSSTOCK).WithTimeout(10).GetJsonAsync<IList<PIECE_NATURE>>();

                if(like){
                    return await (Constant.piece_nature_uri + "?filter[where][and][0][PICCODE]=" + PICCODE + "&filter[where][and][1][PITCODE]=" + PITCODE +  "&filter[where][and][2][PINLIBELLE][like]=" + PINLIBELLE).WithTimeout(10).GetJsonAsync<IList<PIECE_NATURE>>();
                }
                return await (Constant.piece_nature_uri + "?filter[where][and][0][PICCODE]=" + PICCODE + "&filter[where][and][1][PITCODE]=" + PITCODE + "&filter[where][and][2][PINLIBELLE][nlike]=" + PINLIBELLE).WithTimeout(10).GetJsonAsync<IList<PIECE_NATURE>>();
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

        public async Task<IList<PIECE_NATURE>> GetPieceNaturebyPINID(String PINID)
        {
            
            try
            {
                return await (Constant.piece_nature_uri + "?filter[where][PINID]=" + PINID ).WithTimeout(10).GetJsonAsync<IList<PIECE_NATURE>>();
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

        public async Task<IList<depot>> GetDepot(string DEPISACTIF, string DEPISPRINCIPAL = null)
        {
            try
            {
                if (string.IsNullOrEmpty(DEPISPRINCIPAL)){
                    return await (Constant.depot_url + "?filter[where][DEPISACTIF]=" + DEPISACTIF).WithTimeout(10).GetJsonAsync<IList<depot>>();
                    }
                return await (Constant.depot_url + "?filter[where][and][0][DEPISACTIF]=" + DEPISACTIF + "&filter[where][and][1][DEPISPRINCIPAL]=" + DEPISPRINCIPAL ).WithTimeout(10).GetJsonAsync<IList<depot>>();
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

        public async Task<IList<AFFAIRE>> GetAffaire(string info)
        {
            try
            {
                

                if (string.IsNullOrEmpty(info))
                {
                    return await Constant.affaire_uri.WithTimeout(10).GetJsonAsync<IList<AFFAIRE>>();
                }
                return await (Constant.affaire_uri + "?filter[where][or][0][AFFCODE]=" + info + "&filter[where][or][1][AFFINTITULE]=" + info).WithTimeout(10).GetJsonAsync<IList<AFFAIRE>>();
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

        public async Task<ARTICLE> getArticlebyBC(string bc)
        {
            try
            {
                
                return (Constant.article_url + "?filter[where][ARTCODEBARRE]=" + bc).WithTimeout(10).GetJsonAsync<IList<ARTICLE>>().Result[0];
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

        public async Task<NUMAUTO> getNumPiecenyNature(string nature)
        {
            try
            {
                PIECE_PREF  prefs = await GetPIECE_PREF(nature);
                var numauto = (Constant.numauto_url + "?filter[where][NUMID]=" + prefs.NUMID ).WithTimeout(10).GetJsonAsync<IList<NUMAUTO>>().Result;
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

        public async Task<int> userNumber()
        {
            try
            {
                var count = (Constant.user_uri + "/count").WithTimeout(10).GetJsonAsync<Count>();
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

        public async Task<bool> addUser(UTILISATEUR user)
        {
            try{
                
                var id = await userNumber();

                user.USRID = id + 1;
                if (user.USERGRP == 50)
                    user.PROID = 1;
                else
                    user.PROID = 2;
                return (Constant.user_uri).WithTimeout(10).WithTimeout(10).PostJsonAsync(new {
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

        public async Task<bool> addUsers(IList<UTILISATEUR> users)
        {
            try{
                foreach (UTILISATEUR u in users)
                {
                    if (!await addUser(u))
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

        public async Task<ARTICLE> getArticlebyid(string id)
        {
            try
            {
                return (Constant.article_url + "?filter[where][ARTID]=" + id).WithTimeout(10).GetJsonAsync<IList<ARTICLE>>().Result[0];
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

        public async Task<ARTFAMILLES_CPT> GetARTFAMILLES_CPTbyARFID(string ARFID , string ARFCLASS = null)
        {
            try
            {
                if (string.IsNullOrEmpty(ARFCLASS)){
                    return (Constant.ARTFAMILLES_CPTs_url + "?filter[where][ARFID]=" + ARFID).WithTimeout(10).GetJsonAsync<IList<ARTFAMILLES_CPT>>().Result[0];
                }
                return (Constant.ARTFAMILLES_CPTs_url + "?filter[where][and][0][ARFID] = " + ARFID + " & filter[where][and][1][ARFCLASS] = " + ARFCLASS).WithTimeout(10).GetJsonAsync<IList<ARTFAMILLES_CPT>>().Result[0];

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

        public async Task<ARTTARIFLIGNE> GetRTTARIFLIGNEbyARTID(string ARTID)
        {
            try
            {
                return (Constant.ARTTARIFLIGNEs_url + "?filter[where][ARTID]=" + ARTID).WithTimeout(10).GetJsonAsync<IList<ARTTARIFLIGNE>>().Result[0];
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

        public async Task<TVA> GetTVAbyTVACODE(string TVACODE)
        {
            try
            {
                return (Constant.TVAs_url + "?filter[where][TVACODE]=" + TVACODE).WithTimeout(10).GetJsonAsync<IList<TVA>>().Result[0];
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

        public async Task<ARTUNITE> GetRTUNITE(string type)
        {
            try
            {
                return (Constant.ARTUNITEs_url + "?filter[where][AUTCODE]=" + type).WithTimeout(10).GetJsonAsync<IList<ARTUNITE>>().Result[0];
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

        public async Task<ARTDEPOT> GetARTDEPOTbyDepid(string ARTID , string DEPID)
        {
            try
            {
                var artdep = (Constant.ARTDEPOTs_url + "?filter[where][and][0][DEPID]=" + DEPID + "&filter[where][and][1][ARTID]=" + ARTID).WithTimeout(10).GetJsonAsync<IList<ARTDEPOT>>().Result[0];
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

        public async Task<IList<ARTDEPOT>> GetARTDEPOTbyDepArtid(string ARTID)
        {
            try
            {
                return (Constant.ARTDEPOTs_url + "?filter[where][ARTID]=" + ARTID).WithTimeout(10).GetJsonAsync<IList<ARTDEPOT>>().Result;
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

        public async Task<bool> PostToStock(IList<StockLigne> stocks)
        {
            try{
                foreach(StockLigne s in stocks){
                    if(await PostStockElement(s, stocks.Count))
                        Console.WriteLine(s.designation + "post succefully ");
                }
                return true;
            }catch(Exception ex){
                Console.WriteLine(ex.StackTrace);
                noInternetConnection();
                return false;
            }
        }

        public async Task<bool> PostStockElement(StockLigne obj, int num)
        {
            try{
                MEMOS m = new MEMOS
                {
                    MEMOISALERTE = "N"
                };
                string PCDNUMEXT = "";
                string type = "";
                int PCDID = await getPieceDiversNumber() + 1;
                //ARTDEPOT artdepo = null ;
                //ARTDEPOT deptransfer = null;

                if (obj.depin != null && obj.depout != null){
                    PCDNUMEXT = "Bon de transfert de dépot";
                    type = "BTR";
                  //  artdepo = GetARTDEPOTbyDepid(obj.article.ARTID.ToString(), obj.depin.DEPID.ToString());
                    //deptransfer = GetARTDEPOTbyDepid(obj.article.ARTID.ToString(), obj.depin.DEPID.ToString());

                }
                else if (obj.depin != null){
                    PCDNUMEXT = "Bon entrée en stock";
                    type = "SIN";
                //    artdepo = GetARTDEPOTbyDepid(obj.article.ARTID.ToString(), obj.depin.DEPID.ToString());
                }
                else if(obj.depout != null) {
                    PCDNUMEXT = "Bon sortir en stock";
                    type = "SOUT";
                  //  artdepo = GetARTDEPOTbyDepid(obj.article.ARTID.ToString(), obj.depout.DEPID.ToString());
                }


                //if (type.Equals( "SIN")){
                //    artdepo.DEPID = obj.depin.DEPID;
                //    artdepo.ARDSTOCKREEL += int.Parse(obj.quantite);
                //    if(PatchArtdepot(artdepo, obj.article.ARTID.ToString(), obj.depin.DEPID.ToString())){
                //        Console.WriteLine("artdepot updated");
                //    }

                //}
                //else if (type.Equals("SOUT"))
                //{
                //    try{
                //        artdepo.DEPID = obj.depout.DEPID;
                //        artdepo.ARDSTOCKREEL -= int.Parse(obj.quantite);
                //        PatchArtdepot(artdepo, obj.article.ARTID.ToString(), obj.depout.DEPID.ToString());
                //    }catch(Exception e){
                //        Console.WriteLine(e.Message);
                //    }

                //}
                //else if (type.Equals("BTR")){

                //    artdepo.DEPID = obj.depout.DEPID;
                //    artdepo.ARDSTOCKREEL -= int.Parse(obj.quantite);
                //    PatchArtdepot(artdepo, obj.article.ARTID.ToString() , obj.depout.DEPID.ToString());

                //    deptransfer.DEPID = obj.depin.DEPID;
                //    artdepo.ARDSTOCKREEL +=  int.Parse(obj.quantite);
                //    PatchArtdepot(artdepo, obj.article.ARTID.ToString() , obj.depin.DEPID.ToString());
                //}


                PIECE_PREF prefs = await GetPIECE_PREF(obj.pIECE_NATURE.PINID.ToString() , "o");

                PIECEDIVERS p = new PIECEDIVERS
                {
                        PCDID = PCDID,
                        PCDNUM = obj.numpiece,
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
                        MEMOID = await getMemosNumber() + 1,
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
                    TVACODE =  (int?)GetTVAbyTVACODE(obj.artfamilles_cpt.TVACODE_FR.ToString()).Result.TVACODE,
                    PLDID = await getPieceDiversLignesNumber() + 1,

                };

                if(obj.sense == 1){
                    OPERATIONSTOCK o = new OPERATIONSTOCK
                    {
                        OPEID = await getOperationStockNumber() + 1,
                        DATECREATE = DateTime.Now,
                        DATEUPDATE = DateTime.Now,
                        OPEINTITULE = obj.depin.DEPINTITULE,
                        //USRMODIF = "ADM",
                        OPEDATE = DateTime.Now,
                        ARTID = obj.article.ARTID,
                        DEPID =  obj.depin.DEPID,
                        USRMODIF = Helper.Session.user.USRNOM,
                        PICCODE = "S",
                        PINID = obj.pIECE_NATURE.PINID,
                        OPENATURESTOCK = "R",
                        OPEQUANTITE = int.Parse(obj.quantite),
                        OPESENS = int.Parse(obj.sense.ToString()),
                        OPETYPE = "N",
                        OPEISMAJPA = "O",
                        OPEISBLOQUE = "N",
                        SOCID = 1,
                        OPEISCLOS = "N",
                        PCID = pl.PCDID,
                        PLID = pl.PLDID,
                        CTMID = 0,
                        OPEREFPIECE = obj.numpiece,
                        OPEPUNET = obj.prix != null ? float.Parse(obj.prix) : 0,
                        //tirid
                        //opuint
                    };
                    await PostOperationStock(o);
                }
                else if (obj.sense == -1)
                {
                    OPERATIONSTOCK o = new OPERATIONSTOCK
                    {
                        OPEID = await getOperationStockNumber() + 1,
                        DATECREATE = DateTime.Now,
                        OPEINTITULE = obj.depout.DEPINTITULE,
                        DATEUPDATE = DateTime.Now,
                        //USRMODIF = "ADM",
                        OPEDATE = DateTime.Now,
                        ARTID = obj.article.ARTID,
                        DEPID = obj.depout.DEPID,
                        USRMODIF = Helper.Session.user.USRNOM,
                        PICCODE = "S",
                        PINID = obj.pIECE_NATURE.PINID,
                        OPENATURESTOCK = "R",
                        OPEQUANTITE = -int.Parse(obj.quantite) ,
                        OPESENS = int.Parse(obj.sense.ToString()),
                        OPETYPE = "N",
                        OPEISMAJPA = "O",
                        OPEISBLOQUE = "N",
                        SOCID = 1,
                        OPEISCLOS = "N",
                        PCID = pl.PCDID,
                        PLID = pl.PLDID,
                        CTMID = 0,
                        OPEREFPIECE = obj.numpiece,
                        OPEPUNET = obj.prix != null ? float.Parse(obj.prix) : 0,
                        //tirid
                        //opuint
                    };
                      await PostOperationStock(o);
                } else if(obj.sense == 0){
                    OPERATIONSTOCK oi = new OPERATIONSTOCK
                    {
                        OPEID = await getOperationStockNumber() + 1,
                        DATECREATE = DateTime.Now,
                        DATEUPDATE = DateTime.Now,
                        OPEINTITULE = obj.depin.DEPINTITULE,
                        //USRMODIF = "ADM",
                        OPEDATE = DateTime.Now,
                        ARTID = obj.article.ARTID,
                        DEPID = obj.depin.DEPID,
                        USRMODIF = Helper.Session.user.USRNOM,
                        PICCODE = "S",
                        PINID = obj.pIECE_NATURE.PINID,
                        OPENATURESTOCK = "R",
                        OPEQUANTITE = int.Parse(obj.quantite),
                        OPESENS = int.Parse(obj.sense.ToString()),
                        OPETYPE = "N",
                        OPEISMAJPA = "O",
                        OPEISBLOQUE = "N",
                        SOCID = 1,
                        OPEISCLOS = "N",
                        PCID = pl.PCDID,
                        PLID = pl.PLDID,
                        CTMID = 0,
                        OPEREFPIECE = obj.numpiece,
                        OPEPUNET = obj.prix != null ? float.Parse(obj.prix) : 0,
                        //tirid
                        //opuint
                    };
                    await PostOperationStock(oi);
                    OPERATIONSTOCK oo = new OPERATIONSTOCK
                    {
                        OPEID = await getOperationStockNumber() + 1,
                        DATECREATE = DateTime.Now,
                        DATEUPDATE = DateTime.Now,
                        //USRMODIF = "ADM",
                        OPEDATE = DateTime.Now,
                        ARTID = obj.article.ARTID,
                        OPEINTITULE = obj.depout.DEPINTITULE,
                        DEPID = obj.depout.DEPID,
                        USRMODIF = Helper.Session.user.USRNOM,
                        PICCODE = "S",
                        PINID = obj.pIECE_NATURE.PINID,
                        OPENATURESTOCK = "R",
                        OPEQUANTITE = -int.Parse(obj.quantite),
                        OPESENS = int.Parse(obj.sense.ToString()),
                        OPETYPE = "N",
                        OPEISMAJPA = "O",
                        OPEISBLOQUE = "N",
                        SOCID = 1,
                        OPEISCLOS = "N",
                        PCID = pl.PCDID,
                        PLID = pl.PLDID,
                        CTMID = 0,
                        OPEREFPIECE = obj.numpiece,
                        OPEPUNET = obj.prix != null ? float.Parse(obj.prix) : 0,
                        //tirid
                        //opuint
                    };
                    await PostOperationStock(oo);
                }


                return         
                       await PostPiecedivers(p) &&
                       await PostPiecediversLigne(pl) &&
                       await PostIdentityTable("operationstock") &&
                       await PostIdentityTable("piecedivers");

            }catch(Exception e){
                noInternetConnection();
                Console.WriteLine(e.StackTrace);
                    return false;
            } 

        }

        public async Task<bool> PostPiecedivers(PIECEDIVERS piecediverse)
        {
            try
            {
                
                return  (Constant.PIECEDIVERs_url).WithTimeout(10).PostJsonAsync(piecediverse).Result.IsSuccessStatusCode;
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

        public async Task<bool> PostOperationStock(OPERATIONSTOCK operationStock)
        {
            try
            {
                var reelQuantity = (float)GetARTDEPOTbyDepid(operationStock.ARTID.ToString(), operationStock.DEPID.ToString()).Result.ARDSTOCKREEL;
                var Quantity = reelQuantity.ToString();
                if(operationStock.OPEQUANTITE < 0){
                    if ((-1 * operationStock.OPEQUANTITE) > int.Parse(Quantity))
                    {
                        DialogService d = new DialogService();
                        d.ShowMessage("quantite doit etre inferieure a " + Quantity, true);
                        return false;
                    }
                }
                return (Constant.OPERATIONSTOCKs_url).WithTimeout(10).PostJsonAsync(operationStock).Result.IsSuccessStatusCode;
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

        public async Task<bool> PostIdentityTable(string idTable)
        {
            try
            {
                var table = await GetIDENTIFIANTTABLE(idTable);

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

        public async Task<IDENTIFIANTTABLE> GetIDENTIFIANTTABLE(string idTable)
        {
            try
            {
                return  (Constant.IDENTIFIANTTABLEs_url + "/" + idTable).WithTimeout(10).GetJsonAsync<IDENTIFIANTTABLE>().Result;
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

        public async Task<int> getPieceDiversNumber()
        {
            try
            {
                var count = (Constant.PIECEDIVERs_url + "/count").WithTimeout(10).GetJsonAsync<Count>();
                return count.Result.count + 1;
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

        public async Task<int> getOperationStockNumber()
        {
            try
            {
                return (int)(Constant.OPERATIONSTOCKs_url + "?filter[order]=OPEID DESC").WithTimeout(10).GetJsonAsync<List<OPERATIONSTOCK>>().Result[0].OPEID;
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

        public async Task<bool> PostMemos(MEMOS memos)
        {
            try
            {
                
                return (Constant.MEMOS_url).WithTimeout(10).PostJsonAsync(memos).Result.IsSuccessStatusCode;
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

        public async Task<int> getMemosNumber()
        {
            try
            {
                
                var count = (Constant.MEMOS_url + "/count").WithTimeout(10).GetJsonAsync<Count>();
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

        public async Task<PIECE_PREF> GetPIECE_PREF(string id , string PIPISDEFAULT = null)
        {
            try{
                if(PIPISDEFAULT != null){
                    return (Constant.pieceprefs_url + "?filter[where][and][0][PINID] = " + id + " & filter[where][and][1][PIPISDEFAULT] = " + PIPISDEFAULT).WithTimeout(10).GetJsonAsync<IList<PIECE_PREF>>().Result[0];
                }
                return (Constant.pieceprefs_url + "?filter[where][PINID]=" + id).WithTimeout(10).GetJsonAsync<IList<PIECE_PREF>>().Result[0];
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

        public async Task<int> getPieceDiversLignesNumber()
        {
            try
            {
                var count = (Constant.PIECEDIVERSLIGNE_url + "/count").WithTimeout(10).GetJsonAsync<Count>();
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

        public async Task<bool> PostPiecediversLigne(PIECEDIVERSLIGNES piecediverseligne)
        {
            try
            {
                
                return (Constant.PIECEDIVERSLIGNE_url).WithTimeout(10).PostJsonAsync(piecediverseligne).Result.IsSuccessStatusCode;
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

        public async Task<bool> PatchArtdepot(ARTDEPOT artdepot, string artid , string depid)
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
                return (Constant.PIECEDIVERSLIGNE_url + "?filter[where][ARTID]=" + artid).WithTimeout(10).GetJsonAsync<IList<PIECEVENTELIGNE>>().Result[0];
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

        public Task<bool> PostStockElement(StockLigne obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PostSellLignes(IList<SellElements> sells)
        {
            try
            {
                foreach (SellElements s in sells)
                {
                    if (await PostSellLigne(s, sells.Count))
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

        public async Task<bool> PostSellLigne(SellElements sell, int num)
        {
            try
            {
                PIECEVENTE pv = new PIECEVENTE
                {
                    PCVID = await getPieceVente() + 1,
                    PCVNUM = sell.numpiece, // a partir de piece nature 
                    PCVNUMEXT =  null, // test 
                    PITCODE = sell.pIECE_NATURE.PITCODE,
                    PINID = sell.pIECE_NATURE.PINID,
                    PINCODE = sell.pIECE_NATURE.PINCODE,
                    EXEID = await getEXERCICE() + 1, // need to add exercice 


                    NUMID = GetPIECE_PREF(sell.pIECE_NATURE.PINID.ToString()).Result.NUMID,
                    //AFFID =  sell.affaire.AFFID,
                    TRFID = sell.tiers != null ? int.Parse(sell.tiers.TRFID) : 0,
                    TIRID = sell.tiers != null ? sell.tiers.TIRID : 1,
                    TIRID_FAC = 0,//not tested
                    TIRID_LIV = 0,//not tested
                    //TIRID_REP = null,
                    ADRID_FAC = sell.tiers != null ? sell.tiers.ADRID : 0,
                    ADRID_LIV = 0,
                    DEPID = sell.depot.DEPID,
                    EXPID = 0,

                    //working
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
                    //working



                    //USRMODIF = Helper.Session.user.USRNOM,
                    DATEUPDATE = DateTime.Now,
                    DATECREATE = DateTime.Now,
                    MEMOID = sell.tiers != null ?  sell.tiers.MEMOID : 0,

                    //working
                    PCVNBIMPRESSION = 0,
                    PABID = 0,
                    SOCID = 67,
                    PCVISEDI = "N",
                    PCVEDIETAT = "A",
                    PCVEDIDATE = DateTime.Now,
                    PCVEDICODELIV = "t", //tesst
                    PCVDATELIVRAISON = DateTime.Now,
                    PCVISDEB = "N",
                    PCVDEBREGIME = "t", //test
                    PCVDEBTRANSACTION = "t", //test
                    PCVDEBLIVRAISON = "t", //test
                    PCVDEBTRANSPORT = "t", //test
                    PCVCRITREGROUPE = "t", //test
                    CAPID  = 0, //test
                    PCVVOLUME = 0,
                    PCVUNITEVOLUME = 1,
                    PCVISPIECEFRAIS = "N",
                    PCVREMISEPIED = 0,
                    TPVID = 1, //test
                    TYNCODE = "t", //test

                    //working


                   // PCVNBPTSCARTE = 0, //test
                    //ANSID = 0, //test
                    //USRCREATE = Session.user.USRLOGIN,
                    //PCVISECOM = "N",
                    //PRFID = 0,
                    //OXID = 0,
                    //"PCVOBJET": "string",
                    TIRID_CPT = sell.tiers != null ? sell.tiers.TIRID : 1
                };
                PRODUIT produit = await getProduitbyARTID(sell.articles.ARTID.ToString(), "O");
                ARTFAMILLES_CPT artfamilles = await GetARTFAMILLES_CPTbyARFID("36", "ART");
                PIECEVENTELIGNE pvl = new PIECEVENTELIGNE
                {
                    PLVID = await getPieceVenteLigne() + 1, // to be verified
                    PCVID = pv.PCVID,
                    PLVNUMLIGNE = 0, // just added
                    PLVTYPE = "L",
                    PLVDATE = DateTime.Now,
                    DEPID = sell.depot.DEPID,
                    AFFID = 0,//sell.affaire.AFFID,
                    TIRIDREP = null,
                    TIRIDFOU = 0,//produit.TIRID,
                    PROID = 0,//produit.PROID,
                    ARTID = sell.articles.ARTID,
                    ARTTYPE = "A",
                    PLVISFORFAIT = "N",
                    PLVISSOUMISESC = "N",
                    PLVDESIGNATION = sell.articles.ARTDESIGNATION,
                    TVACODE = 0,//artfamilles.TVACODE_FR,
                    TPFCODE = 0,
                    CPTID = 0,//artfamilles.CPTID_FR,
                    ANSID = 7,
                    PLVQTE = sell.LivredQuantity,
                    PLVQTEUS = sell.LivredQuantity,
                    PLVQTETRANSFO = 0,
                    PLVCOEFFUV = 1,
                    //"PLVIDORG": 0,
                    PLVPUBRUT = (int?)sell.mutht,
                    PLVPUNET = (int?)(sell.mtht / sell.LivredQuantity),
                    PLVMNTNET = (int)(sell.mutht * sell.LivredQuantity),
                    PLVMNTNETHT = (float) sell.mtht,
                    PLVLASTPA = 0, // just added
                    PLVPMP = 0, // just added
                    PLVCUMP = 0, // just added
                    PLVREMISE_F = "0",//sell.remise,
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
                    PLVLASTPR = 0, // just added
                    PLVPRMP = 0, // just added
                    PLVCRUMP = 0, // just added
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
                    OPEID = await getOperationStockNumber() + 1,
                    DATECREATE = DateTime.Now,
                    DATEUPDATE = DateTime.Now,
                    //OPEINTITULE = sell.depot.DEPINTITULE,
                    //USRMODIF = "ADM",
                    OPEDATE = DateTime.Now,
                    ARTID = sell.articles.ARTID,
                    DEPID = sell.depot.DEPID,
                    //USRMODIF = Helper.Session.user.USRNOM,
                    PICCODE = "S",
                    PINID = pv.PINID,
                    OPENATURESTOCK = "R",
                    OPEQUANTITE = sell.type.Equals("SBR")? sell.LivredQuantity : -sell.LivredQuantity,
                    OPESENS = (int)(sell.type.Equals("SBR") ? 1 : -1),
                    OPETYPE = "N",
                    OPEISMAJPA = "O",
                    OPEISBLOQUE = "N",
                    SOCID = 1,
                    OPEISCLOS = "N",
                    PCID = pvl.PCVID,
                    PLID = pvl.PLVID,
                    CTMID = 0,
                    TIRID = pv.TIRID,
                    OPEREFPIECE = sell.numpiece,
                    OPEINTITULE = sell.tiers != null ? sell.tiers.TIRSOCIETE : "",
                    OPEPUNET = sell.mutht
                    //opuint

                };
                PIECEVENTEECHEANCE pve = new PIECEVENTEECHEANCE
                {
                    PCVID = pvl.PCVID,
                    PEVID = await getPIECEVENTEECHEANCE_PEVID() + 1 ,
                    PEVDATE = DateTime.Now,
                    PEVMONTANT =  (int?)get_PCVMNTTTC(pvl.PCVID.ToString()).Result,
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
                    PTVMNTTVA = (int?)sell.tva.TVATAUX * pvl.PLVMNTNETHT,
                    PTVTAUXTVA = (int?)sell.tva.TVATAUX,
                    PTVBASETPF = 0,
                    PTVBASETPFESC = 0,
                    PTVMNTTPF = 0,
                    PTVTAUXTPF = 0
                };
                REGLEMENTECHEANCE re = new REGLEMENTECHEANCE
                {
                    ECHID = await getREGLEMENTECHEANCE_ECHID() + 1,
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

                var reelQuantity = (float)GetARTDEPOTbyDepid(sell.articles.ARTID.ToString(), sell.depot.DEPID.ToString()).Result.ARDSTOCKREEL;
                var Quantity = reelQuantity.ToString();
                if (o.OPEQUANTITE < 0)
                {
                    if ((-1 * o.OPEQUANTITE) > int.Parse(Quantity))
                    {
                        DialogService d = new DialogService();
                        d.ShowMessage("quantite doit etre inferieure a " + Quantity, true);
                        return false;
                    }
                }

                if (await PostPIECEVENTE(pv)
                            &&
                    await PostPIECEVENTELIGNE(pvl))
                {
                    if(!sell.type.Equals("SBC")){
                        await PostOperationStock(o);
                        await PostPieceVenteEcheace(pve);
                        await PostPieceVenteTaxe(pvt);
                        await PostReglementEcheace(re);
                    }
                    return true;
                }
                return false;
            }
            catch(Exception e){
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
                return false;
            }
            //return false;
        }

        public async Task<bool> PostPIECEVENTELIGNE(PIECEVENTELIGNE pIECEVENTELIGNE)
        {
            try
            {
                var result = (Constant.PIECEVENTELIGNE_url).PostJsonAsync(pIECEVENTELIGNE);
                return result.Result.IsSuccessStatusCode;
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

        public async Task<bool> PostPIECEVENTE(PIECEVENTE pIECEVENTE)
        {
            try
            {
                var result =  (Constant.PIECEVENTE_url).WithTimeout(10).PostJsonAsync(pIECEVENTE);
                var res = result.Result;
                return res.IsSuccessStatusCode;
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

        public async Task<int> getPieceVente()
        {
            try
            {
                var count = (Constant.PIECEVENTE_url + "/count").WithTimeout(10).GetJsonAsync<Count>();
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

        public async Task<int> getEXERCICE()
        {
            try
            {
                
                var count = (Constant.EXERCICE_url + "/count").WithTimeout(10).GetJsonAsync<Count>();
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

        public async Task<int> getPieceVenteLigne()
        {
            try
            {
                return (int)(Constant.PIECEVENTELIGNE_url + "?filter[order]=PLVID DESC").WithTimeout(10).GetJsonAsync<IList<PIECEVENTELIGNE>>().Result[0].PLVID;
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

        public async Task<PRODUIT> getProduit(string PROCODE, string PROISPRINCIPAL)
        {
            try
            { 
                return (Constant.PRODUIT_url + "?filter[where][and][0][PROCODE]=" + PROCODE + "&filter[where][and][1][PROISPRINCIPAL]=" + PROISPRINCIPAL).WithTimeout(10).GetJsonAsync<PRODUIT>().Result;
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

        public async Task<PRODUIT> getProduitbyARTID(string ARTID, string PROISPRINCIPAL)
        {
            try
            {
                return (Constant.PRODUIT_url + "?filter[where][and][0][ARTID]=" + ARTID + "&filter[where][and][1][PROISPRINCIPAL]=" + PROISPRINCIPAL).WithTimeout(10).GetJsonAsync<PRODUIT>().Result;
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

        public async Task<bool> testServer(string uri = null){
            try{
                if(string.IsNullOrEmpty(uri)){
                    uri = Config.URL + ':' + Config.port;
                }
                ServerResponce res = uri.WithTimeout(5).GetJsonAsync<ServerResponce>().Result;
                if(res != null){
                    var _dialog = new DialogService();
                    _dialog.ShowMessage("connectee : " + res.started + "a partir de : " + res.uptime, false);
                    return true;
                }else{
                    var _dialog = new DialogService();
                    _dialog.ShowMessage("le serveur ne repond pas ", true);
                    return false;
                }
            }
            catch{
                var _dialog = new DialogService();
                _dialog.ShowMessage("verifier les donnee du serveur " , true);
                return false;
            }
		}

        public async Task<int> getPIECEVENTEECHEANCE_PEVID()
        {
            try
            {

                return (int)(Constant.PIECEVENTEECHEANCE_url + "?filter[order]=PEVID DESC").WithTimeout(10).GetJsonAsync<IList<PIECEVENTEECHEANCE>>().Result[0].PEVID;
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

        public async Task<int> getREGLEMENTECHEANCE_ECHID()
        {
            try
            {
                return (int)(Constant.REGLEMENTECHEANCE_url + "?filter[order]=ECHID DESC").WithTimeout(10).GetJsonAsync<IList<REGLEMENTECHEANCE>>().Result[0].ECHID;
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

        public async Task<float> get_PCVMNTTTC(string PCVID)
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

        public async Task<bool> PostPieceVenteEcheace(PIECEVENTEECHEANCE pIECEVENTEECHEANCE)
        {
            try
            {
                return (Constant.PIECEVENTEECHEANCE_url).WithTimeout(10).PostJsonAsync(pIECEVENTEECHEANCE).Result.IsSuccessStatusCode;
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

        public async Task<bool> PostPieceVenteTaxe(PIECEVENTETAXES pIECEVENTETAXES)
        {
            try
            {

                return (Constant.PIECEVENTETAXES_url).WithTimeout(10).PostJsonAsync(pIECEVENTETAXES).Result.IsSuccessStatusCode;
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

        public async Task<bool> PostReglementEcheace(REGLEMENTECHEANCE rEGLEMENTECHEANCE)
        {
            try
            {
                return (Constant.REGLEMENTECHEANCE_url).WithTimeout(10).PostJsonAsync(rEGLEMENTECHEANCE).Result.IsSuccessStatusCode;
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

        public async Task<bool> PostBuyElement(Buyelement buy, int num)
        {
            try
            {
                PIECEACHAT pv = new PIECEACHAT
                {
                    //working
                    PCACOLISAGE = 1, // fix ?
                    PCAPOIDS = 0, // fix ?
                    PCAUNITEPOIDS = 1000, // fix ?
                    JORID = 2, // fix ?
                    ECRSEQUENCE = 0, // fix ?
                    DEVID = 1, // fix ?
                    PCACOURSDEV = 1, // fix ?
                    PCADATEEFFET = DateTime.Now,
                    PCAISSOLDE = "N", // fix ?
                    PCAISCOMPTABILISE = "N", // fix ?
                    PCAISCLOS = "N", // fix ?
                    PCAISPRINT = "N",// fix ?
                    PCANBPRINT = 1,
                    MODID = 15, // fix ?
                    PCAMNTHT = (int?)buy.mtht,
                    PCAMNTTTC = (int?)buy.mttc,
                    PCAMNTAREGLER = (int?)buy.mttc,
                    PCAMNTACOMPTE = 0,
                    PCAMNTTVA = ((int?)(buy.mttc - buy.mtht)),
                    PCAMNTTPF = 0,
                    PCAMNTESCOMPTE = 0,
                    PCAMNTPORT = 0,
                    PCATAUXESCOMPTE = 0,
                    PCACONDREGLEMENT = "Chèque à réception de facture",
                    //USRMODIF = Helper.Session.user.USRNOM,
                    DATEUPDATE = DateTime.Now,
                    DATECREATE = DateTime.Now,
                    MEMOID = 0,//sell.tiers.MEMOID,
                    PCANBIMPRESSION = 0,
                    SOCID = 67,
                    PCADATELIVRAISON = DateTime.Now,
                    PCAISDEB = "N",
                    PCADEBREGIME = "t", //test
                    PCADEBTRANSACTION = "t", //test
                    PCADEBLIVRAISON = "t", //test
                    PCADEBTRANSPORT = "t", //test
                    PCAVOLUME = 0,
                    PCAUNITEVOLUME = 1,
                    PCAISPIECEFRAIS = "N",
                    TYNCODE = "t", //test
                    PCAID = await getPieceAchat() + 1,
                    PCANUM = buy.numpiece.ToString(),
                    PCANUMEXT = "string",
                    PITCODE = buy.pIECE_NATURE.PITCODE,
                    PINID = buy.pIECE_NATURE.PINID,
                    PINCODE = buy.pIECE_NATURE.PINCODE,
                    EXEID = await getEXERCICE() + 1,
                    NUMID = GetPIECE_PREF(buy.pIECE_NATURE.PINID.ToString()).Result.NUMID,
                    AFFID = 0,
                    TIRID = buy.tiers != null ? buy.tiers.TIRID : 1,
                    TIRID_FAC = 0,
                    ADRID_FAC = 0,
                    ADRID_LIV = 0,
                    DEPID = 0,
                    EXPID = 0,
                    PCADATEPRINT = DateTime.Now,
                    USRMODIF = "ADM",
                    ANSID = 0,
                    USRCREATE = "string",
                    PCAOBJET = "string",
                    TIRID_CPT = 0
                };
                PRODUIT produit = await getProduitbyARTID(buy.articles.ARTID.ToString(), "O");
                ARTFAMILLES_CPT artfamilles = await GetARTFAMILLES_CPTbyARFID("36", "ART");
                PIECEACHATLIGNE pvl = new PIECEACHATLIGNE
                {

                    PLAID = await getPieceAchatLigne() + 1, // to be verified
                    PCAID = pv.PCAID,
                    PLANUMLIGNE = 0, // just added
                    PLATYPE = "L",
                    PLADATE = DateTime.Now,
                    DEPID = buy.depot.DEPID,
                    AFFID = 0,//sell.affaire.AFFID,
                    PROID = 0,//produit.PROID,
                    ARTID = buy.articles.ARTID,
                    PLAISSOUMISESC = "N",
                    PLADESIGNATION = buy.articles.ARTDESIGNATION,
                    TVACODE = 0,//artfamilles.TVACODE_FR,
                    TPFCODE = 0,
                    CPTID = 0,//artfamilles.CPTID_FR,
                    ANSID = 7,
                    PLAQTE = buy.LivredQuantity,
                    PLAQTEUS = buy.LivredQuantity,
                    PLAQTETRANSFO = 0,
                    //"PLVIDORG": 0,
                    PLAPUBRUT = (int?)buy.mutht,
                    PLAPUNET = (int?)(buy.mtht / buy.LivredQuantity),
                    PLAMNTNET = (int)(buy.mutht * buy.LivredQuantity),
                    PLAMNTNETHT = (float)buy.mtht,
                    PLAREMISE_F = "0",//sell.remise,
                    PLAREMISE_T = "P",
                    PLAREMISE_MNT = 0,
                    PLAISIMPRIMABLE = "o",
                    PLASTYLEISGRAS = "N",
                    PLASTYLEISITALIC = "N",
                    PLASTYLEISIMPPARTIEL = "N",
                    PLASTYLEISSOULIGNE = "N",
                    PLAD1 = 0,
                    PLAD2 = 0,
                    PLAD3 = 0,
                    PLAD4 = 0,
                    PLAD5 = 0,
                    PLAD6 = 0,
                    PLAD7 = 0,
                    PLAD8 = 0,
                    PLAVOLUME = 0,
                    PLAUNITEVOLUME = 1,
                    PLADENSITE = 0,
                    PIFID = 0,

                                    
                    PLACOEFFUA = 0,
                    PLAIDORG = 0,
                   
                    PLASTOTID = 0,
                    PLAPOIDS = 0,
                    PLAUNITEPOIDS = 0,
                    //PLADIVERS = "string",
                    //PLACOMMENTAIRE = "string",
                    //PLANUMLOT = "string",
                    //PLANUMSERIE = "string",
                    
                    PLAFEFOFABRICATION = DateTime.Now    ,
                    PLAFEFOPEREMPTION = DateTime.Now,
                    PLAFEFODIVERS = "string",
                    TPFCODE1 = 0,
                    TPFCODE2 = 0,
                    TPFCODE3 = 0,
                    TPFCODE4 = 0,
                    TPFCODE5 = 0,
                    TPFCODE6 = 0,
                    //"TPFCODE7": 0,
                    //"TPFCODE8": 0,
                    //"TPFCODE9": 0,
                    //"PLAPROCODE": "string",
                    //"PLAD1": 0,
                    //"PLAD2": 0,
                    //"PLAD3": 0,
                    //"PLAD4": 0,
                    //"PLAD5": 0,
                    //"PLAD6": 0,
                    //"PLAD7": 0,
                    //"PLAD8": 0,
                    
                    PLAFRAIS = 0,
                    PLAFRAISLG = 0,
                    PLAFRAISPC = 0,
                    //PLAFEFODIVERS1 = "string",
                    //PLAFEFODIVERS2 = "string",
                    //PLAFEFODIVERS3 = "string",
                };
                OPERATIONSTOCK o = new OPERATIONSTOCK
                {
                    OPEID = await getOperationStockNumber() + 1,
                    DATECREATE = DateTime.Now,
                    DATEUPDATE = DateTime.Now,
                    USRMODIF = "ADM",
                    OPEDATE = DateTime.Now,
                    ARTID = buy.articles.ARTID,
                    DEPID = buy.depot.DEPID,
                    //OPEINTITULE = buy.depot.DEPINTITULE,
                    //USRMODIF = Helper.Session.user.USRNOM,
                    PICCODE = "A",
                    PINID = pv.PINID,
                    OPENATURESTOCK = "R",
                    OPEQUANTITE = buy.type.Equals("BBR") ? buy.LivredQuantity : -buy.LivredQuantity,
                    OPESENS = (int)(buy.type.Equals("BBR") ? 1 : -1),
                    OPETYPE = "N",
                    OPEISMAJPA = "O",
                    OPEISBLOQUE = "N",
                    SOCID = 1,
                    OPEISCLOS = "N",
                    PCID = pvl.PCAID,
                    PLID = pvl.PLAID,
                    CTMID = 0,
                    TIRID = pv.TIRID,
                    OPEREFPIECE = buy.numpiece,
                    OPEINTITULE = buy.tiers != null ? buy.tiers.TIRSOCIETE : "",
                    OPEPUNET = buy.mutht
                    //opuint

                };
                PIECEACHATECHEANCE pve = new PIECEACHATECHEANCE
                {
                    PCAID = pvl.PCAID,
                    PEAID = await getPIECEACHATECHEANCE_PEAID() + 1,
                    PEADATE = DateTime.Now,
                    PEAMONTANT = await get_PEAMONTANT((pvl.PCAID).ToString()),
                    PEATAUX = 1,
                    RGTID = 2,
                    PEAISREGLE = "N",
                    PEAMNTREGLE = 0,
                    PITCODE = "F"
                };
                PIECEACHATTAXES pvt = new PIECEACHATTAXES
                {
                   PCAID = pvl.PCAID,
                   CODETAXE = 10,
                   PTABASETVA = pvl.PLAMNTNETHT,
                   PTABASETVAESC = 0,
                   PTAMNTTVA = pvl.PLAMNTNETHT * (int?)buy.tva.TVATAUX,
                   PTATAUXTVA = (int?)buy.tva.TVATAUX,
                   PTABASETPF = 0,
                   PTABASETPFESC = 0,
                   PTAMNTTPF = 0,
                   PTATAUXTPF = 0
                };
                REGLEMENTECHEANCE re = new REGLEMENTECHEANCE
                {
                    ECHID = await getREGLEMENTECHEANCE_ECHID() + 1,
                    PEREID = pvl.PCAID,
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

            if (await PostPIECEACHAT(pv)
                            &&
                await PostPIECEACHATLIGNE(pvl)
               )
            {
                if (!buy.type.Equals("BBC"))
                {
                    await PostOperationStock(o);
                    await PostPieceAchatEcheace(pve);
                    await PostPieceAchatTaxe(pvt);
                    await PostReglementEcheace(re);
                }
                return true;
            }
        }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                noInternetConnection();
                return false;
            }
            return false;
        }

        public async Task<bool> PostBuyElements(IList<Buyelement> buy)
        {
            try
            {
                foreach (Buyelement s in buy)
                {
                    if (await PostBuyElement(s, buy.Count))
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

        public async Task<int> getPieceAchat()
        {
            try
            {
                return (int)(Constant.PIECEACHAT_url + "?filter[order]=PCAID DESC").WithTimeout(10).GetJsonAsync<List<PIECEACHAT>>().Result[0].PCAID;
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

        public async Task<int> getPieceAchatLigne()
        {
            try
            {
                return (int)(Constant.PIECEACHATLIGNE_url + "?filter[order]=PLAID DESC").WithTimeout(10).GetJsonAsync<List<PIECEACHATLIGNE>>().Result[0].PLAID;
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

        public async Task<int> getPIECEACHATECHEANCE_PEAID()
        {
            try
            {

                return (int)(Constant.PIECEACHATECHEANCE_url + "?filter[order]=PEAID DESC").WithTimeout(10).GetJsonAsync<List<PIECEACHATECHEANCE>>().Result[0].PEAID;
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

        public async Task<float> get_PEAMONTANT(string PCAID)
        {
            try
            {
                var list = (Constant.PIECEACHAT_url + "?filter[where][PCAID]=" + PCAID).WithTimeout(10).GetJsonAsync<List<PIECEACHAT>>().Result;
                float sum = 0;
                foreach (PIECEACHAT P in list)
                {
                    sum += (float)P.PCAMNTTTC;
                }
                return sum;
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

        public async Task<bool> PostPIECEACHATLIGNE(PIECEACHATLIGNE pIECEACHATLIGNE)
        {
            try
            {
                var result = (Constant.PIECEACHATLIGNE_url).PostJsonAsync(pIECEACHATLIGNE);
                return result.Result.IsSuccessStatusCode;
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

        public async Task<bool> PostPIECEACHAT(PIECEACHAT pIECEACHAT)
        {
            try
            {
                var result = (Constant.PIECEACHAT_url).WithTimeout(10).PostJsonAsync(pIECEACHAT);
                var res = result.Result;
                return res.IsSuccessStatusCode;
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

        public async Task<bool> PostPieceAchatEcheace(PIECEACHATECHEANCE pIECEACHATECHEANCE)
        {
            try
            {
                return (Constant.PIECEACHATECHEANCE_url).WithTimeout(10).PostJsonAsync(pIECEACHATECHEANCE).Result.IsSuccessStatusCode;
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

        public async Task<bool> PostPieceAchatTaxe(PIECEACHATTAXES pIECEACHATTAXES)
        {
            try
            {

                return (Constant.PIECEACHATTAXES_url).WithTimeout(10).PostJsonAsync(pIECEACHATTAXES).Result.IsSuccessStatusCode;
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

        public async Task<bool> UpdateUser(UTILISATEUR newUser, int id)
        {
            try{
                return (Constant.user_uri + "/" + id).PatchJsonAsync(newUser).Result.IsSuccessStatusCode;
            }
            catch{
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                return (Constant.user_uri + "/" + id).DeleteAsync().Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<depot> GetDepotbyARTdepot(int id)
        {
            try
            {
               
                return  (Constant.depot_url + "?filter[where][DEPID]=" + id).WithTimeout(10).GetJsonAsync<IList<depot>>().Result[0];
               
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
    }

}
