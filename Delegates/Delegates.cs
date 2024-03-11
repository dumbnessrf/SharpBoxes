namespace SharpBoxes.Delegates
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class Delegates
    {
        public delegate void ActionWithParam<T>(T t);
        public delegate void ActionWithParam<T1, T2>(T1 t1, T2 t2);
        public delegate void ActionWithParam<T1, T2, T3>(T1 t1, T2 t2, T3 t3);
        public delegate void ActionWithParam<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);
        public delegate void ActionWithParam<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
        public delegate void ActionWithParam<T1, T2, T3, T4, T5, T6>(
            T1 t1,
            T2 t2,
            T3 t3,
            T4 t4,
            T5 t5,
            T6 t6
        );
        public delegate void ActionWithParam<T1, T2, T3, T4, T5, T6, T7>(
            T1 t1,
            T2 t2,
            T3 t3,
            T4 t4,
            T5 t5,
            T6 t6,
            T7 t7
        );
        public delegate void ActionWithParam<T1, T2, T3, T4, T5, T6, T7, T8>(
            T1 t1,
            T2 t2,
            T3 t3,
            T4 t4,
            T5 t5,
            T6 t6,
            T7 t7,
            T8 t8
        );
        public delegate void ActionWithParam<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            T1 t1,
            T2 t2,
            T3 t3,
            T4 t4,
            T5 t5,
            T6 t6,
            T7 t7,
            T8 t8,
            T9 t9
        );

        public delegate T FuncWithParam<T>();
        public delegate T FuncWithParam<T, T1>(T1 t1);
        public delegate T FuncWithParam<T, T1, T2>(T1 t1, T2 t2);
        public delegate T FuncWithParam<T, T1, T2, T3>(T1 t1, T2 t2, T3 t3);
        public delegate T FuncWithParam<T, T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);
        public delegate T FuncWithParam<T, T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
        public delegate T FuncWithParam<T, T1, T2, T3, T4, T5, T6>(
            T1 t1,
            T2 t2,
            T3 t3,
            T4 t4,
            T5 t5,
            T6 t6
        );

        public delegate T FuncWithParam<T, T1, T2, T3, T4, T5, T6, T7>(
            T1 t1,
            T2 t2,
            T3 t3,
            T4 t4,
            T5 t5,
            T6 t6,
            T7 t7
        );
        public delegate T FuncWithParam<T, T1, T2, T3, T4, T5, T6, T7, T8>(
            T1 t1,
            T2 t2,
            T3 t3,
            T4 t4,
            T5 t5,
            T6 t6,
            T7 t7,
            T8 t8
        );
        public delegate T FuncWithParam<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            T1 t1,
            T2 t2,
            T3 t3,
            T4 t4,
            T5 t5,
            T6 t6,
            T7 t7,
            T8 t8,
            T9 t9
        );
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
