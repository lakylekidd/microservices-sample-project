using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.FunctionalTests
{
    public static class Endpoints
    {
        public static class Get
        {
            public static string Orders = "api/v1/orders";
        }

        public static class Post
        {

        }

        public static class Put
        {
            public static string CancelOrder = "api/v1/orders/cancel";
        }
    }
}
