using System;
using System.Collections.Generic;
using System.Text;

namespace PFE.Helper
{
    class Constant
    {

        private static string _baseUrl = Config.URL + ':' + Config.port;

        public static string user_uri { get {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/UTILISATEURs";
            }  
            }
        public static string group_uri
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/UTILISATEURSGRPs";
            }
        }

        public static string tiers_uri { get {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/TIERs";
            }
        }
        public static string affaire_uri
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/AFFAIREs";
            }
        }
        public static string piece_nature_uri
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PIECE_NATUREs";
            }
        }
        public static string depot_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/DEPOTs";
            }
        }
        public static string article_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/ARTICLEs";
            }
        }
        public static string numauto_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/NUMAUTOs";
            }
        }
        public static string pieceprefs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PIECE_PREFs";
            }
        }
        public static string ARTFAMILLES_CPTs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/ARTFAMILLES_CPTs";
            }
        }
        public static string ARTTARIFLIGNEs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/ARTTARIFLIGNEs";
            }
        }
        public static string TVAs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/TVAs";
            }
        }
        public static string ARTUNITEs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/ARTUNITEs";
            }
        }
        public static string ARTDEPOTs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/ARTDEPOTs";
            }
        }
        public static string PIECEDIVERs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PIECEDIVERs";
            }
        }
        public static string OPERATIONSTOCKs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/OPERATIONSTOCKs";
            }
        }
        public static string IDENTIFIANTTABLEs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/IDENTIFIANTTABLEs";
            }
        }
        public static string MEMOS_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/MEMOs";
            }
        }
        public static string PIECEDIVERSLIGNE_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PIECEDIVERSLIGNEs";
            }
        }

        public static string PIECEVENTE_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PIECEVENTEs";
            }
        }


        public static string PIECEVENTELIGNE_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PIECEVENTELIGNEs";
            }
        }
        public static string EXERCICE_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/EXERCICEs";
            }
        }
        public static string PRODUIT_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PRODUITs";
            }
        }


    }
}
