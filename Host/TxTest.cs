using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TxnLog
{
    public class SubTest
    {
        public class A
        {
            public class B
            {
                public int NESTEDTEST { get; set; }
            }
            public B b { get; set; }
        }
        public A a { get; set; }
        public int Value { get; set; }
    }
    class TxTest:Tx
    {
        public TxTest()
        {
            TxnId = 1;
        }
        public SubTest sub { get; set; }
        public string Name { get; set; }
        public int Age;
        public override Result Transfer(DataStream Stream)
        {
            return Stream.Transfer<TxTest>(this);
        }

        public override Result Validate()
        {
            return base.Validate();
        }
        public override Result Prepare()
        {
            return base.Prepare();
        }
        public override Result Update()
        {
            return base.Update();
        }
        public override Result SqlUpdate()
        {
            Console.WriteLine("**************Name: " + Name);
            return base.SqlUpdate();
        }
    }
}
