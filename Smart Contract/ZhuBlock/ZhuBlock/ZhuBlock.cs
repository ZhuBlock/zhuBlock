using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

public class ZhuBlock : SmartContract
{
    public static object Main(string operation, params object[] args)
    {
        switch (operation)
        {
            case "insertHelloWorld":
                return insertHelloWorld();
            case "getHelloworld":
                return getHelloWorld();
            default:
                return false;
        }
    }

    private static bool insertHelloWorld()
    {
        Storage.Put(Storage.CurrentContext, "Hello", "World");
        return true;
    }

    private static byte[] getHelloWorld()
    {
        return Storage.Get(Storage.CurrentContext, "Hello");
    }
}