//-----------------------------------------------------------------------
// <copyright file="QueryableExtensionsTest" company="Ganji.com">
//     Copyright (c) Ganji.com . All rights reserved.
// </copyright>
// <author>Zou Jian</author>
// <addtime>2010-09</addtime>
//-----------------------------------------------------------------------

namespace EfSearchModel.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;

    /// <summary>
    ///这是 QueryableExtensionsTest 的测试类，旨在
    ///包含所有 QueryableExtensionsTest 单元测试
    ///</summary>
    [TestClass]
    public class QueryableExtensionsTest
    {
        #region 准备

        
        private readonly List<MyClass> _table =
            new List<MyClass>
                {
                    new MyClass
                        {
                            Id = 1,
                            Name = "attach",
                            Age = 10,
                            AddTime = (int) UnixTime.FromDateTime(new DateTime(2010, 9, 1)),
                            CanNull = 1,
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
                            AddTime = (int) UnixTime.FromDateTime(new DateTime(2010, 9, 3)),
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
                            AddTime = (int) UnixTime.FromDateTime(new DateTime(2010, 9, 4, 10, 10, 10)),
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
        #endregion
        QueryModel GetModel(params ConditionItem[] items)
        {
            var list = new List<ConditionItem>();
            list.AddRange(items);
            return new QueryModel {Items = list};
        }

        /// <summary>
        ///Where 的测试
        ///</summary>
        [TestMethod]
        public void EqualIntAndString()
        {
            var item = new ConditionItem {Field = "Id", Method = QueryMethod.Equal, Value = "1"};
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void EqualIntAndInt()
        {
            var item = new ConditionItem {Field = "Id", Method = QueryMethod.Equal, Value = 1};
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void LessThan()
        {
            var item = new ConditionItem {Field = "Id", Method = QueryMethod.LessThan, Value = "2"};
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void LessThanOrEqual()
        {
            var item = new ConditionItem {Field = "Id", Method = QueryMethod.LessThanOrEqual, Value = "2"};
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void LikeNoSig()
        {
            var item = new ConditionItem("Name", QueryMethod.Like, "lanc");
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void EqualNullable()
        {
            var item = new ConditionItem {Field = "Age", Method = QueryMethod.Equal, Value = "10"};
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void EqualInputIsUnixDateTime()
        {
            var item = new ConditionItem {Field = "AddTime", Method = QueryMethod.Equal, Value = "2010-09-01"};
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }
        [TestMethod]
        public void EqualInputIsDateTime()
        {
            var item = new ConditionItem { Field = "Time", Method = QueryMethod.Equal, Value = "2010-09-01" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }
        [TestMethod]
        public void DateBlockUnixDateTime()
        {
            var item = new ConditionItem {Field = "AddTime", Method = QueryMethod.DateBlock, Value = "2010-09-04"};
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }
        [TestMethod]
        public void DateBlockDateTime()
        {
            var item = new ConditionItem { Field = "Time", Method = QueryMethod.DateBlock, Value = "2010-09-04" };
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }
        [TestMethod]
        public void DateBetweenUnixTime()
        {
            var item = new ConditionItem {Field = "AddTime", Method = QueryMethod.LessThan, Value = "2010-09-04"};
            var item2 = new ConditionItem {Field = "AddTime", Method = QueryMethod.GreaterThan, Value = "2010-09-04"};
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item,item2));
            Assert.AreEqual(1, actual.Count());
        }
        [TestMethod]
        public void DateBetweenDateTime()
        {
            var item = new ConditionItem { Field = "Time", Method = QueryMethod.LessThan, Value = "2010-09-04" };
            var item2 = new ConditionItem { Field = "Time", Method = QueryMethod.GreaterThan, Value = "2010-09-04" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item,item2));
            Assert.AreEqual(1, actual.Count());
        }
        [TestMethod]
        public void EqualFail()
        {
            var item = new ConditionItem {Field = "AddTime", Method = QueryMethod.Equal, Value = "-1111111"};
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void InOperator()
        {
            var item = new ConditionItem {Field = "Id", Method = QueryMethod.In, Value = new[] {1, 2, 3}};
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(3, actual.Count());
        }
        [TestMethod]
        public void InStringOnly()
        {
            var item = new ConditionItem { Field = "Id", Method = QueryMethod.In, Value = "1,2,3" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(3, actual.Count());
        }
        [TestMethod]
        public void InParamIsString()
        {
            var item = new ConditionItem {Field = "Id", Method = QueryMethod.In, Value = new[] {"1", "2"}};
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void InString()
        {
            var item = new ConditionItem {Field = "Name", Method = QueryMethod.In, Value = new[] {"attach", "chsword"}};
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void InNull()
        {
            var item = new ConditionItem {Field = "CanNull", Method = QueryMethod.In, Value = new[] {"1", "2"}};
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void InNullToIsNull()
        {
            var item = new ConditionItem {Field = "CanNull", Method = QueryMethod.In, Value = new int?[] {1, 2}};
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void IntToIsNull()
        {
            var item = new ConditionItem {Field = "CanNull", Method = QueryMethod.In, Value = new[] {1, 2}};
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void NotEqual()
        {
            var item = new ConditionItem { Field = "Id", Method = QueryMethod.NotEqual, Value = 1 };
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void OrEqual()
        {
            var item1 = new ConditionItem {Field = "Id", Method = QueryMethod.Equal, Value = 1, OrGroup = "a"};
            var item2 = new ConditionItem { Field = "Id", Method = QueryMethod.Equal, Value = 2, OrGroup = "a" };
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(GetModel(item1, item2));
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void AndOrMix()
        {
            var item1 = new ConditionItem {Field = "Id", Method = QueryMethod.Equal, Value = 1, OrGroup = "a"};
            var item2 = new ConditionItem {Field = "Id", Method = QueryMethod.Equal, Value = 2, OrGroup = "a"};
            var item3 = new ConditionItem {Field = "Id", Method = QueryMethod.Equal, Value = 3};
            var sm = new QueryModel();
            sm.Items.AddRange(new[] {item1, item2, item3});

            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(sm);
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void TwoGroup()
        {
            var item1 = new ConditionItem {Field = "Id", Method = QueryMethod.Equal, Value = 1, OrGroup = "a"};
            var item2 = new ConditionItem {Field = "Id", Method = QueryMethod.Equal, Value = 2, OrGroup = "a"};
            var item3 = new ConditionItem {Field = "Id", Method = QueryMethod.Equal, Value = 2, OrGroup = "b"};
            var item4 = new ConditionItem {Field = "Id", Method = QueryMethod.Equal, Value = 3, OrGroup = "b"};
            var sm = new QueryModel();
            sm.Items.AddRange(new[] {item1, item2, item3, item4});
            IQueryable<MyClass> query = _table.AsQueryable();
            IQueryable<MyClass> actual = query.Where(sm);
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void Equal2Level()
        {
            var item = new ConditionItem { Field = "Class.Id", Method = QueryMethod.Equal, Value = "11" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void Equal3Level()
        {
            var item = new ConditionItem { Field = "Class.Class.Id", Method = QueryMethod.Equal, Value = "111" };
            IQueryable<MyClass> query = _table.AsQueryable();

            IQueryable<MyClass> actual = query.Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void LikeStart()
        {
            var item = new ConditionItem { Field = "Name", Method = QueryMethod.Like, Value = "a*" };
            IQueryable<MyClass> actual = _table.AsQueryable().Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void LikeEnds()
        {
            var item = new ConditionItem { Field = "Name", Method = QueryMethod.Like, Value = "*d" };
            IQueryable<MyClass> actual = _table.AsQueryable().Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void LikeContains()
        {
            var item = new ConditionItem { Field = "Name", Method = QueryMethod.Like, Value = "*d*" };
            IQueryable<MyClass> actual = _table.AsQueryable().Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void LikeEndsAndStarts()
        {
            var item = new ConditionItem { Field = "Name", Method = QueryMethod.Like, Value = "c*d" };
            IQueryable<MyClass> actual = _table.AsQueryable().Where(GetModel(item));
            Assert.AreEqual(1, actual.Count());
        }
    }


    internal class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public int AddTime { get; set; }
        public int? CanNull { get; set; }

        public DateTime Time { get; set; }
        public MyClass Class { get; set; }
    }
}
