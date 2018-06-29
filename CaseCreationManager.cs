/// Developer:          Kebin Wen
/// Date:               01/06/2010
/// Class Name:         CaseCreationManager
/// Purpose:            Case Creation Manager business class
///  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;

using DoS.CA.GVS.DataEntity;
using DoS.CA.GVS.DataEntity.EntityClasses;
using DoS.CA.GVS.DataEntity.HelperClasses;
using DoS.CA.GVS.DataEntity.FactoryClasses;
using DoS.CA.GVS.DataAccessLayer.DataEntityMappers;
using DoS.CA.GVS.DataEntity.CollectionClasses;
using DoS.CA.GVS.DataEntity.StoredProcedureCallerClasses;
using DoS.CA.GVS.DataEntity.DaoClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

using DoS.CA.GVS.BusinessEntities.Interfaces;
using DoS.CA.GVS.BusinessEntities;
using DoS.CA.GVS.DataAccessLayer;



namespace DoS.CA.GVS.BusinessLayer.CaseCreation
{
	public class CaseCreationManager
	{
		CaseCreationDAL m_objDAL = new CaseCreationDAL();

		public List<IScreenComponentBusinessEntity> getScreenComponentList(string moduleName)
		{
			GvsScreenComponentCollection coll = m_objDAL.getScreenComponentCollection(moduleName);
			List<IScreenComponentBusinessEntity> list = new List<IScreenComponentBusinessEntity>();

			foreach (GvsScreenComponentEntity dataEntity in coll)
			{
				ScreenComponentBusinessEntity businessEntity = new ScreenComponentBusinessEntity();
				businessEntity.ModuleName = dataEntity.ModuleName;
				businessEntity.PetitionType = dataEntity.PetitionType;
				businessEntity.VisaClass = dataEntity.VisaClass;
				businessEntity.ComponentName = dataEntity.ComponentName;
				businessEntity.Visibility = dataEntity.Visibility;
				businessEntity.AllowMultipleSubcomponent = Convert.ToBoolean(dataEntity.AllowMultipleSubcomponent);
				businessEntity.SubcomponentName = dataEntity.SubcomponentName;

				list.Add(businessEntity);
			}

			return list;
		}

		public List<IScreenElementBusinessEntity> getScreenElementList(string moduleName)
		{
			GvsScreenElementCollection coll = m_objDAL.getScreenElementCollection(moduleName);
			List<IScreenElementBusinessEntity> list = new List<IScreenElementBusinessEntity>();

			foreach (GvsScreenElementEntity dataEntity in coll)
			{
				ScreenElementBusinessEntity businessEntity = new ScreenElementBusinessEntity();
				businessEntity.ModuleName = dataEntity.ModuleName;
				businessEntity.PetitionType = dataEntity.PetitionType;
				businessEntity.ComponentName = dataEntity.ComponentName;
				businessEntity.ElementName = dataEntity.ElementName;
				businessEntity.IsRequired = Convert.ToBoolean(dataEntity.IsRequired);
				businessEntity.IsEditable = Convert.ToBoolean(dataEntity.IsEditable);

				list.Add(businessEntity);
			}

			return list;
		}

		public List<IScreenElementLkBusinessEntity> getScreenElementLkList()
		{
			GvsScreenElementLkCollection coll = m_objDAL.getScreenElementLkCollection();
			List<IScreenElementLkBusinessEntity> list = new List<IScreenElementLkBusinessEntity>();

			foreach (GvsScreenElementLkEntity dataEntity in coll)
			{
				ScreenElementLkBusinessEntity businessEntity = new ScreenElementLkBusinessEntity();
				businessEntity.ElementName = dataEntity.ElementName;
				businessEntity.Description = dataEntity.Description;
				businessEntity.LookupTableName = dataEntity.LookupTableName;
				businessEntity.LookupColumnName = dataEntity.LookupColumnName;

				list.Add(businessEntity);
			}

			return list;
		}

		public List<IScreenElementDependencyBusinessEntity> getScreenElementDependencyList(string moduleName)
		{
			GvsScreenElementDepCollection coll = m_objDAL.getScreenElementDependencyCollection(moduleName);
			List<IScreenElementDependencyBusinessEntity> list = new List<IScreenElementDependencyBusinessEntity>();

			foreach (GvsScreenElementDepEntity dataEntity in coll)
			{
				ScreenElementDependencyBusinessEntity businessEntity = new ScreenElementDependencyBusinessEntity();
				businessEntity.ModuleName = dataEntity.ModuleName;
				businessEntity.ComponentName = dataEntity.ComponentName;
				businessEntity.ElementName = dataEntity.ElementName;
				businessEntity.DependencyComponentName = dataEntity.DepComponentName;
				businessEntity.DependencyElementName = dataEntity.DepElementName;
				businessEntity.DependencyElementValue = dataEntity.DepElementValue;

				list.Add(businessEntity);
			}

			return list;
		}

