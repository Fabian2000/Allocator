using System.Diagnostics;
using System.Runtime.InteropServices;

unsafe
{
    var wrapperI = AllocatorRaw.New<UselessIntWrapper>();
    AllocatorRaw.Clear(wrapperI);
    Debug.WriteLine(wrapperI[0].I);
    AllocatorRaw.Free(wrapperI);
}

[StructLayout(LayoutKind.Sequential)]
public struct UselessIntWrapper
{
    private int i = 0;

    public UselessIntWrapper()
    {
        i = 0;
    }

    public int I { get => i; set => i = value; }
}