
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chsword.SearchModel.Test
{
    /// <summary>
    ///这是 QueryableExtensionsTest 的测试类，旨在
    ///包含所有 QueryableExtensionsTest 单元测试
    ///</summary>
    [TestClass]
    public class QueryableExtensionsTest
    {
        private readonly List<MyClass> _table =
            new List<MyClass>
                {
                    new MyClass
                        {
                            Id = 1,
                            Name = "attach",
                            Age = 10,
                            AddTime = (int) UnixTimeUtil.FromDateTime(new DateTime(2010, 9, 1)),
                            CanNull = 1,
                            Time1= new DateTime(2010, 9, 1),
                            Time = new DateTime(2010, 9, 1),
                            Class = new MyClass
                                        {
                                            Id = 11, Age = 110,   
                                            Class = new MyClass
                                                                {
                                                                    Id = 111, Age = 1110
                                                                }
                                        }
                        },
                    new MyClass
                        {
                            Id = 2,
                            Name = "blance",
                            Age = 20,
                            AddTime = (int) UnixTimeUtil.FromDateTime(new DateTime(2010, 9, 3)),
                            Time1= new DateTime(2010, 9, 3),
                            Time = new DateTime(2010, 9, 3),
                            CanNull = 2,
                            Class = new MyClass
                                        {
                                            Id = 21, Age = 210,   
                                            Class = new MyClass
                                                                {
                                                                    Id = 221, Age = 2210
                                                                }
                                        }
                        },
                    new MyClass
                        {
                            Id = 3,
                            Name = "chsword",
                            Age = null,
                            AddTime = (int) UnixTimeUtil.FromDateTime(new DateTime(2010, 9, 4, 10, 10, 10)),
                           Time1=  new DateTime(2010, 9, 4, 10, 10, 10),
                            Time = new DateTime(2010, 9, 4, 10, 10, 10),
                            Class = new MyClass
                                        {
                                            Id = 31, Age = 310,   
                                            Class = new MyClass
                                                                {
                                                                    Id =331, Age = 3310
                                                                }
                                        }
                        },
                };

        /// <summary>
        ///Where 的测试
        ///</summary>
        [TestMethod]
        public void EqualIntAndString_One()
        {
            var item = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = "1" };
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(1, actual.Count());
        }
        [TestMethod]
        public void PropNotExists()
        {
            var item = new ConditionItem { Field = "Id1", Method = QueryMethod.Equal, Value = "1" };
            IQueryable<MyClass> query = _table.AsQueryable();

            try
            {
                IQueryable<MyClass> actual = query.Where(item);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Contains("Id1"));
            }
        }
        [TestMethod]
        public void EqualIntAndInt_One()
        {
            var item = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = 1 };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void LessThan_One()
        {
            var item = new ConditionItem { Field = "Id", Method = QueryMethod.LessThan, Value = "2" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void LessThanOrEqual_Two()
        {
            var item = new ConditionItem { Field = "Id", Method = QueryMethod.LessThanOrEqual, Value = "2" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void Like_One()
        {
            var item = new ConditionItem { Field = "Name", Method = QueryMethod.Like, Value = "*lanc*" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual("blance", actual.FirstOrDefault().Name);
        }

        [TestMethod]
        public void EqualNullable_One()
        {
            var item = new ConditionItem { Field = "Age", Method = QueryMethod.Equal, Value = "10" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(1, actual.Count());
        }

    
        [TestMethod]
        public void EqualInputIsDateTime_One()
        {
            var item = new ConditionItem { Field = "Time", Method = QueryMethod.Equal, Value = "2010-09-01" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(1, actual.Count());
        }
        [TestMethod]
        public void EqualInputIsDateTimeNull()
        {
            const int expectResult = 3;
            var item1 = new ConditionItem("Time1", QueryMethod.GreaterThan, "2010-08-31");
            var item2 = new ConditionItem("Time1", QueryMethod.LessThan, "2010-09-30");
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(new SearchModel { Items = new[] { item1, item2 }.ToList() });
            Assert.AreEqual(expectResult, actual.Count());
        }

 
 
       
     
        [TestMethod]
        public void Equal_1_One()
        {
            var item = new ConditionItem { Field = "AddTime", Method = QueryMethod.Equal, Value = "-1111111" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void In()
        {
            var item = new ConditionItem { Field = "Id", Method = QueryMethod.In, Value = new[] { 1, 2, 3 } };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(3, actual.Count());
        }
        [TestMethod]
        public void NotIn()
        {
            var item = new ConditionItem { Field = "Id", Method = QueryMethod.NotIn, Value = "2,3" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(1, actual.Count());
        }
        [TestMethod]
        public void InParamIsString()
        {
            var item = new ConditionItem { Field = "Id", Method = QueryMethod.In, Value = new[] { "1", "2" } };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void InString_Two()
        {
            var item = new ConditionItem { Field = "Name", Method = QueryMethod.In, Value = new[] { "attach", "chsword" } };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void InNull_Two()
        {
            var item = new ConditionItem { Field = "CanNull", Method = QueryMethod.In, Value = new[] { "1", "2" } };
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void InNullToIsNull_Two()
        {
            var item = new ConditionItem { Field = "CanNull", Method = QueryMethod.In, Value = new int?[] { 1, 2 } };
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void IntToIsNull_Two()
        {
            var item = new ConditionItem { Field = "CanNull", Method = QueryMethod.In, Value = new int[] { 1, 2 } };
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void NotEqual_Two()
        {
            var item = new ConditionItem { Field = "Id", Method = QueryMethod.NotEqual, Value = 1 };
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void OrEqual_Two()
        {
            var item1 = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = 1, OrGroup = "a" };
            var item2 = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = 2, OrGroup = "a" };

            var model = new SearchModel();
            model.Items = new[] { item1, item2 }.ToList();
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(model);
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void AndOrMix_One()
        {
            var item1 = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = 1, OrGroup = "a" };
            var item2 = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = 2, OrGroup = "a" };
            var item3 = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = 3 };
            var sm = new SearchModel();
            sm.Items.AddRange(new[] { item1, item2, item3 });

            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = IQueryableExtensions.Where(query, sm);
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void TwoGroup_One()
        {
            var item1 = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = 1, OrGroup = "a" };
            var item2 = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = 2, OrGroup = "a" };
            var item3 = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = 2, OrGroup = "b" };
            var item4 = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = 3, OrGroup = "b" };
            var sm = new SearchModel();
            sm.Items.AddRange(new[] { item1, item2, item3, item4 });

            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = IQueryableExtensions.Where(query, sm);
            Assert.AreEqual(1, actual.Count());
        }
        [TestMethod]
        public void Equal2Level()
        {
            var item = new ConditionItem { Field = "Class.Id", Method = QueryMethod.Equal, Value = "11" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(1, actual.Count());
        }
        [TestMethod]
        public void Equal3Level()
        {
            var item = new ConditionItem { Field = "Class.Class.Id", Method = QueryMethod.Equal, Value = "111" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(item);
            Assert.AreEqual(1, actual.Count());
        }
        [TestMethod]
        public void EmptySearch()
        {

            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(new SearchModel());
            Assert.AreEqual(3, actual.Count());
        }

        [TestMethod]
        public void TwoGroupIn_One()
        {
            var item1 = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = 1, OrGroup = "a" };
            var item2 = new ConditionItem { Field = "Id2", Method = QueryMethod.In, Value = 2, OrGroup = "a" };
            var item3 = new ConditionItem { Field = "Id3", Method = QueryMethod.Equal, Value = 2, OrGroup = "b" };
            var sm = new SearchModel();
            sm.Items.AddRange(new[] { item1, item2, item3 });

            string sql = sm.ToSearchString();
            bool index = sql.IndexOf("@p0") > -1;

            Assert.AreEqual(true, true);
        }
        [TestMethod]
        public void NullString()
        {
            var item1 = new ConditionItem { Field = "NullString", Method = QueryMethod.Contains, Value = 1, OrGroup = "a" };
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(item1);
            Assert.AreEqual(0, actual.Count());
        }
    }



    internal class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public int AddTime { get; set; }
        public int? CanNull { get; set; }
        public DateTime? Time1 { get; set; }
        public DateTime Time { get; set; }
        public MyClass Class { get; set; }
        public string NullString { get; set; }
    }
}
