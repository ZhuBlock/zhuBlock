using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

using System;
using System.IO;
using System.Numerics;

public class ZhuBlock : SmartContract
{


    private static readonly byte[] pools_from_owner_size_prefix      = { (byte)'P', (byte)'O', (byte)'O', (byte)'Z' };
	private static readonly byte[] number_of_pools = { (byte)'N', (byte)'B', (byte)'P', (byte)'O' };
    private static readonly byte[] pool_prefix = { (byte)'P', (byte)'O', (byte)'O', (byte)'L' };

    public static object Main(string operation, params object[] args)
    {
		if (args.Length > 0)
		{
			byte[] owner = (byte[])args[0];
         
			if (operation == "addPool")
			{
				if (args.Length != 5) { return false; }

				BigInteger poolHardCap = (BigInteger)args[1];
                BigInteger minContribution = (BigInteger)args[2];
                BigInteger maxContribution = (BigInteger)args[3];
                BigInteger fees = (BigInteger)args[4];

                return CreatePool(owner, poolHardCap, minContribution, maxContribution, fees);
			}
			if (operation == "getPool")
			{
				if (args.Length != 2) { return false; }

				byte[] poolID = (byte[])args[1];

                return GetPool(poolID);
			}
		}

		return false;
    }
    
	//testinvoke e63dcb8933cc5048ed0b9a79f34d4f870956284f addPool ['AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y', '9999', '199', '299', '99']
    // The way we handle fees will have to change because you can't store decimal values in neo's storage
    private static bool CreatePool(byte[] owner, BigInteger poolHardCap, BigInteger minContribution, BigInteger maxContribution, BigInteger fees)
    {
        // can only create a pool for your own address
        if (!Runtime.CheckWitness(owner)) { return false; }

        // make sure we don't have negative numbers
        if (poolHardCap <= 0 || minContribution <= 0 || maxContribution <= 0) { return false; }
        if (fees < 0 || fees > 99 ) { return false; }

        // make sure the maximum contribution is always greater than the minimum contribution
        if (minContribution > maxContribution) { return false; }

        // --------- All verifications are OK ---------

        // create the pool and serialize it: Pool -> byte[]
        Pool pool = new Pool();
        pool.owner = owner;
        pool.poolHardCap = poolHardCap;
        pool.minContribution = minContribution;
        pool.maxContribution = maxContribution;
        pool.fees = fees;
        byte[] serialized = Neo.SmartContract.Framework.Helper.Serialize(pool);

        // increment the number of pool by 1  
        IncreaseNumberOfPoolsByOne();

        //
        var numberOfPools = GetNumberOfPools();
        var key = pool_prefix.Concat(numberOfPools);

        Storage.Put(Storage.CurrentContext, key, serialized);

        //TODO: add ownerPools[poolID, poolID, etc...] --> ownerPools['1', '2', etc...]

        return true;
    }

    // testinvoke e63dcb8933cc5048ed0b9a79f34d4f870956284f getPool ['AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y', '1']
    // To transform the results from hex to string: https://peterlinx.github.io/DataTransformationTools/
    public static Pool GetPool(byte[] poolID)
    {
        var key = pool_prefix.Concat(poolID);

        // transform byte[] -> Pool
        Pool pool = (Pool)Neo.SmartContract.Framework.Helper.Deserialize(Storage.Get(Storage.CurrentContext, key));

        return pool;
    }

       
	private static byte[] GetNumberOfPoolsFromOwner(byte[] owner)
    {
        var key = pools_from_owner_size_prefix.Concat(owner);
        var value = Storage.Get(Storage.CurrentContext, key);

        return value;
    }

	private static bool IncreaseNumberOfPoolsFromOwnerByOne(byte[] owner)
    {
        var key = pools_from_owner_size_prefix.Concat(owner);
        var numberOfPoolsFromOwner = GetNumberOfPoolsFromOwner(owner);

        var poolsFromOwnerCount = numberOfPoolsFromOwner.AsBigInteger() + 1;
        var value = poolsFromOwnerCount.AsByteArray();

        Storage.Put(Storage.CurrentContext, key, value);
        return true;
    }
   
	private static byte[] GetNumberOfPools()
    {
        var key = number_of_pools;
        var value = Storage.Get(Storage.CurrentContext, key);

        return value;
    }

	private static bool IncreaseNumberOfPoolsByOne()
    {
        var key = number_of_pools;
        var numberOfPools = GetNumberOfPools();

        var poolsCount = numberOfPools.AsBigInteger() + 1;
        var value = poolsCount.AsByteArray();

        Storage.Put(Storage.CurrentContext, key, value);
        return true;
    }

	[Serializable]
    public class Pool
    {
		public byte[] owner;
		public BigInteger poolHardCap;
		public BigInteger minContribution;
		public BigInteger maxContribution;
		public BigInteger fees;
    }

}