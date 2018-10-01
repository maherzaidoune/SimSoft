using System;
using System.Collections.Generic;
using System.Text;

namespace PFE.Helper
{
    class Constant
    {
        private string _baseUrl;
        public Constant(string _baseUrl)
        {
            this._baseUrl = _baseUrl;
            Console.WriteLine(_baseUrl);
        }
        public string user_uri { get {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/UTILISATEURs";
            }  
            }
        public string group_uri
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/UTILISATEURSGRPs";
            }
        }

        public string tiers_uri { get {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/TIERs";
            }
        }
        public string affaire_uri
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/AFFAIREs";
            }
        }
        public string piece_nature_uri
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PIECE_NATUREs";
            }
        }
        public string depot_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/DEPOTs";
            }
        }
        public string article_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/ARTICLEs";
            }
        }
        public string numauto_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/NUMAUTOs";
            }
        }
        public string pieceprefs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PIECE_PREFs";
            }
        }
        public string ARTFAMILLES_CPTs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/ARTFAMILLES_CPTs";
            }
        }
        public string ARTTARIFLIGNEs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/ARTTARIFLIGNEs";
            }
        }
        public string TVAs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/TVAs";
            }
        }
        public string ARTUNITEs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/ARTUNITEs";
            }
        }
        public string ARTDEPOTs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/ARTDEPOTs";
            }
        }
        public string PIECEDIVERs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PIECEDIVERs";
            }
        }
        public string OPERATIONSTOCKs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/OPERATIONSTOCKs";
            }
        }
        public string IDENTIFIANTTABLEs_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/IDENTIFIANTTABLEs";
            }
        }
        public string MEMOS_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/MEMOs";
            }
        }
        public string PIECEDIVERSLIGNE_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PIECEDIVERSLIGNEs";
            }
        }

        public string PIECEVENTE_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PIECEVENTEs";
            }
        }


        public string PIECEVENTELIGNE_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/PIECEVENTELIGNEs";
            }
        }
        public string EXERCICE_url
        {
            get
            {
                if (string.IsNullOrEmpty(_baseUrl))
                    throw new Exception(" _baseUr is null ");
                return _baseUrl + "/api/EXERCICEs";
            }
        }
        public string PRODUIT_url
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
