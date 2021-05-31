namespace SeeMore
{
    public static class KernelFactory
    {
        public static Kernel Neighborhood(KernelSize size = KernelSize.SIZE_3x3)
        {
            return new Kernel(size);
        }
    }
}
