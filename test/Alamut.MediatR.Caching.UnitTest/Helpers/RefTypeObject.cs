using System;

namespace Alamut.MediatR.Caching.UnitTest.Helpers
{
    public class RefTypeObject : IEquatable<RefTypeObject>
    {
        public int Foo { get; set; }
        public string Bar { get; set; }
        public DateTime Created { get; set; }

        public bool Equals(RefTypeObject other) =>
            Foo.Equals(other.Foo) && 
                Bar.Equals(other.Bar) &&
                Created.Equals(other.Created);
    }
}