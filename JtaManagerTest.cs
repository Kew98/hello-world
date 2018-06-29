using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FMS.UnitTests.MDG;
using BusinessLayer.Utility;
using BusinessLayer.Entities.JTA.Concrete;
using BusinessLayer;
using System.Collections;


namespace FMS.UnitTests.JTA
{
    [TestFixture]
    [Category("Core")]
    public class JtaManagerTest : MdgRemoteTestBase
    {
        #region Constants
        const string Cmd = "NG";
        const string Iter = "0113";

        const string JtaDoc1 = "NG123";
        const string HeaderKey1 = "1";
        
        const string JtaDoc2 = "NG456";        
        const string HeaderKey2 = "2";

        const string JtaDoc3 = "NG789";        
        const string HeaderKey3 = "3";

        const string Parno1 = "001";
        const string Parno2 = "002";
        const string Parno3 = "003";

        const string Lin1 = "123456";
        const string Lin2 = "234567";
        const string Lin3 = "345678";

        #endregion
        #region Properties        
        private JtaManager mgr;
        #endregion

        #region Utilities
        private void LoadDefaultData()
        {
            FmsObjectCollection details = new FmsObjectCollection();

            mgr = new JtaManager();

            //Create first JTA Document with 2 paragraphs and 2 equipment records
            JtaDocumentHeader doc1 = CreateDocHeader(HeaderKey1, Cmd, JtaDoc1, Iter);
            details.Add(doc1);
            JtaParagraph par1 = CreateParagraphHeader(doc1, Parno1);
            JtaParagraph par2 = CreateParagraphHeader(doc1, Parno2);
            details.Add(par1);
            details.Add(par2);
            JtaEquipment equip1 = CreateEquipment(doc1, Parno1, Lin1);
            JtaEquipment equip2 = CreateEquipment(doc1, Parno2, Lin2);
            details.Add(equip1);
            details.Add(equip2);
            
            //Create second JTA Document with 2 paragraphs and 2 equipment records
            JtaDocumentHeader doc2 = CreateDocHeader(HeaderKey2, Cmd, JtaDoc2, Iter);
            details.Add(doc2);
            JtaParagraph par3 = CreateParagraphHeader(doc2, Parno1);
            JtaParagraph par4 = CreateParagraphHeader(doc2, Parno2);
            details.Add(par3);
            details.Add(par4);
            JtaEquipment equip3 = CreateEquipment(doc2, Parno1, Lin1);
            JtaEquipment equip4 = CreateEquipment(doc2, Parno2, Lin2);
            details.Add(equip3);
            details.Add(equip4);

           
            mgr.Load(details);
        
        }

        private JtaDocumentHeader CreateDocHeader(string key, string cmd, string jta, string iter)
        {
            JtaDocumentHeader docHdr = new JtaDocumentHeader();
            docHdr.Key = key;
            docHdr.Command = cmd;
            docHdr.Docno = jta;
            docHdr.Ccnum = iter;

            return docHdr;
        }

        private JtaParagraph CreateParagraphHeader(JtaDocumentHeader header, string parno)
        {
            JtaParagraph parHdr = new JtaParagraph();
            parHdr.JtaHeader = header;
            parHdr.Parno = parno;

            return parHdr;
        }

        private JtaEquipment CreateEquipment(JtaDocumentHeader header, string parno, string lin)
        {
            JtaEquipment equip = new JtaEquipment();
            equip.JtaHeader = header;
            equip.Parno = parno;
            equip.Lin = lin;

            return equip;
        }
        #endregion

