# Smart Contract

This folder contains the sources of the smart contract.
This README file will help you compile and deploy the smart contract on the private net.


## Compiling the Smart Contract

Read the official documentation on Smart Contracts for Neo

```
http://docs.neo.org/en-us/sc/introduction.html
```

Download neo-compiler:
```
https://github.com/neo-project/neo-compiler
```

## Deploying the Smart Contract on the private net

Now that you have read, followed the online documentation and compiled your Smart Contract, you need to move the compiled Smart Contract ZhuBlock.avm to the following folder:

```
zhuBlock/nOS/nos-local/neo-local/smart-contracts
```

In your docker console import the Smart Contract with the following command: 
```
import contract /smart-contracts/ZhuBlock.avm 0710 05 True False
```
0710 = {input_params} is a single or series of ContractParameterType. Here, we accept string (operation) and a list (args)<br />
05 = {return_type} is a single ContractParameterType. Here we return a ByteArray<br />
More information: http://docs.neo.org/en-us/sc/Parameter.html<br />
True = {needs_storage} is a boolean, either True or False used to indicate whether or not the smart contract uses the Storage.Get/Put/Delete interop API<br />
False = {needs_dynamic_invoke} is also a boolean, indicating whether or not the smart contract will be calling another contract whose address it will not know until runtime.<br /> 
<br />
Fill out the contract details (name, version, author, etc...)
<br />
Here is the output:
```
https://i.imgur.com/razM4h7.png
```

## Invoking the Smart Contract
Copy the smart contract's hash generated from the previous step (in my example: 1357792faf48fdcc455238c7d890ca3cc992e5ec)

Try out these 2 functions in the console:
```
addPool(byte[] owner, BigInteger poolHardCap, BigInteger minContribution, BigInteger maxContribution, BigInteger fees)
testinvoke 1357792faf48fdcc455238c7d890ca3cc992e5ec addPool ['AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y', '9999', '199', '299', '99']

getPool(byte[] owner, byte[] poolID)
testinvoke 1357792faf48fdcc455238c7d890ca3cc992e5ec getPool ['AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y', '1']
```
getPool(...) will return an array of hex.
To transform the results from hex to string: https://peterlinx.github.io/DataTransformationTools/
