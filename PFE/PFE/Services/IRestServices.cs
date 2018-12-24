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
        Task<bool> UpdateUser(UTILISATEUR newUser, int id);
        Task<bool> DeleteUser(int id);
        Task<IList<UTILISATEURSGRP>> GetGroupAsync();
        Task<IList<UTILISATEUR>> GetUserByGroupIdAsync(string groupId);
        Task<bool> Login(UTILISATEUR user);
        Task<IList<TIERS>> GetTiers(string info, string type);
        Task<IList<AFFAIRE>> GetAffaire(string info);
        Task<ARTICLE> getArticlebyBC(string bc);
        Task<ARTICLE> getArticlebyid(string id);
        Task<NUMAUTO> getNumPiecenyNature(string nature);
        Task<int>userNumber();
        Task<bool> addUser(UTILISATEUR user);
        Task<bool> addUsers(IList<UTILISATEUR> users);
        Task<ARTUNITE> GetRTUNITE(string type);
        Task<ARTFAMILLES_CPT> GetARTFAMILLES_CPTbyARFID(string ARFID , string ARFCLASS = null);
        Task<ARTTARIFLIGNE> GetRTTARIFLIGNEbyARTID(string ARID);
        Task<ARTDEPOT> GetARTDEPOTbyDepid(string ARTID, string DEPID);
        Task<IList<ARTDEPOT>> GetARTDEPOTbyDepArtid(string ARTID);
        Task<bool> PatchArtdepot(ARTDEPOT artdepot,string artid,string depid);
        Task<TVA> GetTVAbyTVACODE(string VACODE);
        Task<IList<PIECE_NATURE>> GetPieceNaturebyPINID(String PINID);
        Task<IList<depot>> GetDepot(string DEPISACTIF, string DEPISPRINCIPAL = null);
        Task<IList<PIECE_NATURE>> GetPieceNature(string PICCODE = null, string PITCODE = null, string PINLIBELLE = null, string PINSENSSTOCK = null , bool like = false);
        Task<bool> PostToStock(IList<StockLigne> stocks);
        Task<bool> PostStockElement(StockLigne obj);
        Task<bool> PostSellLignes(IList<SellElements> sells);
        Task<bool> PostSellLigne(SellElements sell, int num);
        Task<bool> PostBuyElement(Buyelement buy,int num);
        Task<bool> PostBuyElements(IList<Buyelement> buy);
        Task<bool> PostPIECEVENTELIGNE(PIECEVENTELIGNE pIECEVENTELIGNE);
        Task<bool> PostPIECEVENTE(PIECEVENTE pIECEVENTE);
        Task<bool> PostPIECEACHATLIGNE(PIECEACHATLIGNE pIECEACHATLIGNE);
        Task<bool> PostPIECEACHAT(PIECEACHAT pIECEACHAT);
        Task<bool> PostPiecedivers(PIECEDIVERS piecediverse);
        Task<bool> PostPiecediversLigne(PIECEDIVERSLIGNES piecediverseligne);
        Task<bool> PostOperationStock(OPERATIONSTOCK operationStock);
        Task<bool> PostPieceVenteEcheace(PIECEVENTEECHEANCE pIECEVENTEECHEANCE);
        Task<bool> PostPieceVenteTaxe(PIECEVENTETAXES pIECEVENTETAXES);
        Task<bool> PostPieceAchatEcheace(PIECEACHATECHEANCE pIECEACHATECHEANCE);
        Task<bool> PostPieceAchatTaxe(PIECEACHATTAXES pIECEACHATTAXES);
        Task<bool> PostReglementEcheace(REGLEMENTECHEANCE rEGLEMENTECHEANCE);
        Task<bool> PostMemos(MEMOS memos);
        Task<IDENTIFIANTTABLE> GetIDENTIFIANTTABLE(string idTable);
        Task<bool> PostIdentityTable(string idTable);
        Task<int> getPieceDiversNumber();
        Task<int> getPieceDiversLignesNumber();
        Task<int> getOperationStockNumber();
        Task<int> getMemosNumber();
        Task<int> getPieceVente();
        Task<int> getPieceAchat();
        Task<int> getPieceAchatLigne();
        Task<int> getEXERCICE();
        Task<int> getPieceVenteLigne();
        Task<int> getPIECEVENTEECHEANCE_PEVID();
        Task<int> getPIECEACHATECHEANCE_PEAID();
        Task<int> getREGLEMENTECHEANCE_ECHID();
        Task<PRODUIT> getProduit(String PROCODE,String PROISPRINCIPAL);
        Task<PRODUIT> getProduitbyARTID(string ARTID, string PROISPRINCIPAL);
        Task<PIECE_PREF> GetPIECE_PREF(string id, string PIPISDEFAULT = null);
        //PIECEVENTELIGNE GetPIECEVENTELIGNEbyARTID(string artid);
        Task<bool> testServer(string uri = null);
        Task<float> get_PCVMNTTTC(string PCVID);
        Task<float> get_PEAMONTANT(string PCAID);
        Task<depot> GetDepotbyARTdepot(int id);
        Task<depot> getDepPrincipal();
        //Task<float> getPrix()
    }
}
