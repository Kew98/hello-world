using System;
using System.Collections.Generic;
using System.Data;
using BusinessLayer;
using BusinessLayer.AsyncProcessing.SavedSetJobs;
using BusinessLayer.Entities.Mdg.Concrete;
using BusinessLayer.Entities.ResLib;
using BusinessLayer.Services;
using BusinessLayer.Utility;
using Common;
using Common.DataLayer.DataEntities;
using FMS.UnitTests.MDG;
using NUnit.Framework;
using USArmy.FMS.DataLayer.DataServices;

namespace FMS.UnitTests.JTA
{
    [TestFixture]
    [Category("Core")]
    public class JtaDataServiceTest : MdgRemoteTestBase
    {
        #region Private Objects
        // private JtaDataService service;

        /// <summary>
        /// Document Header properties
        /// </summary>
        private object p_in_jta_doc_header_id_aa;
        private object p_in_saved_set_id_aa;
        private object p_in_command_aa;
        private object p_in_jta_aa; 
        private object p_in_iter_aa;
        private object p_in_approval_level_aa;
        private object p_in_jta_title_aa;
        private object p_in_navy_docno_aa;
        private object p_in_exec_agent_aa;
        private object p_in_dedte_aa;
        private object p_in_publish_year_aa;
        private object p_in_arind_aa;
        private object p_in_docst_aa;
        private object p_in_class_aa;
        private object p_in_b_jta_aa;
        private object p_in_b_iter_aa;
        private object p_in_s_jta_aa;
        private object p_in_s_iter_aa;
        private object p_in_anlcd_aa;
        private object p_in_lvldown_aa;
        private object p_in_shpdt_aa;
        private object p_in_pdf_file_aa;
        private object p_in_uicod_aa;

        /// <summary>
        /// JTA Paragraph properties
        /// </summary>
        private object p_in_jta_paragraph_id_aa;
        private object p_in_doc_header_id_aa;
        private object p_in_submission_id_aa;
        private object p_in_parno_aa;
        private object p_in_partl_aa;
        private object p_in_uicdr_aa;

        /// <summary>
        /// JTA Equipment properties
        /// </summary>
        private object p_in_jta_equipment_id_aa;
        private object p_in_svc_code_aa;
        private object p_in_linum_aa;
        private object p_in_nsn_aa;
        private object p_in_cots_aa;
        private object p_in_nomen_aa;
        private object p_in_reqeq_aa;
        private object p_in_auteq_aa;
        private object p_in_ermk1_aa;
        private object p_in_ermk2_aa;
        private object p_in_ermk3_aa;
        private object p_in_ermk4_aa;
        #endregion

        #region Initialization & set up
        /// <summary>
        /// Executes once 
        /// </summary>
        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
        }
        /// <summary>
        /// Executes before every test
        /// Initialize all private properties with test/default value
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Create a JTA data service 
            // service =   new JtaDataService();

            // Initialize the JTA document header with default values
            p_in_jta_doc_header_id_aa = new int[] { -1 };
            p_in_saved_set_id_aa = new int[] { 0 };
            p_in_command_aa = new string[] {"B5"};
            p_in_jta_aa = new string[] { "B5W3X9" }; 
            p_in_iter_aa = new string[] {"0110"};
            p_in_approval_level_aa = new string[] { ApprovalLevel.Working.Code };
            p_in_jta_title_aa = new string[] { "UNIT TEST - JTA TITLE" };
            p_in_navy_docno_aa = new string[] { null };
            p_in_exec_agent_aa = new string[] { "EA" };
            p_in_dedte_aa = new DateTime[] { DateTime.Today };
            p_in_publish_year_aa = new string[] { "2012" };
            p_in_arind_aa = new string[] { null };
            p_in_docst_aa = new string[] { "W" };
            p_in_class_aa = new string[] { "U" };
            p_in_b_jta_aa = new string[] { null };
            p_in_b_iter_aa = new string[] { null };
            p_in_s_jta_aa = new string[] { null };
            p_in_s_iter_aa = new string[] { null };
            p_in_anlcd_aa = new string[] { null };
            p_in_lvldown_aa = new DateTime[] { DateTime.Today };
            p_in_shpdt_aa = new DateTime[] { DateTime.Today };
            p_in_pdf_file_aa = new string[] { null };
            p_in_uicod_aa = new string[] { null };

