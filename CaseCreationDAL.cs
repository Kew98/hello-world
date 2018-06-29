/// Developer:          Kebin Wen
/// Date:               01/06/2010
/// Class Name:         CaseCreationDAL
/// Purpose:            Case Creation data access class
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using DoS.CA.GVS.DataEntity;
using DoS.CA.GVS.DataEntity.EntityClasses;
using DoS.CA.GVS.DataEntity.HelperClasses;
using DoS.CA.GVS.DataEntity.FactoryClasses;
using DoS.CA.GVS.DataEntity.CollectionClasses;
using DoS.CA.GVS.DataEntity.StoredProcedureCallerClasses;
using DoS.CA.GVS.DataEntity.DaoClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace DoS.CA.GVS.DataAccessLayer
{
	public class CaseCreationDAL
	{
		public GvsScreenComponentCollection getScreenComponentCollection(string moduleName)
		{
			GvsScreenComponentCollection coll = new GvsScreenComponentCollection(new GvsScreenComponentEntityFactory());
			IPredicateExpression selectFilter = new PredicateExpression();
			selectFilter.Add(GvsScreenComponentFields.ModuleName == moduleName);
			SortExpression sorter = new SortExpression();
			sorter.Add(new SortClause(GvsScreenComponentFields.PetitionType, SortOperator.Ascending));
			sorter.Add(new SortClause(GvsScreenComponentFields.VisaClass, SortOperator.Ascending));

			if (coll != null)
			{
				bool bResult = coll.GetMulti(selectFilter, 0, sorter);
			}

			return coll;
		}

		public GvsScreenElementCollection getScreenElementCollection(string moduleName)
		{
			GvsScreenElementCollection coll = new GvsScreenElementCollection(new GvsScreenElementEntityFactory());
			IPredicateExpression selectFilter = new PredicateExpression();
			selectFilter.Add(GvsScreenElementFields.ModuleName == moduleName);

			if (coll != null)
			{
				bool bResult = coll.GetMulti(selectFilter);
			}

			return coll;
		}

		public GvsScreenElementLkCollection getScreenElementLkCollection()
		{
			GvsScreenElementLkCollection coll = new GvsScreenElementLkCollection(new GvsScreenElementLkEntityFactory());
			IPredicateExpression selectFilter = new PredicateExpression();
			selectFilter.Add(GvsScreenElementLkFields.IsActive == 1);

			if (coll != null)
			{
				bool bResult = coll.GetMulti(selectFilter);
			}

			return coll;
		}

		public GvsScreenElementDepCollection getScreenElementDependencyCollection(string moduleName)
		{
			GvsScreenElementDepCollection coll = new GvsScreenElementDepCollection(new GvsScreenElementDepEntityFactory());
			IPredicateExpression selectFilter = new PredicateExpression();
			selectFilter.Add(GvsScreenElementDepFields.ModuleName == moduleName);

			if (coll != null)
			{
				bool bResult = coll.GetMulti(selectFilter);
			}

			return coll;
		}

		public GvsVisaClassDataCollection getVisaClassDataCollection()
		{
			GvsVisaClassDataCollection coll = new GvsVisaClassDataCollection(new GvsVisaClassDataEntityFactory());

			if (coll != null)
			{
				bool bResult = coll.GetMulti(null);
			}

			return coll;
		}

		public DataTable getLookupCodeList(string tableName)
		{
			IPredicateExpression selectFilter = null;
			SortExpression sorter = null;
			ResultsetFields fields = null;

			switch (tableName.ToUpper())
			{
				case "E_CST_POST":
					//ECstPostCollection coll = new ECstPostCollection(new ECstPostEntityFactory());
					//bool bResult = coll.GetMulti(null);

					fields = new ResultsetFields(2);
					fields.DefineField(ECstPostFields.PostCd, 0, "Code");
					fields.DefineField(ECstPostFields.PostCdDesc, 1, "Description");

					sorter = new SortExpression();
					sorter.Add(new SortClause(ECstPostFields.PostCd, SortOperator.Ascending));
					break;

				case "E_CST_COUNTRY":
					fields = new ResultsetFields(2);
					fields.DefineField(ECstCountryFields.CountryCd, 0, "Code");
					fields.DefineField(ECstCountryFields.CountryCdDesc, 1, "Description");

					sorter = new SortExpression();
					sorter.Add(new SortClause(ECstCountryFields.CountryCd, SortOperator.Ascending));
					break;

				case "B_CST_US_STATE":
					fields = new ResultsetFields(2);
					fields.DefineField(BCstUsStateFields.StateCd, 0, "Code");
					fields.DefineField(BCstUsStateFields.StateCodeDesc, 1, "Description");

					sorter = new SortExpression();
					sorter.Add(new SortClause(BCstUsStateFields.StateCd, SortOperator.Ascending));
					break;

				case "B_CST_OCCUPATION":
					fields = new ResultsetFields(2);
					fields.DefineField(BCstOccupationFields.OccupationCd, 0, "Code");
					fields.DefineField(BCstOccupationFields.OccupationCdDesc, 1, "Description");

					sorter = new SortExpression();
					sorter.Add(new SortClause(BCstOccupationFields.OccupationCd, SortOperator.Ascending));
					break;

				case "D_CST_VISA_CLASS":
					fields = new ResultsetFields(2);
					fields.DefineField(DCstVisaClassFields.VisaClassCd, 0, "Code");
					fields.DefineField(DCstVisaClassFields.VisaClassCd, 1, "Description");

					sorter = new SortExpression();
					sorter.Add(new SortClause(DCstVisaClassFields.VisaClassCd, SortOperator.Ascending));
					break;

				case "GVS_RELATIONSHIP_LK":
					fields = new ResultsetFields(2);
					fields.DefineField(GvsRelationshipLkFields.Relationship, 0, "Code");
					fields.DefineField(GvsRelationshipLkFields.Description, 1, "Description");

					selectFilter = new PredicateExpression();
					selectFilter.Add(GvsRelationshipLkFields.IsActive == 1);
					sorter = new SortExpression();
					sorter.Add(new SortClause(GvsRelationshipLkFields.Relationship, SortOperator.Ascending));
					break;

				case "GVS_PETITIONER_STATUS_LK":
					fields = new ResultsetFields(2);
					fields.DefineField(GvsPetitionerStatusLkFields.PetitionerStatus, 0, "Code");
					fields.DefineField(GvsPetitionerStatusLkFields.Description, 1, "Description");

					selectFilter = new PredicateExpression();
					selectFilter.Add(GvsPetitionerStatusLkFields.IsActive == 1);
					sorter = new SortExpression();
					sorter.Add(new SortClause(GvsPetitionerStatusLkFields.PetitionerStatus, SortOperator.Ascending));
					break;

				case "GVS_PETITION_TYPE_LK":
					fields = new ResultsetFields(2);
					fields.DefineField(GvsPetitionTypeLkFields.PetitionType, 0, "Code");
					fields.DefineField(GvsPetitionTypeLkFields.Description, 1, "Description");

					selectFilter = new PredicateExpression();
					selectFilter.Add(GvsPetitionTypeLkFields.IsActive == 1);
					sorter = new SortExpression();
					sorter.Add(new SortClause(GvsPetitionTypeLkFields.PetitionType, SortOperator.Ascending));
					break;

				case "GVS_PETITIONER_TYPE_LK":
					fields = new ResultsetFields(2);
					fields.DefineField(GvsPetitionerTypeLkFields.PetitionerType, 0, "Code");
					fields.DefineField(GvsPetitionerTypeLkFields.Description, 1, "Description");

					selectFilter = new PredicateExpression();
					selectFilter.Add(GvsPetitionerTypeLkFields.IsActive == 1);
					sorter = new SortExpression();
					sorter.Add(new SortClause(GvsPetitionerTypeLkFields.PetitionerType, SortOperator.Ascending));
					break;

				case "GVS_MARITAL_STATUS_LK":
					fields = new ResultsetFields(2);
					fields.DefineField(GvsMaritalStatusLkFields.MaritalStatus, 0, "Code");
					fields.DefineField(GvsMaritalStatusLkFields.Description, 1, "Description");

					selectFilter = new PredicateExpression();
					selectFilter.Add(GvsMaritalStatusLkFields.IsActive == 1);
					sorter = new SortExpression();
					sorter.Add(new SortClause(GvsMaritalStatusLkFields.MaritalStatus, SortOperator.Ascending));
					break;

				case "GVS_ICAO_VISA_TYPE_LK":
					fields = new ResultsetFields(2);
					fields.DefineField(GvsIcaoVisaTypeLkFields.IcaoVisaType, 0, "Code");
					fields.DefineField(GvsIcaoVisaTypeLkFields.Description, 1, "Description");

					selectFilter = new PredicateExpression();
					selectFilter.Add(GvsIcaoVisaTypeLkFields.IsActive == 1);
					sorter = new SortExpression();
					sorter.Add(new SortClause(GvsIcaoVisaTypeLkFields.IcaoVisaType, SortOperator.Ascending));
					break;

				case "GVS_FTJ_LK":
					fields = new ResultsetFields(2);
					fields.DefineField(GvsFtjLkFields.FtjCd, 0, "Code");
					fields.DefineField(GvsFtjLkFields.Description, 1, "Description");

					selectFilter = new PredicateExpression();
					selectFilter.Add(GvsFtjLkFields.IsActive == 1);
					sorter = new SortExpression();
					sorter.Add(new SortClause(GvsFtjLkFields.FtjCd, SortOperator.Ascending));
					break;

				case "GVS_GENDER_LK":
					fields = new ResultsetFields(2);
					fields.DefineField(GvsGenderLkFields.Gender, 0, "Code");
					fields.DefineField(GvsGenderLkFields.Description, 1, "Description");

					selectFilter = new PredicateExpression();
					selectFilter.Add(GvsGenderLkFields.IsActive == 1);
					sorter = new SortExpression();
					sorter.Add(new SortClause(GvsGenderLkFields.Gender, SortOperator.Ascending));
					break;

				case "GVS_LEGAL_REP_TYPE_LK":
					fields = new ResultsetFields(2);
					fields.DefineField(GvsLegalRepTypeLkFields.LegalRepType, 0, "Code");
					fields.DefineField(GvsLegalRepTypeLkFields.Description, 1, "Description");

					selectFilter = new PredicateExpression();
					selectFilter.Add(GvsLegalRepTypeLkFields.IsActive == 1);
					sorter = new SortExpression();
					sorter.Add(new SortClause(GvsLegalRepTypeLkFields.LegalRepType, SortOperator.Ascending));
					break;

				case "GVS_PASSPORT_TYPE_LK":
					fields = new ResultsetFields(2);
					fields.DefineField(GvsPassportTypeLkFields.PassportType, 0, "Code");
					fields.DefineField(GvsPassportTypeLkFields.Description, 1, "Description");

					selectFilter = new PredicateExpression();
					selectFilter.Add(GvsPassportTypeLkFields.IsActive == 1);
					sorter = new SortExpression();
					sorter.Add(new SortClause(GvsPassportTypeLkFields.PassportType, SortOperator.Ascending));
					break;

				case "GVS_PETITION_VISACLASS":
					fields = new ResultsetFields(2);
					fields.DefineField(GvsPetitionVisaclassFields.VisaClass, 0, "Code");
					fields.DefineField(GvsPetitionVisaclassFields.PetitionType, 1, "PetitionType");

					selectFilter = new PredicateExpression();
					selectFilter.Add(GvsPetitionVisaclassFields.IsActive == 1);
					sorter = new SortExpression();
					sorter.Add(new SortClause(GvsPetitionVisaclassFields.VisaClass, SortOperator.Ascending));
					break;

				default:
					throw (new Exception(string.Format("Table {0} is not supported by getLookupCodeList()", tableName)));
			}

			DataTable dynamicTable = new DataTable();
			TypedListDAO dao = new TypedListDAO();
			dao.GetMultiAsDataTable(fields, dynamicTable, 0, sorter, selectFilter, null, true, null, null, 0, 0);
			return dynamicTable;
		}

        public GvsDhsPetitionCollection getDhsPetitionCollection(string petitionNbr)
        {
            GvsDhsPetitionCollection coll = new GvsDhsPetitionCollection(new GvsDhsPetitionEntityFactory());
            IPredicateExpression selectFilter = new PredicateExpression();
            selectFilter.Add(GvsDhsPetitionFields.PetitionNbr == petitionNbr);

            if (coll != null)
            {
                bool bResult = coll.GetMulti(selectFilter);
            }

            return coll;
        }
        public string SaveCase(GvsCaseEntity caseEntity)
        {
			string caseNumber = "";

            // Revisit : Modify hardcoded transaction name.
            Transaction transaction = new Transaction(IsolationLevel.ReadCommitted, "SaveCase");

            try
            {
                string strIDKey = new string(' ', 40);
                int intResult = ActionProcedures.FGetNextSeqnum("GVS_CASE", ref strIDKey, transaction);
                caseEntity.CaseId = strIDKey;
                caseEntity.CaseNbr = ComputeCaseNumber(caseEntity.PostCd, transaction);
				caseNumber = caseEntity.CaseNbr;
				caseEntity.Transaction = transaction;
                bool blnResult = caseEntity.Save();
				if (blnResult ==  false)
				{
					caseNumber = "";
				}
                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
			}

			return caseNumber;
        }

        protected string ComputeCaseNumber(string postCode, Transaction transaction)
        {
            //GVS System Generated value, This will be formated as PPPYYYYJJJ######. Where PPP- The 3 digit domestic or overseas post  where the case is created; YYYY- System Year; JJJ- Julian day; ###### - 6 digit sequence number;

            StringBuilder caseNumber = new StringBuilder();
            caseNumber.Append(postCode);
            caseNumber.Append(String.Format("{0:0000}", DateTime.Now.Year));
            caseNumber.Append(String.Format("{0:000}", DateTime.Now.DayOfYear));

            string strCaseNbr = new string(' ', 6);
            int intResult = ActionProcedures.FGetNextSeqnum("GVS_CASE_SEQ_NUM", ref strCaseNbr, transaction);
            //System.Random randomNumber = new Random();

            //int caseNbr = randomNumber.Next(999999);
            caseNumber.Append(String.Format("{0:000000}", Convert.ToInt32(strCaseNbr)));

            return caseNumber.ToString();
        }
	}
}