		public List<IVisaClassDataBusinessEntity> getVisaClassDataList()
		{
			GvsVisaClassDataCollection coll = m_objDAL.getVisaClassDataCollection();
			List<IVisaClassDataBusinessEntity> list = new List<IVisaClassDataBusinessEntity>();

			foreach (GvsVisaClassDataEntity dataEntity in coll)
			{
				VisaClassDataBusinessEntity businessEntity = new VisaClassDataBusinessEntity();
				businessEntity.PetitionType = dataEntity.PetitionType;
				businessEntity.VisaClass = dataEntity.VisaClass;
				businessEntity.ElementName = dataEntity.ElementName;
				businessEntity.ElementValue = dataEntity.ElementValue;

				list.Add(businessEntity);
			}

			return list;
		}

		public List<ILookupCodeBusinessEntity> getLookupCodeList(string tableName)
		{
			DataTable table = m_objDAL.getLookupCodeList(tableName);
			List<ILookupCodeBusinessEntity> list = new List<ILookupCodeBusinessEntity>();

			foreach (DataRow row in table.Rows)
			{
				LookupCodeBusinessEntity businessEntity = new LookupCodeBusinessEntity();
				businessEntity.Code = Convert.ToString(row[0]);
				businessEntity.Description = Convert.ToString(row[1]);

				list.Add(businessEntity);
			}

			return list;
		}

        public List<IDHSPetitionBusinessEntity> getDHSPetitionList(string petitionNbr)
        {
			GvsDhsPetitionCollection coll = m_objDAL.getDhsPetitionCollection(petitionNbr);
            List<IDHSPetitionBusinessEntity> list = new List<IDHSPetitionBusinessEntity>();

            foreach (GvsDhsPetitionEntity dataEntity in coll)
            {
                DHSPetitionBusinessEntity businessEntity = new DHSPetitionBusinessEntity();
                
                businessEntity.PetitionNbr = dataEntity.PetitionNbr;
                businessEntity.PetitionType = dataEntity.PetitionType;

                if (dataEntity.PetitionPriorityDate != null)
                {
                    businessEntity.PetitionPriorityDate = dataEntity.PetitionPriorityDate.ToString();
                }
                if (dataEntity.PetitionApprovalDate != null)
                {
                    businessEntity.PetitionApprovalDate = dataEntity.PetitionApprovalDate.ToString();
                }
                if (dataEntity.PetitionReceiptDate != null)
                {
                    businessEntity.PetitionReceiptDate = dataEntity.PetitionReceiptDate.ToString();
                }
                if (dataEntity.PetitionValidFromDate != null)
                {
                    businessEntity.PetitionValidFromDate = dataEntity.PetitionValidFromDate.ToString();
                }
                if (dataEntity.PetitionExpirationDate != null)
                {
                    businessEntity.PetitionExpirationDate = dataEntity.PetitionExpirationDate.ToString();
                }
                if (dataEntity.PetitionStatus != null)
                {
                    businessEntity.PetitionStatus = dataEntity.PetitionStatus;
                }
                if (dataEntity.MaxBeneficiaries != null)
                {
                    businessEntity.MaxBeneficiaries = (int)dataEntity.MaxBeneficiaries;
                }
                if (dataEntity.CurrentBeneficiaries != null)
                {
                    businessEntity.CurrentBeneficiaries = (int)dataEntity.CurrentBeneficiaries;
                }
                
                businessEntity.IsLoaded = (int)dataEntity.IsLoaded;

                list.Add(businessEntity);
            }

            return list;
        }

        public string SaveCase(ICaseBusinessEntity caseBE)
        {
			string strResult = "";
            if (caseBE != null)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    GvsCaseEntity caseDataEntity = CaseCreationMapping.Map(caseBE);
					strResult = m_objDAL.SaveCase(caseDataEntity);
					//if (strResult != "")
					//{
					//    caseBE.PetitionBE.CaseId = caseDataEntity.CaseId;
					//    GvsPetitionEntity petitionDataEntity = new PetitionMapping().Map(caseBE.PetitionBE);
					//    strResult = m_objDAL.SaveCase(caseDataEntity);
					//}
                    */
                    ts.Complete();
                }
            }

			return strResult;
        }


	}
}
