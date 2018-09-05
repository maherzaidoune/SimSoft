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
                IList<PIECE_PREF> prefs = (c.pieceprefs_url  + "?filter[where][PINID]=" +nature).GetJsonAsync<IList<PIECE_PREF>>().Result;
                var numauto =  (c.numauto_url + "?filter[where][NUMID]=" + prefs[0].NUMID ).GetJsonAsync<IList<NUMAUTO>>().Result;
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
    }

}
