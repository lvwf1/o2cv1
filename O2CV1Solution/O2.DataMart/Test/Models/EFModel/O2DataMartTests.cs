using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using O2.DataMart.Models.EFModel;
using EntityFramework.BulkInsert.Extensions;

namespace O2.DataMart.Test.Models.EFModel
{
    [TestClass]
    public class O2DataMartTests
    {
        [TestMethod]
        public void TestReadBackBone()
        {
            using (var db = new O2DataMart())
            {
                var bb = db.BackBones.Include("Person").Include("Property").Include("Mortgage").FirstOrDefault();

                Assert.IsNotNull(bb);
                Assert.IsNotNull(bb.Person);
                Assert.IsNotNull(bb.Property);
                Assert.IsNotNull(bb.Mortgage);
            }
        }

        [TestMethod]
        public void TestBackBoneInsert()
        {
            BackBone bb;

            using (var db = new O2DataMart())
            {
                bb = this.CreateBackBone();
                
                db.Persons.Add(bb.Person);
                db.Properties.Add(bb.Property);
                db.Mortgages.Add(bb.Mortgage);
                db.BackBones.Add(bb);

                db.SaveChanges();
            }

            using (var db = new O2DataMart())
            {
                this.DropBackBone(db, bb);

                db.SaveChanges();
            }
        }

        [TestMethod]
        public void TestBulkBackBoneInsert()
        {
            var backBones = new List<BackBone>();

            for (var i = 0; (i < 10000); ++i)
            {
                backBones.Add(this.CreateBackBone());
            }

            using (var db = new O2DataMart())
            {
                db.BulkInsert(backBones.Select(b=>b.Person));
                db.BulkInsert(backBones.Select(b=>b.Property));
                db.BulkInsert(backBones.Select(b=>b.Mortgage));
                db.BulkInsert(backBones);
                db.SaveChanges();
            }

            using (var db = new O2DataMart())
            {
                foreach (var bb in backBones)
                {
                    this.DropBackBone(db, bb);
                }

                db.SaveChanges();
            }
        }

        private void DropBackBone(O2DataMart db, BackBone bb)
        {
            db.Persons.Remove(db.Persons.Single(p => p.PersonId == bb.PersonId));
            db.Properties.Remove(db.Properties.Single(p => p.PropertyId == bb.PropertyId));
            db.Mortgages.Remove(db.Mortgages.Single(m => m.MortgageId == bb.MortgageId));
            db.BackBones.Remove(db.BackBones.Single(b => b.BackBoneId == bb.BackBoneId));
        }

        private BackBone CreateBackBone()
        {
            var p = new Person {PersonId = Guid.NewGuid(), FirstName = "Testy", LastName = "Tester"};
            var m = new Mortgage {MortgageId = Guid.NewGuid(), LenderName = "Testy Tester"};
            var pty = new Property {PropertyId = Guid.NewGuid(), FullAddress = "Test Property"};

            var bb = new BackBone
            {
                BackBoneId = Guid.NewGuid(),
                Person = p,
                PersonId = p.PersonId,
                Mortgage = m,
                MortgageId = m.MortgageId,
                Property = pty,
                PropertyId = pty.PropertyId
            };

            return bb;
        }
    }
}
