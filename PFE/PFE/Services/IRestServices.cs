using PFE.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PFE.Services
{
    public interface IRestServices
    {
        Task<IList<UTILISATEUR>> GetUserAsync();
        Task<IList<UTILISATEURSGRP>> GetGroupAsync();
        Task<IList<UTILISATEUR>> GetUserByGroupIdAsync(string groupId);
        bool Login(UTILISATEUR user);
        Task<IList<TIERS>> GetTiers(string info);
        Task<IList<AFFAIRE>> GetAffaire(string info);
        ARTICLE getArticlebyBC(string bc);
        ARTICLE getArticlebyid(string id);
        NUMAUTO getNumPiecenyNature(string nature);
        int userNumber();
        bool addUser(UTILISATEUR user);
        bool addUsers(IList<UTILISATEUR> users);
        ARTUNITE GetRTUNITE(string type);
        ARTFAMILLES_CPT GetARTFAMILLES_CPTbyARFID(string ARFID);
        ARTTARIFLIGNE GetRTTARIFLIGNEbyARTID(string ARID);
        ARTDEPOT GetARTDEPOTbyDepid(string DEPID);
        ARTDEPOT GetARTDEPOTbyDepArtid(string ARTID);
        bool PatchArtdepot(ARTDEPOT artdepot,string artid);
        TVA GetTVAbyTVACODE(string VACODE);
        Task<IList<PIECE_NATURE>> GetPieceNaturebyPINID(String PINID);
        Task<IList<depot>> GetDepot(string DEPISACTIF, string DEPISPRINCIPAL = null);
        Task<IList<PIECE_NATURE>> GetPieceNature(string PICCODE = null, string PITCODE = null, string PINLIBELLE = null, string PINSENSSTOCK = null , bool like = false);
        bool PostToStock(IList<StockLigne> stocks);
        bool PostStockElement(StockLigne obj);
        bool PostPiecedivers(PIECEDIVERS piecediverse);
        bool PostPiecediversLigne(PIECEDIVERSLIGNES piecediverseligne);
        bool PostOperationStock(OPERATIONSTOCK operationStock);
        bool PostMemos(MEMOS memos);
        IDENTIFIANTTABLE GetIDENTIFIANTTABLE(string idTable);
        bool PostIdentityTable(string idTable);
        int getPieceDiversNumber();
        int getPieceDiversLignesNumber();
        int getOperationStockNumber();
        int getMemosNumber();
        PIECE_PREF GetPIECE_PREF(string id, string PIPISDEFAULT = null);
        //PIECEVENTELIGNE GetPIECEVENTELIGNEbyARTID(string artid);
    }
}