        #region Tests
        [SetUp]
        public void Setup()
        {
            LoadDefaultData();
        }
        [Test]
        public void LoadJtaManager()
        {            
            Assert.IsNotNull(mgr);
            Assert.IsFalse(mgr.IsEmpty);
            //There should be 2 documents, 4 paragraphs, and 4 equipment records all together.
            Assert.AreEqual(mgr.DocumentHeaderCollection.Count, 2);
            Assert.AreEqual(mgr.ParagraphCollection.Count, 4);
            Assert.AreEqual(mgr.GetCollection(typeof(JtaParagraph), HeaderKey1).Count, 2);
            Assert.AreEqual(mgr.GetCollection(typeof(JtaParagraph), HeaderKey2).Count, 2);
            Assert.AreEqual(mgr.GetCollection(typeof(JtaEquipment)).Count, 4);
            Assert.AreEqual(mgr.GetCollection(typeof(JtaEquipment), HeaderKey1).Count, 2);
            Assert.AreEqual(mgr.GetCollection(typeof(JtaEquipment), HeaderKey2).Count, 2);
        }

        [Test]
        public void CreateNewDetails()
        {                        
            JtaDocumentHeader newDoc = (JtaDocumentHeader) mgr.CreateNewDetail(typeof(JtaDocumentHeader));
            newDoc.Key = HeaderKey3;
            newDoc.Command = Cmd;
            newDoc.Ccnum = Iter;
            Assert.AreEqual(mgr.DocumentHeaderCollection.Count, 3);

            JtaParagraph newParagraph = (JtaParagraph)mgr.CreateNewDetail(typeof(JtaParagraph));
            newParagraph.JtaHeader = newDoc;
            newParagraph.Parno = Parno3;            
            Assert.AreEqual(mgr.ParagraphCollection.Count, 5);            

            JtaEquipment newEquipment = (JtaEquipment)mgr.CreateNewDetail(typeof(JtaEquipment));
            newEquipment.JtaHeader = newDoc;
            newEquipment.Parno = Parno3;
            newEquipment.Lin = Lin3;
            Assert.AreEqual(mgr.GetCollection(typeof(JtaEquipment)).Count, 5);            

        }

        [Test]
        public void TransformJtaManager()
        {
            IDictionary<Type, ICollection> objects = JtaManager.Transform(mgr);

            Assert.IsNotNull(objects);
            Assert.Greater(objects.Count, 0);
            //There should be 2 documents, 4 paragraphs, and 4 equipment records all together.
            Assert.IsTrue(objects.ContainsKey(typeof(JtaDocumentHeader)));
            Assert.AreEqual(objects[typeof(JtaDocumentHeader)].Count, 2);
            Assert.IsTrue(objects.ContainsKey(typeof(JtaParagraph)));
            Assert.AreEqual(objects[typeof(JtaParagraph)].Count, 4);
            Assert.IsTrue(objects.ContainsKey(typeof(JtaEquipment)));
            Assert.AreEqual(objects[typeof(JtaEquipment)].Count, 4);                        
            
        }

        [Test] 
        public void IsModified_DeleteObject()
        {
            ICollection details = mgr.GetCollection(typeof(JtaEquipment));
            
            if (details != null && details.Count > 0)
            {
                JtaEquipment equip = null; 
                foreach (JtaEquipment detail in details)
                {
                    equip = detail;
                    break;
                }
                if (equip != null)
                {
                    mgr.RemoveFromCollection(equip);
                    Assert.AreEqual(mgr.GetCollection(typeof(JtaEquipment)).Count, 3);
                    Assert.IsTrue(mgr.IsModified());
                }
            }

        }

        [Test]
        public void IsModified_ModifyObject()
        {
            ICollection details = mgr.GetCollection(typeof(JtaEquipment));

            if (details != null && details.Count > 0)
            {
                JtaEquipment equip = null;
                foreach (JtaEquipment detail in details)
                {
                    equip = detail;
                    break;
                }
                if (equip != null)
                {
                    equip.Authorized = "1";
                    equip.Required = "1";
                    Assert.IsTrue(mgr.IsModified());
                    Assert.AreEqual(mgr.GetDirtyObjects().Count, 1);
                }
            }

        }
        #endregion

    }
}