            // Initialize the JTA paragraph with default values
            p_in_jta_paragraph_id_aa = new int[] { -1 };
            p_in_doc_header_id_aa = new int[] { 0 };
            p_in_submission_id_aa = new int[] { 0 };
            p_in_parno_aa = new string[] { "001A" };
            p_in_partl_aa = new string[] { "JTA paragraph 001A title." };
            p_in_uicdr_aa = new string[] { "123456" };

            p_in_jta_equipment_id_aa = new int[] { -1 };
            p_in_svc_code_aa = new string[] { "AB" };
            p_in_linum_aa = new string[] { null };
            p_in_nsn_aa = new string[] { "NSN" };
            p_in_cots_aa = new string[] { null };
            p_in_nomen_aa = new string[] { "NOMEN" };
            p_in_reqeq_aa = new int[] { 1 };
            p_in_auteq_aa = new int[] { 1 };
            p_in_ermk1_aa = new string[] { "12" };
            p_in_ermk2_aa = new string[] { "34" };
            p_in_ermk3_aa = new string[] { "56" };
            p_in_ermk4_aa = new string[] { "78" };
        }
        #endregion
        #region The Tests
        /// <summary>
        /// Create one JTA document header
        /// </summary>
        [Test]
        public void CreateOneDocHeader()
        {
            using (JtaDataService service = new JtaDataService(true))
            {                
                // First, we have to create an empty saved set as a container, into which the JTA document header
                //  later be created.
                DataSetEntity results = CreateDocumentHeader(service, CreateSavedSet(service));
                Assert.NotNull(results, "The result from creating JTA document header.");
                DataSet ds = results.DataSet;
                Assert.NotNull(ds, "The dataset from creating JTA document header.");
                DataTable dt = ds.Tables[0];
                Assert.NotNull(dt, "The dataset from creating JTA document header has at least one table.");
                DataRow r = dt.Rows[0];
                Assert.NotNull(dt, "The result table from creating JTA document header has at least one row.");
                int oldkey = Convert.ToInt32(r["OLD_ID"].ToString());
                Assert.AreEqual(-1, oldkey, "The old key should be -1");
                int newkey = Convert.ToInt32(r["NEW_ID"].ToString());
                Assert.Greater(newkey, 0, "The new key should be greater than zero");
                
                // We are done, should roll everything back.
                service.RollbackTransaction();             
            }
        }

        /// <summary>
        /// Create one JTA equipment
        /// </summary>
        [Test]
        public void CreateOneEquipment()
        {
            using (JtaDataService service = new JtaDataService(true))
            {
                DataSetEntity results = CreateEquipment(service);

                Assert.NotNull(results, "The result from creating JTA Equipment.");
                DataSet ds = results.DataSet;
                Assert.NotNull(ds, "The dataset from creating JTA Equipment.");
                DataTable dt = ds.Tables[0];
                Assert.NotNull(dt, "The dataset from creating JTA Equipment has at least one table.");
                DataRow r = dt.Rows[0];
                Assert.NotNull(dt, "The result table from creating JTA Equipment has at least one row.");
                int oldkey = Convert.ToInt32(r["OLD_ID"].ToString());
                Assert.AreEqual(-1, oldkey, "The old key should be -1");
                int newkey = Convert.ToInt32(r["NEW_ID"].ToString());
                Assert.Greater(newkey, 0, "The new key should be greater than zero");

                // We are done, should roll everything back.
                service.RollbackTransaction();
            }
        }

        /// <summary>
        /// Create one JTA document and then remove it.
        /// </summary>
        [Test]
        [Category("Core")]
        public void DeleteDocumentByHeaderId()
        {
            using (JtaDataService service = new JtaDataService(true))
            {
                // First, we have to create an empty saved set as a container, into which the JTA document header
                //  later be created.
                DataSetEntity results = CreateDocumentHeader(service, CreateSavedSet(service));
                Assert.NotNull(results, "The result from creating JTA document header.");
                DataSet ds = results.DataSet;
                Assert.NotNull(ds, "The dataset from creating JTA document header.");
                DataTable dt = ds.Tables[0];
                Assert.NotNull(dt, "The dataset from creating JTA document header has at least one table.");
                DataRow r = dt.Rows[0];
                Assert.NotNull(dt, "The result table from creating JTA document header has at least one row.");
                int oldkey = Convert.ToInt32(r["OLD_ID"].ToString());
                Assert.AreEqual(-1, oldkey, "The old key should be -1");
                int newkey = Convert.ToInt32(r["NEW_ID"].ToString());
                Assert.Greater(newkey, 0, "The new key should be greater than zero");

                // Now, we can remove it.
                string[] document_header_id_array = new string[] { r["NEW_ID"].ToString() };
                service.DeleteByDocumentHeader(document_header_id_array);

                // We are done, should roll everything back.
                service.RollbackTransaction();
            }
        }

        /// <summary>
        /// Create one JTA document and then remove it by saved set id.
        /// </summary>
        [Test]
        [Category("Core")]
        public void DeleteDocumentBySavedSetId()
        {
            using (JtaDataService service = new JtaDataService(true))
            {
                // First, we have to create an empty saved set as a container, into which the JTA document header
                //  later be created.
                Array saved_set_id_array = CreateSavedSet(service);
                DataSetEntity results = CreateDocumentHeader(service, saved_set_id_array);
                Assert.NotNull(results, "The result from creating JTA document header.");
                DataSet ds = results.DataSet;
                Assert.NotNull(ds, "The dataset from creating JTA document header.");
                DataTable dt = ds.Tables[0];
                Assert.NotNull(dt, "The dataset from creating JTA document header has at least one table.");
                DataRow r = dt.Rows[0];
                Assert.NotNull(dt, "The result table from creating JTA document header has at least one row.");
                int oldkey = Convert.ToInt32(r["OLD_ID"].ToString());
                Assert.AreEqual(-1, oldkey, "The old key should be -1");
                int newkey = Convert.ToInt32(r["NEW_ID"].ToString());
                Assert.Greater(newkey, 0, "The new key should be greater than zero");

                // Now, we can remove it, using the saved set ids.
                service.DeleteBySavedSet(saved_set_id_array as string[]);

                // We are done, should roll everything back.
                service.RollbackTransaction();
            }
        }

        /// <summary>
        /// Create one JTA paragraph
        /// </summary>
        [Test]
        public void CreateOneParagraph()
        {
            using (JtaDataService service = new JtaDataService(true))
            {
                DataSetEntity results = CreateParagraph(service);

                Assert.NotNull(results, "The result from creating JTA Paragraph.");
                DataSet ds = results.DataSet;
                Assert.NotNull(ds, "The dataset from creating JTA Paragraph.");
                DataTable dt = ds.Tables[0];
                Assert.NotNull(dt, "The dataset from creating JTA Paragraph has at least one table.");
                DataRow r = dt.Rows[0];
                Assert.NotNull(dt, "The result table from creating JTA Paragraph has at least one row.");
                int oldkey = Convert.ToInt32(r["OLD_ID"].ToString());
                Assert.AreEqual(-1, oldkey, "The old key should be -1");
                int newkey = Convert.ToInt32(r["NEW_ID"].ToString());
                Assert.Greater(newkey, 0, "The new key should be greater than zero");

                // We are done, should roll everything back.
                service.RollbackTransaction();
            }
        }
        #endregion

        #region Utilities
        /// <summary>
        /// A utility to create an empty saved set, in which a JTA document header can be created.
        /// </summary>
        /// <param name="service">The JTA data service</param>
        /// <returns>Saved set keys, as string array</returns>
        private Array CreateSavedSet(JtaDataService service)
        {
            object saved_set_key = null;
            //service.SaveSavedSet(ref saved_set_key, _UserName, null, "Unit test saved set", DateTime.Now);
            service.SaveSavedSet(ref saved_set_key, _UserName, null, Guid.NewGuid().ToString(), DateTime.Now);

            return new string[] { saved_set_key.ToString() };
        }

        /// <summary>
        /// A wrapper to the data service call to save JTA document header
        /// </summary>
        /// <returns>Dataset Entity</returns>
        private DataSetEntity CreateDocumentHeader(JtaDataService service, Array saved_set_id_array)
        {            
            return service.SaveDocumentHeader(
                        p_in_jta_doc_header_id_aa, saved_set_id_array, p_in_command_aa,
                        p_in_jta_aa, p_in_iter_aa, p_in_approval_level_aa, p_in_jta_title_aa,
                        p_in_navy_docno_aa, p_in_exec_agent_aa, p_in_dedte_aa, p_in_publish_year_aa, p_in_arind_aa,
                        p_in_docst_aa, p_in_class_aa, p_in_b_jta_aa, p_in_b_iter_aa, p_in_s_jta_aa, p_in_s_iter_aa,
                        p_in_anlcd_aa, p_in_lvldown_aa, p_in_shpdt_aa, p_in_pdf_file_aa, p_in_uicod_aa);
        }

        /// <summary>
        /// Utility to create a paragraph, for the provided JTA document
        /// </summary>
        /// <param name="service">JTA data service</param>
        /// <param name="document_header_id_array">JTA document header ids array</param>
        /// <returns></returns>
        private DataSetEntity CreateParagraph(JtaDataService service)
        {
            Array saved_set_id_array = CreateSavedSet(service);

            DataSetEntity dse = CreateDocumentHeader(service, saved_set_id_array);
            DataSet ds = dse.DataSet;
            DataTable dt = ds.Tables[0];
            DataRow r = dt.Rows[0];
            Array document_header_id_array = new string[] { r["NEW_ID"].ToString() };

            return service.SaveParagraph(
                        p_in_jta_paragraph_id_aa, document_header_id_array, saved_set_id_array,
                        p_in_parno_aa, p_in_partl_aa, p_in_uicdr_aa);
        }

        /// <summary>
        /// Utility to create a paragraph, for the provided JTA document
        /// </summary>
        /// <param name="service">JTA data service</param>
        /// <param name="document_header_id_array">JTA document header ids array</param>
        /// <returns></returns>
        private DataSetEntity CreateParagraph(JtaDataService service, Array document_header_id_array, Array saved_set_id_array)
        {
            return service.SaveParagraph(
                        p_in_jta_paragraph_id_aa, document_header_id_array, saved_set_id_array,
                        p_in_parno_aa, p_in_partl_aa, p_in_uicdr_aa);
        }

        /// <summary>
        /// Utility to create an equipment
        /// </summary>
        /// <param name="service">JTA data service</param>
        /// <param name="document_header_id_array">JTA document header ids array</param>
        /// <returns></returns>
        private DataSetEntity CreateEquipment(JtaDataService service)
        {
            Array saved_set_id_array = CreateSavedSet(service);

            DataSetEntity dse = CreateDocumentHeader(service, saved_set_id_array);
            DataSet ds = dse.DataSet;
            DataTable dt = ds.Tables[0];
            DataRow r = dt.Rows[0];
            Array document_header_id_array = new string[] { r["NEW_ID"].ToString() };

            dse = CreateParagraph(service, document_header_id_array, saved_set_id_array);
            ds = dse.DataSet;
            dt = ds.Tables[0];
            r = dt.Rows[0];
            Array paragraph_id_array = new string[] { r["NEW_ID"].ToString() };

            return service.SaveEquipment(
                p_in_jta_equipment_id_aa, document_header_id_array, saved_set_id_array, p_in_parno_aa,
                p_in_svc_code_aa, p_in_linum_aa, p_in_nsn_aa, p_in_cots_aa, p_in_nomen_aa,
                p_in_reqeq_aa, p_in_auteq_aa, p_in_ermk1_aa, p_in_ermk2_aa, p_in_ermk3_aa,
                p_in_ermk4_aa);
        }

        #endregion
    }
}
